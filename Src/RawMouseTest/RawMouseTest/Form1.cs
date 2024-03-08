using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Multimedia;
using SharpDX.RawInput;

namespace RawMouseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool closed = false;
        public static bool MouseButtons0;
        public static bool MouseButtons1;
        public static bool MouseButtons2;
        public static bool MouseButtons3;
        public static bool MouseButtons4;
        public static int MouseAxisX;
        public static int MouseAxisY;
        public static int MouseAxisZ;
        private void Form1_Load(object sender, EventArgs e)
        {
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
            Device.MouseInput += Device_MouseInput;
            Task.Run(() => taskEmulate());
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
        private void Device_MouseInput(object sender, MouseInputEventArgs e)
        {
            MouseAxisX = e.X;
            MouseAxisY = e.Y;
            MouseAxisZ = e.WheelDelta;
            if (e.ButtonFlags == MouseButtonFlags.Button1Down)
                MouseButtons0 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button1Up)
                MouseButtons0 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button2Down)
                MouseButtons1 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button2Up)
                MouseButtons1 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button3Down)
                MouseButtons2 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button3Up)
                MouseButtons2 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button4Down)
                MouseButtons3 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button4Up)
                MouseButtons3 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button5Down)
                MouseButtons4 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button5Up)
                MouseButtons4 = false;
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                string str = "MouseAxisX : " + MouseAxisX + Environment.NewLine;
                str += "MouseAxisY : " + MouseAxisY + Environment.NewLine;
                str += "MouseAxisZ : " + MouseAxisZ + Environment.NewLine;
                str += "MouseButtons0 : " + MouseButtons0 + Environment.NewLine;
                str += "MouseButtons1 : " + MouseButtons1 + Environment.NewLine;
                str += "MouseButtons2 : " + MouseButtons2 + Environment.NewLine;
                str += "MouseButtons3 : " + MouseButtons3 + Environment.NewLine;
                str += "MouseButtons4 : " + MouseButtons4 + Environment.NewLine;
                str += Environment.NewLine;
                this.label1.Text = str;
                System.Threading.Thread.Sleep(100);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
        }
    }
}