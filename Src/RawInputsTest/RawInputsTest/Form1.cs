using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using MouseRawInputsAPI;
using KeyboardRawInputsAPI;
using System.Threading;

namespace RawInputsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool running;
        private static int sleeptime = 1;
        private static MouseRawInputs mri = new MouseRawInputs();
        private static KeyboardRawInputs kri = new KeyboardRawInputs();
        private void Form1_Load(object sender, EventArgs e)
        {
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
            mri.Scan();
            kri.Scan();
            mri.BeginPolling();
            kri.BeginPolling();
            Task.Run(() => task());
        }
        private void task()
        {
            for (; ; )
            {
                if (!running)
                    break;
                mri.ViewData();
                kri.ViewData();
                Thread.Sleep(sleeptime);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            Thread.Sleep(100);
            mri.Close();
            kri.Close();
        }
    }
}