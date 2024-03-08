using RawInput_dll;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RawHooksTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool running = true;
        private RawInput _rawinput;
        const bool CaptureOnlyInForeground = false;
        private string str1;
        private void Form1_Load(object sender, EventArgs e)
        {
            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);
            _rawinput.ButtonPressed += OnButtonPressed;
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
            for (; ; )
            {
                if (!running)
                    break;
                this.label1.Text = str1;
                string str = "MouseAxisX : " + MouseAxisX + Environment.NewLine;
                str += "MouseAxisY : " + MouseAxisY + Environment.NewLine;
                str += "MouseAxisZ : " + MouseAxisZ + Environment.NewLine;
                str += "MouseButtons0 : " + MouseButtons0 + Environment.NewLine;
                str += "MouseButtons1 : " + MouseButtons1 + Environment.NewLine;
                str += "MouseButtons2 : " + MouseButtons2 + Environment.NewLine;
                str += "MouseButtons3 : " + MouseButtons3 + Environment.NewLine;
                str += "MouseButtons4 : " + MouseButtons4 + Environment.NewLine;
                str += Environment.NewLine;
                this.label2.Text = str;
                Thread.Sleep(100);
            }
        }
        private void OnButtonPressed(object sender, RawInputEventArg e)
        {
            str1 = "lLastX : " + e.ButtonPressEvent.lLastX.ToString() + ", lLastY : " + e.ButtonPressEvent.lLastY.ToString() + ", ulButtons : " + e.ButtonPressEvent.ulButtons.ToString() + ", ulExtraInformation : " + e.ButtonPressEvent.ulExtraInformation.ToString() + ", usButtonData : " + e.ButtonPressEvent.usButtonData.ToString() + ", usButtonFlags : " + e.ButtonPressEvent.usButtonFlags.ToString();
            MouseAxisX = e.ButtonPressEvent.lLastX;
            MouseAxisY = e.ButtonPressEvent.lLastY;
            MouseAxisZ = e.ButtonPressEvent.usButtonData;
            if (e.ButtonPressEvent.ulButtons == 1)
                MouseButtons0 = true;
            if (e.ButtonPressEvent.ulButtons == 2)
                MouseButtons0 = false;
            if (e.ButtonPressEvent.ulButtons == 4)
                MouseButtons1 = true;
            if (e.ButtonPressEvent.ulButtons == 8)
                MouseButtons1 = false;
            if (e.ButtonPressEvent.ulButtons == 16)
                MouseButtons2 = true;
            if (e.ButtonPressEvent.ulButtons == 32)
                MouseButtons2 = false;
            if (e.ButtonPressEvent.ulButtons == 256)
                MouseButtons3 = true;
            if (e.ButtonPressEvent.ulButtons == 512)
                MouseButtons3 = false;
            if (e.ButtonPressEvent.ulButtons == 64)
                MouseButtons4 = true;
            if (e.ButtonPressEvent.ulButtons == 128)
                MouseButtons4 = false;
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            Thread.Sleep(100);
            _rawinput.ButtonPressed -= OnButtonPressed;
        }
        public bool MouseButtons0;
        public bool MouseButtons1;
        public bool MouseButtons2;
        public bool MouseButtons3;
        public bool MouseButtons4;
        public int MouseAxisX;
        public int MouseAxisY;
        public int MouseAxisZ;
    }
}