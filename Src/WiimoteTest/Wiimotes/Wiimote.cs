using System.Linq;
using Device.Net;
using Hid.Net.Windows;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace Wiimotes
{
    public class Wiimote
    {
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimoteconnect")]
        public static extern bool wiimoteconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotedisconnect")]
        public static extern bool wiimotedisconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotesconnect")]
        public static extern bool wiimotesconnect();
        [DllImport("MotionInputPairing.dll", EntryPoint = "wiimotesdisconnect")]
        public static extern bool wiimotesdisconnect();
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        private static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        private static uint CurrentResolution = 0;
        public byte[] aBuffer = new byte[22];
        private int irmode, number;
        public List<double> vallistirx = new List<double>(), vallistiry = new List<double>();
        public double irxc, iryc, irx0, iry0, irx1, iry1, irx2, iry2, irx3, iry3, irx, iry, WiimoteIRSensors0X, WiimoteIRSensors0Y, WiimoteIRSensors1X, WiimoteIRSensors1Y, WiimoteRawValuesX, WiimoteRawValuesY, WiimoteRawValuesZ, calibrationinit, WiimoteIRSensors0Xcam, WiimoteIRSensors0Ycam, WiimoteIRSensors1Xcam, WiimoteIRSensors1Ycam, WiimoteIRSensorsXcam, WiimoteIRSensorsYcam;
        public bool WiimoteIR0foundcam, WiimoteIR1foundcam, WiimoteIRswitch, WiimoteIR1found, WiimoteIR0found, WiimoteButtonStateA, WiimoteButtonStateB, WiimoteButtonStateMinus, WiimoteButtonStateHome, WiimoteButtonStatePlus, WiimoteButtonStateOne, WiimoteButtonStateTwo, WiimoteButtonStateUp, WiimoteButtonStateDown, WiimoteButtonStateLeft, WiimoteButtonStateRight, WiimoteNunchuckStateC, WiimoteNunchuckStateZ;
        public double WiimoteIR0notfound, stickviewxinit, stickviewyinit, WiimoteNunchuckStateRawValuesX, WiimoteNunchuckStateRawValuesY, WiimoteNunchuckStateRawValuesZ, WiimoteNunchuckStateRawJoystickX, WiimoteNunchuckStateRawJoystickY, centery;
        public IDevice handle;
        private Stream mStream;
        public bool running;
        public Wiimote()
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            running = true;
            while (vallistirx.Count <= 2)
            {
                vallistirx.Add(0);
            }
            while (vallistiry.Count <= 2)
            {
                vallistiry.Add(0);
            }
        }
        public void Close()
        {
            running = false;
            Thread.Sleep(100);
            mStream.Close();
            mStream.Dispose();
            handle.Close();
            handle.Dispose();
            if (number == 0 | number == 1)
            {
                wiimotedisconnect();
            }
            if (number == 2)
            {
                wiimotesdisconnect();
            }
        }
        public async void Scan(string vendor_id, string product_id, string label_id, int number = 0)
        {
            this.number = number;
            if (number == 0)
                do
                    Thread.Sleep(1);
                while (!wiimoteconnect());
            else if (number == 1)
                do
                    Thread.Sleep(1);
                while (!wiimotesconnect());
            using (var hidFactory = new FilterDeviceDefinition((uint)int.Parse(vendor_id, System.Globalization.NumberStyles.HexNumber), (uint)int.Parse(product_id, System.Globalization.NumberStyles.HexNumber), label: label_id).CreateWindowsHidDeviceFactory())
            {
                using (var deviceDefinitions = hidFactory.GetConnectedDeviceDefinitionsAsync())
                {
                    if (number == 0 | number == 1)
                    {
                        handle = await hidFactory.GetDeviceAsync((await deviceDefinitions.ConfigureAwait(false)).First()).ConfigureAwait(false);
                    }
                    else if (number == 2)
                    {
                        handle = await hidFactory.GetDeviceAsync((await deviceDefinitions.ConfigureAwait(false)).Skip(1).First()).ConfigureAwait(false);
                    }
                    await handle.InitializeAsync().ConfigureAwait(false);
                    mStream = handle.GetFileStream();
                }
            }
        }
        public void ProcessStateLogic()
        {
            if (irmode == 1)
            {
                WiimoteIRSensors0X = aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8;
                WiimoteIRSensors0Y = aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8;
                WiimoteIRSensors1X = aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8;
                WiimoteIRSensors1Y = aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8;
                WiimoteIR0found = WiimoteIRSensors0X > 0f & WiimoteIRSensors0X <= 1024f & WiimoteIRSensors0Y > 0f & WiimoteIRSensors0Y <= 768f;
                WiimoteIR1found = WiimoteIRSensors1X > 0f & WiimoteIRSensors1X <= 1024f & WiimoteIRSensors1Y > 0f & WiimoteIRSensors1Y <= 768f;
                if (WiimoteIR0found)
                {
                    WiimoteIRSensors0Xcam = WiimoteIRSensors0X - 512f;
                    WiimoteIRSensors0Ycam = WiimoteIRSensors0Y - 384f;
                }
                if (WiimoteIR1found)
                {
                    WiimoteIRSensors1Xcam = WiimoteIRSensors1X - 512f;
                    WiimoteIRSensors1Ycam = WiimoteIRSensors1Y - 384f;
                }
                if (WiimoteIR0found & WiimoteIR1found)
                {
                    WiimoteIRSensorsXcam = (WiimoteIRSensors0Xcam + WiimoteIRSensors1Xcam) / 2f;
                    WiimoteIRSensorsYcam = (WiimoteIRSensors0Ycam + WiimoteIRSensors1Ycam) / 2f;
                }
                if (WiimoteIR0found)
                {
                    irx0 = 2 * WiimoteIRSensors0Xcam - WiimoteIRSensorsXcam;
                    iry0 = 2 * WiimoteIRSensors0Ycam - WiimoteIRSensorsYcam;
                }
                if (WiimoteIR1found)
                {
                    irx1 = 2 * WiimoteIRSensors1Xcam - WiimoteIRSensorsXcam;
                    iry1 = 2 * WiimoteIRSensors1Ycam - WiimoteIRSensorsYcam;
                }
                irxc = irx0 + irx1;
                iryc = iry0 + iry1;
            }
            else if (irmode == 2)
            {
                WiimoteIR0found = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) > 1 & (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) < 1023;
                WiimoteIR1found = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) > 1 & (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) < 1023;
                if (WiimoteIR0notfound == 0 & WiimoteIR1found)
                    WiimoteIR0notfound = 1;
                if (WiimoteIR0notfound == 1 & !WiimoteIR0found & !WiimoteIR1found)
                    WiimoteIR0notfound = 2;
                if (WiimoteIR0notfound == 2 & WiimoteIR0found)
                {
                    WiimoteIR0notfound = 0;
                    if (!WiimoteIRswitch)
                        WiimoteIRswitch = true;
                    else
                        WiimoteIRswitch = false;
                }
                if (WiimoteIR0notfound == 0 & WiimoteIR0found)
                    WiimoteIR0notfound = 0;
                if (WiimoteIR0notfound == 0 & !WiimoteIR0found & !WiimoteIR1found)
                    WiimoteIR0notfound = 0;
                if (WiimoteIR0notfound == 1 & WiimoteIR0found)
                    WiimoteIR0notfound = 0;
                if (WiimoteIR0found)
                {
                    WiimoteIRSensors0X = aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8;
                    WiimoteIRSensors0Y = aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8;
                }
                if (WiimoteIR1found)
                {
                    WiimoteIRSensors1X = aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8;
                    WiimoteIRSensors1Y = aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8;
                }
                if (WiimoteIRswitch)
                {
                    WiimoteIR0foundcam = WiimoteIR0found;
                    WiimoteIR1foundcam = WiimoteIR1found;
                    WiimoteIRSensors0Xcam = WiimoteIRSensors0X - 512f;
                    WiimoteIRSensors0Ycam = WiimoteIRSensors0Y - 384f;
                    WiimoteIRSensors1Xcam = WiimoteIRSensors1X - 512f;
                    WiimoteIRSensors1Ycam = WiimoteIRSensors1Y - 384f;
                }
                else
                {
                    WiimoteIR1foundcam = WiimoteIR0found;
                    WiimoteIR0foundcam = WiimoteIR1found;
                    WiimoteIRSensors1Xcam = WiimoteIRSensors0X - 512f;
                    WiimoteIRSensors1Ycam = WiimoteIRSensors0Y - 384f;
                    WiimoteIRSensors0Xcam = WiimoteIRSensors1X - 512f;
                    WiimoteIRSensors0Ycam = WiimoteIRSensors1Y - 384f;
                }
                if (WiimoteIR0foundcam & WiimoteIR1foundcam)
                {
                    irx2 = WiimoteIRSensors0Xcam;
                    iry2 = WiimoteIRSensors0Ycam;
                    irx3 = WiimoteIRSensors1Xcam;
                    iry3 = WiimoteIRSensors1Ycam;
                    WiimoteIRSensorsXcam = WiimoteIRSensors0Xcam - WiimoteIRSensors1Xcam;
                    WiimoteIRSensorsYcam = WiimoteIRSensors0Ycam - WiimoteIRSensors1Ycam;
                }
                if (WiimoteIR0foundcam & !WiimoteIR1foundcam)
                {
                    irx2 = WiimoteIRSensors0Xcam;
                    iry2 = WiimoteIRSensors0Ycam;
                    irx3 = WiimoteIRSensors0Xcam - WiimoteIRSensorsXcam;
                    iry3 = WiimoteIRSensors0Ycam - WiimoteIRSensorsYcam;
                }
                if (WiimoteIR1foundcam & !WiimoteIR0foundcam)
                {
                    irx3 = WiimoteIRSensors1Xcam;
                    iry3 = WiimoteIRSensors1Ycam;
                    irx2 = WiimoteIRSensors1Xcam + WiimoteIRSensorsXcam;
                    iry2 = WiimoteIRSensors1Ycam + WiimoteIRSensorsYcam;
                }
                irxc = irx2 + irx3;
                iryc = iry2 + iry3;
            }
            else if (irmode == 3)
            {
                WiimoteIR0found = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) > 1 & (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8) < 1023;
                WiimoteIR1found = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) > 1 & (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8) < 1023;
                if (WiimoteIR0found & WiimoteIR1found)
                {
                    WiimoteIRSensors0X = (aBuffer[6] | ((aBuffer[8] >> 4) & 0x03) << 8);
                    WiimoteIRSensors0Y = (aBuffer[7] | ((aBuffer[8] >> 6) & 0x03) << 8);
                    irx2 = WiimoteIRSensors0X - 512f;
                    iry2 = WiimoteIRSensors0Y - 384f;
                    WiimoteIRSensors1X = (aBuffer[9] | ((aBuffer[8] >> 0) & 0x03) << 8);
                    WiimoteIRSensors1Y = (aBuffer[10] | ((aBuffer[8] >> 2) & 0x03) << 8);
                    irx3 = WiimoteIRSensors1X - 512f;
                    iry3 = WiimoteIRSensors1Y - 384f;
                }
                irxc = (irx2 + irx3) / 512f * 1346f;
                iryc = (iry2 + iry3) / 768f * 782f;
            }
            if (WiimoteIR0found | WiimoteIR1found)
            {
                vallistirx.Add(irx);
                vallistirx.RemoveAt(0);
                vallistiry.Add(iry);
                vallistiry.RemoveAt(0);
                irx = irxc * (1024f / 1346f);
                iry = iryc + centery >= 0 ? Scale(iryc + centery, 0f, 782f + centery, 0f, 1024f) : Scale(iryc + centery, -782f + centery, 0f, -1024f, 0f);
            }
            else
            {
                if (irx - vallistirx.Average() >= 600f)
                    irx = 1024f;
                if (irx - vallistirx.Average() <= -600f)
                    irx = -1024f;
                if (iry - vallistiry.Average() >= 200f)
                    iry = 1024f;
                if (iry - vallistiry.Average() <= -200f)
                    iry = -1024f;
            }
            WiimoteButtonStateA = (aBuffer[2] & 0x08) != 0;
            WiimoteButtonStateB = (aBuffer[2] & 0x04) != 0;
            WiimoteButtonStateMinus = (aBuffer[2] & 0x10) != 0;
            WiimoteButtonStateHome = (aBuffer[2] & 0x80) != 0;
            WiimoteButtonStatePlus = (aBuffer[1] & 0x10) != 0;
            WiimoteButtonStateOne = (aBuffer[2] & 0x02) != 0;
            WiimoteButtonStateTwo = (aBuffer[2] & 0x01) != 0;
            WiimoteButtonStateUp = (aBuffer[1] & 0x08) != 0;
            WiimoteButtonStateDown = (aBuffer[1] & 0x04) != 0;
            WiimoteButtonStateLeft = (aBuffer[1] & 0x01) != 0;
            WiimoteButtonStateRight = (aBuffer[1] & 0x02) != 0;
            WiimoteRawValuesX = aBuffer[3] - 135f + calibrationinit;
            WiimoteRawValuesY = aBuffer[4] - 135f + calibrationinit;
            WiimoteRawValuesZ = aBuffer[5] - 135f + calibrationinit;
            WiimoteNunchuckStateRawJoystickX = aBuffer[16] - 125f + stickviewxinit;
            WiimoteNunchuckStateRawJoystickY = aBuffer[17] - 125f + stickviewyinit;
            WiimoteNunchuckStateRawValuesX = aBuffer[18] - 125f;
            WiimoteNunchuckStateRawValuesY = aBuffer[19] - 125f;
            WiimoteNunchuckStateRawValuesZ = aBuffer[20] - 125f;
            WiimoteNunchuckStateC = (aBuffer[21] & 0x02) == 0;
            WiimoteNunchuckStateZ = (aBuffer[21] & 0x01) == 0;
        }
        public void Init()
        {
            calibrationinit = -aBuffer[4] + 135f;
            stickviewxinit = -aBuffer[16] + 125f;
            stickviewyinit = -aBuffer[17] + 125f;
        }
        private void taskD()
        {
            for (; ; )
            {
                if (!running)
                    break;
                try
                {
                    mStream.Read(aBuffer, 0, 22);
                }
                catch { Thread.Sleep(10); }
                ProcessStateLogic();
            }
        }
        private double Scale(double value, double min, double max, double minScale, double maxScale)
        {
            double scaled = minScale + (double)(value - min) / (max - min) * (maxScale - minScale);
            return scaled;
        }
        public void BeginPolling()
        {
            Task.Run(() => taskD());
        }
    }
}