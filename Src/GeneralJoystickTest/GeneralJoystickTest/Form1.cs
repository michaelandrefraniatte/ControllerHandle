using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.DirectX.DirectInput;

namespace GeneralJoystickTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static int JoystickAxisRx, JoystickAxisRy, JoystickAxisRz, JoystickAxisX, JoystickAxisY, JoystickAxisZ, JoystickAxisExtraX, JoystickAxisExtraY, JoystickPointOfView0, JoystickPointOfView1;
        public static bool JoystickButtons0, JoystickButtons1, JoystickButtons2, JoystickButtons3, JoystickButtons4, JoystickButtons5, JoystickButtons6, JoystickButtons7, JoystickButtons8, JoystickButtons9, JoystickButtons10, JoystickButtons11, JoystickButtons12, JoystickButtons13, JoystickButtons14, JoystickButtons15, JoystickButtons16, JoystickButtons17, JoystickButtons18, JoystickButtons19, JoystickButtons20, JoystickButtons21, JoystickButtons22, JoystickButtons23, JoystickButtons24, JoystickButtons25, JoystickButtons26, JoystickButtons27, JoystickButtons28, JoystickButtons29, JoystickButtons30, JoystickButtons31, JoystickButtons32, JoystickButtons33, JoystickButtons34, JoystickButtons35, JoystickButtons36, JoystickButtons37, JoystickButtons38, JoystickButtons39, JoystickButtons40, JoystickButtons41, JoystickButtons42, JoystickButtons43, JoystickButtons44, JoystickButtons45, JoystickButtons46, JoystickButtons47, JoystickButtons48, JoystickButtons49, JoystickButtons50, JoystickButtons51, JoystickButtons52, JoystickButtons53, JoystickButtons54, JoystickButtons55, JoystickButtons56, JoystickButtons57, JoystickButtons58, JoystickButtons59, JoystickButtons60, JoystickButtons61, JoystickButtons62, JoystickButtons63, JoystickButtons64, JoystickButtons65, JoystickButtons66, JoystickButtons67, JoystickButtons68, JoystickButtons69, JoystickButtons70, JoystickButtons71, JoystickButtons72, JoystickButtons73, JoystickButtons74, JoystickButtons75, JoystickButtons76, JoystickButtons77, JoystickButtons78, JoystickButtons79, JoystickButtons80, JoystickButtons81, JoystickButtons82, JoystickButtons83, JoystickButtons84, JoystickButtons85, JoystickButtons86, JoystickButtons87, JoystickButtons88, JoystickButtons89, JoystickButtons90, JoystickButtons91, JoystickButtons92, JoystickButtons93, JoystickButtons94, JoystickButtons95, JoystickButtons96, JoystickButtons97, JoystickButtons98, JoystickButtons99, JoystickButtons100, JoystickButtons101, JoystickButtons102, JoystickButtons103, JoystickButtons104, JoystickButtons105, JoystickButtons106, JoystickButtons107, JoystickButtons108, JoystickButtons109, JoystickButtons110, JoystickButtons111, JoystickButtons112, JoystickButtons113, JoystickButtons114, JoystickButtons115, JoystickButtons116, JoystickButtons117, JoystickButtons118, JoystickButtons119, JoystickButtons120, JoystickButtons121, JoystickButtons122, JoystickButtons123, JoystickButtons124, JoystickButtons125, JoystickButtons126, JoystickButtons127;
        private bool closed = false;
        private Device joystickDevice;
        private JoystickState state;
        private string[] systemJoysticks;
        private bool[] buttons;
        private byte[] jsButtons;
        private int[] extraAxis;
        private int[] pointofview;
        public string[] FindJoysticks()
        {
            systemJoysticks = null;
            try
            {
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                if (gameControllerList.Count > 0)
                {
                    systemJoysticks = new string[gameControllerList.Count];
                    int i = 0;
                    foreach (DeviceInstance deviceInstance in gameControllerList)
                    {
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        joystickDevice.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                        systemJoysticks[i] = joystickDevice.DeviceInformation.InstanceName;
                        i++;
                    }
                }
            }
            catch { }
            return systemJoysticks;
        }
        public bool AcquireJoystick(string name)
        {
            try
            {
                DeviceList gameControllerList = Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly);
                int i = 0;
                bool found = false;
                foreach (DeviceInstance deviceInstance in gameControllerList)
                {
                    if (deviceInstance.InstanceName == name)
                    {
                        found = true;
                        // create a device from this controller so we can retrieve info.
                        joystickDevice = new Device(deviceInstance.InstanceGuid);
                        joystickDevice.SetCooperativeLevel(this, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
                        break;
                    }
                    i++;
                }
                if (!found)
                    return false;
                joystickDevice.SetDataFormat(DeviceDataFormat.Joystick);
                joystickDevice.Acquire();
            }
            catch { }
            return true;
        }
        public void ReleaseJoystick()
        {
            joystickDevice.Unacquire();
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            string[] sticks = FindJoysticks();
            if (sticks.Length > 0)
            {
                AcquireJoystick(sticks[0]);
                Task.Run(() => taskEmulate());
            }
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                joystickDevice.Poll();
                state = joystickDevice.CurrentJoystickState;
                pointofview = state.GetPointOfView();
                extraAxis = state.GetSlider();
                jsButtons = state.GetButtons();
                buttons = new bool[jsButtons.Length];
                int i = 0;
                foreach (byte button in jsButtons)
                {
                    buttons[i] = button >= 128;
                    i++;
                }
                JoystickAxisRx = state.Rx;
                JoystickAxisRy = state.Ry;
                JoystickAxisRz = state.Rz;
                JoystickAxisX = state.X;
                JoystickAxisY = state.Y;
                JoystickAxisZ = state.Z;
                JoystickAxisExtraX = extraAxis[0];
                JoystickAxisExtraY = extraAxis[1];
                JoystickPointOfView0 = pointofview[0];
                JoystickPointOfView1 = pointofview[1];
                JoystickButtons0 = buttons[0];
                JoystickButtons1 = buttons[1];
                JoystickButtons2 = buttons[2];
                JoystickButtons3 = buttons[3];
                JoystickButtons4 = buttons[4];
                JoystickButtons5 = buttons[5];
                JoystickButtons6 = buttons[6];
                JoystickButtons7 = buttons[7];
                JoystickButtons8 = buttons[8];
                JoystickButtons9 = buttons[9];
                JoystickButtons10 = buttons[10];
                JoystickButtons11 = buttons[11];
                JoystickButtons12 = buttons[12];
                JoystickButtons13 = buttons[13];
                JoystickButtons14 = buttons[14];
                JoystickButtons15 = buttons[15];
                JoystickButtons16 = buttons[16];
                JoystickButtons17 = buttons[17];
                JoystickButtons18 = buttons[18];
                JoystickButtons19 = buttons[19];
                JoystickButtons20 = buttons[20];
                JoystickButtons21 = buttons[21];
                JoystickButtons22 = buttons[22];
                JoystickButtons23 = buttons[23];
                JoystickButtons24 = buttons[24];
                JoystickButtons25 = buttons[25];
                JoystickButtons26 = buttons[26];
                JoystickButtons27 = buttons[27];
                JoystickButtons28 = buttons[28];
                JoystickButtons29 = buttons[29];
                JoystickButtons30 = buttons[30];
                JoystickButtons31 = buttons[31];
                JoystickButtons32 = buttons[32];
                JoystickButtons33 = buttons[33];
                JoystickButtons34 = buttons[34];
                JoystickButtons35 = buttons[35];
                JoystickButtons36 = buttons[36];
                JoystickButtons37 = buttons[37];
                JoystickButtons38 = buttons[38];
                JoystickButtons39 = buttons[39];
                JoystickButtons40 = buttons[40];
                JoystickButtons41 = buttons[41];
                JoystickButtons42 = buttons[42];
                JoystickButtons43 = buttons[43];
                JoystickButtons44 = buttons[44];
                JoystickButtons45 = buttons[45];
                JoystickButtons46 = buttons[46];
                JoystickButtons47 = buttons[47];
                JoystickButtons48 = buttons[48];
                JoystickButtons49 = buttons[49];
                JoystickButtons50 = buttons[50];
                JoystickButtons51 = buttons[51];
                JoystickButtons52 = buttons[52];
                JoystickButtons53 = buttons[53];
                JoystickButtons54 = buttons[54];
                JoystickButtons55 = buttons[55];
                JoystickButtons56 = buttons[56];
                JoystickButtons57 = buttons[57];
                JoystickButtons58 = buttons[58];
                JoystickButtons59 = buttons[59];
                JoystickButtons60 = buttons[60];
                JoystickButtons61 = buttons[61];
                JoystickButtons62 = buttons[62];
                JoystickButtons63 = buttons[63];
                JoystickButtons64 = buttons[64];
                JoystickButtons65 = buttons[65];
                JoystickButtons66 = buttons[66];
                JoystickButtons67 = buttons[67];
                JoystickButtons68 = buttons[68];
                JoystickButtons69 = buttons[69];
                JoystickButtons70 = buttons[70];
                JoystickButtons71 = buttons[71];
                JoystickButtons72 = buttons[72];
                JoystickButtons73 = buttons[73];
                JoystickButtons74 = buttons[74];
                JoystickButtons75 = buttons[75];
                JoystickButtons76 = buttons[76];
                JoystickButtons77 = buttons[77];
                JoystickButtons78 = buttons[78];
                JoystickButtons79 = buttons[79];
                JoystickButtons80 = buttons[80];
                JoystickButtons81 = buttons[81];
                JoystickButtons82 = buttons[82];
                JoystickButtons83 = buttons[83];
                JoystickButtons84 = buttons[84];
                JoystickButtons85 = buttons[85];
                JoystickButtons86 = buttons[86];
                JoystickButtons87 = buttons[87];
                JoystickButtons88 = buttons[88];
                JoystickButtons89 = buttons[89];
                JoystickButtons90 = buttons[90];
                JoystickButtons91 = buttons[91];
                JoystickButtons92 = buttons[92];
                JoystickButtons93 = buttons[93];
                JoystickButtons94 = buttons[94];
                JoystickButtons95 = buttons[95];
                JoystickButtons96 = buttons[96];
                JoystickButtons97 = buttons[97];
                JoystickButtons98 = buttons[98];
                JoystickButtons99 = buttons[99];
                JoystickButtons100 = buttons[100];
                JoystickButtons101 = buttons[101];
                JoystickButtons102 = buttons[102];
                JoystickButtons103 = buttons[103];
                JoystickButtons104 = buttons[104];
                JoystickButtons105 = buttons[105];
                JoystickButtons106 = buttons[106];
                JoystickButtons107 = buttons[107];
                JoystickButtons108 = buttons[108];
                JoystickButtons109 = buttons[109];
                JoystickButtons110 = buttons[110];
                JoystickButtons111 = buttons[111];
                JoystickButtons112 = buttons[112];
                JoystickButtons113 = buttons[113];
                JoystickButtons114 = buttons[114];
                JoystickButtons115 = buttons[115];
                JoystickButtons116 = buttons[116];
                JoystickButtons117 = buttons[117];
                JoystickButtons118 = buttons[118];
                JoystickButtons119 = buttons[119];
                JoystickButtons120 = buttons[120];
                JoystickButtons121 = buttons[121];
                JoystickButtons122 = buttons[122];
                JoystickButtons123 = buttons[123];
                JoystickButtons124 = buttons[124];
                JoystickButtons125 = buttons[125];
                JoystickButtons126 = buttons[126];
                JoystickButtons127 = buttons[127];
                string data = "";
                data += "JoystickAxisRx " + JoystickAxisRx + Environment.NewLine;
                data += "JoystickAxisRy " + JoystickAxisRy + Environment.NewLine;
                data += "JoystickAxisRz " + JoystickAxisRz + Environment.NewLine;
                data += "JoystickAxisX " + JoystickAxisX + Environment.NewLine;
                data += "JoystickAxisY " + JoystickAxisY + Environment.NewLine;
                data += "JoystickAxisZ " + JoystickAxisZ + Environment.NewLine;
                data += "JoystickAxisExtraX " + JoystickAxisExtraX + Environment.NewLine;
                data += "JoystickAxisExtraY " + JoystickAxisExtraY + Environment.NewLine;
                data += "JoystickPointOfView0 " + JoystickPointOfView0 + Environment.NewLine;
                data += "JoystickPointOfView1 " + JoystickPointOfView1 + Environment.NewLine;
                data += "JoystickButtons0 " + JoystickButtons0 + Environment.NewLine;
                data += "JoystickButtons1 " + JoystickButtons1 + Environment.NewLine;
                data += "JoystickButtons2 " + JoystickButtons2 + Environment.NewLine;
                data += "JoystickButtons3 " + JoystickButtons3 + Environment.NewLine;
                data += "JoystickButtons4 " + JoystickButtons4 + Environment.NewLine;
                data += "JoystickButtons5 " + JoystickButtons5 + Environment.NewLine;
                data += "JoystickButtons6 " + JoystickButtons6 + Environment.NewLine;
                data += "JoystickButtons7 " + JoystickButtons7 + Environment.NewLine;
                data += "JoystickButtons8 " + JoystickButtons8 + Environment.NewLine;
                data += "JoystickButtons9 " + JoystickButtons9 + Environment.NewLine;
                data += "JoystickButtons10 " + JoystickButtons10 + Environment.NewLine;
                data += "JoystickButtons11 " + JoystickButtons11 + Environment.NewLine;
                data += "JoystickButtons12 " + JoystickButtons12 + Environment.NewLine;
                data += "JoystickButtons13 " + JoystickButtons13 + Environment.NewLine;
                data += "JoystickButtons14 " + JoystickButtons14 + Environment.NewLine;
                data += "JoystickButtons15 " + JoystickButtons15 + Environment.NewLine;
                data += "JoystickButtons16 " + JoystickButtons16 + Environment.NewLine;
                data += "JoystickButtons17 " + JoystickButtons17 + Environment.NewLine;
                data += "JoystickButtons18 " + JoystickButtons18 + Environment.NewLine;
                data += "JoystickButtons19 " + JoystickButtons19 + Environment.NewLine;
                data += "JoystickButtons20 " + JoystickButtons20 + Environment.NewLine;
                data += "JoystickButtons21 " + JoystickButtons21 + Environment.NewLine;
                data += "JoystickButtons22 " + JoystickButtons22 + Environment.NewLine;
                data += "JoystickButtons23 " + JoystickButtons23 + Environment.NewLine;
                data += "JoystickButtons24 " + JoystickButtons24 + Environment.NewLine;
                data += "JoystickButtons25 " + JoystickButtons25 + Environment.NewLine;
                data += "JoystickButtons26 " + JoystickButtons26 + Environment.NewLine;
                data += "JoystickButtons27 " + JoystickButtons27 + Environment.NewLine;
                data += "JoystickButtons28 " + JoystickButtons28 + Environment.NewLine;
                data += "JoystickButtons29 " + JoystickButtons29 + Environment.NewLine;
                data += "JoystickButtons30 " + JoystickButtons30 + Environment.NewLine;
                data += "JoystickButtons31 " + JoystickButtons31 + Environment.NewLine;
                data += "JoystickButtons32 " + JoystickButtons32 + Environment.NewLine;
                data += "JoystickButtons33 " + JoystickButtons33 + Environment.NewLine;
                data += "JoystickButtons34 " + JoystickButtons34 + Environment.NewLine;
                data += "JoystickButtons35 " + JoystickButtons35 + Environment.NewLine;
                data += "JoystickButtons36 " + JoystickButtons36 + Environment.NewLine;
                data += "JoystickButtons37 " + JoystickButtons37 + Environment.NewLine;
                data += "JoystickButtons38 " + JoystickButtons38 + Environment.NewLine;
                data += "JoystickButtons39 " + JoystickButtons39 + Environment.NewLine;
                data += "JoystickButtons40 " + JoystickButtons40 + Environment.NewLine;
                data += "JoystickButtons41 " + JoystickButtons41 + Environment.NewLine;
                data += "JoystickButtons42 " + JoystickButtons42 + Environment.NewLine;
                data += "JoystickButtons43 " + JoystickButtons43 + Environment.NewLine;
                data += "JoystickButtons44 " + JoystickButtons44 + Environment.NewLine;
                data += "JoystickButtons45 " + JoystickButtons45 + Environment.NewLine;
                data += "JoystickButtons46 " + JoystickButtons46 + Environment.NewLine;
                data += "JoystickButtons47 " + JoystickButtons47 + Environment.NewLine;
                data += "JoystickButtons48 " + JoystickButtons48 + Environment.NewLine;
                data += "JoystickButtons49 " + JoystickButtons49 + Environment.NewLine;
                data += "JoystickButtons50 " + JoystickButtons50 + Environment.NewLine;
                data += "JoystickButtons51 " + JoystickButtons51 + Environment.NewLine;
                data += "JoystickButtons52 " + JoystickButtons52 + Environment.NewLine;
                data += "JoystickButtons53 " + JoystickButtons53 + Environment.NewLine;
                data += "JoystickButtons54 " + JoystickButtons54 + Environment.NewLine;
                data += "JoystickButtons55 " + JoystickButtons55 + Environment.NewLine;
                data += "JoystickButtons56 " + JoystickButtons56 + Environment.NewLine;
                data += "JoystickButtons57 " + JoystickButtons57 + Environment.NewLine;
                data += "JoystickButtons58 " + JoystickButtons58 + Environment.NewLine;
                data += "JoystickButtons59 " + JoystickButtons59 + Environment.NewLine;
                data += "JoystickButtons60 " + JoystickButtons60 + Environment.NewLine;
                data += "JoystickButtons61 " + JoystickButtons61 + Environment.NewLine;
                data += "JoystickButtons62 " + JoystickButtons62 + Environment.NewLine;
                data += "JoystickButtons63 " + JoystickButtons63 + Environment.NewLine;
                data += "JoystickButtons64 " + JoystickButtons64 + Environment.NewLine;
                data += "JoystickButtons65 " + JoystickButtons65 + Environment.NewLine;
                data += "JoystickButtons66 " + JoystickButtons66 + Environment.NewLine;
                data += "JoystickButtons67 " + JoystickButtons67 + Environment.NewLine;
                data += "JoystickButtons68 " + JoystickButtons68 + Environment.NewLine;
                data += "JoystickButtons69 " + JoystickButtons69 + Environment.NewLine;
                data += "JoystickButtons70 " + JoystickButtons70 + Environment.NewLine;
                data += "JoystickButtons71 " + JoystickButtons71 + Environment.NewLine;
                data += "JoystickButtons72 " + JoystickButtons72 + Environment.NewLine;
                data += "JoystickButtons73 " + JoystickButtons73 + Environment.NewLine;
                data += "JoystickButtons74 " + JoystickButtons74 + Environment.NewLine;
                data += "JoystickButtons75 " + JoystickButtons75 + Environment.NewLine;
                data += "JoystickButtons76 " + JoystickButtons76 + Environment.NewLine;
                data += "JoystickButtons77 " + JoystickButtons77 + Environment.NewLine;
                data += "JoystickButtons78 " + JoystickButtons78 + Environment.NewLine;
                data += "JoystickButtons79 " + JoystickButtons79 + Environment.NewLine;
                data += "JoystickButtons80 " + JoystickButtons80 + Environment.NewLine;
                data += "JoystickButtons81 " + JoystickButtons81 + Environment.NewLine;
                data += "JoystickButtons82 " + JoystickButtons82 + Environment.NewLine;
                data += "JoystickButtons83 " + JoystickButtons83 + Environment.NewLine;
                data += "JoystickButtons84 " + JoystickButtons84 + Environment.NewLine;
                data += "JoystickButtons85 " + JoystickButtons85 + Environment.NewLine;
                data += "JoystickButtons86 " + JoystickButtons86 + Environment.NewLine;
                data += "JoystickButtons87 " + JoystickButtons87 + Environment.NewLine;
                data += "JoystickButtons88 " + JoystickButtons88 + Environment.NewLine;
                data += "JoystickButtons89 " + JoystickButtons89 + Environment.NewLine;
                data += "JoystickButtons90 " + JoystickButtons90 + Environment.NewLine;
                data += "JoystickButtons91 " + JoystickButtons91 + Environment.NewLine;
                data += "JoystickButtons92 " + JoystickButtons92 + Environment.NewLine;
                data += "JoystickButtons93 " + JoystickButtons93 + Environment.NewLine;
                data += "JoystickButtons94 " + JoystickButtons94 + Environment.NewLine;
                data += "JoystickButtons95 " + JoystickButtons95 + Environment.NewLine;
                data += "JoystickButtons96 " + JoystickButtons96 + Environment.NewLine;
                data += "JoystickButtons97 " + JoystickButtons97 + Environment.NewLine;
                data += "JoystickButtons98 " + JoystickButtons98 + Environment.NewLine;
                data += "JoystickButtons99 " + JoystickButtons99 + Environment.NewLine;
                data += "JoystickButtons100 " + JoystickButtons100 + Environment.NewLine;
                data += "JoystickButtons101 " + JoystickButtons101 + Environment.NewLine;
                data += "JoystickButtons102 " + JoystickButtons102 + Environment.NewLine;
                data += "JoystickButtons103 " + JoystickButtons103 + Environment.NewLine;
                data += "JoystickButtons104 " + JoystickButtons104 + Environment.NewLine;
                data += "JoystickButtons105 " + JoystickButtons105 + Environment.NewLine;
                data += "JoystickButtons106 " + JoystickButtons106 + Environment.NewLine;
                data += "JoystickButtons107 " + JoystickButtons107 + Environment.NewLine;
                data += "JoystickButtons108 " + JoystickButtons108 + Environment.NewLine;
                data += "JoystickButtons109 " + JoystickButtons109 + Environment.NewLine;
                data += "JoystickButtons110 " + JoystickButtons110 + Environment.NewLine;
                data += "JoystickButtons111 " + JoystickButtons111 + Environment.NewLine;
                data += "JoystickButtons112 " + JoystickButtons112 + Environment.NewLine;
                data += "JoystickButtons113 " + JoystickButtons113 + Environment.NewLine;
                data += "JoystickButtons114 " + JoystickButtons114 + Environment.NewLine;
                data += "JoystickButtons115 " + JoystickButtons115 + Environment.NewLine;
                data += "JoystickButtons116 " + JoystickButtons116 + Environment.NewLine;
                data += "JoystickButtons117 " + JoystickButtons117 + Environment.NewLine;
                data += "JoystickButtons118 " + JoystickButtons118 + Environment.NewLine;
                data += "JoystickButtons119 " + JoystickButtons119 + Environment.NewLine;
                data += "JoystickButtons120 " + JoystickButtons120 + Environment.NewLine;
                data += "JoystickButtons121 " + JoystickButtons121 + Environment.NewLine;
                data += "JoystickButtons122 " + JoystickButtons122 + Environment.NewLine;
                data += "JoystickButtons123 " + JoystickButtons123 + Environment.NewLine;
                data += "JoystickButtons124 " + JoystickButtons124 + Environment.NewLine;
                data += "JoystickButtons125 " + JoystickButtons125 + Environment.NewLine;
                data += "JoystickButtons126 " + JoystickButtons126 + Environment.NewLine;
                data += "JoystickButtons127 " + JoystickButtons127 + Environment.NewLine;
                data += Environment.NewLine;
                this.label1.Text = data;
                System.Threading.Thread.Sleep(100);
            }
        }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
        }
    }
}