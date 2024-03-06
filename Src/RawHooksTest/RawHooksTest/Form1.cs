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
        private void Form1_Load(object sender, EventArgs e)
        {
            Task.Run(() => Start());
        }
        private void Start()
        {
            for (; ; )
            {
                if (!running)
                    break;
                string str1 = "1" + Environment.NewLine;
                this.label1.Text = str1;
                string str2 = "2" + Environment.NewLine;
                this.label2.Text = str2;
                string str3 = "3" + Environment.NewLine;
                this.label3.Text = str3;
                Thread.Sleep(100);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            Thread.Sleep(100);
        }
    }
}