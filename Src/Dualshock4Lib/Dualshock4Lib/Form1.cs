using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using DualShocks4API;

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
        public DualShock4 ds4 = new DualShock4();
        private static string vendor_ds4_id = "54C", product_ds4_id = "9CC", product_ds4_label = "Wireless Controller";
        private static bool running;
        private int sleeptime = 100;
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            Task.Run(() => Start());
        }
        private void Start()
        {
            running = true;
            ds4.Scan(vendor_ds4_id, product_ds4_id, product_ds4_label);
            ds4.BeginPolling();
            Task.Run(() => task());
        }
        private void task()
        {
            while (running)
            {
                string str = "PS4ControllerLeftStickX : " + ds4.PS4ControllerLeftStickX + Environment.NewLine;
                str += "PS4ControllerLeftStickY : " + ds4.PS4ControllerLeftStickY + Environment.NewLine;
                str += "PS4ControllerRightStickX : " + ds4.PS4ControllerRightStickX + Environment.NewLine;
                str += "PS4ControllerRightStickY : " + ds4.PS4ControllerRightStickY + Environment.NewLine;
                str += "PS4ControllerLeftTriggerPosition : " + ds4.PS4ControllerLeftTriggerPosition + Environment.NewLine;
                str += "PS4ControllerRightTriggerPosition : " + ds4.PS4ControllerRightTriggerPosition + Environment.NewLine;
                str += "PS4ControllerTouchX : " + ds4.PS4ControllerTouchX + Environment.NewLine;
                str += "PS4ControllerTouchY : " + ds4.PS4ControllerTouchY + Environment.NewLine;
                str += "PS4ControllerTouchOn : " + ds4.PS4ControllerTouchOn + Environment.NewLine;
                str += "PS4ControllerGyroX : " + ds4.PS4ControllerGyroX + Environment.NewLine;
                str += "PS4ControllerGyroY : " + ds4.PS4ControllerGyroY + Environment.NewLine;
                str += "PS4ControllerAccelX : " + ds4.PS4ControllerAccelX + Environment.NewLine;
                str += "PS4ControllerAccelY : " + ds4.PS4ControllerAccelY + Environment.NewLine;
                str += "PS4ControllerButtonCrossPressed : " + ds4.PS4ControllerButtonCrossPressed + Environment.NewLine;
                str += "PS4ControllerButtonCirclePressed : " + ds4.PS4ControllerButtonCirclePressed + Environment.NewLine;
                str += "PS4ControllerButtonSquarePressed : " + ds4.PS4ControllerButtonSquarePressed + Environment.NewLine;
                str += "PS4ControllerButtonTrianglePressed : " + ds4.PS4ControllerButtonTrianglePressed + Environment.NewLine;
                str += "PS4ControllerButtonDPadUpPressed : " + ds4.PS4ControllerButtonDPadUpPressed + Environment.NewLine;
                str += "PS4ControllerButtonDPadRightPressed : " + ds4.PS4ControllerButtonDPadRightPressed + Environment.NewLine;
                str += "PS4ControllerButtonDPadDownPressed : " + ds4.PS4ControllerButtonDPadDownPressed + Environment.NewLine;
                str += "PS4ControllerButtonDPadLeftPressed : " + ds4.PS4ControllerButtonDPadLeftPressed + Environment.NewLine;
                str += "PS4ControllerButtonL1Pressed : " + ds4.PS4ControllerButtonL1Pressed + Environment.NewLine;
                str += "PS4ControllerButtonR1Pressed : " + ds4.PS4ControllerButtonR1Pressed + Environment.NewLine;
                str += "PS4ControllerButtonL2Pressed : " + ds4.PS4ControllerButtonL2Pressed + Environment.NewLine;
                str += "PS4ControllerButtonR2Pressed : " + ds4.PS4ControllerButtonR2Pressed + Environment.NewLine;
                str += "PS4ControllerButtonL3Pressed : " + ds4.PS4ControllerButtonL3Pressed + Environment.NewLine;
                str += "PS4ControllerButtonR3Pressed : " + ds4.PS4ControllerButtonR3Pressed + Environment.NewLine;
                str += "PS4ControllerButtonCreatePressed : " + ds4.PS4ControllerButtonCreatePressed + Environment.NewLine;
                str += "PS4ControllerButtonMenuPressed : " + ds4.PS4ControllerButtonMenuPressed + Environment.NewLine;
                str += "PS4ControllerButtonLogoPressed : " + ds4.PS4ControllerButtonLogoPressed + Environment.NewLine;
                str += "PS4ControllerButtonTouchpadPressed : " + ds4.PS4ControllerButtonTouchpadPressed + Environment.NewLine;
                str += "PS4ControllerButtonMicPressed : " + ds4.PS4ControllerButtonMicPressed + Environment.NewLine;
                str += Environment.NewLine;
                label1.Text = str;
                Thread.Sleep(sleeptime);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                running = false;
                Thread.Sleep(100);
                ds4.Close();
            }
            catch { }
        }
    }
}