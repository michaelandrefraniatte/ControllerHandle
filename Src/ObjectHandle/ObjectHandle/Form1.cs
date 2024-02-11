using Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectHandle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        int i = 0;
        Class1 obj1 = new Class1();
        Class1 obj2 = new Class1();
        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            obj1.AddToList(i);
            obj2.AddToList(i);
            obj1.AddToStaticList(i);
            obj2.AddToStaticList(i);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            obj1.ShowList();
            obj2.ShowList();
            obj1.ShowStaticList();
            obj2.ShowStaticList();
        }
    }
}