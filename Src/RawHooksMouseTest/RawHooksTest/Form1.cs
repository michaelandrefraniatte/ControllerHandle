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
        private void Start()
        {
            for (; ; )
            {
                if (!running)
                    break;
                this.label1.Text = str1;
                string str2 = "2" + Environment.NewLine;
                this.label2.Text = str2;
                Thread.Sleep(100);
            }
        }
        private void OnButtonPressed(object sender, RawInputEventArg e)
        {
            str1 = "lLastX : " + e.ButtonPressEvent.lLastX.ToString() + ", lLastY : " + e.ButtonPressEvent.lLastY.ToString() + ", ulButtons : " + e.ButtonPressEvent.ulButtons.ToString() + ", ulExtraInformation : " + e.ButtonPressEvent.ulExtraInformation.ToString() + ", usButtonData : " + e.ButtonPressEvent.usButtonData.ToString() + ", usButtonFlags : " + e.ButtonPressEvent.usButtonFlags.ToString();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            Thread.Sleep(100);
            _rawinput.ButtonPressed -= OnButtonPressed;
        }
    }
}