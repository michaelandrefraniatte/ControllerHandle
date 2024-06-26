
#include <stdio.h>
#include "/home/mike/Projects/hello/hello/wiimote_api.h"
#include "/home/mike/Projects/hello/hello/wiimote.h"

int main ()
{
	//00:19:1D:94:3D:D3
	wiimote_t wi;
   wiimote_connect(&wi, "00:19:1D:94:3D:D3");
   wi.mode.ir  = 1;		// enable ir-sensor
   wi.mode.ext = 1;		// enable nunchuk (not necessary)
   wi.mode.acc = 1;		// enable accelerometer
   while (wiimote_is_open(&wi)) {
     wiimote_update(&wi);
     if (wi.keys.home) {
       wiimote_disconnect(&wi);
     }
     printf("joyx=%d joyy=%d\n", wi.ext.nunchuk.joyx, wi.ext.nunchuk.joyy);
     printf("irx=%d iry=%d size=%d\n", wi.ir1.x, wi.ir1.y, wi.ir1.size);
     printf("x=%d y=%d z=%d\n", wi.axis.x, wi.axis.y, wi.axis.z);
   }
}



