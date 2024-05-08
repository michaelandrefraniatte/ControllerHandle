using controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace xbc_test
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
        private static bool controller1_send_back, controller1_send_start, controller1_send_A, controller1_send_B, controller1_send_X, controller1_send_Y, controller1_send_up, controller1_send_left, controller1_send_down, controller1_send_right, controller1_send_leftstick, controller1_send_rightstick, controller1_send_leftbumper, controller1_send_rightbumper, controller1_send_xbox;
        private static double controller1_send_leftstickx, controller1_send_leftsticky, controller1_send_rightstickx, controller1_send_rightsticky, controller1_send_lefttriggerposition, controller1_send_righttriggerposition;
        private static bool controller2_send_back, controller2_send_start, controller2_send_A, controller2_send_B, controller2_send_X, controller2_send_Y, controller2_send_up, controller2_send_left, controller2_send_down, controller2_send_right, controller2_send_leftstick, controller2_send_rightstick, controller2_send_leftbumper, controller2_send_rightbumper, controller2_send_xbox;
        private static double controller2_send_leftstickx, controller2_send_leftsticky, controller2_send_rightstickx, controller2_send_rightsticky, controller2_send_lefttriggerposition, controller2_send_righttriggerposition;
        public bool running;
        private static int inc = 0;
        private XBoxController scp1 = new XBoxController();
        private XBoxController scp2 = new XBoxController();
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            SetProcessPriority();
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
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                running = false;
                Thread.Sleep(100);
                scp1.Disconnect();
                scp2.Disconnect();
            }
            catch { }
        }
        private void SetProcessPriority()
        {
            using (Process p = Process.GetCurrentProcess())
            {
                p.PriorityClass = ProcessPriorityClass.RealTime;
            }
        }
        private void Start()
        {
            running = true;
            scp1.Connect(1);
            scp2.Connect(2);
            Task.Run(() => taskX());
        }
        private void taskX()
        {
            for (; ; )
            {
                if (!running)
                    break;
                inc++;
                if (inc <= 200 & inc >= 100)
                {
                    controller1_send_A = true;
                    controller1_send_rightstickx = 16000;
                    controller2_send_B = true;
                    controller2_send_rightsticky = 16000;
                }
                else
                {
                    controller1_send_A = false;
                    controller1_send_rightstickx = 0;
                    controller2_send_B = false;
                    controller2_send_rightsticky = 0;
                }
                if (inc > 200)
                    inc = 0;
                scp1.Set(controller1_send_back, controller1_send_start, controller1_send_A, controller1_send_B, controller1_send_X, controller1_send_Y, controller1_send_up, controller1_send_left, controller1_send_down, controller1_send_right, controller1_send_leftstick, controller1_send_rightstick, controller1_send_leftbumper, controller1_send_rightbumper, controller1_send_leftstickx, controller1_send_leftsticky, controller1_send_rightstickx, controller1_send_rightsticky, controller1_send_lefttriggerposition, controller1_send_righttriggerposition, controller1_send_xbox);
                scp2.Set(controller2_send_back, controller2_send_start, controller2_send_A, controller2_send_B, controller2_send_X, controller2_send_Y, controller2_send_up, controller2_send_left, controller2_send_down, controller2_send_right, controller2_send_leftstick, controller2_send_rightstick, controller2_send_leftbumper, controller2_send_rightbumper, controller2_send_leftstickx, controller2_send_leftsticky, controller2_send_rightstickx, controller2_send_rightsticky, controller2_send_lefttriggerposition, controller2_send_righttriggerposition, controller2_send_xbox);
                Thread.Sleep(10);
            }
        }
    }
}