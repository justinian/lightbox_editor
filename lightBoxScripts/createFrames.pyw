import sys
import os.path

out = file( os.path.join(os.path.dirname(os.path.abspath(sys.argv[0]))), "w" )
for arg in sys.argv:
	out.writeline( arg )

