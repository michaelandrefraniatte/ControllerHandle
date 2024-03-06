using RawInput_dll;
using System.Globalization;
using System.Windows.Forms;

namespace Mouse
{
    public partial class Form1 : Form
    {
        private readonly RawInput _rawinput;
        const bool CaptureOnlyInForeground = false;
        public Form1()
        {
            InitializeComponent();
            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);
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
    }
}