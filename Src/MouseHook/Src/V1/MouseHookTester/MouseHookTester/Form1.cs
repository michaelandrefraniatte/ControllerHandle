using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Drawing;
using System.Linq;
using System.Numerics;
namespace MouseHookTester
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
        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out int x, out int y);
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int X, int Y);
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        public delegate bool ConsoleEventDelegate(int eventType);
        MouseHook mouseHook = new MouseHook();
        public static IntPtr lParam, wParam;
        private static double mousexp, mouseyp;
        public static int width = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        public static int height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        public static int MouseHookX, MouseHookY, MouseHookWheel, MouseHookButtonX, MouseDesktopHookX, MouseDesktopHookY, irx, iry, gyrx, gyry, oldMouseDesktopHookX, oldMouseDesktopHookY, newMouseDesktopHookX, newMouseDesktopHookY, actualMouseDesktopHookX, actualMouseDesktopHookY, mousehookwheelcount, mousehookbuttoncount, mousex, mousey, MouseHookTime;
        public static bool MouseHookLeftButton, MouseHookRightButton, MouseHookLeftDoubleClick, MouseHookRightDoubleClick, MouseHookMiddleButton, MouseHookXButton, mousehookwheelbool, mousehookbuttonbool;
        public static System.Collections.Generic.List<int> time = new System.Collections.Generic.List<int>(), relx = new System.Collections.Generic.List<int>(), rely = new System.Collections.Generic.List<int>();
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
                    mouseHook.Hook += new MouseHook.MouseHookCallback(MouseHook_Hook);
                    mouseHook.Install();
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
            Process[] processes = Process.GetProcessesByName("MouseHookTester");
            if (processes.Length > 1)
                return true;
            else
                return false;
        }
        public void taskX()
        {
            for (; ; )
            {
                SetCursorPos(irx <= 0 ? 0 : width, iry <= 0 ? 0 : height);
                if (MouseHookWheel != 0)
                    mousehookwheelbool = true;
                if (mousehookwheelbool)
                    mousehookwheelcount++;
                if (mousehookwheelcount >= 60)
                {
                    mousehookwheelbool = false;
                    MouseHook.MouseHookWheel = 0;
                    MouseHookWheel = 0;
                    mousehookwheelcount = 0;
                }
                if (MouseHookButtonX != 0)
                    mousehookbuttonbool = true;
                if (mousehookbuttonbool)
                    mousehookbuttoncount++;
                if (mousehookbuttoncount >= 60)
                {
                    mousehookbuttonbool = false;
                    MouseHook.MouseHookButtonX = 0;
                    MouseHookButtonX = 0;
                    mousehookbuttoncount = 0;
                }
                if (time.Count() <= 30)
                    time.Add(MouseHookTime + irx + iry);
                else 
                { 
                    time.Add(MouseHookTime + irx + iry); 
                    time.RemoveAt(0);
                }
                if (time.All(x => x == time.First()))
                {
                    mousex = 0;
                    mousey = 0;
                    MouseHookX = 0;
                    MouseHookY = 0;
                }
                else
                {
                    mousex = irx;
                    mousey = iry;
                }
                if (MouseHookWheel != 0)
                    mousehookwheelcount++;
                else
                    mousehookwheelcount = 0;
                if (mousehookwheelcount >= 60)
                {
                    MouseHookWheel = 0;
                    mousehookwheelcount = 0;
                }
                if (MouseHookButtonX != 0)
                    mousehookbuttoncount++;
                else
                    mousehookbuttoncount = 0;
                if (mousehookbuttoncount >= 60)
                {
                    MouseHookButtonX = 0;
                    mousehookbuttoncount = 0;
                }
                if (relx.Count() < 50)
                    relx.Add(irx);
                else
                {
                    relx.Add(irx);
                    relx.RemoveAt(0);
                }
                if (rely.Count() < 50)
                    rely.Add(iry);
                else
                {
                    rely.Add(iry);
                    rely.RemoveAt(0);
                }
                gyrx = relx.Last() - relx.First();
                gyry = rely.Last() - rely.First();
                mousexp += mousex;
                mouseyp += mousey;
                Point dPoint = new Point(mousex, mousey);
                string str = "mouse : " + dPoint + Environment.NewLine;
                dPoint = new Point(MouseHookX, MouseHookY);
                str += "MouseHook : " + dPoint + Environment.NewLine;
                dPoint = new Point(MouseDesktopHookX, MouseDesktopHookY);
                str += "MouseDesktopHook : " + dPoint + Environment.NewLine;
                dPoint = new Point(irx, iry);
                str += "ir : " + dPoint + Environment.NewLine;
                dPoint = new Point((int)(mousexp / 30f + mousex / 3f), (int)(mouseyp / 30f + mousey / 3f));
                str += "mousep : " + dPoint + Environment.NewLine;
                dPoint = new Point(gyrx, gyry);
                str += "gyr : " + dPoint + Environment.NewLine;
                dPoint = new Point((int)(EulerAnglesbRight.X * 1024f * 300f), (int)(EulerAnglesbRightVer.X * 1024f * 300f));
                str += "EulerAnglesRight : " + dPoint + Environment.NewLine;
                str += "MouseHookRightButton : " + MouseHookRightButton + Environment.NewLine;
                str += "MouseHookLeftButton : " + MouseHookLeftButton + Environment.NewLine;
                str += "MouseHookMiddleButton : " + MouseHookMiddleButton + Environment.NewLine;
                str += "MouseHookXButton : " + MouseHookXButton + Environment.NewLine;
                str += "MouseHookLeftDoubleClick : " + MouseHookLeftDoubleClick + Environment.NewLine;
                str += "MouseHookRightDoubleClick : " + MouseHookRightDoubleClick + Environment.NewLine;
                str += "MouseHookWheel : " + MouseHookWheel + Environment.NewLine;
                str += "MouseHookButtonX : " + MouseHookButtonX + Environment.NewLine;
                str += "MouseHookTime : " + MouseHookTime + Environment.NewLine;
                this.label1.Text = str;
                Thread.Sleep(1);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ExtractIMUValuesRight();
            }
            catch { }
        }
        private void MouseHook_Hook(MouseHook.MSLLHOOKSTRUCT mouseStruct) { }
        public static double Scale(double value, double min, double max, double minScale, double maxScale)
        {
            double scaled = minScale + (double)(value - min) / (max - min) * (maxScale - minScale);
            return scaled;
        }
        public void CloseControl()
        {
            if (!AlreadyRunning())
            {
                try
                {
                    mouseHook.Hook -= new MouseHook.MouseHookCallback(MouseHook_Hook);
                    mouseHook.Uninstall();
                }
                catch { }
            }
        }
        public void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            CloseControl();
        }
        private static Quaternion GetVectorbRight()
        {
            Vector3 v1 = new Vector3(j_bRight.X, i_bRight.X, k_bRight.X);
            Vector3 v2 = -(new Vector3(j_bRight.Z, i_bRight.Z, k_bRight.Z));
            return QuaternionLookRotationRight(v1, v2);
        }
        private static Quaternion QuaternionLookRotationRight(Vector3 forward, Vector3 up)
        {
            Vector3 vector = Vector3.Normalize(forward);
            Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            var m00 = vector2.X;
            var m01 = vector2.Y;
            var m02 = vector2.Z;
            var m10 = vector3.X;
            var m11 = vector3.Y;
            var m12 = vector3.Z;
            var m20 = vector.X;
            var m21 = vector.Y;
            var m22 = vector.Z;
            double num8 = (m00 + m11) + m22;
            var quaternion = new Quaternion();
            if (num8 > 0f)
            {
                var num = (double)Math.Sqrt(num8 + 1f);
                quaternion.W = (float)num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (float)(m12 - m21) * (float)num;
                quaternion.Y = (float)(m20 - m02) * (float)num;
                quaternion.Z = (float)(m01 - m10) * (float)num;
                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                var num7 = (double)Math.Sqrt(((1f + m00) - m11) - m22);
                var num4 = 0.5f / num7;
                quaternion.X = 0.5f * (float)num7;
                quaternion.Y = (float)(m01 + m10) * (float)num4;
                quaternion.Z = (float)(m02 + m20) * (float)num4;
                quaternion.W = (float)(m12 - m21) * (float)num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                var num6 = (double)Math.Sqrt(((1f + m11) - m00) - m22);
                var num3 = 0.5f / num6;
                quaternion.X = (float)(m10 + m01) * (float)num3;
                quaternion.Y = 0.5f * (float)num6;
                quaternion.Z = (float)(m21 + m12) * (float)num3;
                quaternion.W = (float)(m20 - m02) * (float)num3;
                return quaternion;
            }
            var num5 = (double)Math.Sqrt(((1f + m22) - m00) - m11);
            var num2 = 0.5f / num5;
            quaternion.X = (float)(m20 + m02) * (float)num2;
            quaternion.Y = (float)(m21 + m12) * (float)num2;
            quaternion.Z = 0.5f * (float)num5;
            quaternion.W = (float)(m01 - m10) * (float)num2;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesRight(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            if (test > 0.4999f * unit)
            {
                pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W);
                pitchYawRoll.X = (float)Math.PI * 0.5f;
                pitchYawRoll.Z = 0f;
                return pitchYawRoll;
            }
            else if (test < -0.4999f * unit)
            {
                pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W);
                pitchYawRoll.X = -(float)Math.PI * 0.5f;
                pitchYawRoll.Z = 0f;
                return pitchYawRoll;
            }
            else
            {
                pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
                pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
                pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            }
            return pitchYawRoll;
        }
        private static Quaternion GetVectorbRightVer()
        {
            Vector3 v1 = new Vector3(j_bRightVer.X, i_bRightVer.X, k_bRightVer.X);
            Vector3 v2 = -(new Vector3(j_bRightVer.Z, i_bRightVer.Z, k_bRightVer.Z));
            return QuaternionLookRotationRightVer(v1, v2);
        }
        private static Quaternion QuaternionLookRotationRightVer(Vector3 forward, Vector3 up)
        {
            Vector3 vector = Vector3.Normalize(forward);
            Vector3 vector2 = Vector3.Normalize(Vector3.Cross(up, vector));
            Vector3 vector3 = Vector3.Cross(vector, vector2);
            var m00 = vector2.X;
            var m01 = vector2.Y;
            var m02 = vector2.Z;
            var m10 = vector3.X;
            var m11 = vector3.Y;
            var m12 = vector3.Z;
            var m20 = vector.X;
            var m21 = vector.Y;
            var m22 = vector.Z;
            double num8 = (m00 + m11) + m22;
            var quaternion = new Quaternion();
            if (num8 > 0f)
            {
                var num = (double)Math.Sqrt(num8 + 1f);
                quaternion.W = (float)num * 0.5f;
                num = 0.5f / num;
                quaternion.X = (float)(m12 - m21) * (float)num;
                quaternion.Y = (float)(m20 - m02) * (float)num;
                quaternion.Z = (float)(m01 - m10) * (float)num;
                return quaternion;
            }
            if ((m00 >= m11) && (m00 >= m22))
            {
                var num7 = (double)Math.Sqrt(((1f + m00) - m11) - m22);
                var num4 = 0.5f / num7;
                quaternion.X = 0.5f * (float)num7;
                quaternion.Y = (float)(m01 + m10) * (float)num4;
                quaternion.Z = (float)(m02 + m20) * (float)num4;
                quaternion.W = (float)(m12 - m21) * (float)num4;
                return quaternion;
            }
            if (m11 > m22)
            {
                var num6 = (double)Math.Sqrt(((1f + m11) - m00) - m22);
                var num3 = 0.5f / num6;
                quaternion.X = (float)(m10 + m01) * (float)num3;
                quaternion.Y = 0.5f * (float)num6;
                quaternion.Z = (float)(m21 + m12) * (float)num3;
                quaternion.W = (float)(m20 - m02) * (float)num3;
                return quaternion;
            }
            var num5 = (double)Math.Sqrt(((1f + m22) - m00) - m11);
            var num2 = 0.5f / num5;
            quaternion.X = (float)(m20 + m02) * (float)num2;
            quaternion.Y = (float)(m21 + m12) * (float)num2;
            quaternion.Z = 0.5f * (float)num5;
            quaternion.W = (float)(m01 - m10) * (float)num2;
            return quaternion;
        }
        private static Vector3 ToEulerAnglesRightVer(Quaternion q)
        {
            Vector3 pitchYawRoll = new Vector3();
            double sqw = q.W * q.W;
            double sqx = q.X * q.X;
            double sqy = q.Y * q.Y;
            double sqz = q.Z * q.Z;
            double unit = sqx + sqy + sqz + sqw;
            double test = q.X * q.Y + q.Z * q.W;
            if (test > 0.4999f * unit)
            {
                pitchYawRoll.Y = 2f * (float)Math.Atan2(q.X, q.W);
                pitchYawRoll.X = (float)Math.PI * 0.5f;
                pitchYawRoll.Z = 0f;
                return pitchYawRoll;
            }
            else if (test < -0.4999f * unit)
            {
                pitchYawRoll.Y = -2f * (float)Math.Atan2(q.X, q.W);
                pitchYawRoll.X = -(float)Math.PI * 0.5f;
                pitchYawRoll.Z = 0f;
                return pitchYawRoll;
            }
            else
            {
                pitchYawRoll.Y = (float)Math.Atan2(2f * q.Y * q.W - 2f * q.X * q.Z, sqx - sqy - sqz + sqw);
                pitchYawRoll.X = (float)Math.Asin(2f * test / unit);
                pitchYawRoll.Z = (float)Math.Atan2(2f * q.X * q.W - 2f * q.Y * q.Z, -sqx + sqy - sqz + sqw);
            }
            return pitchYawRoll;
        }
        private static void ExtractIMUValuesRight()
        {
            gyr_gRight.X = 0 * (1.0f / 4000000f);
            gyr_gRight.Y = 0 * (1.0f / 4000000f);
            gyr_gRight.Z = gyrx * (1.0f / 4000000f);
            i_bRight = new Vector3(1, 0, 0);
            k_bRight = new Vector3(0, 0, 1);
            j_bRight.Y = 1f;
            j_bRight.Z = 0f;
            if (MouseHookWheel > 0 | j_bRight.X == 0f | EulerAnglesbRight.X == 0f)
            {
                j_bRight = new Vector3(0, 1, 0);
                InitEulerAnglesbRight = ToEulerAnglesRight(GetVectorbRight());
            }
            j_bRight += Vector3.Cross(gyr_gRight, j_bRight);
            EulerAnglesbRight = ToEulerAnglesRight(GetVectorbRight()) - InitEulerAnglesbRight;
            gyr_gRightVer.X = 0 * (1.0f / 4000000f);
            gyr_gRightVer.Y = 0 * (1.0f / 4000000f);
            gyr_gRightVer.Z = gyry * (1.0f / 4000000f);
            i_bRightVer = new Vector3(1, 0, 0);
            k_bRightVer = new Vector3(0, 0, 1);
            j_bRightVer.Y = 1f;
            j_bRightVer.Z = 0f;
            if (MouseHookWheel > 0 | j_bRightVer.X == 0f | EulerAnglesbRightVer.X == 0f)
            {
                j_bRightVer = new Vector3(0, 1, 0);
                InitEulerAnglesbRightVer = ToEulerAnglesRightVer(GetVectorbRightVer());
            }
            j_bRightVer += Vector3.Cross(gyr_gRightVer, j_bRightVer);
            EulerAnglesbRightVer = ToEulerAnglesRightVer(GetVectorbRightVer()) - InitEulerAnglesbRightVer;
        }
        private static Vector3 gyr_gRightHor = new Vector3();
        private static Vector3 i_cRightHor = new Vector3(1, 0, 0);
        private static Vector3 j_cRightHor = new Vector3(0, 1, 0);
        private static Vector3 k_cRightHor = new Vector3(0, 0, 1);
        private static Vector3 i_bRightHor = new Vector3(1, 0, 0);
        private static Vector3 j_bRightHor = new Vector3(0, 1, 0);
        private static Vector3 k_bRightHor = new Vector3(0, 0, 1);
        private static Vector3 i_aRightHor = new Vector3(1, 0, 0);
        private static Vector3 j_aRightHor = new Vector3(0, 1, 0);
        private static Vector3 k_aRightHor = new Vector3(0, 0, 1);
        private static Vector3 InitEulerAnglesaRightHor, EulerAnglesaRightHor, InitEulerAnglesbRightHor, EulerAnglesbRightHor, InitEulerAnglescRightHor, EulerAnglescRightHor, EulerAnglesRightHor;
        private static Vector3 gyr_gRightVer = new Vector3();
        private static Vector3 i_cRightVer = new Vector3(1, 0, 0);
        private static Vector3 j_cRightVer = new Vector3(0, 1, 0);
        private static Vector3 k_cRightVer = new Vector3(0, 0, 1);
        private static Vector3 i_bRightVer = new Vector3(1, 0, 0);
        private static Vector3 j_bRightVer = new Vector3(0, 1, 0);
        private static Vector3 k_bRightVer = new Vector3(0, 0, 1);
        private static Vector3 i_aRightVer = new Vector3(1, 0, 0);
        private static Vector3 j_aRightVer = new Vector3(0, 1, 0);
        private static Vector3 k_aRightVer = new Vector3(0, 0, 1);
        private static Vector3 InitEulerAnglesaRightVer, EulerAnglesaRightVer, InitEulerAnglesbRightVer, EulerAnglesbRightVer, InitEulerAnglescRightVer, EulerAnglescRightVer, EulerAnglesRightVer;
        private static double[] stickRight = { 0, 0 };
        private static byte[] stick_rawRight = { 0, 0, 0 };
        private static UInt16[] stick_calibrationRight = { 0, 0 };
        private static UInt16[] stick_precalRight = { 0, 0 };
        private static Vector3 acc_gRight = new Vector3();
        private static Vector3 gyr_gRight = new Vector3();
        private const uint report_lenRight = 25;
        private static Vector3 i_cRight = new Vector3(1, 0, 0);
        private static Vector3 j_cRight = new Vector3(0, 1, 0);
        private static Vector3 k_cRight = new Vector3(0, 0, 1);
        private static Vector3 i_bRight = new Vector3(1, 0, 0);
        private static Vector3 j_bRight = new Vector3(0, 1, 0);
        private static Vector3 k_bRight = new Vector3(0, 0, 1);
        private static Vector3 i_aRight = new Vector3(1, 0, 0);
        private static Vector3 j_aRight = new Vector3(0, 1, 0);
        private static Vector3 k_aRight = new Vector3(0, 0, 1);
        private static Vector3 InitDirectAnglesRight, DirectAnglesRight;
        private static Vector3 InitEulerAnglesaRight, EulerAnglesaRight, InitEulerAnglesbRight, EulerAnglesbRight, InitEulerAnglescRight, EulerAnglescRight, EulerAnglesRight;
        private static bool RightButtonSHOULDER_1, RightButtonSHOULDER_2, RightButtonSR, RightButtonSL, RightButtonDPAD_DOWN, RightButtonDPAD_RIGHT, RightButtonDPAD_UP, RightButtonDPAD_LEFT, RightButtonPLUS, RightButtonSTICK, RightButtonHOME, ISRIGHT;
        private static byte[] report_bufRight = new byte[report_lenRight];
        private static byte[] buf_Right = new byte[report_lenRight];
        private static float acc_gcalibrationRightX, acc_gcalibrationRightY, acc_gcalibrationRightZ, gyr_gcalibrationRightX, gyr_gcalibrationRightY, gyr_gcalibrationRightZ;
        private static double[] GetStickRight()
        {
            return stickRight;
        }
    }
    class MouseHook
    {
        public static int MouseHookX, MouseHookY, MouseHookWheel, MouseHookButtonX, MouseDesktopHookX, MouseDesktopHookY, irx, iry, MouseHookTime;
        public static bool MouseHookLeftButton, MouseHookRightButton, MouseHookLeftDoubleClick, MouseHookRightDoubleClick, MouseHookMiddleButton, MouseHookXButton;
        public delegate IntPtr MouseHookHandler(int nCode, IntPtr wParam, IntPtr lParam);
        public MouseHookHandler hookHandler;
        public MSLLHOOKSTRUCT mouseStruct;
        public delegate void MouseHookCallback(MSLLHOOKSTRUCT mouseStruct);
        public event MouseHookCallback Hook;
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
        ~MouseHook()
        {
            Uninstall();
        }
        public IntPtr SetHook(MouseHookHandler proc)
        {
            using (ProcessModule module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }
        public IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            mouseStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
            if (MouseHook.MouseMessages.WM_RBUTTONDOWN == (MouseHook.MouseMessages)wParam)
                MouseHookRightButton = true;
            if (MouseHook.MouseMessages.WM_RBUTTONUP == (MouseHook.MouseMessages)wParam)
                MouseHookRightButton = false;
            if (MouseHook.MouseMessages.WM_LBUTTONDOWN == (MouseHook.MouseMessages)wParam)
                MouseHookLeftButton = true;
            if (MouseHook.MouseMessages.WM_LBUTTONUP == (MouseHook.MouseMessages)wParam)
                MouseHookLeftButton = false;
            if (MouseHook.MouseMessages.WM_MBUTTONDOWN == (MouseHook.MouseMessages)wParam)
                MouseHookMiddleButton = true;
            if (MouseHook.MouseMessages.WM_MBUTTONUP == (MouseHook.MouseMessages)wParam)
                MouseHookMiddleButton = false;
            if (MouseHook.MouseMessages.WM_XBUTTONDOWN == (MouseHook.MouseMessages)wParam)
                MouseHookXButton = true;
            if (MouseHook.MouseMessages.WM_XBUTTONUP == (MouseHook.MouseMessages)wParam)
                MouseHookXButton = false;
            if (MouseHook.MouseMessages.WM_LBUTTONDBLCLK == (MouseHook.MouseMessages)wParam)
                MouseHookLeftDoubleClick = true;
            else
                MouseHookLeftDoubleClick = false;
            if (MouseHook.MouseMessages.WM_RBUTTONDBLCLK == (MouseHook.MouseMessages)wParam)
                MouseHookRightDoubleClick = true;
            else
                MouseHookRightDoubleClick = false;
            if (MouseHook.MouseMessages.WM_MOUSEWHEEL == (MouseHook.MouseMessages)wParam)
                MouseHookWheel = (int)mouseStruct.mouseData; // 7864320, -7864320
            else
                MouseHookWheel = 0;
            if (MouseHook.MouseMessages.WM_XBUTTONDOWN == (MouseHook.MouseMessages)wParam)
                MouseHookButtonX = (int)mouseStruct.mouseData; //131072, 65536
            else
                MouseHookButtonX = 0;
            GetCursorPos(out MouseDesktopHookX, out MouseDesktopHookY);
            MouseHookX = mouseStruct.pt.x;
            MouseHookY = mouseStruct.pt.y;
            irx = (MouseHookX - MouseDesktopHookX) * 15;
            iry = (MouseHookY - MouseDesktopHookY) * 30;
            Form1.irx = irx;
            Form1.iry = iry;
            MouseHookTime = (int)mouseStruct.time;
            Form1.MouseHookTime = MouseHookTime;
            Form1.MouseHookRightButton = MouseHookRightButton;
            Form1.MouseHookLeftButton = MouseHookLeftButton;
            Form1.MouseHookMiddleButton = MouseHookMiddleButton;
            Form1.MouseHookXButton = MouseHookXButton;
            Form1.MouseHookLeftDoubleClick = MouseHookLeftDoubleClick;
            Form1.MouseHookRightDoubleClick = MouseHookRightDoubleClick;
            Form1.MouseHookWheel = MouseHookWheel;
            Form1.MouseHookButtonX = MouseHookButtonX;
            Form1.MouseHookX = MouseHookX;
            Form1.MouseHookY = MouseHookY;
            Form1.MouseDesktopHookX = MouseDesktopHookX;
            Form1.MouseDesktopHookY = MouseDesktopHookY;
            Form1.wParam = wParam;
            Form1.lParam = lParam;
            Hook((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
        public const int WH_MOUSE_LL = 14;
        public enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_XBUTTONDOWN = 0x020B,
            WM_XBUTTONUP = 0x020C
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandler lpfn, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("User32.dll")]
        public static extern bool GetCursorPos(out int x, out int y);
        [DllImport("user32.dll")]
        public static extern void SetCursorPos(int X, int Y);
    }
}
