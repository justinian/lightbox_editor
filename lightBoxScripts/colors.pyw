import time
import colorsys
import pygame

PIXEL_SIZE = (16,16)
PIXEL_COUNT = (8,8)
PIXEL_COUNT_TOTAL = PIXEL_COUNT[0]*PIXEL_COUNT[1]
FRAME_BYTES = PIXEL_COUNT_TOTAL * 3

plasma_palette = []
plasma_indexes = []
plasma_frame = []
plasma_next_frame = []

def init_plasma():
	for h in range(256):
		r,g,b = colorsys.hsv_to_rgb(h/256.0,1.0,1.0)
		plasma_palette.append( (int(r*255)<<16)+(int(g*255)<<8)+int(b*255) )
	print "Plasma pallate length:", len(plasma_palette)

	from math import sin
	for x in range( PIXEL_COUNT[0] ):
		for y in range( PIXEL_COUNT[1] ):
			#the plasma buffer is a sum of sines
			color = int(( 128.0 + (128.0 * sin(x*8.0 / 16.0)) + 128.0 + (128.0 * sin(y*8.0 / 16.0))) / 2) & 0xFF
			plasma_indexes.append( color )
			plasma_next_frame.append( plasma_palette[color] )

def generate_plasma():
	global plasma_frame
	global plasma_next_frame

	plasma_frame = plasma_next_frame
	plasma_next_frame = []

	for x in range( PIXEL_COUNT[0] ):
		for y in range( PIXEL_COUNT[1] ):
			i = y*PIXEL_COUNT[1] + x
			plasma_indexes[i] = (plasma_indexes[i] + 1) % 256
			plasma_next_frame.append( plasma_palette[plasma_indexes[i]] )

def parse_frame( data ):
	frame = []
	import struct
	for i in range(PIXEL_COUNT_TOTAL):
		r,g,b = struct.unpack("3B", data[i*3:i*3+3])
		frame.append( (r<<16) + (g<<8) + b )
	return frame

def load_frames_file( filename ):
	frames = []
	inputfile = open(filename, "rb")
	data = inputfile.read(FRAME_BYTES)
	while data and len(data) == FRAME_BYTES:
		frames.append( parse_frame(data) )
		data = inputfile.read(FRAME_BYTES)
	return frames

def unpack_color( color ):
	return color >> 16, color >> 8 & 0xFF, color & 0xFF

def lerp( f, o, n ):
	return o + f*(n-o)

def lerp_color( td, oldcolor, newcolor ):
	tdf = td/100.0
	ro, go, bo = unpack_color( oldcolor )
	ho, so, vo = colorsys.rgb_to_hsv( ro/255.0, go/255.0, bo/255.0 )
	rn, gn, bn = unpack_color( newcolor )
	hn, sn, vn = colorsys.rgb_to_hsv( rn/255.0, gn/255.0, bn/255.0 )
	return colorsys.hsv_to_rgb( lerp(tdf, ho, hn), lerp(tdf, so, sn), lerp(tdf, vo, vn) )

def draw_pixel( screen, x, y, color ):
	xBase = x*PIXEL_SIZE[0]
	yBase = y*PIXEL_SIZE[1]
	color = (int(color[0]*255), int(color[1]*255), int(color[2]*255))
	pygame.draw.rect( screen, color, (xBase, yBase, PIXEL_SIZE[0], PIXEL_SIZE[1]) )

def render( screen, framespan, last_frame, next_frame ):
	for y in range(PIXEL_COUNT[1]):
		for x in range(PIXEL_COUNT[0]):
			i = y*PIXEL_COUNT[1] + x
			draw_pixel( screen, x, y, lerp_color(framespan, last_frame[i], next_frame[i]) )

def main():
	pygame.init()
	screen = pygame.display.set_mode( (PIXEL_SIZE[0]*PIXEL_COUNT[0], PIXEL_SIZE[0]*PIXEL_COUNT[0]) )

	import sys
	frames = None
	if len(sys.argv) > 1:
		frames = load_frames_file( sys.argv[1] )
	else:
		init_plasma()

	frame = 0
	framespan = 0
	while True:
		if not frames:
			generate_plasma()

		for event in pygame.event.get():
			if event.type == pygame.QUIT:
				return
			elif event.type == pygame.KEYDOWN and event.key == pygame.K_ESCAPE:
				return

		if frames:
			render( screen, framespan, frames[frame % len(frames)], frames[(frame+1) % len(frames)] )
		else:
			render( screen, framespan, plasma_frame, plasma_next_frame )

		pygame.display.flip()

		framespan += 1
		if framespan >= 100:
			framespan = 0
			frame += 1
		time.sleep(0.005)

if __name__ == "__main__": main()
