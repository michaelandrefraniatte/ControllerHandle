#include <iostream>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>
#include <dos.h>
#include <windows.h>
#include <ctime>
#include <conio.h>
#include <GameInput.h>
using namespace std;
using std::cout;
using std::endl;

IGameInput* g_gameInput = nullptr;
IGameInputDevice* g_gamepad = nullptr;

int main()
{
    char c;
    std::cout << "press esc to exit! " << std::endl;
    HRESULT hr = GameInputCreate(&g_gameInput);
    std::cout << hr << "\n";
    std::cout << g_gameInput << "\n";
    while (true) 
    {
        // Ask for the latest reading from devices that provide fixed-format
        // gamepad state. If a device has been assigned to g_gamepad, filter
        // readings to just the ones coming from that device. Otherwise, if
        // g_gamepad is null, it will allow readings from any device.
        IGameInputReading* reading;
        if (SUCCEEDED(g_gameInput->GetCurrentReading(GameInputKindGamepad, g_gamepad, &reading)))
        {
            // If no device has been assigned to g_gamepad yet, set it
            // to the first device we receive input from. (This must be
            // the one the player is using because it's generating input.)
            if (!g_gamepad) 
                reading->GetDevice(&g_gamepad);
            // Retrieve the fixed-format gamepad state from the reading.
            GameInputGamepadState state;
            reading->GetGamepadState(&state);
            reading->Release();
            // Application-specific code to process the gamepad state goes here.
            std::cout << state.leftThumbstickX << "\n";
            std::cout << state.leftThumbstickY << "\n";
            std::cout << state.rightThumbstickX << "\n";
            std::cout << state.rightThumbstickY << "\n";
        }
        // If an error is returned from GetCurrentReading(), it means the
        // gamepad we were reading from has disconnected. Reset the
        // device pointer, and go back to looking for an active gamepad.
        else if (g_gamepad)
        {
            g_gamepad->Release();
            g_gamepad = nullptr;
        }
        c = getchar();
        if (c == 27)
            break;
        Sleep(5);
    }
    if (g_gamepad)
        g_gamepad->Release();
    if (g_gameInput)
        g_gameInput->Release();
    std::cout << "exited: " << std::endl;
    return 0;
}