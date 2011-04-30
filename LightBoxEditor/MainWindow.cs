using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LightBoxEditor
{
	public partial class MainWindow : Form
	{
		ushort currentFrameIndex;
		System.Collections.Generic.List<Frame> frames;
		bool drawing;
		bool playing;
		float lerpamount;

		public Frame CurrentFrame
		{
			get { return frames[currentFrameIndex]; }
		}

		public Frame NextFrame
		{
			get { if( currentFrameIndex + 1 < frames.Count ) return frames[currentFrameIndex + 1]; return null; }
		}

		public MainWindow()
		{
			InitializeComponent();
			colorDialog.Color = Color.Red;
			currentColorImageBox.BackColor = Color.Red;
			currentFrameIndex = 0;

			framePicker.Minimum = 0;
			framePicker.Maximum = 0;

			frames = new List<Frame>();
			frames.Add(new Frame());

			frameDisplay.BackColor = Color.Black;
			drawing = false;
			playing = false;
			lerpamount = 0.0f;

			saveFileDialog.AddExtension = true;
			saveFileDialog.OverwritePrompt = true;
			saveFileDialog.DefaultExt = "dat";
			saveFileDialog.Filter = "Frame Data (*.dat)|*.dat|All Files|*.*";

			openFileDialog.AddExtension = true;
			openFileDialog.CheckFileExists = true;
			openFileDialog.DefaultExt = "dat";
			openFileDialog.Multiselect = false;
			openFileDialog.Filter = "Frame Data (*.dat)|*.dat|All Files|*.*";
		}

		private void currentColorImageBox_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				currentColorImageBox.BackColor = colorDialog.Color;
			}
		}

		private void setPixel(int x, int y, Color c)
		{
			int i = y * 8 + x;
			if (x < 0 || x >= 8)
				return;

			 if (y < 0 || y >= 8)
				return;

			if (CurrentFrame.pixels[i] != c)
			{
				CurrentFrame.pixels[i] = c;
				frameDisplay.Invalidate();
			}
		}

		private void frameDisplay_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			SolidBrush b = new SolidBrush(Color.Red);
			for( int y=0; y<8; ++y )
			{
				for( int x=0; x<8; ++x )
				{
					int i = y*8 + x;
					if( lerpamount == 0.0f || ! lerpColorsCheckbox.Checked )
						b.Color = CurrentFrame.pixels[i];
					else
						b.Color = CurrentFrame.lerpColor( i, lerpamount, NextFrame );

					g.FillRectangle( b, new Rectangle( x * 50, y * 50, 50, 50 ) );
				}
			}

			b.Dispose();
			g.Dispose();
		}

		private void frameDisplay_Click(object sender, EventArgs e)
		{
			Point p = (e as MouseEventArgs).Location;
			setPixel(p.X / 50, p.Y / 50, colorDialog.Color);
			drawing = false;
		}

		private void frameDisplay_MouseDown(object sender, MouseEventArgs e)
		{
			drawing = true;
			setPixel(e.X / 50, e.Y / 50, colorDialog.Color);
		}

		private void frameDisplay_MouseUp(object sender, MouseEventArgs e)
		{
			if( drawing )
				setPixel(e.X / 50, e.Y / 50, colorDialog.Color);
			drawing = false;
		}

		private void frameDisplay_MouseMove(object sender, MouseEventArgs e)
		{
			if( drawing )
				setPixel(e.X / 50, e.Y / 50, colorDialog.Color);
		}

		private void addFrameButton_Click(object sender, EventArgs e)
		{
			frames.Insert(currentFrameIndex + 1, new Frame(CurrentFrame));
			framePicker.Maximum = frames.Count - 1;
			jumpToFrame(currentFrameIndex + 1);
		}

		private void jumpToFrame(int frame)
		{
			framePicker.Value = frame;
			currentFrameIndex = (ushort)frame;
			frameDisplay.Invalidate();
		}

		private void framePicker_ValueChanged(object sender, EventArgs e)
		{
			jumpToFrame((int)framePicker.Value);
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			new About().ShowDialog();
		}

		private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Go ask Justin.", "You need help?");
		}

		private void startPlaying()
		{
			if (playing)
				return;

			playing = true;

			frameDisplay.Enabled = false;
			framePicker.Enabled = false;
			addFrameButton.Enabled = false;
			colorGroupBox.Enabled = false;
			playButton.Text = "Stop";

			jumpToFrame(0);
			timer1.Start();
		}

		private void stopPlaying()
		{
			if (!playing)
				return;

			timer1.Stop();
			lerpamount = 0.0f;
			playing = false;

			frameDisplay.Enabled = true;
			framePicker.Enabled = true;
			addFrameButton.Enabled = true;
			colorGroupBox.Enabled = true;
			playButton.Text = "Play";
		}

		private void playButton_Click(object sender, EventArgs e)
		{
			if (!playing)
			{
				startPlaying();
			}
			else
			{
				stopPlaying();
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if( currentFrameIndex + 1 >= frames.Count )
			{
				stopPlaying();
			}
			else
			{
				lerpamount += 0.1f;
				if( lerpamount >= 1.0f )
				{
					lerpamount = 0.0f;
					jumpToFrame( currentFrameIndex + 1 );
				}
				else
				{
					frameDisplay.Invalidate();
				}
			}
		}

		private void saveFile()
		{
			Stream outs = saveFileDialog.OpenFile();
			foreach( Frame f in frames )
				f.writeFrame( outs );
			outs.Close();
			outs.Dispose();
		}

		private void openFile()
		{
			frames.Clear();

			Stream ins = openFileDialog.OpenFile();
			while( ins.Length - ins.Position >= 192 )
			{
				Frame f = new Frame();
				f.readFrame( ins );
				frames.Add( f );
			}
			ins.Close();
			ins.Dispose();

			saveFileDialog.FileName = openFileDialog.FileName;

			framePicker.Maximum = frames.Count - 1;
			jumpToFrame( 0 );
		}

		private void saveToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( saveFileDialog.FileName == null || saveFileDialog.FileName == "" )
				saveAsToolStripMenuItem_Click(sender, e);
			else
				saveFile();
		}

		private void saveAsToolStripMenuItem_Click( object sender, EventArgs e )
		{
			if( saveFileDialog.ShowDialog() == DialogResult.OK )
			{
				saveFile();
			}
		}

		private void openToolStripMenuItem_Click( object sender, EventArgs e )
		{
			DialogResult r = openFileDialog.ShowDialog();
			if( r == DialogResult.OK )
			{
				openFile();
			}
		}

		private void newToolStripMenuItem_Click( object sender, EventArgs e )
		{
			saveFileDialog.FileName = null;
			frames.Clear();
			frames.Add( new Frame() );
			framePicker.Maximum = 0;
			jumpToFrame( 0 );
		}
	}
}
