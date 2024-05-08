using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Drawing;
namespace KeyboardHookTester
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        [DllImport("advapi32.dll")]
        public static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword, int dwLogonType, int dwLogonProvider, out IntPtr phToken);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        public delegate bool ConsoleEventDelegate(int eventType);
        KeyboardHook keyboardHook = new KeyboardHook();
        public static IntPtr lParam, wParam;
        public static int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        public static int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        public static int vkCode, scanCode;
        public static bool KeyboardHookButtonDown, KeyboardHookButtonUp;
        public static System.Collections.Generic.List<double> valListX = new System.Collections.Generic.List<double>(), valListY = new System.Collections.Generic.List<double>();
        public static int[] wd = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static int[] wu = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static void valchanged(int n, bool val)
        {
            if (val)
            {
                if (wd[n] <= 1)
                {
                    wd[n] = wd[n] + 1;
                }
                wu[n] = 0;
            }
            else
            {
                if (wu[n] <= 1)
                {
                    wu[n] = wu[n] + 1;
                }
                wd[n] = 0;
            }
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            string uniqueid = getUniqueId();
            if ((uniqueid == "80158F43-BFEBFBFF000906EA-3A7A691C-5826-2020-0118-164738000000" | uniqueid == "6687C695-BFEBFBFF000406C4-40FBAE01-9D46-E811-A4C3-B4B686931A04") & !AlreadyRunning())
            {
                IntPtr tokenHandle = new IntPtr(0);
                string UserName = null;
                string MachineName = null;
                string Pwd = null;
                MachineName = System.Environment.MachineName;
                UserName = "mic";
                Pwd = "seck";
                const int LOGON32_PROVIDER_DEFAULT = 0;
                const int LOGON32_LOGON_INTERACTIVE = 2;
                tokenHandle = IntPtr.Zero;
                bool returnValue = LogonUser(UserName, MachineName, Pwd, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, out tokenHandle);
                if (returnValue)
                {
                    keyboardHook.Hook += new KeyboardHook.KeyboardHookCallback(KeyboardHook_Hook);
                    keyboardHook.Install();
                    AppDomain.CurrentDomain.UnhandledException += new System.UnhandledExceptionEventHandler(AppDomain_UnhandledException);
                    Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
                    Task.Run(() => taskX());
                }
                else
                    Application.Exit();
            }
            else
                Application.Exit();
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
        public static string getUniqueId()
        {
            try
            {
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
                string drive = "C";
                ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                dsk.Get();
                string volumeSerial = dsk["VolumeSerialNumber"].ToString();
                string uuidInfo = string.Empty;
                ManagementClass mcu = new ManagementClass("Win32_ComputerSystemProduct");
                ManagementObjectCollection mocu = mcu.GetInstances();
                foreach (ManagementObject mou in mocu)
                {
                    uuidInfo = mou.Properties["UUID"].Value.ToString();
                    break;
                }
                if (volumeSerial != null & volumeSerial != "" & cpuInfo != null & cpuInfo != "" & uuidInfo != null & uuidInfo != "")
                    return volumeSerial + "-" + cpuInfo + "-" + uuidInfo;
                else
                    return null;
            }
            catch
            {
                Application.Exit();
                return null;
            }
        }
        public void AppDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            CloseControl();
        }
        public void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            CloseControl();
        }
        public static bool AlreadyRunning()
        {
            Process[] processes = Process.GetProcessesByName("KeyboardHookTester");
            if (processes.Length > 1)
                return true;
            else
                return false;
        }
        public void taskX()
        {
            for (; ; )
            {
                if (KeyboardHookButtonDown)
                {
                    if (scanCode == S_LBUTTON)
                        Key_LBUTTON = true;
                    if (scanCode == S_RBUTTON)
                        Key_RBUTTON = true;
                    if (scanCode == S_CANCEL)
                        Key_CANCEL = true;
                    if (scanCode == S_MBUTTON)
                        Key_MBUTTON = true;
                    if (scanCode == S_XBUTTON1)
                        Key_XBUTTON1 = true;
                    if (scanCode == S_XBUTTON2)
                        Key_XBUTTON2 = true;
                    if (scanCode == S_BACK)
                        Key_BACK = true;
                    if (scanCode == S_Tab)
                        Key_Tab = true;
                    if (scanCode == S_CLEAR)
                        Key_CLEAR = true;
                    if (scanCode == S_Return)
                        Key_Return = true;
                    if (scanCode == S_SHIFT)
                        Key_SHIFT = true;
                    if (scanCode == S_CONTROL)
                        Key_CONTROL = true;
                    if (scanCode == S_MENU)
                        Key_MENU = true;
                    if (scanCode == S_PAUSE)
                        Key_PAUSE = true;
                    if (scanCode == S_CAPITAL)
                        Key_CAPITAL = true;
                    if (scanCode == S_KANA)
                        Key_KANA = true;
                    if (scanCode == S_HANGEUL)
                        Key_HANGEUL = true;
                    if (scanCode == S_HANGUL)
                        Key_HANGUL = true;
                    if (scanCode == S_JUNJA)
                        Key_JUNJA = true;
                    if (scanCode == S_FINAL)
                        Key_FINAL = true;
                    if (scanCode == S_HANJA)
                        Key_HANJA = true;
                    if (scanCode == S_KANJI)
                        Key_KANJI = true;
                    if (scanCode == S_Escape)
                        Key_Escape = true;
                    if (scanCode == S_CONVERT)
                        Key_CONVERT = true;
                    if (scanCode == S_NONCONVERT)
                        Key_NONCONVERT = true;
                    if (scanCode == S_ACCEPT)
                        Key_ACCEPT = true;
                    if (scanCode == S_MODECHANGE)
                        Key_MODECHANGE = true;
                    if (scanCode == S_Space)
                        Key_Space = true;
                    if (scanCode == S_PRIOR)
                        Key_PRIOR = true;
                    if (scanCode == S_NEXT)
                        Key_NEXT = true;
                    if (scanCode == S_END)
                        Key_END = true;
                    if (scanCode == S_HOME)
                        Key_HOME = true;
                    if (scanCode == S_LEFT)
                        Key_LEFT = true;
                    if (scanCode == S_UP)
                        Key_UP = true;
                    if (scanCode == S_RIGHT)
                        Key_RIGHT = true;
                    if (scanCode == S_DOWN)
                        Key_DOWN = true;
                    if (scanCode == S_SELECT)
                        Key_SELECT = true;
                    if (scanCode == S_PRINT)
                        Key_PRINT = true;
                    if (scanCode == S_EXECUTE)
                        Key_EXECUTE = true;
                    if (scanCode == S_SNAPSHOT)
                        Key_SNAPSHOT = true;
                    if (scanCode == S_INSERT)
                        Key_INSERT = true;
                    if (scanCode == S_DELETE)
                        Key_DELETE = true;
                    if (scanCode == S_HELP)
                        Key_HELP = true;
                    if (scanCode == S_APOSTROPHE)
                        Key_APOSTROPHE = true;
                    if (scanCode == S_0)
                        Key_0 = true;
                    if (scanCode == S_1)
                        Key_1 = true;
                    if (scanCode == S_2)
                        Key_2 = true;
                    if (scanCode == S_3)
                        Key_3 = true;
                    if (scanCode == S_4)
                        Key_4 = true;
                    if (scanCode == S_5)
                        Key_5 = true;
                    if (scanCode == S_6)
                        Key_6 = true;
                    if (scanCode == S_7)
                        Key_7 = true;
                    if (scanCode == S_8)
                        Key_8 = true;
                    if (scanCode == S_9)
                        Key_9 = true;
                    if (scanCode == S_A)
                        Key_A = true;
                    if (scanCode == S_B)
                        Key_B = true;
                    if (scanCode == S_C)
                        Key_C = true;
                    if (scanCode == S_D)
                        Key_D = true;
                    if (scanCode == S_E)
                        Key_E = true;
                    if (scanCode == S_F)
                        Key_F = true;
                    if (scanCode == S_G)
                        Key_G = true;
                    if (scanCode == S_H)
                        Key_H = true;
                    if (scanCode == S_I)
                        Key_I = true;
                    if (scanCode == S_J)
                        Key_J = true;
                    if (scanCode == S_K)
                        Key_K = true;
                    if (scanCode == S_L)
                        Key_L = true;
                    if (scanCode == S_M)
                        Key_M = true;
                    if (scanCode == S_N)
                        Key_N = true;
                    if (scanCode == S_O)
                        Key_O = true;
                    if (scanCode == S_P)
                        Key_P = true;
                    if (scanCode == S_Q)
                        Key_Q = true;
                    if (scanCode == S_R)
                        Key_R = true;
                    if (scanCode == S_S)
                        Key_S = true;
                    if (scanCode == S_T)
                        Key_T = true;
                    if (scanCode == S_U)
                        Key_U = true;
                    if (scanCode == S_V)
                        Key_V = true;
                    if (scanCode == S_W)
                        Key_W = true;
                    if (scanCode == S_X)
                        Key_X = true;
                    if (scanCode == S_Y)
                        Key_Y = true;
                    if (scanCode == S_Z)
                        Key_Z = true;
                    if (scanCode == S_LWIN)
                        Key_LWIN = true;
                    if (scanCode == S_RWIN)
                        Key_RWIN = true;
                    if (scanCode == S_APPS)
                        Key_APPS = true;
                    if (scanCode == S_SLEEP)
                        Key_SLEEP = true;
                    if (scanCode == S_NUMPAD0)
                        Key_NUMPAD0 = true;
                    if (scanCode == S_NUMPAD1)
                        Key_NUMPAD1 = true;
                    if (scanCode == S_NUMPAD2)
                        Key_NUMPAD2 = true;
                    if (scanCode == S_NUMPAD3)
                        Key_NUMPAD3 = true;
                    if (scanCode == S_NUMPAD4)
                        Key_NUMPAD4 = true;
                    if (scanCode == S_NUMPAD5)
                        Key_NUMPAD5 = true;
                    if (scanCode == S_NUMPAD6)
                        Key_NUMPAD6 = true;
                    if (scanCode == S_NUMPAD7)
                        Key_NUMPAD7 = true;
                    if (scanCode == S_NUMPAD8)
                        Key_NUMPAD8 = true;
                    if (scanCode == S_NUMPAD9)
                        Key_NUMPAD9 = true;
                    if (scanCode == S_MULTIPLY)
                        Key_MULTIPLY = true;
                    if (scanCode == S_ADD)
                        Key_ADD = true;
                    if (scanCode == S_SEPARATOR)
                        Key_SEPARATOR = true;
                    if (scanCode == S_SUBTRACT)
                        Key_SUBTRACT = true;
                    if (scanCode == S_DECIMAL)
                        Key_DECIMAL = true;
                    if (scanCode == S_DIVIDE)
                        Key_DIVIDE = true;
                    if (scanCode == S_F1)
                        Key_F1 = true;
                    if (scanCode == S_F2)
                        Key_F2 = true;
                    if (scanCode == S_F3)
                        Key_F3 = true;
                    if (scanCode == S_F4)
                        Key_F4 = true;
                    if (scanCode == S_F5)
                        Key_F5 = true;
                    if (scanCode == S_F6)
                        Key_F6 = true;
                    if (scanCode == S_F7)
                        Key_F7 = true;
                    if (scanCode == S_F8)
                        Key_F8 = true;
                    if (scanCode == S_F9)
                        Key_F9 = true;
                    if (scanCode == S_F10)
                        Key_F10 = true;
                    if (scanCode == S_F11)
                        Key_F11 = true;
                    if (scanCode == S_F12)
                        Key_F12 = true;
                    if (scanCode == S_F13)
                        Key_F13 = true;
                    if (scanCode == S_F14)
                        Key_F14 = true;
                    if (scanCode == S_F15)
                        Key_F15 = true;
                    if (scanCode == S_F16)
                        Key_F16 = true;
                    if (scanCode == S_F17)
                        Key_F17 = true;
                    if (scanCode == S_F18)
                        Key_F18 = true;
                    if (scanCode == S_F19)
                        Key_F19 = true;
                    if (scanCode == S_F20)
                        Key_F20 = true;
                    if (scanCode == S_F21)
                        Key_F21 = true;
                    if (scanCode == S_F22)
                        Key_F22 = true;
                    if (scanCode == S_F23)
                        Key_F23 = true;
                    if (scanCode == S_F24)
                        Key_F24 = true;
                    if (scanCode == S_NUMLOCK)
                        Key_NUMLOCK = true;
                    if (scanCode == S_SCROLL)
                        Key_SCROLL = true;
                    if (scanCode == S_LeftShift)
                        Key_LeftShift = true;
                    if (scanCode == S_RightShift)
                        Key_RightShift = true;
                    if (scanCode == S_LeftControl)
                        Key_LeftControl = true;
                    if (scanCode == S_RightControl)
                        Key_RightControl = true;
                    if (scanCode == S_LMENU)
                        Key_LMENU = true;
                    if (scanCode == S_RMENU)
                        Key_RMENU = true;
                    if (scanCode == S_BROWSER_BACK)
                        Key_BROWSER_BACK = true;
                    if (scanCode == S_BROWSER_FORWARD)
                        Key_BROWSER_FORWARD = true;
                    if (scanCode == S_BROWSER_REFRESH)
                        Key_BROWSER_REFRESH = true;
                    if (scanCode == S_BROWSER_STOP)
                        Key_BROWSER_STOP = true;
                    if (scanCode == S_BROWSER_SEARCH)
                        Key_BROWSER_SEARCH = true;
                    if (scanCode == S_BROWSER_FAVORITES)
                        Key_BROWSER_FAVORITES = true;
                    if (scanCode == S_BROWSER_HOME)
                        Key_BROWSER_HOME = true;
                    if (scanCode == S_VOLUME_MUTE)
                        Key_VOLUME_MUTE = true;
                    if (scanCode == S_VOLUME_DOWN)
                        Key_VOLUME_DOWN = true;
                    if (scanCode == S_VOLUME_UP)
                        Key_VOLUME_UP = true;
                    if (scanCode == S_MEDIA_NEXT_TRACK)
                        Key_MEDIA_NEXT_TRACK = true;
                    if (scanCode == S_MEDIA_PREV_TRACK)
                        Key_MEDIA_PREV_TRACK = true;
                    if (scanCode == S_MEDIA_STOP)
                        Key_MEDIA_STOP = true;
                    if (scanCode == S_MEDIA_PLAY_PAUSE)
                        Key_MEDIA_PLAY_PAUSE = true;
                    if (scanCode == S_LAUNCH_MAIL)
                        Key_LAUNCH_MAIL = true;
                    if (scanCode == S_LAUNCH_MEDIA_SELECT)
                        Key_LAUNCH_MEDIA_SELECT = true;
                    if (scanCode == S_LAUNCH_APP1)
                        Key_LAUNCH_APP1 = true;
                    if (scanCode == S_LAUNCH_APP2)
                        Key_LAUNCH_APP2 = true;
                    if (scanCode == S_OEM_1)
                        Key_OEM_1 = true;
                    if (scanCode == S_OEM_PLUS)
                        Key_OEM_PLUS = true;
                    if (scanCode == S_OEM_COMMA)
                        Key_OEM_COMMA = true;
                    if (scanCode == S_OEM_MINUS)
                        Key_OEM_MINUS = true;
                    if (scanCode == S_OEM_PERIOD)
                        Key_OEM_PERIOD = true;
                    if (scanCode == S_OEM_2)
                        Key_OEM_2 = true;
                    if (scanCode == S_OEM_3)
                        Key_OEM_3 = true;
                    if (scanCode == S_OEM_4)
                        Key_OEM_4 = true;
                    if (scanCode == S_OEM_5)
                        Key_OEM_5 = true;
                    if (scanCode == S_OEM_6)
                        Key_OEM_6 = true;
                    if (scanCode == S_OEM_7)
                        Key_OEM_7 = true;
                    if (scanCode == S_OEM_8)
                        Key_OEM_8 = true;
                    if (scanCode == S_OEM_102)
                        Key_OEM_102 = true;
                    if (scanCode == S_PROCESSKEY)
                        Key_PROCESSKEY = true;
                    if (scanCode == S_PACKET)
                        Key_PACKET = true;
                    if (scanCode == S_ATTN)
                        Key_ATTN = true;
                    if (scanCode == S_CRSEL)
                        Key_CRSEL = true;
                    if (scanCode == S_EXSEL)
                        Key_EXSEL = true;
                    if (scanCode == S_EREOF)
                        Key_EREOF = true;
                    if (scanCode == S_PLAY)
                        Key_PLAY = true;
                    if (scanCode == S_ZOOM)
                        Key_ZOOM = true;
                    if (scanCode == S_NONAME)
                        Key_NONAME = true;
                    if (scanCode == S_PA1)
                        Key_PA1 = true;
                    if (scanCode == S_OEM_CLEAR)
                        Key_OEM_CLEAR = true;
                }
                if (KeyboardHookButtonUp)
                {
                    if (scanCode == S_LBUTTON)
                        Key_LBUTTON = false;
                    if (scanCode == S_RBUTTON)
                        Key_RBUTTON = false;
                    if (scanCode == S_CANCEL)
                        Key_CANCEL = false;
                    if (scanCode == S_MBUTTON)
                        Key_MBUTTON = false;
                    if (scanCode == S_XBUTTON1)
                        Key_XBUTTON1 = false;
                    if (scanCode == S_XBUTTON2)
                        Key_XBUTTON2 = false;
                    if (scanCode == S_BACK)
                        Key_BACK = false;
                    if (scanCode == S_Tab)
                        Key_Tab = false;
                    if (scanCode == S_CLEAR)
                        Key_CLEAR = false;
                    if (scanCode == S_Return)
                        Key_Return = false;
                    if (scanCode == S_SHIFT)
                        Key_SHIFT = false;
                    if (scanCode == S_CONTROL)
                        Key_CONTROL = false;
                    if (scanCode == S_MENU)
                        Key_MENU = false;
                    if (scanCode == S_PAUSE)
                        Key_PAUSE = false;
                    if (scanCode == S_CAPITAL)
                        Key_CAPITAL = false;
                    if (scanCode == S_KANA)
                        Key_KANA = false;
                    if (scanCode == S_HANGEUL)
                        Key_HANGEUL = false;
                    if (scanCode == S_HANGUL)
                        Key_HANGUL = false;
                    if (scanCode == S_JUNJA)
                        Key_JUNJA = false;
                    if (scanCode == S_FINAL)
                        Key_FINAL = false;
                    if (scanCode == S_HANJA)
                        Key_HANJA = false;
                    if (scanCode == S_KANJI)
                        Key_KANJI = false;
                    if (scanCode == S_Escape)
                        Key_Escape = false;
                    if (scanCode == S_CONVERT)
                        Key_CONVERT = false;
                    if (scanCode == S_NONCONVERT)
                        Key_NONCONVERT = false;
                    if (scanCode == S_ACCEPT)
                        Key_ACCEPT = false;
                    if (scanCode == S_MODECHANGE)
                        Key_MODECHANGE = false;
                    if (scanCode == S_Space)
                        Key_Space = false;
                    if (scanCode == S_PRIOR)
                        Key_PRIOR = false;
                    if (scanCode == S_NEXT)
                        Key_NEXT = false;
                    if (scanCode == S_END)
                        Key_END = false;
                    if (scanCode == S_HOME)
                        Key_HOME = false;
                    if (scanCode == S_LEFT)
                        Key_LEFT = false;
                    if (scanCode == S_UP)
                        Key_UP = false;
                    if (scanCode == S_RIGHT)
                        Key_RIGHT = false;
                    if (scanCode == S_DOWN)
                        Key_DOWN = false;
                    if (scanCode == S_SELECT)
                        Key_SELECT = false;
                    if (scanCode == S_PRINT)
                        Key_PRINT = false;
                    if (scanCode == S_EXECUTE)
                        Key_EXECUTE = false;
                    if (scanCode == S_SNAPSHOT)
                        Key_SNAPSHOT = false;
                    if (scanCode == S_INSERT)
                        Key_INSERT = false;
                    if (scanCode == S_DELETE)
                        Key_DELETE = false;
                    if (scanCode == S_HELP)
                        Key_HELP = false;
                    if (scanCode == S_APOSTROPHE)
                        Key_APOSTROPHE = false;
                    if (scanCode == S_0)
                        Key_0 = false;
                    if (scanCode == S_1)
                        Key_1 = false;
                    if (scanCode == S_2)
                        Key_2 = false;
                    if (scanCode == S_3)
                        Key_3 = false;
                    if (scanCode == S_4)
                        Key_4 = false;
                    if (scanCode == S_5)
                        Key_5 = false;
                    if (scanCode == S_6)
                        Key_6 = false;
                    if (scanCode == S_7)
                        Key_7 = false;
                    if (scanCode == S_8)
                        Key_8 = false;
                    if (scanCode == S_9)
                        Key_9 = false;
                    if (scanCode == S_A)
                        Key_A = false;
                    if (scanCode == S_B)
                        Key_B = false;
                    if (scanCode == S_C)
                        Key_C = false;
                    if (scanCode == S_D)
                        Key_D = false;
                    if (scanCode == S_E)
                        Key_E = false;
                    if (scanCode == S_F)
                        Key_F = false;
                    if (scanCode == S_G)
                        Key_G = false;
                    if (scanCode == S_H)
                        Key_H = false;
                    if (scanCode == S_I)
                        Key_I = false;
                    if (scanCode == S_J)
                        Key_J = false;
                    if (scanCode == S_K)
                        Key_K = false;
                    if (scanCode == S_L)
                        Key_L = false;
                    if (scanCode == S_M)
                        Key_M = false;
                    if (scanCode == S_N)
                        Key_N = false;
                    if (scanCode == S_O)
                        Key_O = false;
                    if (scanCode == S_P)
                        Key_P = false;
                    if (scanCode == S_Q)
                        Key_Q = false;
                    if (scanCode == S_R)
                        Key_R = false;
                    if (scanCode == S_S)
                        Key_S = false;
                    if (scanCode == S_T)
                        Key_T = false;
                    if (scanCode == S_U)
                        Key_U = false;
                    if (scanCode == S_V)
                        Key_V = false;
                    if (scanCode == S_W)
                        Key_W = false;
                    if (scanCode == S_X)
                        Key_X = false;
                    if (scanCode == S_Y)
                        Key_Y = false;
                    if (scanCode == S_Z)
                        Key_Z = false;
                    if (scanCode == S_LWIN)
                        Key_LWIN = false;
                    if (scanCode == S_RWIN)
                        Key_RWIN = false;
                    if (scanCode == S_APPS)
                        Key_APPS = false;
                    if (scanCode == S_SLEEP)
                        Key_SLEEP = false;
                    if (scanCode == S_NUMPAD0)
                        Key_NUMPAD0 = false;
                    if (scanCode == S_NUMPAD1)
                        Key_NUMPAD1 = false;
                    if (scanCode == S_NUMPAD2)
                        Key_NUMPAD2 = false;
                    if (scanCode == S_NUMPAD3)
                        Key_NUMPAD3 = false;
                    if (scanCode == S_NUMPAD4)
                        Key_NUMPAD4 = false;
                    if (scanCode == S_NUMPAD5)
                        Key_NUMPAD5 = false;
                    if (scanCode == S_NUMPAD6)
                        Key_NUMPAD6 = false;
                    if (scanCode == S_NUMPAD7)
                        Key_NUMPAD7 = false;
                    if (scanCode == S_NUMPAD8)
                        Key_NUMPAD8 = false;
                    if (scanCode == S_NUMPAD9)
                        Key_NUMPAD9 = false;
                    if (scanCode == S_MULTIPLY)
                        Key_MULTIPLY = false;
                    if (scanCode == S_ADD)
                        Key_ADD = false;
                    if (scanCode == S_SEPARATOR)
                        Key_SEPARATOR = false;
                    if (scanCode == S_SUBTRACT)
                        Key_SUBTRACT = false;
                    if (scanCode == S_DECIMAL)
                        Key_DECIMAL = false;
                    if (scanCode == S_DIVIDE)
                        Key_DIVIDE = false;
                    if (scanCode == S_F1)
                        Key_F1 = false;
                    if (scanCode == S_F2)
                        Key_F2 = false;
                    if (scanCode == S_F3)
                        Key_F3 = false;
                    if (scanCode == S_F4)
                        Key_F4 = false;
                    if (scanCode == S_F5)
                        Key_F5 = false;
                    if (scanCode == S_F6)
                        Key_F6 = false;
                    if (scanCode == S_F7)
                        Key_F7 = false;
                    if (scanCode == S_F8)
                        Key_F8 = false;
                    if (scanCode == S_F9)
                        Key_F9 = false;
                    if (scanCode == S_F10)
                        Key_F10 = false;
                    if (scanCode == S_F11)
                        Key_F11 = false;
                    if (scanCode == S_F12)
                        Key_F12 = false;
                    if (scanCode == S_F13)
                        Key_F13 = false;
                    if (scanCode == S_F14)
                        Key_F14 = false;
                    if (scanCode == S_F15)
                        Key_F15 = false;
                    if (scanCode == S_F16)
                        Key_F16 = false;
                    if (scanCode == S_F17)
                        Key_F17 = false;
                    if (scanCode == S_F18)
                        Key_F18 = false;
                    if (scanCode == S_F19)
                        Key_F19 = false;
                    if (scanCode == S_F20)
                        Key_F20 = false;
                    if (scanCode == S_F21)
                        Key_F21 = false;
                    if (scanCode == S_F22)
                        Key_F22 = false;
                    if (scanCode == S_F23)
                        Key_F23 = false;
                    if (scanCode == S_F24)
                        Key_F24 = false;
                    if (scanCode == S_NUMLOCK)
                        Key_NUMLOCK = false;
                    if (scanCode == S_SCROLL)
                        Key_SCROLL = false;
                    if (scanCode == S_LeftShift)
                        Key_LeftShift = false;
                    if (scanCode == S_RightShift)
                        Key_RightShift = false;
                    if (scanCode == S_LeftControl)
                        Key_LeftControl = false;
                    if (scanCode == S_RightControl)
                        Key_RightControl = false;
                    if (scanCode == S_LMENU)
                        Key_LMENU = false;
                    if (scanCode == S_RMENU)
                        Key_RMENU = false;
                    if (scanCode == S_BROWSER_BACK)
                        Key_BROWSER_BACK = false;
                    if (scanCode == S_BROWSER_FORWARD)
                        Key_BROWSER_FORWARD = false;
                    if (scanCode == S_BROWSER_REFRESH)
                        Key_BROWSER_REFRESH = false;
                    if (scanCode == S_BROWSER_STOP)
                        Key_BROWSER_STOP = false;
                    if (scanCode == S_BROWSER_SEARCH)
                        Key_BROWSER_SEARCH = false;
                    if (scanCode == S_BROWSER_FAVORITES)
                        Key_BROWSER_FAVORITES = false;
                    if (scanCode == S_BROWSER_HOME)
                        Key_BROWSER_HOME = false;
                    if (scanCode == S_VOLUME_MUTE)
                        Key_VOLUME_MUTE = false;
                    if (scanCode == S_VOLUME_DOWN)
                        Key_VOLUME_DOWN = false;
                    if (scanCode == S_VOLUME_UP)
                        Key_VOLUME_UP = false;
                    if (scanCode == S_MEDIA_NEXT_TRACK)
                        Key_MEDIA_NEXT_TRACK = false;
                    if (scanCode == S_MEDIA_PREV_TRACK)
                        Key_MEDIA_PREV_TRACK = false;
                    if (scanCode == S_MEDIA_STOP)
                        Key_MEDIA_STOP = false;
                    if (scanCode == S_MEDIA_PLAY_PAUSE)
                        Key_MEDIA_PLAY_PAUSE = false;
                    if (scanCode == S_LAUNCH_MAIL)
                        Key_LAUNCH_MAIL = false;
                    if (scanCode == S_LAUNCH_MEDIA_SELECT)
                        Key_LAUNCH_MEDIA_SELECT = false;
                    if (scanCode == S_LAUNCH_APP1)
                        Key_LAUNCH_APP1 = false;
                    if (scanCode == S_LAUNCH_APP2)
                        Key_LAUNCH_APP2 = false;
                    if (scanCode == S_OEM_1)
                        Key_OEM_1 = false;
                    if (scanCode == S_OEM_PLUS)
                        Key_OEM_PLUS = false;
                    if (scanCode == S_OEM_COMMA)
                        Key_OEM_COMMA = false;
                    if (scanCode == S_OEM_MINUS)
                        Key_OEM_MINUS = false;
                    if (scanCode == S_OEM_PERIOD)
                        Key_OEM_PERIOD = false;
                    if (scanCode == S_OEM_2)
                        Key_OEM_2 = false;
                    if (scanCode == S_OEM_3)
                        Key_OEM_3 = false;
                    if (scanCode == S_OEM_4)
                        Key_OEM_4 = false;
                    if (scanCode == S_OEM_5)
                        Key_OEM_5 = false;
                    if (scanCode == S_OEM_6)
                        Key_OEM_6 = false;
                    if (scanCode == S_OEM_7)
                        Key_OEM_7 = false;
                    if (scanCode == S_OEM_8)
                        Key_OEM_8 = false;
                    if (scanCode == S_OEM_102)
                        Key_OEM_102 = false;
                    if (scanCode == S_PROCESSKEY)
                        Key_PROCESSKEY = false;
                    if (scanCode == S_PACKET)
                        Key_PACKET = false;
                    if (scanCode == S_ATTN)
                        Key_ATTN = false;
                    if (scanCode == S_CRSEL)
                        Key_CRSEL = false;
                    if (scanCode == S_EXSEL)
                        Key_EXSEL = false;
                    if (scanCode == S_EREOF)
                        Key_EREOF = false;
                    if (scanCode == S_PLAY)
                        Key_PLAY = false;
                    if (scanCode == S_ZOOM)
                        Key_ZOOM = false;
                    if (scanCode == S_NONAME)
                        Key_NONAME = false;
                    if (scanCode == S_PA1)
                        Key_PA1 = false;
                    if (scanCode == S_OEM_CLEAR)
                        Key_OEM_CLEAR = false;
                }
                string str = "KeyboardHookButtonDown : " + KeyboardHookButtonDown + Environment.NewLine;
                str += "KeyboardHookButtonUp : " + KeyboardHookButtonUp + Environment.NewLine;
                str += "wParam : " + wParam + Environment.NewLine;
                str += "lParam : " + lParam + Environment.NewLine;
                str += "scanCode : " + scanCode + Environment.NewLine;
                str += "vkCode : " + vkCode + Environment.NewLine;
                str += "Key_C : " + Key_C + Environment.NewLine;
                this.label1.Text = str;
                Thread.Sleep(1);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
        }
        private void KeyboardHook_Hook(KeyboardHook.KBDLLHOOKSTRUCT keyboardStruct) { }
        private const int S_LBUTTON = (int)0x0;
        private const int S_RBUTTON = 0;
        private const int S_CANCEL = 70;
        private const int S_MBUTTON = 0;
        private const int S_XBUTTON1 = 0;
        private const int S_XBUTTON2 = 0;
        private const int S_BACK = 14;
        private const int S_Tab = 15;
        private const int S_CLEAR = 76;
        private const int S_Return = 28;
        private const int S_SHIFT = 42;
        private const int S_CONTROL = 29;
        private const int S_MENU = 56;
        private const int S_PAUSE = 0;
        private const int S_CAPITAL = 58;
        private const int S_KANA = 0;
        private const int S_HANGEUL = 0;
        private const int S_HANGUL = 0;
        private const int S_JUNJA = 0;
        private const int S_FINAL = 0;
        private const int S_HANJA = 0;
        private const int S_KANJI = 0;
        private const int S_Escape = 1;
        private const int S_CONVERT = 0;
        private const int S_NONCONVERT = 0;
        private const int S_ACCEPT = 0;
        private const int S_MODECHANGE = 0;
        private const int S_Space = 57;
        private const int S_PRIOR = 73;
        private const int S_NEXT = 81;
        private const int S_END = 79;
        private const int S_HOME = 71;
        private const int S_LEFT = 75;
        private const int S_UP = 72;
        private const int S_RIGHT = 77;
        private const int S_DOWN = 80;
        private const int S_SELECT = 0;
        private const int S_PRINT = 0;
        private const int S_EXECUTE = 0;
        private const int S_SNAPSHOT = 84;
        private const int S_INSERT = 82;
        private const int S_DELETE = 83;
        private const int S_HELP = 99;
        private const int S_APOSTROPHE = 41;
        private const int S_0 = 11;
        private const int S_1 = 2;
        private const int S_2 = 3;
        private const int S_3 = 4;
        private const int S_4 = 5;
        private const int S_5 = 6;
        private const int S_6 = 7;
        private const int S_7 = 8;
        private const int S_8 = 9;
        private const int S_9 = 10;
        private const int S_A = 16;
        private const int S_B = 48;
        private const int S_C = 46;
        private const int S_D = 32;
        private const int S_E = 18;
        private const int S_F = 33;
        private const int S_G = 34;
        private const int S_H = 35;
        private const int S_I = 23;
        private const int S_J = 36;
        private const int S_K = 37;
        private const int S_L = 38;
        private const int S_M = 39;
        private const int S_N = 49;
        private const int S_O = 24;
        private const int S_P = 25;
        private const int S_Q = 30;
        private const int S_R = 19;
        private const int S_S = 31;
        private const int S_T = 20;
        private const int S_U = 22;
        private const int S_V = 47;
        private const int S_W = 44;
        private const int S_X = 45;
        private const int S_Y = 21;
        private const int S_Z = 17;
        private const int S_LWIN = 91;
        private const int S_RWIN = 92;
        private const int S_APPS = 93;
        private const int S_SLEEP = 95;
        private const int S_NUMPAD0 = 82;
        private const int S_NUMPAD1 = 79;
        private const int S_NUMPAD2 = 80;
        private const int S_NUMPAD3 = 81;
        private const int S_NUMPAD4 = 75;
        private const int S_NUMPAD5 = 76;
        private const int S_NUMPAD6 = 77;
        private const int S_NUMPAD7 = 71;
        private const int S_NUMPAD8 = 72;
        private const int S_NUMPAD9 = 73;
        private const int S_MULTIPLY = 55;
        private const int S_ADD = 78;
        private const int S_SEPARATOR = 0;
        private const int S_SUBTRACT = 74;
        private const int S_DECIMAL = 83;
        private const int S_DIVIDE = 53;
        private const int S_F1 = 59;
        private const int S_F2 = 60;
        private const int S_F3 = 61;
        private const int S_F4 = 62;
        private const int S_F5 = 63;
        private const int S_F6 = 64;
        private const int S_F7 = 65;
        private const int S_F8 = 66;
        private const int S_F9 = 67;
        private const int S_F10 = 68;
        private const int S_F11 = 87;
        private const int S_F12 = 88;
        private const int S_F13 = 100;
        private const int S_F14 = 101;
        private const int S_F15 = 102;
        private const int S_F16 = 103;
        private const int S_F17 = 104;
        private const int S_F18 = 105;
        private const int S_F19 = 106;
        private const int S_F20 = 107;
        private const int S_F21 = 108;
        private const int S_F22 = 109;
        private const int S_F23 = 110;
        private const int S_F24 = 118;
        private const int S_NUMLOCK = 69;
        private const int S_SCROLL = 70;
        private const int S_LeftShift = 42;
        private const int S_RightShift = 54;
        private const int S_LeftControl = 29;
        private const int S_RightControl = 29;
        private const int S_LMENU = 56;
        private const int S_RMENU = 56;
        private const int S_BROWSER_BACK = 106;
        private const int S_BROWSER_FORWARD = 105;
        private const int S_BROWSER_REFRESH = 103;
        private const int S_BROWSER_STOP = 104;
        private const int S_BROWSER_SEARCH = 101;
        private const int S_BROWSER_FAVORITES = 102;
        private const int S_BROWSER_HOME = 50;
        private const int S_VOLUME_MUTE = 32;
        private const int S_VOLUME_DOWN = 46;
        private const int S_VOLUME_UP = 48;
        private const int S_MEDIA_NEXT_TRACK = 25;
        private const int S_MEDIA_PREV_TRACK = 16;
        private const int S_MEDIA_STOP = 36;
        private const int S_MEDIA_PLAY_PAUSE = 34;
        private const int S_LAUNCH_MAIL = 108;
        private const int S_LAUNCH_MEDIA_SELECT = 109;
        private const int S_LAUNCH_APP1 = 107;
        private const int S_LAUNCH_APP2 = 33;
        private const int S_OEM_1 = 27;
        private const int S_OEM_PLUS = 13;
        private const int S_OEM_COMMA = 50;
        private const int S_OEM_MINUS = 0;
        private const int S_OEM_PERIOD = 51;
        private const int S_OEM_2 = 52;
        private const int S_OEM_3 = 40;
        private const int S_OEM_4 = 12;
        private const int S_OEM_5 = 43;
        private const int S_OEM_6 = 26;
        private const int S_OEM_7 = 41;
        private const int S_OEM_8 = 53;
        private const int S_OEM_102 = 86;
        private const int S_PROCESSKEY = 0;
        private const int S_PACKET = 0;
        private const int S_ATTN = 0;
        private const int S_CRSEL = 0;
        private const int S_EXSEL = 0;
        private const int S_EREOF = 93;
        private const int S_PLAY = 0;
        private const int S_ZOOM = 98;
        private const int S_NONAME = 0;
        private const int S_PA1 = 0;
        private const int S_OEM_CLEAR = 0;
        private bool Key_LBUTTON;
        private bool Key_RBUTTON;
        private bool Key_CANCEL;
        private bool Key_MBUTTON;
        private bool Key_XBUTTON1;
        private bool Key_XBUTTON2;
        private bool Key_BACK;
        private bool Key_Tab;
        private bool Key_CLEAR;
        private bool Key_Return;
        private bool Key_SHIFT;
        private bool Key_CONTROL;
        private bool Key_MENU;
        private bool Key_PAUSE;
        private bool Key_CAPITAL;
        private bool Key_KANA;
        private bool Key_HANGEUL;
        private bool Key_HANGUL;
        private bool Key_JUNJA;
        private bool Key_FINAL;
        private bool Key_HANJA;
        private bool Key_KANJI;
        private bool Key_Escape;
        private bool Key_CONVERT;
        private bool Key_NONCONVERT;
        private bool Key_ACCEPT;
        private bool Key_MODECHANGE;
        private bool Key_Space;
        private bool Key_PRIOR;
        private bool Key_NEXT;
        private bool Key_END;
        private bool Key_HOME;
        private bool Key_LEFT;
        private bool Key_UP;
        private bool Key_RIGHT;
        private bool Key_DOWN;
        private bool Key_SELECT;
        private bool Key_PRINT;
        private bool Key_EXECUTE;
        private bool Key_SNAPSHOT;
        private bool Key_INSERT;
        private bool Key_DELETE;
        private bool Key_HELP;
        private bool Key_APOSTROPHE;
        private bool Key_0;
        private bool Key_1;
        private bool Key_2;
        private bool Key_3;
        private bool Key_4;
        private bool Key_5;
        private bool Key_6;
        private bool Key_7;
        private bool Key_8;
        private bool Key_9;
        private bool Key_A;
        private bool Key_B;
        private bool Key_C;
        private bool Key_D;
        private bool Key_E;
        private bool Key_F;
        private bool Key_G;
        private bool Key_H;
        private bool Key_I;
        private bool Key_J;
        private bool Key_K;
        private bool Key_L;
        private bool Key_M;
        private bool Key_N;
        private bool Key_O;
        private bool Key_P;
        private bool Key_Q;
        private bool Key_R;
        private bool Key_S;
        private bool Key_T;
        private bool Key_U;
        private bool Key_V;
        private bool Key_W;
        private bool Key_X;
        private bool Key_Y;
        private bool Key_Z;
        private bool Key_LWIN;
        private bool Key_RWIN;
        private bool Key_APPS;
        private bool Key_SLEEP;
        private bool Key_NUMPAD0;
        private bool Key_NUMPAD1;
        private bool Key_NUMPAD2;
        private bool Key_NUMPAD3;
        private bool Key_NUMPAD4;
        private bool Key_NUMPAD5;
        private bool Key_NUMPAD6;
        private bool Key_NUMPAD7;
        private bool Key_NUMPAD8;
        private bool Key_NUMPAD9;
        private bool Key_MULTIPLY;
        private bool Key_ADD;
        private bool Key_SEPARATOR;
        private bool Key_SUBTRACT;
        private bool Key_DECIMAL;
        private bool Key_DIVIDE;
        private bool Key_F1;
        private bool Key_F2;
        private bool Key_F3;
        private bool Key_F4;
        private bool Key_F5;
        private bool Key_F6;
        private bool Key_F7;
        private bool Key_F8;
        private bool Key_F9;
        private bool Key_F10;
        private bool Key_F11;
        private bool Key_F12;
        private bool Key_F13;
        private bool Key_F14;
        private bool Key_F15;
        private bool Key_F16;
        private bool Key_F17;
        private bool Key_F18;
        private bool Key_F19;
        private bool Key_F20;
        private bool Key_F21;
        private bool Key_F22;
        private bool Key_F23;
        private bool Key_F24;
        private bool Key_NUMLOCK;
        private bool Key_SCROLL;
        private bool Key_LeftShift;
        private bool Key_RightShift;
        private bool Key_LeftControl;
        private bool Key_RightControl;
        private bool Key_LMENU;
        private bool Key_RMENU;
        private bool Key_BROWSER_BACK;
        private bool Key_BROWSER_FORWARD;
        private bool Key_BROWSER_REFRESH;
        private bool Key_BROWSER_STOP;
        private bool Key_BROWSER_SEARCH;
        private bool Key_BROWSER_FAVORITES;
        private bool Key_BROWSER_HOME;
        private bool Key_VOLUME_MUTE;
        private bool Key_VOLUME_DOWN;
        private bool Key_VOLUME_UP;
        private bool Key_MEDIA_NEXT_TRACK;
        private bool Key_MEDIA_PREV_TRACK;
        private bool Key_MEDIA_STOP;
        private bool Key_MEDIA_PLAY_PAUSE;
        private bool Key_LAUNCH_MAIL;
        private bool Key_LAUNCH_MEDIA_SELECT;
        private bool Key_LAUNCH_APP1;
        private bool Key_LAUNCH_APP2;
        private bool Key_OEM_1;
        private bool Key_OEM_PLUS;
        private bool Key_OEM_COMMA;
        private bool Key_OEM_MINUS;
        private bool Key_OEM_PERIOD;
        private bool Key_OEM_2;
        private bool Key_OEM_3;
        private bool Key_OEM_4;
        private bool Key_OEM_5;
        private bool Key_OEM_6;
        private bool Key_OEM_7;
        private bool Key_OEM_8;
        private bool Key_OEM_102;
        private bool Key_PROCESSKEY;
        private bool Key_PACKET;
        private bool Key_ATTN;
        private bool Key_CRSEL;
        private bool Key_EXSEL;
        private bool Key_EREOF;
        private bool Key_PLAY;
        private bool Key_ZOOM;
        private bool Key_NONAME;
        private bool Key_PA1;
        private bool Key_OEM_CLEAR;
        public void CloseControl()
        {
            if (!AlreadyRunning())
            {
                try
                {
                    keyboardHook.Hook -= new KeyboardHook.KeyboardHookCallback(KeyboardHook_Hook);
                    keyboardHook.Uninstall();
                }
                catch { }
            }
        }
        public void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseControl();
        }
    }
    class KeyboardHook
    {
        public static bool KeyboardHookButtonDown, KeyboardHookButtonUp;
        public delegate IntPtr KeyboardHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
        public KeyboardHookHandler hookHandler;
        public KBDLLHOOKSTRUCT keyboardStruct;
        public delegate void KeyboardHookCallback(KBDLLHOOKSTRUCT keyboardStruct);
        public event KeyboardHookCallback Hook;
        public IntPtr hookID = IntPtr.Zero;
        public static int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        public static int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        public void Install()
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }
        public void Uninstall()
        {
            if (hookID == IntPtr.Zero)
                return;
            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;
        }
        ~KeyboardHook()
        {
            Uninstall();
        }
        public IntPtr SetHook(KeyboardHookHandler proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }
        public IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            keyboardStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
            if (KeyboardHook.KeyboardMessages.WM_KEYDOWN == (KeyboardHook.KeyboardMessages)wParam)
                KeyboardHookButtonDown = true;
            else
                KeyboardHookButtonDown = false;
            if (KeyboardHook.KeyboardMessages.WM_KEYUP == (KeyboardHook.KeyboardMessages)wParam)
                KeyboardHookButtonUp = true;
            else
                KeyboardHookButtonUp = false;
            Form1.KeyboardHookButtonDown = KeyboardHookButtonDown;
            Form1.KeyboardHookButtonUp = KeyboardHookButtonUp;
            Form1.vkCode = (int)keyboardStruct.vkCode;
            Form1.scanCode = (int)keyboardStruct.scanCode;
            Form1.wParam = wParam;
            Form1.lParam = lParam;
            Hook((KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT)));
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }

        public const int WH_KEYBOARD_LL = 13;
        public enum KeyboardMessages
        {
            WM_ACTIVATE = 0x0006,
            WM_APPCOMMAND = 0x0319,
            WM_CHAR = 0x0102,
            WM_DEADCHAR = 0x010,
            WM_HOTKEY = 0x0312,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_KILLFOCUS = 0x0008,
            WM_SETFOCUS = 0x0007,
            WM_SYSDEADCHAR = 0x0107,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
            WM_UNICHAR = 0x0109
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, KeyboardHookHandler lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
