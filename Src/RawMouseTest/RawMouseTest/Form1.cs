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
        public static bool MouseButtons5;
        public static bool MouseButtons6;
        public static bool MouseButtons7;
        public static int MouseAxisX;
        public static int MouseAxisY;
        public static int MouseAxisZ;
        private void Form1_Load(object sender, EventArgs e)
        {
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
            Device.MouseInput += Device_MouseInput;
            Task.Run(() => taskEmulate());
        }
        private void Device_MouseInput(object sender, MouseInputEventArgs e)
        {
            MouseAxisX = e.X;
            MouseAxisY = e.Y;
            MouseAxisZ = e.WheelDelta;
            if (e.ButtonFlags == MouseButtonFlags.LeftButtonDown)
                MouseButtons0 = true;
            if (e.ButtonFlags == MouseButtonFlags.LeftButtonUp)
                MouseButtons0 = false;
            if (e.ButtonFlags == MouseButtonFlags.RightButtonDown)
                MouseButtons1 = true;
            if (e.ButtonFlags == MouseButtonFlags.RightButtonUp)
                MouseButtons1 = false;
            if (e.ButtonFlags == MouseButtonFlags.MiddleButtonDown)
                MouseButtons2 = true;
            if (e.ButtonFlags == MouseButtonFlags.MiddleButtonUp)
                MouseButtons2 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button1Down)
                MouseButtons3 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button1Up)
                MouseButtons3 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button2Down)
                MouseButtons4 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button2Up)
                MouseButtons4 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button3Down)
                MouseButtons5 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button3Up)
                MouseButtons5 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button4Down)
                MouseButtons6 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button4Up)
                MouseButtons6 = false;
            if (e.ButtonFlags == MouseButtonFlags.Button5Down)
                MouseButtons7 = true;
            if (e.ButtonFlags == MouseButtonFlags.Button5Up)
                MouseButtons7 = false;
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
                str += "MouseButtons5 : " + MouseButtons5 + Environment.NewLine;
                str += "MouseButtons6 : " + MouseButtons6 + Environment.NewLine;
                str += "MouseButtons7 : " + MouseButtons7 + Environment.NewLine;
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