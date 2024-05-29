using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ValueStateChanged
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Keys vKey);
        public valuechanged ValueChanged = new valuechanged();
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Run(() => Start());
        }
        private void Start()
        {
            while (true)
            {
                ValueChanged[0] = GetAsyncKeyState(Keys.Space);
                if (valuechanged._ValueChanged[0] & valuechanged._valuechanged[0])
                {
                    textBox1.Text = "wd";
                }
                else
                {
                    textBox1.Text = "";
                }
                if (valuechanged._ValueChanged[0] & !valuechanged._valuechanged[0])
                {
                    textBox2.Text = "wu";
                }
                else
                {
                    textBox2.Text = "";
                }
                System.Threading.Thread.Sleep(70);
            }
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
        public class valuechanged
        {
            public static bool[] _valuechanged = { false };
            public static bool[] _ValueChanged = { false };
            public bool this[int index]
            {
                get { return _ValueChanged[index]; }
                set
                {
                    if (_valuechanged[index] != value)
                        _ValueChanged[index] = true;
                    else
                        _ValueChanged[index] = false;
                    _valuechanged[index] = value;
                }
            }
        }
    }
}