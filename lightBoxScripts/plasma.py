from math import sin
import colorsys

SINE_SCALAR = 0.5

def make_color( r, g, b ):
	return (int(r*255) << 16) + (int(g*255) << 8) + int(b*255)

def generate_frame( offset ):
	frame = []
	for y in range(8):
		frame.append( [] )
		for x in range(8):
			h = (( 0.5 + sin(x*SINE_SCALAR)*0.5 ) + ( 0.5 + sin(y*SINE_SCALAR)*0.5 ))/2
			h = float( (int((h+offset)*1000) % 1000) / 1000.0 )
			r,g,b = colorsys.hsv_to_rgb(h, 1.0, 1.0)
			frame[-1].append( make_color(r,g,b) )
	return frame

def write_pixel( out, pixel ):
	import struct
	out.write( struct.pack("3B", pixel >> 16, (pixel >> 8) & 0xFF, pixel & 0xFF) )


out = open( "frames.dat", "wb" )

for i in range(100):
	pixels = generate_frame( float(i)/100.0 )
	for j in range(8):
		for k in range(8):
			even = (j % 2) == 0
			if not even:
				k = 7-k
			write_pixel( out, pixels[j][k] )

