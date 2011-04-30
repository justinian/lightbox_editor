r = 0xff0000

letters = {
	'a': [
		[0, 0, r, 0, 0],
		[0, r, 0, r, 0],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, r, r, r, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		],

	'e': [
		[r, r, r, r, r],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, r, r, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, r, r, r, r],
		],

	'h': [
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, r, r, r, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		],

	'l': [
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, 0, 0, 0, 0],
		[r, r, r, r, r],
		],

	'o': [
		[0, r, r, r, 0],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[r, 0, 0, 0, r],
		[0, r, r, r, 0],
		],

	' ': [
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		[0, 0, 0, 0, 0],
		],
}

border_w = 2

def write_pixel( out, pixel ):
	import struct
	out.write( struct.pack("3B", pixel >> 16, (pixel >> 8) & 0xFF, pixel & 0xFF) )

import sys
message = sys.argv[1].lower()

pixels = []
for i in range(8):
	pixels.append([])

for l in message:
	for i in range(8):
		pixels[i].extend( letters[l][i] )
		for j in range(border_w):
			pixels[i].append( 0 )

out = open( "frames.dat", "wb" )

for i in range(len(pixels[0])):
	for j in range(8):
		for k in range(8):
			write_pixel( out, pixels[j][ (k+i) % len(pixels[0]) ] )

