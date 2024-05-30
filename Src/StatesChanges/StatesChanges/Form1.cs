using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StatesChanges
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(Keys vKey);
        private static int[] wd = { 2 };
        private static int[] wu = { 2 };
        public static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Run(() => Start());
        }
        private void Start()
        {
            while (true)
            {
                valchanged(0, GetAsyncKeyState(Keys.Space));
                if (wd[0] == 1)
                {
                    textBox1.Text = "wd";
                }
                else
                {
                    textBox1.Text = "";
                }
                if (wu[0] == 1)
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
    }
}