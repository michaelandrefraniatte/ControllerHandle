using System;
using RawInput_dll;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Runtime.Remoting.Messaging;

namespace Mouse
{
    public partial class Form1 : Form
    {
        private readonly RawInput _rawinput;

        const bool CaptureOnlyInForeground = true;
        // Todo: add checkbox to form when checked/uncheck create method to call that does the same as Keyboard ctor 

        public Form1()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);

            _rawinput.AddMessageFilter();   // Adding a message filter will cause keypresses to be handled
            Win32.DeviceAudit();            // Writes a file DeviceAudit.txt to the current directory

            _rawinput.ButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed(object sender, RawInputEventArg e)
        {
            lbHandle.Text = e.ButtonPressEvent.DeviceHandle.ToString();
            lbType.Text = e.ButtonPressEvent.DeviceType;
            lbName.Text = e.ButtonPressEvent.DeviceName;
            lbDescription.Text = e.ButtonPressEvent.Name;
            lbNumKeyboards.Text = _rawinput.NumberOfMouses.ToString(CultureInfo.InvariantCulture);
            lbSource.Text = e.ButtonPressEvent.Source;
            lLastX.Text = "lLastX : " + e.ButtonPressEvent.lLastX.ToString();
            lLastY.Text = "lLastY : " + e.ButtonPressEvent.lLastY.ToString();
            ulButtons.Text = "ulButtons : " + e.ButtonPressEvent.ulButtons.ToString();
            ulExtraInformation.Text = "ulExtraInformation : " + e.ButtonPressEvent.ulExtraInformation.ToString();
            usButtonData.Text = "usButtonData : " + e.ButtonPressEvent.usButtonData.ToString();
            usButtonFlags.Text = "usButtonFlags : " + e.ButtonPressEvent.usButtonFlags.ToString();
            usFlags.Text = "usFlags : " + e.ButtonPressEvent.usFlags.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _rawinput.ButtonPressed -= OnButtonPressed;
        }

        private static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

            if (null == ex) return;

            // Log this error. Logging the exception doesn't correct the problem but at least now
            // you may have more insight as to why the exception is being thrown.
            Debug.WriteLine("Unhandled Exception: " + ex.Message);
            Debug.WriteLine("Unhandled Exception: " + ex);
            MessageBox.Show(ex.Message);
        }
    }
}