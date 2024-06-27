#include <GameInput.h>
#include <iostream>
#include <thread>

IGameInput* g_gameInput = nullptr;
IGameInputDevice* g_device = nullptr;

HRESULT InitializeInput()
{
    return GameInputCreate(&g_gameInput);
}

void ShutdownInput()
{
    if (g_device) 
        g_device->Release();
    if (g_gameInput) 
        g_gameInput->Release();
}

bool PollGamepadInput()
{
    IGameInputReading* reading;
    if (SUCCEEDED(g_gameInput->GetCurrentReading(GameInputKindGamepad, g_device, &reading)))
    {
        if (!g_device)
        {
            reading->GetDevice(&g_device);

            auto info = g_device->GetDeviceInfo();
            std::cout << "Family: " << info->deviceFamily << std::endl;
            std::cout << "Capabilities: " << info->capabilities << std::endl;
            std::cout << "Vendor: " << info->vendorId << std::endl;
            std::cout << "Product: " << info->productId << std::endl;

            if (info->displayName)
                std::cout << "DisplayName: " << info->displayName->data << std::endl;
        }

        // Retrieve the fixed-format gamepad state from the reading.
        GameInputGamepadState state;
        reading->GetGamepadState(&state);
        reading->Release();

        // Application-specific code to process the gamepad state goes here.
        std::cout << state.leftThumbstickX << std::endl;
        std::cout << state.leftThumbstickY << std::endl;
        std::cout << state.rightThumbstickX << std::endl;
        std::cout << state.rightThumbstickY << std::endl;

        return true;
    }

    if (g_device)
    {
        g_device->Release();
        g_device = nullptr;
    }

    return true;
}

int main()
{
    if (InitializeInput() != S_OK)
        return -1;

    while (PollGamepadInput())
        std::this_thread::sleep_for(std::chrono::milliseconds(1));

    ShutdownInput();
    return 0;
}