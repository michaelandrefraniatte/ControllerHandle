using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vjoy_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool closed = false;
        private static int inc = 0;
        private static int vjoynumber = 2;
        private static bool Controller1VJoy_Send_1, Controller1VJoy_Send_2, Controller1VJoy_Send_3, Controller1VJoy_Send_4, Controller1VJoy_Send_5, Controller1VJoy_Send_6, Controller1VJoy_Send_7, Controller1VJoy_Send_8;
        private static double Controller1VJoy_Send_X, Controller1VJoy_Send_Y, Controller1VJoy_Send_Z, Controller1VJoy_Send_WHL, Controller1VJoy_Send_SL0, Controller1VJoy_Send_SL1, Controller1VJoy_Send_RX, Controller1VJoy_Send_RY, Controller1VJoy_Send_RZ, Controller1VJoy_Send_POV, Controller1VJoy_Send_Hat, Controller1VJoy_Send_HatExt1, Controller1VJoy_Send_HatExt2, Controller1VJoy_Send_HatExt3;
        private static bool Controller2VJoy_Send_1, Controller2VJoy_Send_2, Controller2VJoy_Send_3, Controller2VJoy_Send_4, Controller2VJoy_Send_5, Controller2VJoy_Send_6, Controller2VJoy_Send_7, Controller2VJoy_Send_8;
        private static double Controller2VJoy_Send_X, Controller2VJoy_Send_Y, Controller2VJoy_Send_Z, Controller2VJoy_Send_WHL, Controller2VJoy_Send_SL0, Controller2VJoy_Send_SL1, Controller2VJoy_Send_RX, Controller2VJoy_Send_RY, Controller2VJoy_Send_RZ, Controller2VJoy_Send_POV, Controller2VJoy_Send_Hat, Controller2VJoy_Send_HatExt1, Controller2VJoy_Send_HatExt2, Controller2VJoy_Send_HatExt3;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                controllersvjoy.VJoyController.Connect(vjoynumber);
            }
            catch { }
            Task.Run(() => Start());
        }
        private void Start()
        {
            while (!closed)
            {
                inc++;
                if (inc <= 200 & inc >= 100)
                {
                    Controller1VJoy_Send_1 = true;
                    Controller2VJoy_Send_2 = true;
                    Controller1VJoy_Send_X = 16000;
                    Controller2VJoy_Send_Y = 16000;
                }
                else
                {
                    Controller1VJoy_Send_1 = false;
                    Controller2VJoy_Send_2 = false;
                    Controller1VJoy_Send_X = 0;
                    Controller2VJoy_Send_Y = 0;
                }
                if (inc > 200)
                    inc = 0;
                controllersvjoy.VJoyController.SubmitReport1(Controller1VJoy_Send_1, Controller1VJoy_Send_2, Controller1VJoy_Send_3, Controller1VJoy_Send_4, Controller1VJoy_Send_5, Controller1VJoy_Send_6, Controller1VJoy_Send_7, Controller1VJoy_Send_8, Controller1VJoy_Send_X, Controller1VJoy_Send_Y, Controller1VJoy_Send_Z, Controller1VJoy_Send_WHL, Controller1VJoy_Send_SL0, Controller1VJoy_Send_SL1, Controller1VJoy_Send_RX, Controller1VJoy_Send_RY, Controller1VJoy_Send_RZ, Controller1VJoy_Send_POV, Controller1VJoy_Send_Hat, Controller1VJoy_Send_HatExt1, Controller1VJoy_Send_HatExt2, Controller1VJoy_Send_HatExt3);
                if (vjoynumber > 1)
                {
                    controllersvjoy.VJoyController.SubmitReport2(Controller2VJoy_Send_1, Controller2VJoy_Send_2, Controller2VJoy_Send_3, Controller2VJoy_Send_4, Controller2VJoy_Send_5, Controller2VJoy_Send_6, Controller2VJoy_Send_7, Controller2VJoy_Send_8, Controller2VJoy_Send_X, Controller2VJoy_Send_Y, Controller2VJoy_Send_Z, Controller2VJoy_Send_WHL, Controller2VJoy_Send_SL0, Controller2VJoy_Send_SL1, Controller2VJoy_Send_RX, Controller2VJoy_Send_RY, Controller2VJoy_Send_RZ, Controller2VJoy_Send_POV, Controller2VJoy_Send_Hat, Controller2VJoy_Send_HatExt1, Controller2VJoy_Send_HatExt2, Controller2VJoy_Send_HatExt3);
                }
                Thread.Sleep(10);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
            Thread.Sleep(100);
            controllersvjoy.VJoyController.Disconnect(vjoynumber);
        }
    }
}