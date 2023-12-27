using DualSensesAPI;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public DualSense ds = new DualSense();
        private static string vendor_ds_id = "54C", product_ds_id = "CE6", product_ds_label = "DualSense";
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
            ds.Scan(vendor_ds_id, product_ds_id, product_ds_label);
            Thread.Sleep(2000);
            ds.BeginPolling();
            Task.Run(() => task());
        }
        private void task()
        {
            while (running)
            {
                string str = "PS5ControllerLeftStickX : " + ds.PS5ControllerLeftStickX + Environment.NewLine;
                str += "PS5ControllerLeftStickY : " + ds.PS5ControllerLeftStickY + Environment.NewLine;
                str += "PS5ControllerRightStickX : " + ds.PS5ControllerRightStickX + Environment.NewLine;
                str += "PS5ControllerRightStickY : " + ds.PS5ControllerRightStickY + Environment.NewLine;
                str += "PS5ControllerLeftTriggerPosition : " + ds.PS5ControllerLeftTriggerPosition + Environment.NewLine;
                str += "PS5ControllerRightTriggerPosition : " + ds.PS5ControllerRightTriggerPosition + Environment.NewLine;
                str += "PS5ControllerTouchX : " + ds.PS5ControllerTouchX + Environment.NewLine;
                str += "PS5ControllerTouchY : " + ds.PS5ControllerTouchY + Environment.NewLine;
                str += "PS5ControllerTouchOn : " + ds.PS5ControllerTouchOn + Environment.NewLine;
                str += "PS5ControllerGyroX : " + ds.PS5ControllerGyroX + Environment.NewLine;
                str += "PS5ControllerGyroY : " + ds.PS5ControllerGyroY + Environment.NewLine;
                str += "PS5ControllerAccelX : " + ds.PS5ControllerAccelX + Environment.NewLine;
                str += "PS5ControllerAccelY : " + ds.PS5ControllerAccelY + Environment.NewLine;
                str += "PS5ControllerButtonCrossPressed : " + ds.PS5ControllerButtonCrossPressed + Environment.NewLine;
                str += "PS5ControllerButtonCirclePressed : " + ds.PS5ControllerButtonCirclePressed + Environment.NewLine;
                str += "PS5ControllerButtonSquarePressed : " + ds.PS5ControllerButtonSquarePressed + Environment.NewLine;
                str += "PS5ControllerButtonTrianglePressed : " + ds.PS5ControllerButtonTrianglePressed + Environment.NewLine;
                str += "PS5ControllerButtonDPadUpPressed : " + ds.PS5ControllerButtonDPadUpPressed + Environment.NewLine;
                str += "PS5ControllerButtonDPadRightPressed : " + ds.PS5ControllerButtonDPadRightPressed + Environment.NewLine;
                str += "PS5ControllerButtonDPadDownPressed : " + ds.PS5ControllerButtonDPadDownPressed + Environment.NewLine;
                str += "PS5ControllerButtonDPadLeftPressed : " + ds.PS5ControllerButtonDPadLeftPressed + Environment.NewLine;
                str += "PS5ControllerButtonL1Pressed : " + ds.PS5ControllerButtonL1Pressed + Environment.NewLine;
                str += "PS5ControllerButtonR1Pressed : " + ds.PS5ControllerButtonR1Pressed + Environment.NewLine;
                str += "PS5ControllerButtonL2Pressed : " + ds.PS5ControllerButtonL2Pressed + Environment.NewLine;
                str += "PS5ControllerButtonR2Pressed : " + ds.PS5ControllerButtonR2Pressed + Environment.NewLine;
                str += "PS5ControllerButtonL3Pressed : " + ds.PS5ControllerButtonL3Pressed + Environment.NewLine;
                str += "PS5ControllerButtonR3Pressed : " + ds.PS5ControllerButtonR3Pressed + Environment.NewLine;
                str += "PS5ControllerButtonCreatePressed : " + ds.PS5ControllerButtonCreatePressed + Environment.NewLine;
                str += "PS5ControllerButtonMenuPressed : " + ds.PS5ControllerButtonMenuPressed + Environment.NewLine;
                str += "PS5ControllerButtonLogoPressed : " + ds.PS5ControllerButtonLogoPressed + Environment.NewLine;
                str += "PS5ControllerButtonTouchpadPressed : " + ds.PS5ControllerButtonTouchpadPressed + Environment.NewLine;
                str += "PS5ControllerButtonFnLPressed : " + ds.PS5ControllerButtonFnLPressed + Environment.NewLine;
                str += "PS5ControllerButtonFnRPressed : " + ds.PS5ControllerButtonFnRPressed + Environment.NewLine;
                str += "PS5ControllerButtonBLPPressed : " + ds.PS5ControllerButtonBLPPressed + Environment.NewLine;
                str += "PS5ControllerButtonBRPPressed : " + ds.PS5ControllerButtonBRPPressed + Environment.NewLine;
                str += "PS5ControllerButtonMicPressed : " + ds.PS5ControllerButtonMicPressed + Environment.NewLine;
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
                ds.Close();
            }
            catch { }
        }
    }
}
