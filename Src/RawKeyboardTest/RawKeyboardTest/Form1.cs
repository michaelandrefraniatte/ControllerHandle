using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.Multimedia;
using SharpDX.RawInput;

namespace RawKeyboardTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool closed = false;
        public static bool KeyboardKeyEscape;
        public static bool KeyboardKeyD1;
        public static bool KeyboardKeyD2;
        public static bool KeyboardKeyD3;
        public static bool KeyboardKeyD4;
        public static bool KeyboardKeyD5;
        public static bool KeyboardKeyD6;
        public static bool KeyboardKeyD7;
        public static bool KeyboardKeyD8;
        public static bool KeyboardKeyD9;
        public static bool KeyboardKeyD0;
        public static bool KeyboardKeyMinus;
        public static bool KeyboardKeyEquals;
        public static bool KeyboardKeyBack;
        public static bool KeyboardKeyTab;
        public static bool KeyboardKeyQ;
        public static bool KeyboardKeyW;
        public static bool KeyboardKeyE;
        public static bool KeyboardKeyR;
        public static bool KeyboardKeyT;
        public static bool KeyboardKeyY;
        public static bool KeyboardKeyU;
        public static bool KeyboardKeyI;
        public static bool KeyboardKeyO;
        public static bool KeyboardKeyP;
        public static bool KeyboardKeyLeftBracket;
        public static bool KeyboardKeyRightBracket;
        public static bool KeyboardKeyReturn;
        public static bool KeyboardKeyLeftControl;
        public static bool KeyboardKeyA;
        public static bool KeyboardKeyS;
        public static bool KeyboardKeyD;
        public static bool KeyboardKeyF;
        public static bool KeyboardKeyG;
        public static bool KeyboardKeyH;
        public static bool KeyboardKeyJ;
        public static bool KeyboardKeyK;
        public static bool KeyboardKeyL;
        public static bool KeyboardKeySemicolon;
        public static bool KeyboardKeyApostrophe;
        public static bool KeyboardKeyGrave;
        public static bool KeyboardKeyLeftShift;
        public static bool KeyboardKeyBackslash;
        public static bool KeyboardKeyZ;
        public static bool KeyboardKeyX;
        public static bool KeyboardKeyC;
        public static bool KeyboardKeyV;
        public static bool KeyboardKeyB;
        public static bool KeyboardKeyN;
        public static bool KeyboardKeyM;
        public static bool KeyboardKeyComma;
        public static bool KeyboardKeyPeriod;
        public static bool KeyboardKeySlash;
        public static bool KeyboardKeyRightShift;
        public static bool KeyboardKeyMultiply;
        public static bool KeyboardKeyLeftAlt;
        public static bool KeyboardKeySpace;
        public static bool KeyboardKeyCapital;
        public static bool KeyboardKeyF1;
        public static bool KeyboardKeyF2;
        public static bool KeyboardKeyF3;
        public static bool KeyboardKeyF4;
        public static bool KeyboardKeyF5;
        public static bool KeyboardKeyF6;
        public static bool KeyboardKeyF7;
        public static bool KeyboardKeyF8;
        public static bool KeyboardKeyF9;
        public static bool KeyboardKeyF10;
        public static bool KeyboardKeyNumberLock;
        public static bool KeyboardKeyScrollLock;
        public static bool KeyboardKeyNumPad7;
        public static bool KeyboardKeyNumPad8;
        public static bool KeyboardKeyNumPad9;
        public static bool KeyboardKeySubtract;
        public static bool KeyboardKeyNumPad4;
        public static bool KeyboardKeyNumPad5;
        public static bool KeyboardKeyNumPad6;
        public static bool KeyboardKeyAdd;
        public static bool KeyboardKeyNumPad1;
        public static bool KeyboardKeyNumPad2;
        public static bool KeyboardKeyNumPad3;
        public static bool KeyboardKeyNumPad0;
        public static bool KeyboardKeyDecimal;
        public static bool KeyboardKeyOem102;
        public static bool KeyboardKeyF11;
        public static bool KeyboardKeyF12;
        public static bool KeyboardKeyF13;
        public static bool KeyboardKeyF14;
        public static bool KeyboardKeyF15;
        public static bool KeyboardKeyKana;
        public static bool KeyboardKeyAbntC1;
        public static bool KeyboardKeyConvert;
        public static bool KeyboardKeyNoConvert;
        public static bool KeyboardKeyYen;
        public static bool KeyboardKeyAbntC2;
        public static bool KeyboardKeyNumPadEquals;
        public static bool KeyboardKeyPreviousTrack;
        public static bool KeyboardKeyAT;
        public static bool KeyboardKeyColon;
        public static bool KeyboardKeyUnderline;
        public static bool KeyboardKeyKanji;
        public static bool KeyboardKeyStop;
        public static bool KeyboardKeyAX;
        public static bool KeyboardKeyUnlabeled;
        public static bool KeyboardKeyNextTrack;
        public static bool KeyboardKeyNumPadEnter;
        public static bool KeyboardKeyRightControl;
        public static bool KeyboardKeyMute;
        public static bool KeyboardKeyCalculator;
        public static bool KeyboardKeyPlayPause;
        public static bool KeyboardKeyMediaStop;
        public static bool KeyboardKeyVolumeDown;
        public static bool KeyboardKeyVolumeUp;
        public static bool KeyboardKeyWebHome;
        public static bool KeyboardKeyNumPadComma;
        public static bool KeyboardKeyDivide;
        public static bool KeyboardKeyPrintScreen;
        public static bool KeyboardKeyRightAlt;
        public static bool KeyboardKeyPause;
        public static bool KeyboardKeyHome;
        public static bool KeyboardKeyUp;
        public static bool KeyboardKeyPageUp;
        public static bool KeyboardKeyLeft;
        public static bool KeyboardKeyRight;
        public static bool KeyboardKeyEnd;
        public static bool KeyboardKeyDown;
        public static bool KeyboardKeyPageDown;
        public static bool KeyboardKeyInsert;
        public static bool KeyboardKeyDelete;
        public static bool KeyboardKeyLeftWindowsKey;
        public static bool KeyboardKeyRightWindowsKey;
        public static bool KeyboardKeyApplications;
        public static bool KeyboardKeyPower;
        public static bool KeyboardKeySleep;
        public static bool KeyboardKeyWake;
        public static bool KeyboardKeyWebSearch;
        public static bool KeyboardKeyWebFavorites;
        public static bool KeyboardKeyWebRefresh;
        public static bool KeyboardKeyWebStop;
        public static bool KeyboardKeyWebForward;
        public static bool KeyboardKeyWebBack;
        public static bool KeyboardKeyMyComputer;
        public static bool KeyboardKeyMail;
        public static bool KeyboardKeyMediaSelect;
        public static bool KeyboardKeyUnknown;
        private void Form1_Load(object sender, EventArgs e)
        {
            Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            Device.KeyboardInput += Device_KeyboardInput;
            Task.Run(() => taskEmulate());
        }
        private void Device_KeyboardInput(object sender, KeyboardInputEventArgs e)
        {
            if (e.State == KeyState.KeyDown & e.Key == Keys.Escape)
                KeyboardKeyEscape = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Escape)
                KeyboardKeyEscape = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D1)
                KeyboardKeyD1 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D1)
                KeyboardKeyD1 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D2)
                KeyboardKeyD2 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D2)
                KeyboardKeyD2 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D3)
                KeyboardKeyD3 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D3)
                KeyboardKeyD3 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D4)
                KeyboardKeyD4 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D4)
                KeyboardKeyD4 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D5)
                KeyboardKeyD5 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D5)
                KeyboardKeyD5 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D6)
                KeyboardKeyD6 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D6)
                KeyboardKeyD6 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D7)
                KeyboardKeyD7 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D7)
                KeyboardKeyD7 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D8)
                KeyboardKeyD8 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D8)
                KeyboardKeyD8 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D9)
                KeyboardKeyD9 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D9)
                KeyboardKeyD9 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D0)
                KeyboardKeyD0 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D0)
                KeyboardKeyD0 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.OemMinus)
                KeyboardKeyMinus = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.OemMinus)
                KeyboardKeyMinus = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Exsel)
                KeyboardKeyEquals = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Exsel)
                KeyboardKeyEquals = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Back)
                KeyboardKeyBack = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Back)
                KeyboardKeyBack = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Tab)
                KeyboardKeyTab = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Tab)
                KeyboardKeyTab = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Q)
                KeyboardKeyQ = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Q)
                KeyboardKeyQ = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.W)
                KeyboardKeyW = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.W)
                KeyboardKeyW = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.E)
                KeyboardKeyE = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.E)
                KeyboardKeyE = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.R)
                KeyboardKeyR = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.R)
                KeyboardKeyR = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.T)
                KeyboardKeyT = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.T)
                KeyboardKeyT = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Y)
                KeyboardKeyY = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Y)
                KeyboardKeyY = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.U)
                KeyboardKeyU = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.U)
                KeyboardKeyU = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.I)
                KeyboardKeyI = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.I)
                KeyboardKeyI = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.O)
                KeyboardKeyO = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.O)
                KeyboardKeyO = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.P)
                KeyboardKeyP = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.P)
                KeyboardKeyP = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LButton)
                KeyboardKeyLeftBracket = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LButton)
                KeyboardKeyLeftBracket = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.RButton)
                KeyboardKeyRightBracket = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.RButton)
                KeyboardKeyRightBracket = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Return)
                KeyboardKeyReturn = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Return)
                KeyboardKeyReturn = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LControlKey)
                KeyboardKeyLeftControl = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LControlKey)
                KeyboardKeyLeftControl = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.A)
                KeyboardKeyA = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.A)
                KeyboardKeyA = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.S)
                KeyboardKeyS = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.S)
                KeyboardKeyS = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.D)
                KeyboardKeyD = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.D)
                KeyboardKeyD = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F)
                KeyboardKeyF = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F)
                KeyboardKeyF = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.G)
                KeyboardKeyG = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.G)
                KeyboardKeyG = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.H)
                KeyboardKeyH = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.H)
                KeyboardKeyH = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.J)
                KeyboardKeyJ = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.J)
                KeyboardKeyJ = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.K)
                KeyboardKeyK = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.K)
                KeyboardKeyK = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.L)
                KeyboardKeyL = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.L)
                KeyboardKeyL = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.OemSemicolon)
                KeyboardKeySemicolon = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.OemSemicolon)
                KeyboardKeySemicolon = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LaunchApplication1)
                KeyboardKeyApostrophe = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LaunchApplication1)
                KeyboardKeyApostrophe = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.ControlKey)
                KeyboardKeyGrave = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.ControlKey)
                KeyboardKeyGrave = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LShiftKey)
                KeyboardKeyLeftShift = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LShiftKey)
                KeyboardKeyLeftShift = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Back)
                KeyboardKeyBackslash = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Back)
                KeyboardKeyBackslash = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Z)
                KeyboardKeyZ = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Z)
                KeyboardKeyZ = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.X)
                KeyboardKeyX = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.X)
                KeyboardKeyX = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.C)
                KeyboardKeyC = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.C)
                KeyboardKeyC = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.V)
                KeyboardKeyV = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.V)
                KeyboardKeyV = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.B)
                KeyboardKeyB = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.B)
                KeyboardKeyB = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.N)
                KeyboardKeyN = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.N)
                KeyboardKeyN = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.M)
                KeyboardKeyM = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.M)
                KeyboardKeyM = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LControlKey)
                KeyboardKeyComma = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LControlKey)
                KeyboardKeyComma = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.OemPeriod)
                KeyboardKeyPeriod = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.OemPeriod)
                KeyboardKeyPeriod = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Separator)
                KeyboardKeySlash = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Separator)
                KeyboardKeySlash = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.RShiftKey)
                KeyboardKeyRightShift = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.RShiftKey)
                KeyboardKeyRightShift = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Multiply)
                KeyboardKeyMultiply = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Multiply)
                KeyboardKeyMultiply = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Alt)
                KeyboardKeyLeftAlt = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Alt)
                KeyboardKeyLeftAlt = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Space)
                KeyboardKeySpace = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Space)
                KeyboardKeySpace = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Capital)
                KeyboardKeyCapital = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Capital)
                KeyboardKeyCapital = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F1)
                KeyboardKeyF1 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F1)
                KeyboardKeyF1 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F2)
                KeyboardKeyF2 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F2)
                KeyboardKeyF2 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F3)
                KeyboardKeyF3 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F3)
                KeyboardKeyF3 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F4)
                KeyboardKeyF4 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F4)
                KeyboardKeyF4 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F5)
                KeyboardKeyF5 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F5)
                KeyboardKeyF5 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F6)
                KeyboardKeyF6 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F6)
                KeyboardKeyF6 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F7)
                KeyboardKeyF7 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F7)
                KeyboardKeyF7 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F8)
                KeyboardKeyF8 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F8)
                KeyboardKeyF8 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F9)
                KeyboardKeyF9 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F9)
                KeyboardKeyF9 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F10)
                KeyboardKeyF10 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F10)
                KeyboardKeyF10 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumLock)
                KeyboardKeyNumberLock = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumLock)
                KeyboardKeyNumberLock = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.CapsLock)
                KeyboardKeyScrollLock = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.CapsLock)
                KeyboardKeyScrollLock = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad7)
                KeyboardKeyNumPad7 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad7)
                KeyboardKeyNumPad7 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad8)
                KeyboardKeyNumPad8 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad8)
                KeyboardKeyNumPad8 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad9)
                KeyboardKeyNumPad9 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad9)
                KeyboardKeyNumPad9 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Subtract)
                KeyboardKeySubtract = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Subtract)
                KeyboardKeySubtract = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad4)
                KeyboardKeyNumPad4 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad4)
                KeyboardKeyNumPad4 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad5)
                KeyboardKeyNumPad5 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad5)
                KeyboardKeyNumPad5 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad6)
                KeyboardKeyNumPad6 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad6)
                KeyboardKeyNumPad6 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Add)
                KeyboardKeyAdd = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Add)
                KeyboardKeyAdd = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad1)
                KeyboardKeyNumPad1 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad1)
                KeyboardKeyNumPad1 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad2)
                KeyboardKeyNumPad2 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad2)
                KeyboardKeyNumPad2 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad3)
                KeyboardKeyNumPad3 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad3)
                KeyboardKeyNumPad3 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumPad0)
                KeyboardKeyNumPad0 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumPad0)
                KeyboardKeyNumPad0 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Decimal)
                KeyboardKeyDecimal = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Decimal)
                KeyboardKeyDecimal = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Oem102)
                KeyboardKeyOem102 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Oem102)
                KeyboardKeyOem102 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F11)
                KeyboardKeyF11 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F11)
                KeyboardKeyF11 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F12)
                KeyboardKeyF12 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F12)
                KeyboardKeyF12 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F13)
                KeyboardKeyF13 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F13)
                KeyboardKeyF13 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F14)
                KeyboardKeyF14 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F14)
                KeyboardKeyF14 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.F15)
                KeyboardKeyF15 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.F15)
                KeyboardKeyF15 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.KanaMode)
                KeyboardKeyKana = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.KanaMode)
                KeyboardKeyKana = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.IMEAccept)
                KeyboardKeyAbntC1 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.IMEAccept)
                KeyboardKeyAbntC1 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.IMEConvert)
                KeyboardKeyConvert = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.IMEConvert)
                KeyboardKeyConvert = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.IMEConvert)
                KeyboardKeyNoConvert = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.IMEConvert)
                KeyboardKeyNoConvert = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Clear)
                KeyboardKeyYen = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Clear)
                KeyboardKeyYen = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Back)
                KeyboardKeyAbntC2 = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Back)
                KeyboardKeyAbntC2 = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Execute)
                KeyboardKeyNumPadEquals = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Execute)
                KeyboardKeyNumPadEquals = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaPreviousTrack)
                KeyboardKeyPreviousTrack = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaPreviousTrack)
                KeyboardKeyPreviousTrack = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Attn)
                KeyboardKeyAT = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Attn)
                KeyboardKeyAT = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.OemSemicolon)
                KeyboardKeyColon = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.OemSemicolon)
                KeyboardKeyColon = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LineFeed)
                KeyboardKeyUnderline = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LineFeed)
                KeyboardKeyUnderline = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.KanjiMode)
                KeyboardKeyKanji = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.KanjiMode)
                KeyboardKeyKanji = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaStop)
                KeyboardKeyStop = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaStop)
                KeyboardKeyStop = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Add)
                KeyboardKeyAX = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Add)
                KeyboardKeyAX = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaPreviousTrack)
                KeyboardKeyUnlabeled = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaPreviousTrack)
                KeyboardKeyUnlabeled = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaNextTrack)
                KeyboardKeyNextTrack = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaNextTrack)
                KeyboardKeyNextTrack = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Enter)
                KeyboardKeyNumPadEnter = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Enter)
                KeyboardKeyNumPadEnter = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.RControlKey)
                KeyboardKeyRightControl = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.RControlKey)
                KeyboardKeyRightControl = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.VolumeMute)
                KeyboardKeyMute = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.VolumeMute)
                KeyboardKeyMute = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Cancel)
                KeyboardKeyCalculator = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Cancel)
                KeyboardKeyCalculator = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaPlayPause)
                KeyboardKeyPlayPause = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaPlayPause)
                KeyboardKeyPlayPause = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.MediaStop)
                KeyboardKeyMediaStop = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.MediaStop)
                KeyboardKeyMediaStop = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.VolumeDown)
                KeyboardKeyVolumeDown = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.VolumeDown)
                KeyboardKeyVolumeDown = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.VolumeUp)
                KeyboardKeyVolumeUp = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.VolumeUp)
                KeyboardKeyVolumeUp = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserHome)
                KeyboardKeyWebHome = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserHome)
                KeyboardKeyWebHome = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.NumLock)
                KeyboardKeyNumPadComma = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.NumLock)
                KeyboardKeyNumPadComma = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Divide)
                KeyboardKeyDivide = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Divide)
                KeyboardKeyDivide = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.PrintScreen)
                KeyboardKeyPrintScreen = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.PrintScreen)
                KeyboardKeyPrintScreen = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Alt)
                KeyboardKeyRightAlt = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Alt)
                KeyboardKeyRightAlt = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Pause)
                KeyboardKeyPause = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Pause)
                KeyboardKeyPause = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Home)
                KeyboardKeyHome = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Home)
                KeyboardKeyHome = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Up)
                KeyboardKeyUp = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Up)
                KeyboardKeyUp = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.PageUp)
                KeyboardKeyPageUp = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.PageUp)
                KeyboardKeyPageUp = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Left)
                KeyboardKeyLeft = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Left)
                KeyboardKeyLeft = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Right)
                KeyboardKeyRight = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Right)
                KeyboardKeyRight = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.End)
                KeyboardKeyEnd = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.End)
                KeyboardKeyEnd = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Down)
                KeyboardKeyDown = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Down)
                KeyboardKeyDown = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.PageDown)
                KeyboardKeyPageDown = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.PageDown)
                KeyboardKeyPageDown = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Insert)
                KeyboardKeyInsert = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Insert)
                KeyboardKeyInsert = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Delete)
                KeyboardKeyDelete = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Delete)
                KeyboardKeyDelete = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LWin)
                KeyboardKeyLeftWindowsKey = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LWin)
                KeyboardKeyLeftWindowsKey = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.RWin)
                KeyboardKeyRightWindowsKey = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.RWin)
                KeyboardKeyRightWindowsKey = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Apps)
                KeyboardKeyApplications = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Apps)
                KeyboardKeyApplications = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Pa1)
                KeyboardKeyPower = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Pa1)
                KeyboardKeyPower = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Sleep)
                KeyboardKeySleep = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Sleep)
                KeyboardKeySleep = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Sleep)
                KeyboardKeyWake = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Sleep)
                KeyboardKeyWake = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserSearch)
                KeyboardKeyWebSearch = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserSearch)
                KeyboardKeyWebSearch = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserFavorites)
                KeyboardKeyWebFavorites = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserFavorites)
                KeyboardKeyWebFavorites = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserRefresh)
                KeyboardKeyWebRefresh = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserRefresh)
                KeyboardKeyWebRefresh = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserStop)
                KeyboardKeyWebStop = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserStop)
                KeyboardKeyWebStop = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserForward)
                KeyboardKeyWebForward = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserForward)
                KeyboardKeyWebForward = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.BrowserBack)
                KeyboardKeyWebBack = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.BrowserBack)
                KeyboardKeyWebBack = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Attn)
                KeyboardKeyMyComputer = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Attn)
                KeyboardKeyMyComputer = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.LaunchMail)
                KeyboardKeyMail = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.LaunchMail)
                KeyboardKeyMail = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.Select)
                KeyboardKeyMediaSelect = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.Select)
                KeyboardKeyMediaSelect = false;
            if (e.State == KeyState.KeyDown & e.Key == Keys.None)
                KeyboardKeyUnknown = true;
            if (e.State == KeyState.KeyUp & e.Key == Keys.None)
                KeyboardKeyUnknown = false;
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                string str = "KeyboardKeyA : " + KeyboardKeyA + Environment.NewLine;
                str += "KeyboardKeyB : " + KeyboardKeyB + Environment.NewLine;
                str += "KeyboardKeyC : " + KeyboardKeyC + Environment.NewLine;
                str += "KeyboardKeyD : " + KeyboardKeyD + Environment.NewLine;
                str += "KeyboardKeyE : " + KeyboardKeyE + Environment.NewLine;
                str += "KeyboardKeyF : " + KeyboardKeyF + Environment.NewLine;
                str += "KeyboardKeyG : " + KeyboardKeyG + Environment.NewLine;
                str += "KeyboardKeyH : " + KeyboardKeyH + Environment.NewLine;
                str += "KeyboardKeyI : " + KeyboardKeyI + Environment.NewLine;
                str += "KeyboardKeyJ : " + KeyboardKeyJ + Environment.NewLine;
                str += "KeyboardKeyK : " + KeyboardKeyK + Environment.NewLine;
                str += "KeyboardKeyL : " + KeyboardKeyL + Environment.NewLine;
                str += "KeyboardKeyM : " + KeyboardKeyM + Environment.NewLine;
                str += "KeyboardKeyN : " + KeyboardKeyN + Environment.NewLine;
                str += "KeyboardKeyO : " + KeyboardKeyO + Environment.NewLine;
                str += "KeyboardKeyP : " + KeyboardKeyP + Environment.NewLine;
                str += "KeyboardKeyQ : " + KeyboardKeyQ + Environment.NewLine;
                str += "KeyboardKeyR : " + KeyboardKeyR + Environment.NewLine;
                str += "KeyboardKeyS : " + KeyboardKeyS + Environment.NewLine;
                str += "KeyboardKeyT : " + KeyboardKeyT + Environment.NewLine;
                str += "KeyboardKeyU : " + KeyboardKeyU + Environment.NewLine;
                str += "KeyboardKeyV : " + KeyboardKeyV + Environment.NewLine;
                str += "KeyboardKeyW : " + KeyboardKeyW + Environment.NewLine;
                str += "KeyboardKeyX : " + KeyboardKeyX + Environment.NewLine;
                str += "KeyboardKeyY : " + KeyboardKeyY + Environment.NewLine;
                str += "KeyboardKeyZ : " + KeyboardKeyZ + Environment.NewLine;
                str += "KeyboardKeyEscape : " + KeyboardKeyEscape + Environment.NewLine;
                str += "KeyboardKeyD1 : " + KeyboardKeyD1 + Environment.NewLine;
                str += "KeyboardKeyD2 : " + KeyboardKeyD2 + Environment.NewLine;
                str += "KeyboardKeyD3 : " + KeyboardKeyD3 + Environment.NewLine;
                str += "KeyboardKeyD4 : " + KeyboardKeyD4 + Environment.NewLine;
                str += "KeyboardKeyD5 : " + KeyboardKeyD5 + Environment.NewLine;
                str += "KeyboardKeyD6 : " + KeyboardKeyD6 + Environment.NewLine;
                str += "KeyboardKeyD7 : " + KeyboardKeyD7 + Environment.NewLine;
                str += "KeyboardKeyD8 : " + KeyboardKeyD8 + Environment.NewLine;
                str += "KeyboardKeyD9 : " + KeyboardKeyD9 + Environment.NewLine;
                str += "KeyboardKeyD0 : " + KeyboardKeyD0 + Environment.NewLine;
                str += "KeyboardKeyMinus : " + KeyboardKeyMinus + Environment.NewLine;
                str += "KeyboardKeyEquals : " + KeyboardKeyEquals + Environment.NewLine;
                str += "KeyboardKeyBack : " + KeyboardKeyBack + Environment.NewLine;
                str += "KeyboardKeyTab : " + KeyboardKeyTab + Environment.NewLine;
                str += "KeyboardKeyLeftBracket : " + KeyboardKeyLeftBracket + Environment.NewLine;
                str += "KeyboardKeyRightBracket : " + KeyboardKeyRightBracket + Environment.NewLine;
                str += "KeyboardKeyReturn : " + KeyboardKeyReturn + Environment.NewLine;
                str += "KeyboardKeyLeftControl : " + KeyboardKeyLeftControl + Environment.NewLine;
                str += "KeyboardKeySemicolon : " + KeyboardKeySemicolon + Environment.NewLine;
                str += "KeyboardKeyApostrophe : " + KeyboardKeyApostrophe + Environment.NewLine;
                str += "KeyboardKeyGrave : " + KeyboardKeyGrave + Environment.NewLine;
                str += "KeyboardKeyLeftShift : " + KeyboardKeyLeftShift + Environment.NewLine;
                str += "KeyboardKeyBackslash : " + KeyboardKeyBackslash + Environment.NewLine;
                str += "KeyboardKeyComma : " + KeyboardKeyComma + Environment.NewLine;
                str += "KeyboardKeyPeriod : " + KeyboardKeyPeriod + Environment.NewLine;
                str += "KeyboardKeySlash : " + KeyboardKeySlash + Environment.NewLine;
                str += "KeyboardKeyRightShift : " + KeyboardKeyRightShift + Environment.NewLine;
                str += "KeyboardKeyMultiply : " + KeyboardKeyMultiply + Environment.NewLine;
                str += "KeyboardKeyLeftAlt : " + KeyboardKeyLeftAlt + Environment.NewLine;
                str += "KeyboardKeySpace : " + KeyboardKeySpace + Environment.NewLine;
                str += "KeyboardKeyCapital : " + KeyboardKeyCapital + Environment.NewLine;
                str += "KeyboardKeyF1 : " + KeyboardKeyF1 + Environment.NewLine;
                str += "KeyboardKeyF2 : " + KeyboardKeyF2 + Environment.NewLine;
                str += "KeyboardKeyF3 : " + KeyboardKeyF3 + Environment.NewLine;
                str += "KeyboardKeyF4 : " + KeyboardKeyF4 + Environment.NewLine;
                str += "KeyboardKeyF5 : " + KeyboardKeyF5 + Environment.NewLine;
                str += "KeyboardKeyF6 : " + KeyboardKeyF6 + Environment.NewLine;
                str += "KeyboardKeyF7 : " + KeyboardKeyF7 + Environment.NewLine;
                str += "KeyboardKeyF8 : " + KeyboardKeyF8 + Environment.NewLine;
                str += "KeyboardKeyF9 : " + KeyboardKeyF9 + Environment.NewLine;
                str += "KeyboardKeyF10 : " + KeyboardKeyF10 + Environment.NewLine;
                str += "KeyboardKeyF11 : " + KeyboardKeyF11 + Environment.NewLine;
                str += "KeyboardKeyF12 : " + KeyboardKeyF12 + Environment.NewLine;
                str += "KeyboardKeyF13 : " + KeyboardKeyF13 + Environment.NewLine;
                str += "KeyboardKeyF14 : " + KeyboardKeyF14 + Environment.NewLine;
                str += "KeyboardKeyF15 : " + KeyboardKeyF15 + Environment.NewLine;
                str += "KeyboardKeyNumberLock : " + KeyboardKeyNumberLock + Environment.NewLine;
                str += "KeyboardKeyScrollLock : " + KeyboardKeyScrollLock + Environment.NewLine;
                str += "KeyboardKeyNumPad0 : " + KeyboardKeyNumPad0 + Environment.NewLine;
                str += "KeyboardKeyNumPad1 : " + KeyboardKeyNumPad1 + Environment.NewLine;
                str += "KeyboardKeyNumPad2 : " + KeyboardKeyNumPad2 + Environment.NewLine;
                str += "KeyboardKeyNumPad3 : " + KeyboardKeyNumPad3 + Environment.NewLine;
                str += "KeyboardKeyNumPad4 : " + KeyboardKeyNumPad4 + Environment.NewLine;
                str += "KeyboardKeyNumPad5 : " + KeyboardKeyNumPad5 + Environment.NewLine;
                str += "KeyboardKeyNumPad6 : " + KeyboardKeyNumPad6 + Environment.NewLine;
                str += "KeyboardKeyNumPad7 : " + KeyboardKeyNumPad7 + Environment.NewLine;
                str += "KeyboardKeyNumPad8 : " + KeyboardKeyNumPad8 + Environment.NewLine;
                str += "KeyboardKeyNumPad9 : " + KeyboardKeyNumPad9 + Environment.NewLine;
                str += "KeyboardKeySubtract : " + KeyboardKeySubtract + Environment.NewLine;
                str += "KeyboardKeyAdd : " + KeyboardKeyAdd + Environment.NewLine;
                str += "KeyboardKeyDecimal : " + KeyboardKeyDecimal + Environment.NewLine;
                str += "KeyboardKeyOem102 : " + KeyboardKeyOem102 + Environment.NewLine;
                str += "KeyboardKeyKana : " + KeyboardKeyKana + Environment.NewLine;
                str += "KeyboardKeyAbntC1 : " + KeyboardKeyAbntC1 + Environment.NewLine;
                str += "KeyboardKeyConvert : " + KeyboardKeyConvert + Environment.NewLine;
                str += "KeyboardKeyNoConvert : " + KeyboardKeyNoConvert + Environment.NewLine;
                str += "KeyboardKeyYen : " + KeyboardKeyYen + Environment.NewLine;
                str += "KeyboardKeyAbntC2 : " + KeyboardKeyAbntC2 + Environment.NewLine;
                str += "KeyboardKeyNumPadEquals : " + KeyboardKeyNumPadEquals + Environment.NewLine;
                str += "KeyboardKeyPreviousTrack : " + KeyboardKeyPreviousTrack + Environment.NewLine;
                str += "KeyboardKeyAT : " + KeyboardKeyAT + Environment.NewLine;
                str += "KeyboardKeyColon : " + KeyboardKeyColon + Environment.NewLine;
                str += "KeyboardKeyUnderline : " + KeyboardKeyUnderline + Environment.NewLine;
                str += "KeyboardKeyKanji : " + KeyboardKeyKanji + Environment.NewLine;
                str += "KeyboardKeyStop : " + KeyboardKeyStop + Environment.NewLine;
                str += "KeyboardKeyAX : " + KeyboardKeyAX + Environment.NewLine;
                str += "KeyboardKeyUnlabeled : " + KeyboardKeyUnlabeled + Environment.NewLine;
                str += "KeyboardKeyNextTrack : " + KeyboardKeyNextTrack + Environment.NewLine;
                str += "KeyboardKeyNumPadEnter : " + KeyboardKeyNumPadEnter + Environment.NewLine;
                str += "KeyboardKeyRightControl : " + KeyboardKeyRightControl + Environment.NewLine;
                str += "KeyboardKeyMute : " + KeyboardKeyMute + Environment.NewLine;
                str += "KeyboardKeyCalculator : " + KeyboardKeyCalculator + Environment.NewLine;
                str += "KeyboardKeyPlayPause : " + KeyboardKeyPlayPause + Environment.NewLine;
                str += "KeyboardKeyMediaStop : " + KeyboardKeyMediaStop + Environment.NewLine;
                str += "KeyboardKeyVolumeDown : " + KeyboardKeyVolumeDown + Environment.NewLine;
                str += "KeyboardKeyVolumeUp : " + KeyboardKeyVolumeUp + Environment.NewLine;
                str += "KeyboardKeyWebHome : " + KeyboardKeyWebHome + Environment.NewLine;
                str += "KeyboardKeyNumPadComma : " + KeyboardKeyNumPadComma + Environment.NewLine;
                str += "KeyboardKeyDivide : " + KeyboardKeyDivide + Environment.NewLine;
                str += "KeyboardKeyPrintScreen : " + KeyboardKeyPrintScreen + Environment.NewLine;
                str += "KeyboardKeyRightAlt : " + KeyboardKeyRightAlt + Environment.NewLine;
                str += "KeyboardKeyPause : " + KeyboardKeyPause + Environment.NewLine;
                str += "KeyboardKeyHome : " + KeyboardKeyHome + Environment.NewLine;
                str += "KeyboardKeyUp : " + KeyboardKeyUp + Environment.NewLine;
                str += "KeyboardKeyPageUp : " + KeyboardKeyPageUp + Environment.NewLine;
                str += "KeyboardKeyLeft : " + KeyboardKeyLeft + Environment.NewLine;
                str += "KeyboardKeyRight : " + KeyboardKeyRight + Environment.NewLine;
                str += "KeyboardKeyEnd : " + KeyboardKeyEnd + Environment.NewLine;
                str += "KeyboardKeyDown : " + KeyboardKeyDown + Environment.NewLine;
                str += "KeyboardKeyPageDown : " + KeyboardKeyPageDown + Environment.NewLine;
                str += "KeyboardKeyInsert : " + KeyboardKeyInsert + Environment.NewLine;
                str += "KeyboardKeyDelete : " + KeyboardKeyDelete + Environment.NewLine;
                str += "KeyboardKeyLeftWindowsKey : " + KeyboardKeyLeftWindowsKey + Environment.NewLine;
                str += "KeyboardKeyRightWindowsKey : " + KeyboardKeyRightWindowsKey + Environment.NewLine;
                str += "KeyboardKeyApplications : " + KeyboardKeyApplications + Environment.NewLine;
                str += "KeyboardKeyPower : " + KeyboardKeyPower + Environment.NewLine;
                str += "KeyboardKeySleep : " + KeyboardKeySleep + Environment.NewLine;
                str += "KeyboardKeyWake : " + KeyboardKeyWake + Environment.NewLine;
                str += "KeyboardKeyWebSearch : " + KeyboardKeyWebSearch + Environment.NewLine;
                str += "KeyboardKeyWebFavorites : " + KeyboardKeyWebFavorites + Environment.NewLine;
                str += "KeyboardKeyWebRefresh : " + KeyboardKeyWebRefresh + Environment.NewLine;
                str += "KeyboardKeyWebStop : " + KeyboardKeyWebStop + Environment.NewLine;
                str += "KeyboardKeyWebForward : " + KeyboardKeyWebForward + Environment.NewLine;
                str += "KeyboardKeyWebBack : " + KeyboardKeyWebBack + Environment.NewLine;
                str += "KeyboardKeyMyComputer : " + KeyboardKeyMyComputer + Environment.NewLine;
                str += "KeyboardKeyMail : " + KeyboardKeyMail + Environment.NewLine;
                str += "KeyboardKeyMediaSelect : " + KeyboardKeyMediaSelect + Environment.NewLine;
                str += "KeyboardKeyUnknown : " + KeyboardKeyUnknown + Environment.NewLine;
                str += Environment.NewLine;
                this.label1.Text = str;
                System.Threading.Thread.Sleep(100);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
        }
    }
}