using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace InfiniteWorker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static bool closed = false;
        private static Action action;
        private static Task task;
        private void button1_Click(object sender, EventArgs e)
        {
            task = Task.Run(action = () => Start());
        }
        private void Start()
        {
            int inc = 0;
            while (!closed)
            {
                inc++;
                label1.Text = inc.ToString();
                System.Threading.Thread.Sleep(1000);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (task.IsCompleted)
                {
                    closed = false;
                    task = Task.Run(action = () => Start());
                }
            }
            catch { }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            closed = true;
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
        }
    }
}
