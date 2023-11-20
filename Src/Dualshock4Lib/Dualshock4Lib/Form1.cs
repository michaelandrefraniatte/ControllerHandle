using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using DualShock4API;
using System.Numerics;

namespace Dualshock4Lib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        private static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        private static uint CurrentResolution = 0;
        private static bool running;
        private int sleeptime = 1;
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            Task.Run(() => Start());
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                running = false;
                Thread.Sleep(100);
            }
            catch { }
        }
        private void Start()
        {
            running = true;
            ds4 = DS4ChooseController();
            if (ds4 != null)
            {
                Task.Run(() => DS4MainAsyncPolling());
                Task.Run(() => taskX());
            }
        }
        private void taskX()
        {
            while (running)
            {
                DS4ProcessStateLogic();
                string text = "";
                text += "PS4ControllerButtonCrossPressed: " + PS4ControllerButtonCrossPressed + Environment.NewLine;
                text += "PS4ControllerButtonCirclePressed: " + PS4ControllerButtonCirclePressed + Environment.NewLine;
                text += "PS4ControllerButtonSquarePressed: " + PS4ControllerButtonSquarePressed + Environment.NewLine;
                text += "PS4ControllerButtonTrianglePressed: " + PS4ControllerButtonTrianglePressed + Environment.NewLine;
                text += "PS4ControllerButtonDPadUpPressed: " + PS4ControllerButtonDPadUpPressed + Environment.NewLine;
                text += "PS4ControllerButtonDPadRightPressed: " + PS4ControllerButtonDPadRightPressed + Environment.NewLine;
                text += "PS4ControllerButtonDPadDownPressed: " + PS4ControllerButtonDPadDownPressed + Environment.NewLine;
                text += "PS4ControllerButtonDPadLeftPressed: " + PS4ControllerButtonDPadLeftPressed + Environment.NewLine;
                text += "PS4ControllerButtonL1Pressed: " + PS4ControllerButtonL1Pressed + Environment.NewLine;
                text += "PS4ControllerButtonR1Pressed: " + PS4ControllerButtonR1Pressed + Environment.NewLine;
                text += "PS4ControllerButtonL2Pressed: " + PS4ControllerButtonL2Pressed + Environment.NewLine;
                text += "PS4ControllerButtonR2Pressed: " + PS4ControllerButtonR2Pressed + Environment.NewLine;
                text += "PS4ControllerButtonL3Pressed: " + PS4ControllerButtonL3Pressed + Environment.NewLine;
                text += "PS4ControllerButtonR3Pressed: " + PS4ControllerButtonR3Pressed + Environment.NewLine;
                text += "PS4ControllerButtonCreatePressed: " + PS4ControllerButtonCreatePressed + Environment.NewLine;
                text += "PS4ControllerButtonMenuPressed: " + PS4ControllerButtonMenuPressed + Environment.NewLine;
                text += "PS4ControllerButtonLogoPressed: " + PS4ControllerButtonLogoPressed + Environment.NewLine;
                text += "PS4ControllerButtonTouchpadPressed: " + PS4ControllerButtonTouchpadPressed + Environment.NewLine;
                text += "PS4ControllerButtonMicPressed: " + PS4ControllerButtonMicPressed + Environment.NewLine;
                text += "PS4ControllerTouchOn: " + PS4ControllerTouchOn + Environment.NewLine;
                text += "PS4ControllerAccelX: " + PS4ControllerAccelX + Environment.NewLine;
                text += "PS4ControllerAccelY: " + PS4ControllerAccelY + Environment.NewLine;
                text += "PS4ControllerGyroX: " + PS4ControllerGyroX + Environment.NewLine;
                text += "PS4ControllerGyroY: " + PS4ControllerGyroY + Environment.NewLine;
                text += "PS4ControllerLeftStickX: " + PS4ControllerLeftStickX + Environment.NewLine;
                text += "PS4ControllerLeftStickY: " + PS4ControllerLeftStickY + Environment.NewLine;
                text += "PS4ControllerRightStickX: " + PS4ControllerRightStickX + Environment.NewLine;
                text += "PS4ControllerRightStickY: " + PS4ControllerRightStickY + Environment.NewLine;
                text += "PS4ControllerRightTriggerPosition: " + PS4ControllerRightTriggerPosition + Environment.NewLine;
                text += "PS4ControllerLeftTriggerPosition: " + PS4ControllerLeftTriggerPosition + Environment.NewLine;
                text += "PS4ControllerTouchX: " + PS4ControllerTouchX + Environment.NewLine;
                text += "PS4ControllerTouchY: " + PS4ControllerTouchY + Environment.NewLine;
                label1.Text = text;
                Thread.Sleep(sleeptime);
            }
        }
        public static bool PS4ControllerButtonCrossPressed;
        public static bool PS4ControllerButtonCirclePressed;
        public static bool PS4ControllerButtonSquarePressed;
        public static bool PS4ControllerButtonTrianglePressed;
        public static bool PS4ControllerButtonDPadUpPressed;
        public static bool PS4ControllerButtonDPadRightPressed;
        public static bool PS4ControllerButtonDPadDownPressed;
        public static bool PS4ControllerButtonDPadLeftPressed;
        public static bool PS4ControllerButtonL1Pressed;
        public static bool PS4ControllerButtonR1Pressed;
        public static bool PS4ControllerButtonL2Pressed;
        public static bool PS4ControllerButtonR2Pressed;
        public static bool PS4ControllerButtonL3Pressed;
        public static bool PS4ControllerButtonR3Pressed;
        public static bool PS4ControllerButtonCreatePressed;
        public static bool PS4ControllerButtonMenuPressed;
        public static bool PS4ControllerButtonLogoPressed;
        public static bool PS4ControllerButtonTouchpadPressed;
        public static bool PS4ControllerButtonMicPressed;
        public static bool PS4ControllerTouchOn;
        private static double PS4ControllerLeftStickX, PS4ControllerLeftStickY, PS4ControllerRightStickX, PS4ControllerRightStickY, PS4ControllerRightTriggerPosition, PS4ControllerLeftTriggerPosition, PS4ControllerTouchX, PS4ControllerTouchY;
        public static bool PS4ControllerAccelCenter;
        public static double PS4ControllerAccelX, PS4ControllerAccelY, PS4ControllerGyroX, PS4ControllerGyroY;
        public static Vector3 gyr_gPS4 = new Vector3();
        public static Vector3 acc_gPS4 = new Vector3();
        public static Vector3 InitDirectAnglesPS4, DirectAnglesPS4;
        private static DualShock4 ds4;
        public static void DS4ProcessStateLogic()
        {
            PS4ControllerLeftStickX = DualShock4.LeftAnalogStick.X;
            PS4ControllerLeftStickY = DualShock4.LeftAnalogStick.Y;
            PS4ControllerRightStickX = -DualShock4.RightAnalogStick.X;
            PS4ControllerRightStickY = -DualShock4.RightAnalogStick.Y;
            PS4ControllerLeftTriggerPosition = DualShock4.L2;
            PS4ControllerRightTriggerPosition = DualShock4.R2;
            PS4ControllerTouchX = DualShock4.Touchpad1.X;
            PS4ControllerTouchY = DualShock4.Touchpad1.Y;
            PS4ControllerTouchOn = DualShock4.Touchpad1.IsDown;
            gyr_gPS4.X = DualShock4.Gyro.Z;
            gyr_gPS4.Y = -DualShock4.Gyro.X;
            gyr_gPS4.Z = -DualShock4.Gyro.Y;
            PS4ControllerGyroX = gyr_gPS4.Z;
            PS4ControllerGyroY = gyr_gPS4.Y;
            acc_gPS4 = new Vector3(DualShock4.Accelerometer.X, DualShock4.Accelerometer.Z, DualShock4.Accelerometer.Y);
            if (PS4ControllerAccelCenter)
                InitDirectAnglesPS4 = acc_gPS4;
            DirectAnglesPS4 = acc_gPS4 - InitDirectAnglesPS4;
            PS4ControllerAccelX = -(DirectAnglesPS4.Y + DirectAnglesPS4.Z) / 6f;
            PS4ControllerAccelY = DirectAnglesPS4.X / 6f;
            PS4ControllerButtonCrossPressed = DualShock4.CrossButton;
            PS4ControllerButtonCirclePressed = DualShock4.CircleButton;
            PS4ControllerButtonSquarePressed = DualShock4.SquareButton;
            PS4ControllerButtonTrianglePressed = DualShock4.TriangleButton;
            PS4ControllerButtonDPadUpPressed = DualShock4.DPadUpButton;
            PS4ControllerButtonDPadRightPressed = DualShock4.DPadRightButton;
            PS4ControllerButtonDPadDownPressed = DualShock4.DPadDownButton;
            PS4ControllerButtonDPadLeftPressed = DualShock4.DPadLeftButton;
            PS4ControllerButtonL1Pressed = DualShock4.L1Button;
            PS4ControllerButtonR1Pressed = DualShock4.R1Button;
            PS4ControllerButtonL2Pressed = DualShock4.L2Button;
            PS4ControllerButtonR2Pressed = DualShock4.R2Button;
            PS4ControllerButtonL3Pressed = DualShock4.L3Button;
            PS4ControllerButtonR3Pressed = DualShock4.R3Button;
            PS4ControllerButtonCreatePressed = DualShock4.CreateButton;
            PS4ControllerButtonMenuPressed = DualShock4.MenuButton;
            PS4ControllerButtonLogoPressed = DualShock4.LogoButton;
            PS4ControllerButtonTouchpadPressed = DualShock4.TouchpadButton;
            PS4ControllerButtonMicPressed = DualShock4.MicButton;
        }
        static T DS4Choose<T>(T ts)
        {
            return ts;
        }
        static DualShock4 DS4ChooseController()
        {
            DualShock4 available = DualShock4.EnumerateControllers("54C", "9CC", "Wireless Controller");
            if (available == null)
            {
                return null;
            }
            return DS4Choose(available);
        }
        static void DS4MainAsyncPolling()
        {
            ds4.Acquire();
            while (running)
            {
                ds4.BeginPolling();
            }
            ds4.EndPolling();
            ds4.Release();
        }
    }
}
