using RawInput_dll;
using System.Globalization;
using System.Windows.Forms;

namespace Keyboard
{
    public partial class Keyboard : Form
    {
        private readonly RawInput _rawinput;
        const bool CaptureOnlyInForeground = false;
        public Keyboard()
        {
            InitializeComponent();
            _rawinput = new RawInput(Handle, CaptureOnlyInForeground);
            _rawinput.AddMessageFilter();
            _rawinput.KeyPressed += OnKeyPressed;   
        }
        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            lbHandle.Text = e.KeyPressEvent.DeviceHandle.ToString();
            lbType.Text = e.KeyPressEvent.DeviceType;
            lbName.Text = e.KeyPressEvent.DeviceName;
            lbDescription.Text = e.KeyPressEvent.Name;
            lbKey.Text = e.KeyPressEvent.VKey.ToString(CultureInfo.InvariantCulture);
            lbNumKeyboards.Text = _rawinput.NumberOfKeyboards.ToString(CultureInfo.InvariantCulture);
            lbVKey.Text = e.KeyPressEvent.VKeyName;
            lbSource.Text = e.KeyPressEvent.Source;
            lbKeyPressState.Text = e.KeyPressEvent.KeyPressState;
            lbMessage.Text = string.Format("0x{0:X4} ({0})", e.KeyPressEvent.Message);
        }
        private void Keyboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            _rawinput.KeyPressed -= OnKeyPressed;
        }
    }
}