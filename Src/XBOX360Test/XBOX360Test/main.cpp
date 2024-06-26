#include <math.h>
#include <iostream>
#include <process.h>
#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <conio.h>
#include <string>
#ifndef _XBOX_CONTROLLER_H_
#define _XBOX_CONTROLLER_H_
// No MFC
#define WIN32_LEAN_AND_MEAN
// We need the Windows Header and the XInput Header
#include <windows.h>
#include <XInput.h>
// Now, the XInput Library
// NOTE: COMMENT THIS OUT IF YOU ARE NOT USING A COMPILER THAT SUPPORTS THIS METHOD OF LINKING LIBRARIES
#pragma comment(lib, "XInput.lib")
// XBOX Controller Class Definition
class CXBOXController
{
private:
	XINPUT_STATE _controllerState;
	int _controllerNum;
public:
	CXBOXController(int playerNumber);
	XINPUT_STATE GetState();
	bool IsConnected();
	void Vibrate(int leftVal = 0, int rightVal = 0);
};
#endif
CXBOXController::CXBOXController(int playerNumber)
{
	// Set the Controller Number
	_controllerNum = playerNumber - 1;
}
XINPUT_STATE CXBOXController::GetState()
{
	// Zeroise the state
	ZeroMemory(&_controllerState, sizeof(XINPUT_STATE));

	// Get the state
	XInputGetState(_controllerNum, &_controllerState);

	return _controllerState;
}
bool CXBOXController::IsConnected()
{
	// Zeroise the state
	ZeroMemory(&_controllerState, sizeof(XINPUT_STATE));

	// Get the state
	DWORD Result = XInputGetState(_controllerNum, &_controllerState);

	if(Result == ERROR_SUCCESS)
	{
		return true;
	}
	else
	{
		return false;
	}
}
void CXBOXController::Vibrate(int leftVal, int rightVal)
{
	// Create a Vibraton State
	XINPUT_VIBRATION Vibration;
	// Zeroise the Vibration
	ZeroMemory(&Vibration, sizeof(XINPUT_VIBRATION));
	// Set the Vibration Values
	Vibration.wLeftMotorSpeed = leftVal;
	Vibration.wRightMotorSpeed = rightVal;
	// Vibrate the controller
	XInputSetState(_controllerNum, &Vibration);
}
static int WidthS = 400;
        static int HeightS = 300;
        static int WA = 2;
        static int WJ1NJXWD = 2;
        static int WJ2NJXWD = 2;
        static int WJ1NJYWD = 2;
        static int WJ2NJYWD = 2;
        static int WJ1NJXWU = 2;
        static int WJ2NJXWU = 2;
        static int WJ1NJYWU = 2;
        static int WJ2NJYWU = 2;
        static int WJ1NAXWU = 2;
        static int WJ2NAXWU = 2;
        static int WJ1NAXWD = 2;
        static int WJ2NAXWD = 2;
        static int WAYSU = 2;
        static int WAYSD = 2;
        static int WHU = 2;
        static int WPU = 2;
        static int WMU = 2;
        static int WOU = 2;
        static int WTU = 2;
        static int WHD = 2;
        static int WPD = 2;
        static int WMD = 2;
        static int WOD = 2;
        static int WTD = 2;
        static int WJNAYSU = 2;
        static int WJNAYSD = 2;
        static int WDU = 2;
        static int WLU = 2;
        static int WRU = 2;
        static int WUD = 2;
        static int WDD = 2;
        static int WLD = 2;
        static int WRD = 2;
        static double Rand2swp = double();
        static double Rand2swyp = double();
        static double Rand2swm = double();
        static double Rand2swym = double();
        static double irx = double();
        static double iry = double();
        static double irx2e = double();
        static double iry2e = double();
        static double irx3e = double();
        static double iry3e = double();
        static int mousex = int();
        static int mousey = int();
        static double mousexm = double();
        static double mouseym = double();
        static double mousexr = double();
        static double mouseyr = double();
        static double mouseyp = double();
        static double mousexp = double();
        static int WAA = 2;
        static int WBBU = 2;
        static int WBBD = 2;
        static int WUU = 2;
        static int WZZU = 2;
        static int WZCU = 2;
        static int WCCU = 2;
        static int WZZD = 2;
        static int WZCD = 2;
        static int WCCD = 2;
        static int keys123 = int();
        static int keys456 = int();    
		static const int _INPUT_MOUSE = 0;
		static const int _MOUSEEVENTF_MOVE = 0x0001;
		static const int _MOUSEEVENTF_ABSOLUTE = 0x8000;
		static const int _MOUSEEVENTF_LEFTDOWN = 0x0002;
		static const int _MOUSEEVENTF_LEFTUP = 0x0004;
		static const int _MOUSEEVENTF_RIGHTDOWN = 0x0008;
		static const int _MOUSEEVENTF_RIGHTUP = 0x0010;
        static const int _KEYEVENTF_EXTENDEDKEY = 0x0001;
		static const int _KEYEVENTF_KEYUP = 0x0002;
		static int Mousedirectinputx = int();
        static int Mousedirectinputy = int();
        static int varxout = int();
        static int varyout = int();
		static int varxin = int();
        static int varyin = int();
        static int varpx = int();
        static int varpy = int();
        static int connecting = int();
        static double randirx1 = double();
        static double randiry1 = double();
		static int WF1U = 2;
        static int WF1D = 2;
		static bool F1B = false;
		static int WF2U = 2;
        static int WF2D = 2;
		static bool F2B = false;
		static int WF3U = 2;
        static int WF3D = 2;
		static bool F3B = false;
		static int WF4U = 2;
        static int WF4D = 2;
		static bool F4B = false;
			int signx = int();
			int signy = int();
CXBOXController* Player1;
 
int main(int argc, char* argv[])
{
	printf("*Controller by Mic Frametaux*");
	printf("\nSTART+UP = MW3");
	printf("\nSTART+RIGHT = BRINK");
	printf("\nSTART+DOWN = METRO");
	printf("\nSTART+LEFT = TITANFALL");
	printf("\nLEFT STICK = WASD");
	printf("\nRIGHT TRIGGER = E");
	printf("\nLEFT TRIGGER = Q");
	printf("\nA BUTTON = F(R)");
	printf("\nB BUTTON = V");
	printf("\nX BUTTON = SPACE");
	printf("\nY BUTTON = SHIFT");
	printf("\nRIGHT SHOULDER = LEFT CLICK");
	printf("\nLEFT SHOULDER = RIGHT CLICK");
	printf("\nBACK = CTRL");
	printf("\nSTART = G");
	printf("\nDOWN = C");
	printf("\nUP = X");
	printf("\nLEFT = 1,2,3");
	printf("\nRIGHT = 4,5,6");
	printf("\nSTART+SELECT = CLOSE");
	Player1 = new CXBOXController(1);
	while(true)
	{
		Player1 = new CXBOXController(1);
			if(Player1->IsConnected())
		{
			if (Player1->GetState().Gamepad.sThumbRX<0) {signx=-1;}
			if (Player1->GetState().Gamepad.sThumbRX>=0) {signx=1;}
			if (Player1->GetState().Gamepad.sThumbRY<0) {signy=-1;}
			if (Player1->GetState().Gamepad.sThumbRY>=0) {signy=1;}
            mousex = - (Player1->GetState().Gamepad.sThumbRX*Player1->GetState().Gamepad.sThumbRX)*signx/2000000;	
            mousey = - (Player1->GetState().Gamepad.sThumbRY*Player1->GetState().Gamepad.sThumbRY)*signy/4000000;		
						INPUT input[1];
						if (F1B == true)
			{
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = 65555 / 2 - mousex * 65555 / 400 / 2;
							input[0].mi.dy = mousey * 65555 / 300 / 2 + 65555 / 2;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
							SendInput(1, input, sizeof(INPUT));	
			}
                        if (F3B == true)  //Metro
                        {
                            mousexp = mousexp + mousex / 12;
                            mouseyp = mouseyp + mousey / 12;
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = -(int)mousexp;
							input[0].mi.dy = (int)mouseyp;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
							SendInput(1, input, sizeof(INPUT));	
                        }
                        if (F2B == true)  //Brink slow
                        {
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = -mousex / 8;
							input[0].mi.dy =  mousey / 8;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE;
							SendInput(1, input, sizeof(INPUT));	
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = -mousex / 40;
							input[0].mi.dy = mousey / 40;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
							SendInput(1, input, sizeof(INPUT));	
                        }
                        if (F4B == true) //Titanfall
                        {
                            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_LEFT_SHOULDER)
                            {
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = -mousex / 8;
							input[0].mi.dy = mousey / 8;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
							SendInput(1, input, sizeof(INPUT));	
                            }
                            else
                            {
							::ZeroMemory(input, sizeof(input));		
							input[0].type = _INPUT_MOUSE;		
							input[0].mi.dx = -mousex / 8;
							input[0].mi.dy = mousey / 8;
							input[0].mi.dwFlags = MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE;
							SendInput(1, input, sizeof(INPUT));	
                            }
                            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_LEFT_SHOULDER)
                            {
                                if (WAA <= 3)
                                    WAA = WAA + 1;
                                WA = 0;
                            }
                            else
                            {
                                if (WA <= 3)
                                    WA = WA + 1;
                                WAA = 0;
                            }
                            if (WAA == 1)
                            {
                                mouse_event(0x0008, 0, 0, 0, 0);;
                            }
                            if (WA == 1)
                            {
                                mouse_event(0x0010, 0, 0, 0, 0);;
                            }
                        }
			if ((Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_UP) && (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START))
            {
               if (WF1D <= 3)
                    WF1D = WF1D + 1;
                WF1U = 0;
            }
            else
            {
                if (WF1U <= 3)
                    WF1U = WF1U + 1;
                WF1D = 0;
            }
            if (WF1D == 1)
            {
				if (F1B == false)
					F1B=true;
				else
					F1B=false;
			}
			if ((Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_RIGHT) && (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START))
            {
                if (WF2D <= 3)
                    WF2D = WF2D + 1;
                WF2U = 0;
            }
            else
            {
                if (WF2U <= 3)
                    WF2U = WF2U + 1;
                WF2D = 0;
            }
            if (WF2D == 1)
            {
				if (F2B == false)
					F2B=true;
				else
					F2B=false;
			}
			if ((Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_DOWN) && (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START))
            {
                if (WF3D <= 3)
                    WF3D = WF3D + 1;
                WF3U = 0;
            }
            else
            {
                if (WF3U <= 3)
                    WF3U = WF3U + 1;
                WF3D = 0;
            }
            if (WF3D == 1)
            {
				if (F3B == false)
					F3B=true;
				else
					F3B=false;
			}
			if ((Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_LEFT) && (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START))
            {
                if (WF4D <= 3)
                    WF4D = WF4D + 1;
                WF4U = 0;
            }
            else
            {
                if (WF4U <= 3)
                    WF4U = WF4U + 1;
                WF4D = 0;
            }
            if (WF4D == 1)
            {
				if (F4B == false)
					F4B=true;
				else
					F4B=false;
			}
   ///////////////////////////////////////////
            if (Player1->GetState().Gamepad.sThumbLX * 2 >= 50*7000 / 100)
            {
                if (WJ1NJXWD <= 3)
                    WJ1NJXWD = WJ1NJXWD + 1;
                WJ1NJXWU = 0;
            }
            else
            {
                if (WJ1NJXWU <= 3)
                    WJ1NJXWU = WJ1NJXWU + 1;
                WJ1NJXWD = 0;
            }
            if (WJ1NJXWD == 1)
            {
                keybd_event(0x44, 0x20, 0, 0);
            }
            if (WJ1NJXWU == 1)
            {
                keybd_event(0x44, 0x20, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.sThumbLX * 2 <= -50*7000 / 100)
            {
                if (WJ2NJXWD <= 3)
                    WJ2NJXWD = WJ2NJXWD + 1;
                WJ2NJXWU = 0;
            }
            else
            {
                if (WJ2NJXWU <= 3)
                    WJ2NJXWU = WJ2NJXWU + 1;
                WJ2NJXWD = 0;
            }
            if (WJ2NJXWD == 1)
            {
                keybd_event(0x41, 0x1E, 0, 0);
            }
            if (WJ2NJXWU == 1)
            {
                keybd_event(0x41, 0x1E, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.sThumbLY * 2 >= 50*7000 / 100)
            {
                if (WJ1NJYWD <= 3)
                    WJ1NJYWD = WJ1NJYWD + 1;
                WJ1NJYWU = 0;
            }
            else
            {
                if (WJ1NJYWU <= 3)
                    WJ1NJYWU = WJ1NJYWU + 1;
                WJ1NJYWD = 0;
            }
            if (WJ1NJYWD == 1)
            {
                keybd_event(0x57, 0x11, 0, 0);
            }
            if (WJ1NJYWU == 1)
            {
                keybd_event(0x57, 0x11, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.sThumbLY * 2 <= -50*7000 / 100)
            {
                if (WJ2NJYWD <= 3)
                    WJ2NJYWD = WJ2NJYWD + 1;
                WJ2NJYWU = 0;
            }
            else
            {
                if (WJ2NJYWU <= 3)
                    WJ2NJYWU = WJ2NJYWU + 1;
                WJ2NJYWD = 0;
            }
            if (WJ2NJYWD == 1)
            {
                keybd_event(0x53, 0x1F, 0, 0);
            }
            if (WJ2NJYWU == 1)
            {
                keybd_event(0x53, 0x1F, 0x0002, 0);
            }
			if (Player1->GetState().Gamepad.bRightTrigger >= 0.2) //E
            {
                if (WJ1NAXWD <= 3)
                    WJ1NAXWD = WJ1NAXWD + 1;
                WJ1NAXWU = 0;
            }
            else
            {
                if (WJ1NAXWU <= 3)
                    WJ1NAXWU = WJ1NAXWU + 1;
                WJ1NAXWD = 0;
            }
            if (WJ1NAXWD == 1)
            {
                keybd_event(0x45, 0x12, 0, 0);
            }
            if (WJ1NAXWU == 1)
            {
                keybd_event(0x45, 0x12, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.bLeftTrigger >= 0.2)//Q
            {
                if (WJ2NAXWD <= 3)
                    WJ2NAXWD = WJ2NAXWD + 1;
                WJ2NAXWU = 0;
            }
            else
            {
                if (WJ2NAXWU <= 3)
                    WJ2NAXWU = WJ2NAXWU + 1;
                WJ2NAXWD = 0;
            }
            if (WJ2NAXWD == 1)
            {
                keybd_event(0x51, 0x10, 0, 0);
            }
            if (WJ2NAXWU == 1)
            {
                keybd_event(0x51, 0x10, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_B)//V
            {
                if (WJNAYSD <= 3)
                    WJNAYSD = WJNAYSD + 1;
                WJNAYSU = 0;
            }
            else
            {
                if (WJNAYSU <= 3)
                    WJNAYSU = WJNAYSU + 1;
                WJNAYSD = 0;
            }
            if (WJNAYSD == 1)
            {
                keybd_event(0x56, 0x2F, 0, 0);
            }
            if (WJNAYSU == 1)
            {
                keybd_event(0x56, 0x2F, 0x0002, 0);
            }
			if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_RIGHT_SHOULDER)
            {
                if (WBBD <= 3)
                    WBBD = WBBD + 1;
                WBBU = 0;
            }
            else
            {
                if (WBBU <= 3)
                    WBBU = WBBU + 1;
                WBBD = 0;
            }
            if (WBBD == 1)
            {
                mouse_event(0x0002, 0, 0, 0, 0);
            }
            if (WBBU == 1)
            {
                mouse_event(0x0004, 0, 0, 0, 0);
            }
			if (F4B == false) //Titanfall
            {
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_LEFT_SHOULDER)
            {
                if (WAA <= 3)
                    WAA = WAA + 1;
                WA = 0;
            }
            else
            {
                if (WA <= 3)
                    WA = WA + 1;
                WAA = 0;
            }
            if (WAA == 1)
            {
                mouse_event(0x0008, 0, 0, 0, 0);
            }
            if (WA == 1)
            {
                mouse_event(0x0010, 0, 0, 0, 0);
            }
			}
			if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_BACK) //CTRL
            {
                if (WZCD <= 3)
                    WZCD = WZCD + 1;
                WZCU = 0;
            }
            else
            {
                if (WZCU <= 3)
                    WZCU = WZCU + 1;
                WZCD = 0;
            }
            if (WZCD == 1)
            {
                keybd_event(0x11, 0x1D, 0, 0);
            }
            if (WZCU == 1)
            {
                keybd_event(0x11, 0x1D, 0x0002, 0);       
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_X) //SPACE
            {
                if (WCCD <= 3)
                    WCCD = WCCD + 1;
                WCCU = 0;
            }
            else
            {
                if (WCCU <= 3)
                    WCCU = WCCU + 1;
                WCCD = 0;
            }
            if (WCCD == 1)
            {
                keybd_event(0x20, 0x39, 0, 0);
            }
            if (WCCU == 1)
            {
                keybd_event(0x20, 0x39, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_Y) //SHIFT
            {
                if (WZZD <= 3)
                    WZZD = WZZD + 1;
                WZZU = 0;
            }
            else
            {
                if (WZZU <= 3)
                    WZZU = WZZU + 1;
                WZZD = 0;
            }
            if (WZZD == 1)
            {
                keybd_event(0x10, 0x2A, 0, 0);
            }
            if (WZZU == 1)
            {
                keybd_event(0x10, 0x2A, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_DOWN)
            {
                if (WDD <= 3)
                    WDD = WDD + 1;
                WDU = 0;
            }
            else
            {
                if (WDU <= 3)
                    WDU = WDU + 1;
                WDD = 0;
            }
            if (WDD == 1)
            {
                keybd_event(0x43, 0x2E, 0, 0);
            }
            if (WDU == 1)
            {
                keybd_event(0x43, 0x2E, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_A)//F & R
            {
                if (WHD <= 3)
                    WHD = WHD + 1;
                WHU = 0;
            }
            else
            {
                if (WHU <= 3)
                    WHU = WHU + 1;
                WHD = 0;
            }
            if (WHD == 1)
            {
                keybd_event(0x46, 0x21, 0, 0);
                keybd_event(0x52, 0x13, 0, 0);
            }
            if (WHU == 1)
            {
                keybd_event(0x46, 0x21, 0x0002, 0);
                keybd_event(0x52, 0x13, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START) //G
            {
                if (WPD <= 3)
                    WPD = WPD + 1;
                WPU = 0;
            }
            else
            {
                if (WPU <= 3)
                    WPU = WPU + 1;
                WPD = 0;
            }
            if (WPD == 1)
            {
                keybd_event(0x47, 0x22, 0, 0);
            }
            if (WPU == 1)
            {
                keybd_event(0x47, 0x22, 0x0002, 0);
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_LEFT)
            {
                if (WLD <= 3)
                    WLD = WLD + 1;
                WLU = 0;
            }
            else
            {
                if (WLU <= 3)
                    WLU = WLU + 1;
                WLD = 0;
            }
            if (WLD == 1)
            {
                if (keys123 == 0)
                {
                    keybd_event(0x31, 0x02, 0, 0);
                }
                if (keys123 == 1)
                {
                    keybd_event(0x32, 0x03, 0, 0);
                }
                if (keys123 == 2)
                {
                    keybd_event(0x33, 0x04, 0, 0);
                }
            }
            if (WLU == 1)
            {
                if (keys123 == 0)
                {
                    keybd_event(0x31, 0x02, 0x0002, 0);
                    keys123 = 1;
                }
                else
                {
                    if (keys123 == 1)
                    {
                        keybd_event(0x32, 0x03, 0x0002, 0);
                        keys123 = 2;
                    }
                    else
                        if (keys123 == 2)
                        {
                            keybd_event(0x33, 0x04, 0x0002, 0);
                            keys123 = 0;
                        }
                }
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_RIGHT)
            {
                if (WRD <= 3)
                    WRD = WRD + 1;
                WRU = 0;
            }
            else
            {
                if (WRU <= 3)
                    WRU = WRU + 1;
                WRD = 0;
            }
            if (WRD == 1)
            {
                if (keys456 == 0)
                {
                    keybd_event(0x34, 0x05, 0, 0);
                }
                if (keys456 == 1)
                {
                    keybd_event(0x35, 0x06, 0, 0);
                }
                if (keys456 == 2)
                {
                    keybd_event(0x36, 0x07, 0, 0);
                }
            }
            if (WRU == 1)
            {
                if (keys456 == 0)
                {
                    keybd_event(0x34, 0x05, 0x0002, 0);
                    keys456 = 1;
                }
                else
                {
                    if (keys456 == 1)
                    {
                        keybd_event(0x35, 0x06, 0x0002, 0);
                        keys456 = 2;
                    }
                    else
                        if (keys456 == 2)
                        {
                            keybd_event(0x36, 0x07, 0x0002, 0);
                            keys456 = 0;
                        }
                }
            }
            if (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_DPAD_UP)
            {
                if (WUD <= 3)
                    WUD = WUD + 1;
                WUU = 0;
            }
            else
            {
                if (WUU <= 3)
                    WUU = WUU + 1;
                WUD = 0;
            }
            if (WUD == 1)
            {
                keybd_event(0x58, 0x2D, 0, 0);
            }
            if (WUU == 1)
            {
                keybd_event(0x58, 0x2D, 0x0002, 0);
            }
			if ((Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_START) && (Player1->GetState().Gamepad.wButtons & XINPUT_GAMEPAD_BACK))
				break;
		Sleep(1);
		}
		else
		{
			std::cout << "\n\tERROR! PLAYER 1 - XBOX 360 Controller Not Found!\n";
		Sleep(1);
		}
}
}

