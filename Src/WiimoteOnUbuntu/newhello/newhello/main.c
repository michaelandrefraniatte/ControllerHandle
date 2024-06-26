 #define _XOPEN_SOURCE 500
 #define XOPEN
#include <stdio.h>
#include <X11/Xlib.h>
//#include <X11/X.h>
#include <X11/Xutil.h>
#include <X11/extensions/XTest.h>
//#include <X11/keysym.h>
#include <stdlib.h>
#include <xdo.h>
#include <unistd.h>
#include <sys/types.h>
#include "/home/mike/Projects/hello/hello/wiimote_api.h"
#include "/home/mike/Projects/hello/hello/wiimote.h"
//#include "/home/mike/Projects/hello/hello/Xwindows.h"
//#include "/home/mike/Projects/hello/hello/Xlib.h"

int main ()
{
	//SetCursorPos(100, 100);
	//00:19:1D:94:3D:D3
	//Display *dpy;
    //Window root_window;
	//dpy = XOpenDisplay(0);
	//root_window = XRootWindow(dpy, 0);
	//XSelectInput(dpy, root_window, KeyReleaseMask);
	//XWarpPointer(NULL, NULL, NULL, 0, 0, 0, 0, 100, 100);
	//Gdk.Display.Default.WarpPointer(Gdk.Display.DefaultScreen, 100, 100);
		//useconds_t * test = 0;
		//Window * win = CURRENTWINDOW;
		//xdo_t * xdo = NULL;
		//const char * shift = "Shift_L";
	//xdo_keysequence(xdo, CURRENTWINDOW, "A", 0);
	system("xdotool key Shift+a");
	//xdo_mousemove_relative(xdo, 100, 100);
	//system("xdotool mousemove --sync 1000 1000");
	char str[256];
	int a = 10;
	int b = 10;
	int a1;
	int b1;
	int a2;
	int b2;
	sprintf(str, "xdotool mousemove_relative -- %d %d", -a, b);
	system(str);
	//sleep(100);
	wiimote_t wi;                                                                                                                                  
   wiimote_connect(&wi, "00:19:1D:94:3D:D3");
   wi.mode.ir  = 1;		// enable ir-sensor
   wi.mode.ext = 1;		// enable nunchuk (not necessary)
   wi.mode.acc = 1;		// enable accelerometer
   //wi.mode.nunchuk.acc = 1;
   while (wiimote_is_open(&wi)) {
     wiimote_update(&wi);
     if (wi.keys.home) {
       wiimote_disconnect(&wi);
     }
     if (wi.keys.a) {
       system("xdotool key s");
     }   
     if (wi.keys.b) {
       system("xdotool click 1");
     }   
     //printf("joyx=%d joyy=%d\n", wi.ext.nunchuk.joyx, wi.ext.nunchuk.joyy);
     printf("irx=%d iry=%d size=%d\n", wi.ir1.x, wi.ir1.y, wi.ir1.size);
     printf("irx=%d iry=%d size=%d\n", wi.ir2.x, wi.ir2.y, wi.ir2.size);
     printf("x=%d y=%d z=%d\n", wi.ext.nunchuk.axis.x, wi.ext.nunchuk.axis.y, wi.ext.nunchuk.axis.z);
     //printf("x=%d y=%d z=%d\n", wi.axis.x, wi.axis.y, wi.axis.z);
  a1 = wi.ir1.x;
  b1 = wi.ir1.y;
  a2 = wi.ir2.x;
  b2 = wi.ir2.y;
  if (wi.ir1.x < 1791 & wi.ir2.x < 1791 & wi.ir1.y < 1791 & wi.ir2.y < 1791) {
  sprintf(str, "xdotool  mousemove_relative -- %d %d", 18  - (wi.ir1.x)/100 - (wi.ir2.x) / 100, -18 + (wi.ir1.y)/100 + (wi.ir2.y)/100);
	system(str);
  }
	sleep(0.020);
  }
}



