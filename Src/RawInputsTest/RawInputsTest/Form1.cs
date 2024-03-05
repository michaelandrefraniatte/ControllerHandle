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