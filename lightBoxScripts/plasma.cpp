/*
  MeggyJr_Plasma.pde
 
 Example file using the The Meggy Jr Simplified Library (MJSL)
  from the Meggy Jr RGB library for Arduino
   
 Color cycling plasma   
    
 Version 0.4 - 15/1/2009
 Copyright (c) 2009 Ken Corey.  All right reserved.
 Copyright (c) 2008 Windell H. Oskay.  All right reserved.
 http://www.evilmadscientist.com/
 
 This library is free software: you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation, either version 3 of the License, or
 (at your option) any later version.
 
 This library is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.
 
 You should have received a copy of the GNU General Public License
 along with this library.  If not, see <http://www.gnu.org/licenses/>.
 	  
 */
#include <MeggyJrSimple.h>    // Required code, line 1 of 2.
#include <math.h>

#define screenWidth 8
#define screenHeight 8
#define paletteSize 64

// uint16_t palette[256];

uint16_t palette[64] = {
  0x0f00,0x0f10,0x0f30,0x0f40,0x0f50,0x0f70,0x0f80,0x0fa0,
  0x0fb0,0x0fd0,0x0fe0,0x0ff0,0x0df0,0x0cf0,0x0af0,0x09f0,
  0x07f0,0x06f0,0x05f0,0x03f0,0x02f0,0x00f0,0x00f1,0x00f2,
  0x00f4,0x00f5,0x00f6,0x00f8,0x00f9,0x00fb,0x00fc,0x00fe,
  0x00ff,0x00ef,0x00cf,0x00bf,0x009f,0x008f,0x006f,0x005f,
  0x004f,0x002f,0x001f,0x000f,0x020f,0x030f,0x050f,0x060f,
  0x070f,0x090f,0x0a0f,0x0c0f,0x0d0f,0x0f0f,0x0f0e,0x0f0d,
  0x0f0b,0x0f0a,0x0f08,0x0f07,0x0f05,0x0f04,0x0f03,0x0f01
};


typedef struct
{
  int r;
  int g;
  int b;
} ColorRGB;

//a color with 3 components: h, s and v
typedef struct 
{
  int h;
  int s;
  int v;
} ColorHSV;

int plasma[screenWidth][screenHeight];
long paletteShift;
byte state;

void MySetPxClr(byte x, byte y, byte c[3])
{
  x &= 7;
  y &= 7;
  Meg.SetPxClr(x,y,c);
}

//Converts an HSV color to RGB color
void HSVtoRGB(void *vRGB, void *vHSV) 
{
  float r, g, b, h, s, v; //this function works with floats between 0 and 1
  float f, p, q, t;
  int i;
  ColorRGB *colorRGB=(ColorRGB *)vRGB;
  ColorHSV *colorHSV=(ColorHSV *)vHSV;

  h = (float)(colorHSV->h / 256.0);
  s = (float)(colorHSV->s / 256.0);
  v = (float)(colorHSV->v / 256.0);

  //if saturation is 0, the color is a shade of grey
  if(s == 0.0) {
    b = v;
    g = b;
    r = g;
  }
  //if saturation > 0, more complex calculations are needed
  else
  {
    h *= 6.0; //to bring hue to a number between 0 and 6, better for the calculations
    i = (int)(floor(h)); //e.g. 2.7 becomes 2 and 3.01 becomes 3 or 4.9999 becomes 4
    f = h - i;//the fractional part of h

    p = (float)(v * (1.0 - s));
    q = (float)(v * (1.0 - (s * f)));
    t = (float)(v * (1.0 - (s * (1.0 - f))));

    switch(i)
    {
      case 0: r=v; g=t; b=p; break;
      case 1: r=q; g=v; b=p; break;
      case 2: r=p; g=v; b=t; break;
      case 3: r=p; g=q; b=v; break;
      case 4: r=t; g=p; b=v; break;
      case 5: r=v; g=p; b=q; break;
      default: r = g = b = 0; break;
    }
  }
  colorRGB->r = (int)(r * 255.0);
  colorRGB->g = (int)(g * 255.0);
  colorRGB->b = (int)(b * 255.0);
}

int RGBtoINT(void *vRGB)
{
  ColorRGB *colorRGB=(ColorRGB *)vRGB;

  return (colorRGB->r<<16) + (colorRGB->g<<8) + colorRGB->b;
}

/* 
  Though not called in regular use, this was used to generate the
  numbers in the palette array above
 */

/*
void
FillColorPalette()
{
  uint16_t color[3];
  int x,j;
  //generate the palette
  ColorRGB colorRGB;
  ColorHSV colorHSV;

  j=0;
  for(x = 0; x < 256; x+=4)
  {
    colorHSV.h=x; 
    colorHSV.s=255; 
    colorHSV.v=255;
    HSVtoRGB(&colorRGB, &colorHSV);

    color[0] = (colorRGB.r>>4)&0xf;
    color[1] = (colorRGB.g>>4)&0xf;
    color[2] = (colorRGB.b>>4)&0xf;

    palette[j++] = (color[0]<<8)+(color[1]<<4)+color[2];
  }
}
*/


void setup()                    // run once, when the sketch starts
{
  byte color;
  int x,y;

  MeggyJrSimpleSetup();      // Required code, line 2 of 2.

  ClearSlate();
  DisplaySlate();

  // start with morphing plasma, but allow going to color cycling if desired.
  state=1;
  paletteShift=128000;

  //generate the plasma once
  for(x = 0; x < screenWidth; x++)
    for(y = 0; y < screenHeight; y++)
    {
      //the plasma buffer is a sum of sines
      color = (byte)
      (
            128.0 + (128.0 * sin(x*8.0 / 16.0))
          + 128.0 + (128.0 * sin(y*8.0 / 16.0))
      ) / 2;
#ifndef MeggySimulator
      color>>4;
#endif
      x &= 7;
      y &= 7;
      plasma[x][y] = color;
    }

    //FillColorPalette();
}

/*
void
plasma_semi (byte x1, byte y1, byte w, byte h, double zoom)
{
  int x, y;
  byte color[3];
  double a=0.0,b=0.0,c=0.0,d=0.0;

    for(x = x1; x <= w; x++)
    for(y = y1; y <= h; y++)
    {
        color[0] = (byte)
        (
              128.0 + (128.0 * sin(x*zoom / 8.0))
            + 128.0 + (128.0 * sin(y*zoom / 8.0))
        ) / 2;
        color[1] = color[0];
        color[2] = color[0];
        MySetPxClr(x, y, color);
    }
}
*/
/*
void
CycleColorPalette()
{
  byte color[3];
  int x,y;
  //generate the palette
  ColorRGB colorRGB;
  ColorHSV colorHSV;

  for(x = 0; x < screenWidth; x++)
  for(y = 0; y < screenHeight; y++)
  {
    colorHSV.h=(plasma[x][y]+paletteShift)&0xff; 
    colorHSV.s=255; 
    colorHSV.v=255;
    HSVtoRGB(&colorRGB, &colorHSV);

    color[0] = colorRGB.r>>4;
    color[1] = colorRGB.g>>4;
    color[2] = colorRGB.b>>4;

    Meg.SetPxClr(x,y,color);
  }
}
*/

void
CycleColorPalette()
{
  byte color[3];
  int x,y;
  uint16_t buffer;
  //generate the palette
//  ColorRGB colorRGB;
//  ColorHSV colorHSV;

  for(x = 0; x < screenWidth; x++)
  for(y = 0; y < screenHeight; y++)
  {
    buffer=((plasma[x][y]+paletteShift)&0xff)>>2; 

    color[0] = (palette[buffer]>>8)&0xf;
    color[1] = (palette[buffer]>>4)&0xf;
    color[2] = palette[buffer]&0xf;

    Meg.SetPxClr(x,y,color);
  }
}

double
dist(double a, double b, double c, double d) 
{
  return sqrt((c-a)*(c-a)+(d-b)*(d-b));
}

void
plasma_morph()
{
  int x,y;
  double value;
  byte color[3];
  uint16_t buffer;
//  ColorRGB colorRGB;
//  ColorHSV colorHSV;

  for(x = 0; x < screenWidth; x++)
  for(y = 0; y < screenHeight; y++)
  {
/*
    value = sin(dist(x + paletteShift, y, 128.0, 128.0) / 8.0)
                 + sin(dist(x, y, 64.0, 64.0) / 8.0)
                 + sin(dist(x, y + paletteShift / 7, 192.0, 64) / 7.0)
                 + sin(dist(x, y, 192.0, 100.0) / 8.0);
*/
    value = sin(dist(x + paletteShift, y, 128.0, 128.0) / 8.0)
//                 + sin(dist(x, y, 64.0, 64.0) / 8.0)
                 + sin(dist(x, y + paletteShift / 7, 192.0, 64) / 7.0)
//                 + sin(dist(x, y, 192.0, 100.0) / 8.0)
                 ;
/*
  old, expensive way...
    colorHSV.h=(int)((4 + value) * 128)&0xff;
    colorHSV.s=255; 
    colorHSV.v=255;
    HSVtoRGB(&colorRGB, &colorHSV);

    color[0] = colorRGB.r>>4;
    color[1] = colorRGB.g>>4;
    color[2] = colorRGB.b>>4;
//    MySetPxClr(x, y, ColorRGB(color, color * 2, 255 - color));
    MySetPxClr(x, y, color);
*/
    buffer=((int)((4.0+value)*32.0))&0x3f;

    color[0] = (palette[buffer]>>8)&0xf;
    color[1] = (palette[buffer]>>4)&0xf;
    color[2] = palette[buffer]&0xf;

    Meg.SetPxClr(x,y,color);

  }  
}

void
HandleKeys()
{
  
  CheckButtonsPress();
  
  if (Button_A) {
    ClearSlate();
    if (state==0) {
      state=1;
    } else if (state==1) {
      state=0;
    }
  }
  if (Button_B) {
  }
}

void loop()                     // run over and over again
{

  HandleKeys();

  paletteShift+=1;

  switch(state) {
    case 0:
      CycleColorPalette();
      break;
    case 1:
      plasma_morph();
      break;
    default:
      state=0;
      break;
  }

#ifdef MeggySimulator
  delay(50);                  // waits for a moment
#endif
}


