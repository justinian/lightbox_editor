using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LightBoxEditor
{
	public class Frame
	{
		public static void ColorToHSV( Color color, out double hue, out double saturation, out double value )
		{
			int max = Math.Max( color.R, Math.Max( color.G, color.B ) );
			int min = Math.Min( color.R, Math.Min( color.G, color.B ) );

			hue = color.GetHue();
			saturation = (max == 0) ? 0 : 1d - (1d * min / max);
			value = max / 255d;
		}

		public static Color ColorFromHSV( double hue, double saturation, double value )
		{
			int hi = Convert.ToInt32( Math.Floor( hue / 60 ) ) % 6;
			double f = hue / 60 - Math.Floor( hue / 60 );

			value = value * 255;
			int v = Convert.ToInt32( value );
			int p = Convert.ToInt32( value * (1 - saturation) );
			int q = Convert.ToInt32( value * (1 - f * saturation) );
			int t = Convert.ToInt32( value * (1 - (1 - f) * saturation) );

			if( hi == 0 )
				return Color.FromArgb( 255, v, t, p );
			else if( hi == 1 )
				return Color.FromArgb( 255, q, v, p );
			else if( hi == 2 )
				return Color.FromArgb( 255, p, v, t );
			else if( hi == 3 )
				return Color.FromArgb( 255, p, q, v );
			else if( hi == 4 )
				return Color.FromArgb( 255, t, p, v );
			else
				return Color.FromArgb( 255, v, p, q );
		}

		public Color[] pixels;

		public Frame()
		{
			pixels = new Color[64];
			for( int i=0; i<64; ++i )
				pixels[i] = Color.Black;
		}

		public Frame( Frame f )
		{
			pixels = new Color[64];
			for (int i = 0; i < 64; ++i)
				pixels[i] = f.pixels[i];
		}

		static double lerp( double v1, double v2, float amount )
		{
			return (v1 * (1-amount)) + (amount* v2);
		}

		public Color lerpColor( int pixel, float amount, Frame nextFrame )
		{
			Color mine = pixels[pixel];
			Color next = nextFrame.pixels[pixel];

			double h1, s1, v1, h2, s2, v2;
			ColorToHSV( mine, out h1, out s1, out v1 );
			ColorToHSV( next, out h2, out s2, out v2 );
			return ColorFromHSV( lerp( h1, h2, amount ), lerp( s1, s2, amount ), lerp( v1, v2, amount ) );
		}

		public void writeFrame( System.IO.Stream outs )
		{
			Console.Write( "Writing frame order:" );
			for( int y = 0; y < 8; ++y )
			{
				for( int x = 0; x < 8; ++x )
				{
					// Odd frames are right-to-left, even are left-to-right
					int i = (y % 2 == 1) ? (y * 8 + (7 - x)) : (y * 8 + x);

					Console.Write( " {0}", i );

					int cval = pixels[i].ToArgb();
					outs.WriteByte( (byte)((cval >> 16) & 0xFF) );
					outs.WriteByte( (byte)((cval >> 8) & 0xFF) );
					outs.WriteByte( (byte)(cval & 0xFF) );
				}
			}
			Console.Write( "\n" );
		}

		public void readFrame( System.IO.Stream ins )
		{
			Console.Write( "Reading frame order:" );
			for( int y = 0; y < 8; ++y )
			{
				for( int x = 0; x < 8; ++x )
				{
					// Odd frames are right-to-left, even are left-to-right
					int i = (y % 2 == 1) ? (y * 8 + (7 - x)) : (y * 8 + x);
					pixels[i] = Color.FromArgb( ins.ReadByte(), ins.ReadByte(), ins.ReadByte() );
					Console.Write( " {0}", i );
				}
			}
			Console.Write( "\n" );
		}
	}
}
