using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualDualshock4_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool closed = false;
        private static int inc = 0;
        private static int ds4number = 2;
        private static bool Controller1DS4_Send_Options, Controller1DS4_Send_Option, Controller1DS4_Send_ThumbLeft, Controller1DS4_Send_ThumbRight, Controller1DS4_Send_ShoulderLeft, Controller1DS4_Send_ShoulderRight, Controller1DS4_Send_Cross, Controller1DS4_Send_Circle, Controller1DS4_Send_Square, Controller1DS4_Send_Triangle, Controller1DS4_Send_Ps, Controller1DS4_Send_Touchpad, Controller1DS4_Send_Share, Controller1DS4_Send_DPadUp, Controller1DS4_Send_DPadDown, Controller1DS4_Send_DPadLeft, Controller1DS4_Send_DPadRight, Controller1DS4_Send_LeftTrigger, Controller1DS4_Send_RightTrigger;
        private static double Controller1DS4_Send_LeftTriggerPosition, Controller1DS4_Send_RightTriggerPosition;
        private static double Controller1DS4_Send_LeftThumbX, Controller1DS4_Send_RightThumbX, Controller1DS4_Send_LeftThumbY, Controller1DS4_Send_RightThumbY;
        private static bool Controller2DS4_Send_Options, Controller2DS4_Send_Option, Controller2DS4_Send_ThumbLeft, Controller2DS4_Send_ThumbRight, Controller2DS4_Send_ShoulderLeft, Controller2DS4_Send_ShoulderRight, Controller2DS4_Send_Cross, Controller2DS4_Send_Circle, Controller2DS4_Send_Square, Controller2DS4_Send_Triangle, Controller2DS4_Send_Ps, Controller2DS4_Send_Touchpad, Controller2DS4_Send_Share, Controller2DS4_Send_DPadUp, Controller2DS4_Send_DPadDown, Controller2DS4_Send_DPadLeft, Controller2DS4_Send_DPadRight, Controller2DS4_Send_LeftTrigger, Controller2DS4_Send_RightTrigger;
        private static double Controller2DS4_Send_LeftTriggerPosition, Controller2DS4_Send_RightTriggerPosition;
        private static double Controller2DS4_Send_LeftThumbX, Controller2DS4_Send_RightThumbX, Controller2DS4_Send_LeftThumbY, Controller2DS4_Send_RightThumbY;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                controllerds4.DS4Controller.Connect(ds4number);
            }
            catch
            {
                if (MessageBox.Show("Cannot find ViGEm bus driver. Please check to install driver. " + "Press OK button to go to driver download page.", "Driver Not Found", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    Process process = new Process();
                    process.StartInfo.UseShellExecute = true;
                    process.StartInfo.FileName = "https://github.com/ViGEm/ViGEmBus/releases";
                    process.Start();
                }
                throw;
            }
            Task.Run(() => Start());
        }
        private void Start()
        {
            while (!closed)
            {
                inc++;
                if (inc <= 200 & inc >= 100)
                {
                    Controller1DS4_Send_ThumbLeft = true;
                    Controller2DS4_Send_ThumbRight = true;
                    Controller1DS4_Send_LeftTriggerPosition = 250;
                    Controller2DS4_Send_RightTriggerPosition = 250;
                    Controller1DS4_Send_LeftThumbX = 30000;
                    Controller1DS4_Send_LeftThumbY = 30000;
                    Controller2DS4_Send_RightThumbX = 30000;
                    Controller2DS4_Send_RightThumbY = 30000;
                }
                else
                {
                    Controller1DS4_Send_ThumbLeft = false;
                    Controller2DS4_Send_ThumbRight = false;
                    Controller1DS4_Send_LeftTriggerPosition = 0;
                    Controller2DS4_Send_RightTriggerPosition = 0;
                    Controller1DS4_Send_LeftThumbX = 0;
                    Controller1DS4_Send_LeftThumbY = 0;
                    Controller2DS4_Send_RightThumbX = 0;
                    Controller2DS4_Send_RightThumbY = 0;
                }
                if (inc > 200)
                    inc = 0;
                controllerds4.DS4Controller.SubmitReport1(Controller1DS4_Send_Options, Controller1DS4_Send_Option, Controller1DS4_Send_ThumbLeft, Controller1DS4_Send_ThumbRight, Controller1DS4_Send_ShoulderLeft, Controller1DS4_Send_ShoulderRight, Controller1DS4_Send_Cross, Controller1DS4_Send_Circle, Controller1DS4_Send_Square, Controller1DS4_Send_Triangle, Controller1DS4_Send_Ps, Controller1DS4_Send_Touchpad, Controller1DS4_Send_Share, Controller1DS4_Send_DPadUp, Controller1DS4_Send_DPadDown, Controller1DS4_Send_DPadLeft, Controller1DS4_Send_DPadRight, Controller1DS4_Send_LeftThumbX, Controller1DS4_Send_RightThumbX, Controller1DS4_Send_LeftThumbY, Controller1DS4_Send_RightThumbY, Controller1DS4_Send_LeftTrigger, Controller1DS4_Send_RightTrigger, Controller1DS4_Send_LeftTriggerPosition, Controller1DS4_Send_RightTriggerPosition);
                if (ds4number > 1)
                {
                    controllerds4.DS4Controller.SubmitReport2(Controller2DS4_Send_Options, Controller2DS4_Send_Option, Controller2DS4_Send_ThumbLeft, Controller2DS4_Send_ThumbRight, Controller2DS4_Send_ShoulderLeft, Controller2DS4_Send_ShoulderRight, Controller2DS4_Send_Cross, Controller2DS4_Send_Circle, Controller2DS4_Send_Square, Controller2DS4_Send_Triangle, Controller2DS4_Send_Ps, Controller2DS4_Send_Touchpad, Controller2DS4_Send_Share, Controller2DS4_Send_DPadUp, Controller2DS4_Send_DPadDown, Controller2DS4_Send_DPadLeft, Controller2DS4_Send_DPadRight, Controller2DS4_Send_LeftThumbX, Controller2DS4_Send_RightThumbX, Controller2DS4_Send_LeftThumbY, Controller2DS4_Send_RightThumbY, Controller2DS4_Send_LeftTrigger, Controller2DS4_Send_RightTrigger, Controller2DS4_Send_LeftTriggerPosition, Controller2DS4_Send_RightTriggerPosition);
                }
                Thread.Sleep(10);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
            Thread.Sleep(100);
            controllerds4.DS4Controller.Disconnect(ds4number);
        }
    }
}