using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using JoyconChargingGripsAPI;

namespace JoyconsChargingGripLib
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
        private JoyconChargingGrip jcg = new JoyconChargingGrip();
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            Task.Run(() => Start());
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e.KeyData);
        }
        private void OnKeyDown(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (keyData == Keys.Escape)
            {
                this.Close();
            }
        }
        private void Start()
        {
            running = true;
            jcg.Scan();
            jcg.BeginPolling();
            Thread.Sleep(1000);
            jcg.Init();
            Task.Run(() => task());
        }
        private void task()
        {
            for (; ; )
            {
                if (!running)
                    break;
                if (jcg.JoyconRightButtonPLUS)
                    jcg.Init();
                try
                {
                    string str = "JoyconLeftStickX : " + jcg.JoyconLeftStickX + Environment.NewLine;
                    str += "JoyconLeftStickY : " + jcg.JoyconLeftStickY + Environment.NewLine;
                    str += "JoyconLeftButtonSHOULDER_1 : " + jcg.JoyconLeftButtonSHOULDER_1 + Environment.NewLine;
                    str += "JoyconLeftButtonSHOULDER_2 : " + jcg.JoyconLeftButtonSHOULDER_2 + Environment.NewLine;
                    str += "JoyconLeftButtonSR : " + jcg.JoyconLeftButtonSR + Environment.NewLine;
                    str += "JoyconLeftButtonSL : " + jcg.JoyconLeftButtonSL + Environment.NewLine;
                    str += "JoyconLeftButtonDPAD_DOWN : " + jcg.JoyconLeftButtonDPAD_DOWN + Environment.NewLine;
                    str += "JoyconLeftButtonDPAD_RIGHT : " + jcg.JoyconLeftButtonDPAD_RIGHT + Environment.NewLine;
                    str += "JoyconLeftButtonDPAD_UP : " + jcg.JoyconLeftButtonDPAD_UP + Environment.NewLine;
                    str += "JoyconLeftButtonDPAD_LEFT : " + jcg.JoyconLeftButtonDPAD_LEFT + Environment.NewLine;
                    str += "JoyconLeftButtonMINUS : " + jcg.JoyconLeftButtonMINUS + Environment.NewLine;
                    str += "JoyconLeftButtonCAPTURE : " + jcg.JoyconLeftButtonCAPTURE + Environment.NewLine;
                    str += "JoyconLeftButtonSTICK : " + jcg.JoyconLeftButtonSTICK + Environment.NewLine;
                    str += "JoyconLeftButtonACC : " + jcg.JoyconLeftButtonACC + Environment.NewLine;
                    str += "JoyconLeftButtonSMA : " + jcg.JoyconLeftButtonSMA + Environment.NewLine;
                    str += "JoyconLeftRollLeft : " + jcg.JoyconLeftRollLeft + Environment.NewLine;
                    str += "JoyconLeftRollRight : " + jcg.JoyconLeftRollRight + Environment.NewLine;
                    str += "JoyconLeftAccelX : " + jcg.JoyconLeftAccelX + Environment.NewLine;
                    str += "JoyconLeftAccelY : " + jcg.JoyconLeftAccelY + Environment.NewLine;
                    str += "JoyconLeftGyroX : " + jcg.JoyconLeftGyroX + Environment.NewLine;
                    str += "JoyconLeftGyroY : " + jcg.JoyconLeftGyroY + Environment.NewLine;
                    str += Environment.NewLine;
                    this.label1.Text = str;
                }
                catch { }
                try
                {
                    string str = "JoyconRightStickX : " + jcg.JoyconRightStickX + Environment.NewLine;
                    str += "JoyconRightStickY : " + jcg.JoyconRightStickY + Environment.NewLine;
                    str += "JoyconRightButtonSHOULDER_1 : " + jcg.JoyconRightButtonSHOULDER_1 + Environment.NewLine;
                    str += "JoyconRightButtonSHOULDER_2 : " + jcg.JoyconRightButtonSHOULDER_2 + Environment.NewLine;
                    str += "JoyconRightButtonSR : " + jcg.JoyconRightButtonSR + Environment.NewLine;
                    str += "JoyconRightButtonSL : " + jcg.JoyconRightButtonSL + Environment.NewLine;
                    str += "JoyconRightButtonDPAD_DOWN : " + jcg.JoyconRightButtonDPAD_DOWN + Environment.NewLine;
                    str += "JoyconRightButtonDPAD_RIGHT : " + jcg.JoyconRightButtonDPAD_RIGHT + Environment.NewLine;
                    str += "JoyconRightButtonDPAD_UP : " + jcg.JoyconRightButtonDPAD_UP + Environment.NewLine;
                    str += "JoyconRightButtonDPAD_LEFT : " + jcg.JoyconRightButtonDPAD_LEFT + Environment.NewLine;
                    str += "JoyconRightButtonPLUS : " + jcg.JoyconRightButtonPLUS + Environment.NewLine;
                    str += "JoyconRightButtonHOME : " + jcg.JoyconRightButtonHOME + Environment.NewLine;
                    str += "JoyconRightButtonSTICK : " + jcg.JoyconRightButtonSTICK + Environment.NewLine;
                    str += "JoyconRightButtonACC : " + jcg.JoyconRightButtonACC + Environment.NewLine;
                    str += "JoyconRightButtonSPA : " + jcg.JoyconRightButtonSPA + Environment.NewLine;
                    str += "JoyconRightRollLeft : " + jcg.JoyconRightRollLeft + Environment.NewLine;
                    str += "JoyconRightRollRight : " + jcg.JoyconRightRollRight + Environment.NewLine;
                    str += "JoyconRightAccelX : " + jcg.JoyconRightAccelX + Environment.NewLine;
                    str += "JoyconRightAccelY : " + jcg.JoyconRightAccelY + Environment.NewLine;
                    str += "JoyconRightGyroX : " + jcg.JoyconRightGyroX + Environment.NewLine;
                    str += "JoyconRightGyroY : " + jcg.JoyconRightGyroY + Environment.NewLine;
                    str += Environment.NewLine;
                    this.label2.Text = str;
                } 
                catch { }
                /*jcg.ViewData();*/
                Thread.Sleep(100);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            Thread.Sleep(100);
            jcg.Close();
        }
    }
}