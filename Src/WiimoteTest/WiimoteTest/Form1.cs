using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Wiimotes;

namespace WiimoteTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        private static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        private static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        private static uint CurrentResolution = 0;
        public Wiimote wm = new Wiimote();
        private static string vendor_id = "0002057E", product_id = "0306", product_label = "Wiimote";
        private static bool running;
        private int sleeptime = 100;
        private void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            Task.Run(() => Start());
        }
        private void Start()
        {
            running = true;
            wm.Scan(vendor_id, product_id, product_label);
            wm.BeginPolling();
            Task.Run(() => task());
        }
        private void task()
        {
            while (running)
            {
                string str = "irx : " + wm.irx + Environment.NewLine;
                str += "iry : " + wm.iry + Environment.NewLine;
                str += "WiimoteButtonStateA : " + wm.WiimoteButtonStateA + Environment.NewLine;
                str += "WiimoteButtonStateB : " + wm.WiimoteButtonStateB + Environment.NewLine;
                str += "WiimoteButtonStateMinus : " + wm.WiimoteButtonStateMinus + Environment.NewLine;
                str += "WiimoteButtonStateHome : " + wm.WiimoteButtonStateHome + Environment.NewLine;
                str += "WiimoteButtonStatePlus : " + wm.WiimoteButtonStatePlus + Environment.NewLine;
                str += "WiimoteButtonStateOne : " + wm.WiimoteButtonStateOne + Environment.NewLine;
                str += "WiimoteButtonStateTwo : " + wm.WiimoteButtonStateTwo + Environment.NewLine;
                str += "WiimoteButtonStateUp : " + wm.WiimoteButtonStateUp + Environment.NewLine;
                str += "WiimoteButtonStateDown : " + wm.WiimoteButtonStateDown + Environment.NewLine;
                str += "WiimoteButtonStateLeft : " + wm.WiimoteButtonStateLeft + Environment.NewLine;
                str += "WiimoteButtonStateRight : " + wm.WiimoteButtonStateRight + Environment.NewLine;
                str += "WiimoteRawValuesX : " + wm.WiimoteRawValuesX + Environment.NewLine;
                str += "WiimoteRawValuesY : " + wm.WiimoteRawValuesY + Environment.NewLine;
                str += "WiimoteRawValuesZ : " + wm.WiimoteRawValuesZ + Environment.NewLine;
                str += "WiimoteNunchuckStateRawJoystickX : " + wm.WiimoteNunchuckStateRawJoystickX + Environment.NewLine;
                str += "WiimoteNunchuckStateRawJoystickY : " + wm.WiimoteNunchuckStateRawJoystickY + Environment.NewLine;
                str += "WiimoteNunchuckStateRawValuesX : " + wm.WiimoteNunchuckStateRawValuesX + Environment.NewLine;
                str += "WiimoteNunchuckStateRawValuesY : " + wm.WiimoteNunchuckStateRawValuesY + Environment.NewLine;
                str += "WiimoteNunchuckStateRawValuesZ : " + wm.WiimoteNunchuckStateRawValuesZ + Environment.NewLine;
                str += "WiimoteNunchuckStateC : " + wm.WiimoteNunchuckStateC + Environment.NewLine;
                str += "WiimoteNunchuckStateZ : " + wm.WiimoteNunchuckStateZ + Environment.NewLine;
                str += Environment.NewLine;
                label1.Text = str;
                Thread.Sleep(sleeptime);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                running = false;
                Thread.Sleep(100);
                wm.Close();
            }
            catch { }
        }
    }
}