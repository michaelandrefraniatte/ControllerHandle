using DualSenseAPI;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace DualsenseLib
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
        private void Start()
        {
            running = true;
            ds = ChooseController();
            if (ds != null)
            {
                Task.Run(() => MainAsyncPolling());
                Task.Run(() => taskX());
            }
        }
        private void taskX()
        {
            while (running)
            {
                ProcessStateLogic();
                string text = "";
                text += "PS5ControllerButtonCrossPressed: " + PS5ControllerButtonCrossPressed + Environment.NewLine;
                text += "PS5ControllerButtonCirclePressed: " + PS5ControllerButtonCirclePressed + Environment.NewLine;
                text += "PS5ControllerButtonSquarePressed: " + PS5ControllerButtonSquarePressed + Environment.NewLine;
                text += "PS5ControllerButtonTrianglePressed: " + PS5ControllerButtonTrianglePressed + Environment.NewLine;
                text += "PS5ControllerButtonDPadUpPressed: " + PS5ControllerButtonDPadUpPressed + Environment.NewLine;
                text += "PS5ControllerButtonDPadRightPressed: " + PS5ControllerButtonDPadRightPressed + Environment.NewLine;
                text += "PS5ControllerButtonDPadDownPressed: " + PS5ControllerButtonDPadDownPressed + Environment.NewLine;
                text += "PS5ControllerButtonDPadLeftPressed: " + PS5ControllerButtonDPadLeftPressed + Environment.NewLine;
                text += "PS5ControllerButtonL1Pressed: " + PS5ControllerButtonL1Pressed + Environment.NewLine;
                text += "PS5ControllerButtonR1Pressed: " + PS5ControllerButtonR1Pressed + Environment.NewLine;
                text += "PS5ControllerButtonL2Pressed: " + PS5ControllerButtonL2Pressed + Environment.NewLine;
                text += "PS5ControllerButtonR2Pressed: " + PS5ControllerButtonR2Pressed + Environment.NewLine;
                text += "PS5ControllerButtonL3Pressed: " + PS5ControllerButtonL3Pressed + Environment.NewLine;
                text += "PS5ControllerButtonR3Pressed: " + PS5ControllerButtonR3Pressed + Environment.NewLine;
                text += "PS5ControllerButtonCreatePressed: " + PS5ControllerButtonCreatePressed + Environment.NewLine;
                text += "PS5ControllerButtonMenuPressed: " + PS5ControllerButtonMenuPressed + Environment.NewLine;
                text += "PS5ControllerButtonLogoPressed: " + PS5ControllerButtonLogoPressed + Environment.NewLine;
                text += "PS5ControllerButtonTouchpadPressed: " + PS5ControllerButtonTouchpadPressed + Environment.NewLine;
                text += "PS5ControllerButtonMicPressed: " + PS5ControllerButtonMicPressed + Environment.NewLine;
                text += "PS5ControllerTouchOn: " + PS5ControllerTouchOn + Environment.NewLine;
                text += "PS5ControllerAccelX: " + PS5ControllerAccelX + Environment.NewLine;
                text += "PS5ControllerAccelY: " + PS5ControllerAccelY + Environment.NewLine;
                text += "PS5ControllerGyroX: " + PS5ControllerGyroX + Environment.NewLine;
                text += "PS5ControllerGyroY: " + PS5ControllerGyroY + Environment.NewLine;
                text += "PS5ControllerLeftStickX: " + PS5ControllerLeftStickX + Environment.NewLine;
                text += "PS5ControllerLeftStickY: " + PS5ControllerLeftStickY + Environment.NewLine;
                text += "PS5ControllerRightStickX: " + PS5ControllerRightStickX + Environment.NewLine;
                text += "PS5ControllerRightStickY: " + PS5ControllerRightStickY + Environment.NewLine;
                text += "PS5ControllerRightTriggerPosition: " + PS5ControllerRightTriggerPosition + Environment.NewLine;
                text += "PS5ControllerLeftTriggerPosition: " + PS5ControllerLeftTriggerPosition + Environment.NewLine;
                text += "PS5ControllerTouchX: " + PS5ControllerTouchX + Environment.NewLine;
                text += "PS5ControllerTouchY: " + PS5ControllerTouchY + Environment.NewLine;
                label1.Text = text;
                Thread.Sleep(sleeptime);
            }
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
        public static bool PS5ControllerButtonCrossPressed;
        public static bool PS5ControllerButtonCirclePressed;
        public static bool PS5ControllerButtonSquarePressed;
        public static bool PS5ControllerButtonTrianglePressed;
        public static bool PS5ControllerButtonDPadUpPressed;
        public static bool PS5ControllerButtonDPadRightPressed;
        public static bool PS5ControllerButtonDPadDownPressed;
        public static bool PS5ControllerButtonDPadLeftPressed;
        public static bool PS5ControllerButtonL1Pressed;
        public static bool PS5ControllerButtonR1Pressed;
        public static bool PS5ControllerButtonL2Pressed;
        public static bool PS5ControllerButtonR2Pressed;
        public static bool PS5ControllerButtonL3Pressed;
        public static bool PS5ControllerButtonR3Pressed;
        public static bool PS5ControllerButtonCreatePressed;
        public static bool PS5ControllerButtonMenuPressed;
        public static bool PS5ControllerButtonLogoPressed;
        public static bool PS5ControllerButtonTouchpadPressed;
        public static bool PS5ControllerButtonMicPressed;
        public static bool PS5ControllerTouchOn;
        private static double PS5ControllerLeftStickX, PS5ControllerLeftStickY, PS5ControllerRightStickX, PS5ControllerRightStickY, PS5ControllerRightTriggerPosition, PS5ControllerLeftTriggerPosition, PS5ControllerTouchX, PS5ControllerTouchY;
        public static bool PS5ControllerAccelCenter;
        public static double PS5ControllerAccelX, PS5ControllerAccelY, PS5ControllerGyroX, PS5ControllerGyroY;
        public static Vector3 gyr_gPS5 = new Vector3();
        public static Vector3 acc_gPS5 = new Vector3();
        public static Vector3 InitDirectAnglesPS5, DirectAnglesPS5;
        private static DualSense ds;
        public static void ProcessStateLogic()
        {
            PS5ControllerLeftStickX = DualSense.LeftAnalogStick.X;
            PS5ControllerLeftStickY = DualSense.LeftAnalogStick.Y;
            PS5ControllerRightStickX = -DualSense.RightAnalogStick.X;
            PS5ControllerRightStickY = -DualSense.RightAnalogStick.Y;
            PS5ControllerLeftTriggerPosition = DualSense.L2;
            PS5ControllerRightTriggerPosition = DualSense.R2;
            PS5ControllerTouchX = DualSense.Touchpad1.X;
            PS5ControllerTouchY = DualSense.Touchpad1.Y;
            PS5ControllerTouchOn = DualSense.Touchpad1.IsDown;
            gyr_gPS5.X = DualSense.Gyro.Z;
            gyr_gPS5.Y = -DualSense.Gyro.X;
            gyr_gPS5.Z = -DualSense.Gyro.Y;
            PS5ControllerGyroX = gyr_gPS5.Z;
            PS5ControllerGyroY = gyr_gPS5.Y;
            acc_gPS5 = new Vector3(DualSense.Accelerometer.X, DualSense.Accelerometer.Z, DualSense.Accelerometer.Y);
            if (PS5ControllerAccelCenter)
                InitDirectAnglesPS5 = acc_gPS5;
            DirectAnglesPS5 = acc_gPS5 - InitDirectAnglesPS5;
            PS5ControllerAccelX = -(DirectAnglesPS5.Y + DirectAnglesPS5.Z) / 6f;
            PS5ControllerAccelY = DirectAnglesPS5.X / 6f;
            PS5ControllerButtonCrossPressed = DualSense.CrossButton;
            PS5ControllerButtonCirclePressed = DualSense.CircleButton;
            PS5ControllerButtonSquarePressed = DualSense.SquareButton;
            PS5ControllerButtonTrianglePressed = DualSense.TriangleButton;
            PS5ControllerButtonDPadUpPressed = DualSense.DPadUpButton;
            PS5ControllerButtonDPadRightPressed = DualSense.DPadRightButton;
            PS5ControllerButtonDPadDownPressed = DualSense.DPadDownButton;
            PS5ControllerButtonDPadLeftPressed = DualSense.DPadLeftButton;
            PS5ControllerButtonL1Pressed = DualSense.L1Button;
            PS5ControllerButtonR1Pressed = DualSense.R1Button;
            PS5ControllerButtonL2Pressed = DualSense.L2Button;
            PS5ControllerButtonR2Pressed = DualSense.R2Button;
            PS5ControllerButtonL3Pressed = DualSense.L3Button;
            PS5ControllerButtonR3Pressed = DualSense.R3Button;
            PS5ControllerButtonCreatePressed = DualSense.CreateButton;
            PS5ControllerButtonMenuPressed = DualSense.MenuButton;
            PS5ControllerButtonLogoPressed = DualSense.LogoButton;
            PS5ControllerButtonTouchpadPressed = DualSense.TouchpadButton;
            PS5ControllerButtonMicPressed = DualSense.MicButton;
        }
        static T Choose<T>(T ts)
        {
            return ts;
        }
        static DualSense ChooseController()
        {
            DualSense available = DualSense.EnumerateControllers("54C", "CE6", "DualSense");
            if (available == null)
            {
                return null;
            }
            return Choose(available);
        }
        static void MainAsyncPolling()
        {
            ds.Acquire();
            while (running)
            {
                ds.BeginPolling();
            }
            ds.EndPolling();
            ds.Release();
        }
    }
}
