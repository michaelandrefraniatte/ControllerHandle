using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.DirectInput;
namespace GenericJoystickTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Joystick[] joystick = new Joystick[] { null, null, null, null };
        private static int num = 0;
        private static bool closed;
        public static bool ButtonAPressed;
        public static bool ButtonBPressed;
        public static bool ButtonXPressed;
        public static bool ButtonYPressed;
        public static bool ButtonDownPressed;
        public static bool ButtonUpPressed;
        public static bool ButtonLeftPressed;
        public static bool ButtonRightPressed;
        public static bool ButtonShoulderLeftPressed;
        public static bool ButtonShoulderRightPressed;
        public static bool ThumbpadLeftPressed;
        public static bool ThumbpadRightPressed;
        public static bool TriggerLeftPressed;
        public static bool TriggerRightPressed;
        public static bool ButtonShoulderLeftLeft;
        public static bool ButtonShoulderLeftRight;
        public static bool ButtonShoulderRightLeft;
        public static bool ButtonShoulderRightRight;
        public static bool ButtonMinus;
        public static bool ButtonPlus;
        public static bool ButtonCapture;
        public static bool ButtonHome;
        public static bool ThumbLeftDown;
        public static bool ThumbLeftUp;
        public static bool ThumbLeftLeft;
        public static bool ThumbLeftRight;
        public static bool ThumbRightDown;
        public static bool ThumbRightUp;
        public static bool ThumbRightLeft;
        public static bool ThumbRightRight;
        public static int Joystick1AxisX;
        public static int Joystick1AxisY;
        public static int Joystick1AxisZ;
        public static int Joystick1RotationX;
        public static int Joystick1RotationY;
        public static int Joystick1RotationZ;
        public static int Joystick1Sliders0;
        public static int Joystick1Sliders1; 
        public static int Joystick1PointOfViewControllers0;
        public static int Joystick1PointOfViewControllers1;
        public static int Joystick1PointOfViewControllers2;
        public static int Joystick1PointOfViewControllers3;
        public static int Joystick1VelocityX;
        public static int Joystick1VelocityY;
        public static int Joystick1VelocityZ;
        public static int Joystick1AngularVelocityX;
        public static int Joystick1AngularVelocityY;
        public static int Joystick1AngularVelocityZ;
        public static int Joystick1VelocitySliders0;
        public static int Joystick1VelocitySliders1;
        public static int Joystick1AccelerationX;
        public static int Joystick1AccelerationY;
        public static int Joystick1AccelerationZ;
        public static int Joystick1AngularAccelerationX;
        public static int Joystick1AngularAccelerationY;
        public static int Joystick1AngularAccelerationZ;
        public static int Joystick1AccelerationSliders0;
        public static int Joystick1AccelerationSliders1;
        public static int Joystick1ForceX;
        public static int Joystick1ForceY;
        public static int Joystick1ForceZ;
        public static int Joystick1TorqueX;
        public static int Joystick1TorqueY;
        public static int Joystick1TorqueZ;
        public static int Joystick1ForceSliders0;
        public static int Joystick1ForceSliders1;
        public static bool Joystick1Buttons0, Joystick1Buttons1, Joystick1Buttons2, Joystick1Buttons3, Joystick1Buttons4, Joystick1Buttons5, Joystick1Buttons6, Joystick1Buttons7, Joystick1Buttons8, Joystick1Buttons9, Joystick1Buttons10, Joystick1Buttons11, Joystick1Buttons12, Joystick1Buttons13, Joystick1Buttons14, Joystick1Buttons15, Joystick1Buttons16, Joystick1Buttons17, Joystick1Buttons18, Joystick1Buttons19, Joystick1Buttons20, Joystick1Buttons21, Joystick1Buttons22, Joystick1Buttons23, Joystick1Buttons24, Joystick1Buttons25, Joystick1Buttons26, Joystick1Buttons27, Joystick1Buttons28, Joystick1Buttons29, Joystick1Buttons30, Joystick1Buttons31, Joystick1Buttons32, Joystick1Buttons33, Joystick1Buttons34, Joystick1Buttons35, Joystick1Buttons36, Joystick1Buttons37, Joystick1Buttons38, Joystick1Buttons39, Joystick1Buttons40, Joystick1Buttons41, Joystick1Buttons42, Joystick1Buttons43, Joystick1Buttons44, Joystick1Buttons45, Joystick1Buttons46, Joystick1Buttons47, Joystick1Buttons48, Joystick1Buttons49, Joystick1Buttons50, Joystick1Buttons51, Joystick1Buttons52, Joystick1Buttons53, Joystick1Buttons54, Joystick1Buttons55, Joystick1Buttons56, Joystick1Buttons57, Joystick1Buttons58, Joystick1Buttons59, Joystick1Buttons60, Joystick1Buttons61, Joystick1Buttons62, Joystick1Buttons63, Joystick1Buttons64, Joystick1Buttons65, Joystick1Buttons66, Joystick1Buttons67, Joystick1Buttons68, Joystick1Buttons69, Joystick1Buttons70, Joystick1Buttons71, Joystick1Buttons72, Joystick1Buttons73, Joystick1Buttons74, Joystick1Buttons75, Joystick1Buttons76, Joystick1Buttons77, Joystick1Buttons78, Joystick1Buttons79, Joystick1Buttons80, Joystick1Buttons81, Joystick1Buttons82, Joystick1Buttons83, Joystick1Buttons84, Joystick1Buttons85, Joystick1Buttons86, Joystick1Buttons87, Joystick1Buttons88, Joystick1Buttons89, Joystick1Buttons90, Joystick1Buttons91, Joystick1Buttons92, Joystick1Buttons93, Joystick1Buttons94, Joystick1Buttons95, Joystick1Buttons96, Joystick1Buttons97, Joystick1Buttons98, Joystick1Buttons99, Joystick1Buttons100, Joystick1Buttons101, Joystick1Buttons102, Joystick1Buttons103, Joystick1Buttons104, Joystick1Buttons105, Joystick1Buttons106, Joystick1Buttons107, Joystick1Buttons108, Joystick1Buttons109, Joystick1Buttons110, Joystick1Buttons111, Joystick1Buttons112, Joystick1Buttons113, Joystick1Buttons114, Joystick1Buttons115, Joystick1Buttons116, Joystick1Buttons117, Joystick1Buttons118, Joystick1Buttons119, Joystick1Buttons120, Joystick1Buttons121, Joystick1Buttons122, Joystick1Buttons123, Joystick1Buttons124, Joystick1Buttons125, Joystick1Buttons126, Joystick1Buttons127;
        public static int Joystick2AxisX;
        public static int Joystick2AxisY;
        public static int Joystick2AxisZ;
        public static int Joystick2RotationX;
        public static int Joystick2RotationY;
        public static int Joystick2RotationZ;
        public static int Joystick2Sliders0;
        public static int Joystick2Sliders1;
        public static int Joystick2PointOfViewControllers0;
        public static int Joystick2PointOfViewControllers1;
        public static int Joystick2PointOfViewControllers2;
        public static int Joystick2PointOfViewControllers3;
        public static int Joystick2VelocityX;
        public static int Joystick2VelocityY;
        public static int Joystick2VelocityZ;
        public static int Joystick2AngularVelocityX;
        public static int Joystick2AngularVelocityY;
        public static int Joystick2AngularVelocityZ;
        public static int Joystick2VelocitySliders0;
        public static int Joystick2VelocitySliders1;
        public static int Joystick2AccelerationX;
        public static int Joystick2AccelerationY;
        public static int Joystick2AccelerationZ;
        public static int Joystick2AngularAccelerationX;
        public static int Joystick2AngularAccelerationY;
        public static int Joystick2AngularAccelerationZ;
        public static int Joystick2AccelerationSliders0;
        public static int Joystick2AccelerationSliders1;
        public static int Joystick2ForceX;
        public static int Joystick2ForceY;
        public static int Joystick2ForceZ;
        public static int Joystick2TorqueX;
        public static int Joystick2TorqueY;
        public static int Joystick2TorqueZ;
        public static int Joystick2ForceSliders0;
        public static int Joystick2ForceSliders1;
        public static bool Joystick2Buttons0, Joystick2Buttons1, Joystick2Buttons2, Joystick2Buttons3, Joystick2Buttons4, Joystick2Buttons5, Joystick2Buttons6, Joystick2Buttons7, Joystick2Buttons8, Joystick2Buttons9, Joystick2Buttons10, Joystick2Buttons11, Joystick2Buttons12, Joystick2Buttons13, Joystick2Buttons14, Joystick2Buttons15, Joystick2Buttons16, Joystick2Buttons17, Joystick2Buttons18, Joystick2Buttons19, Joystick2Buttons20, Joystick2Buttons21, Joystick2Buttons22, Joystick2Buttons23, Joystick2Buttons24, Joystick2Buttons25, Joystick2Buttons26, Joystick2Buttons27, Joystick2Buttons28, Joystick2Buttons29, Joystick2Buttons30, Joystick2Buttons31, Joystick2Buttons32, Joystick2Buttons33, Joystick2Buttons34, Joystick2Buttons35, Joystick2Buttons36, Joystick2Buttons37, Joystick2Buttons38, Joystick2Buttons39, Joystick2Buttons40, Joystick2Buttons41, Joystick2Buttons42, Joystick2Buttons43, Joystick2Buttons44, Joystick2Buttons45, Joystick2Buttons46, Joystick2Buttons47, Joystick2Buttons48, Joystick2Buttons49, Joystick2Buttons50, Joystick2Buttons51, Joystick2Buttons52, Joystick2Buttons53, Joystick2Buttons54, Joystick2Buttons55, Joystick2Buttons56, Joystick2Buttons57, Joystick2Buttons58, Joystick2Buttons59, Joystick2Buttons60, Joystick2Buttons61, Joystick2Buttons62, Joystick2Buttons63, Joystick2Buttons64, Joystick2Buttons65, Joystick2Buttons66, Joystick2Buttons67, Joystick2Buttons68, Joystick2Buttons69, Joystick2Buttons70, Joystick2Buttons71, Joystick2Buttons72, Joystick2Buttons73, Joystick2Buttons74, Joystick2Buttons75, Joystick2Buttons76, Joystick2Buttons77, Joystick2Buttons78, Joystick2Buttons79, Joystick2Buttons80, Joystick2Buttons81, Joystick2Buttons82, Joystick2Buttons83, Joystick2Buttons84, Joystick2Buttons85, Joystick2Buttons86, Joystick2Buttons87, Joystick2Buttons88, Joystick2Buttons89, Joystick2Buttons90, Joystick2Buttons91, Joystick2Buttons92, Joystick2Buttons93, Joystick2Buttons94, Joystick2Buttons95, Joystick2Buttons96, Joystick2Buttons97, Joystick2Buttons98, Joystick2Buttons99, Joystick2Buttons100, Joystick2Buttons101, Joystick2Buttons102, Joystick2Buttons103, Joystick2Buttons104, Joystick2Buttons105, Joystick2Buttons106, Joystick2Buttons107, Joystick2Buttons108, Joystick2Buttons109, Joystick2Buttons110, Joystick2Buttons111, Joystick2Buttons112, Joystick2Buttons113, Joystick2Buttons114, Joystick2Buttons115, Joystick2Buttons116, Joystick2Buttons117, Joystick2Buttons118, Joystick2Buttons119, Joystick2Buttons120, Joystick2Buttons121, Joystick2Buttons122, Joystick2Buttons123, Joystick2Buttons124, Joystick2Buttons125, Joystick2Buttons126, Joystick2Buttons127;
        private void Form1_Shown(object sender, EventArgs e)
        {
            DirectInput directInput = new DirectInput();
            Guid[] joystickGuid = new Guid[] { Guid.Empty , Guid.Empty, Guid.Empty, Guid.Empty };
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
            {
                joystickGuid[num] = deviceInstance.InstanceGuid;
                num++;
                if (num > 4)
                    break;
            }
            if (num < 4)
            {
                foreach (var deviceInstance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                {
                    joystickGuid[num] = deviceInstance.InstanceGuid;
                    num++;
                    if (num > 4)
                        break;
                }
            }
            if (joystickGuid[0] == Guid.Empty)
            {
                this.label1.Text = "No joystick/Gamepad found." + Environment.NewLine;
            }
            else
            {
                this.label1.Text = "";
                for (int inc = 0; inc < num; inc++)
                {
                    joystick[inc] = new Joystick(directInput, joystickGuid[inc]);
                    this.label1.Text += "Found Joystick/Gamepad with GUID: " + joystickGuid[inc] + Environment.NewLine;
                    var allEffects = joystick[inc].GetEffects();
                    foreach (var effectInfo in allEffects)
                        this.label1.Text += "Effect available:" + effectInfo.Name + Environment.NewLine;
                    joystick[inc].Properties.BufferSize = 128;
                    joystick[inc].Acquire();
                }
                Task.Run(() => taskEmulate());
            }
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                for (int inc = 0; inc < num; inc++)
                {
                    joystick[inc].Poll();
                    var datas = joystick[inc].GetBufferedData();
                    foreach (var state in datas)
                    {
                        if (inc == 0 & state.ToString().Contains("Buttons0,") & state.ToString().Contains("Value: 128"))
                            ButtonAPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons0,") & state.ToString().Contains("Value: 0"))
                            ButtonAPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons2,") & state.ToString().Contains("Value: 128"))
                            ButtonBPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons2,") & state.ToString().Contains("Value: 0"))
                            ButtonBPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons1,") & state.ToString().Contains("Value: 128"))
                            ButtonXPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons1,") & state.ToString().Contains("Value: 0"))
                            ButtonXPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons3,") & state.ToString().Contains("Value: 128"))
                            ButtonYPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons3,") & state.ToString().Contains("Value: 0"))
                            ButtonYPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons1,") & state.ToString().Contains("Value: 128"))
                            ButtonDownPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons1,") & state.ToString().Contains("Value: 0"))
                            ButtonDownPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons2,") & state.ToString().Contains("Value: 128"))
                            ButtonUpPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons2,") & state.ToString().Contains("Value: 0"))
                            ButtonUpPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons0,") & state.ToString().Contains("Value: 128"))
                            ButtonLeftPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons0,") & state.ToString().Contains("Value: 0"))
                            ButtonLeftPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons3,") & state.ToString().Contains("Value: 128"))
                            ButtonRightPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons3,") & state.ToString().Contains("Value: 0"))
                            ButtonRightPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons14,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderLeftPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons14,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderLeftPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons14,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderRightPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons14,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderRightPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons10,") & state.ToString().Contains("Value: 128"))
                            ThumbpadLeftPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons10,") & state.ToString().Contains("Value: 0"))
                            ThumbpadLeftPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons11,") & state.ToString().Contains("Value: 128"))
                            ThumbpadRightPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons11,") & state.ToString().Contains("Value: 0"))
                            ThumbpadRightPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons15,") & state.ToString().Contains("Value: 128"))
                            TriggerLeftPressed = true;
                        if (inc == 1 & state.ToString().Contains("Buttons15,") & state.ToString().Contains("Value: 0"))
                            TriggerLeftPressed = false;
                        if (inc == 0 & state.ToString().Contains("Buttons15,") & state.ToString().Contains("Value: 128"))
                            TriggerRightPressed = true;
                        if (inc == 0 & state.ToString().Contains("Buttons15,") & state.ToString().Contains("Value: 0"))
                            TriggerRightPressed = false;
                        if (inc == 1 & state.ToString().Contains("Buttons4,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderLeftLeft = true;
                        if (inc == 1 & state.ToString().Contains("Buttons4,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderLeftLeft = false;
                        if (inc == 1 & state.ToString().Contains("Buttons5,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderLeftRight = true;
                        if (inc == 1 & state.ToString().Contains("Buttons5,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderLeftRight = false;
                        if (inc == 0 & state.ToString().Contains("Buttons4,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderRightLeft = true;
                        if (inc == 0 & state.ToString().Contains("Buttons4,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderRightLeft = false;
                        if (inc == 0 & state.ToString().Contains("Buttons5,") & state.ToString().Contains("Value: 128"))
                            ButtonShoulderRightRight = true;
                        if (inc == 0 & state.ToString().Contains("Buttons5,") & state.ToString().Contains("Value: 0"))
                            ButtonShoulderRightRight = false;
                        if (inc == 1 & state.ToString().Contains("Buttons8,") & state.ToString().Contains("Value: 128"))
                            ButtonMinus = true;
                        if (inc == 1 & state.ToString().Contains("Buttons8,") & state.ToString().Contains("Value: 0"))
                            ButtonMinus = false;
                        if (inc == 0 & state.ToString().Contains("Buttons9,") & state.ToString().Contains("Value: 128"))
                            ButtonPlus = true;
                        if (inc == 0 & state.ToString().Contains("Buttons9,") & state.ToString().Contains("Value: 0"))
                            ButtonPlus = false;
                        if (inc == 1 & state.ToString().Contains("Buttons13,") & state.ToString().Contains("Value: 128"))
                            ButtonCapture = true;
                        if (inc == 1 & state.ToString().Contains("Buttons13,") & state.ToString().Contains("Value: 0"))
                            ButtonCapture = false;
                        if (inc == 0 & state.ToString().Contains("Buttons12,") & state.ToString().Contains("Value: 128"))
                            ButtonHome = true;
                        if (inc == 0 & state.ToString().Contains("Buttons12,") & state.ToString().Contains("Value: 0"))
                            ButtonHome = false;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 9000"))
                            ThumbLeftDown = true;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 18000") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 27000")))
                            ThumbLeftDown = false;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 27000"))
                            ThumbLeftUp = true;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 18000") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 9000")))
                            ThumbLeftUp = false;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 18000"))
                            ThumbLeftLeft = true;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 27000") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 9000")))
                            ThumbLeftLeft = false;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 0"))
                            ThumbLeftRight = true;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 27000") | state.ToString().Contains("Value: 18000") | state.ToString().Contains("Value: 9000")))
                            ThumbLeftRight = false;
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 22500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbLeftDown = false;
                            ThumbLeftRight = false;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 4500") | state.ToString().Contains("Value: 22500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbLeftDown = false;
                            ThumbLeftLeft = false;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 4500") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbLeftUp = false;
                            ThumbLeftLeft = false;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 4500") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 22500")))
                        {
                            ThumbLeftUp = false;
                            ThumbLeftRight = false;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 4500"))
                        {
                            ThumbLeftDown = true;
                            ThumbLeftRight = true;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 13500"))
                        {
                            ThumbLeftDown = true;
                            ThumbLeftLeft = true;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 22500"))
                        {
                            ThumbLeftUp = true;
                            ThumbLeftLeft = true;
                        }
                        if (inc == 1 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 31500"))
                        {
                            ThumbLeftUp = true;
                            ThumbLeftRight = true;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 27000"))
                            ThumbRightDown = true;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 9000") | state.ToString().Contains("Value: 18000")))
                            ThumbRightDown = false;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 9000"))
                            ThumbRightUp = true;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 18000") | state.ToString().Contains("Value: 27000")))
                            ThumbRightUp = false;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 0"))
                            ThumbRightLeft = true;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 9000") | state.ToString().Contains("Value: 18000") | state.ToString().Contains("Value: 27000")))
                            ThumbRightLeft = false;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 18000"))
                            ThumbRightRight = true;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 0") | state.ToString().Contains("Value: 9000") | state.ToString().Contains("Value: 27000")))
                            ThumbRightRight = false;
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 4500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbRightDown = false;
                            ThumbRightRight = false;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 22500") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 4500")))
                        {
                            ThumbRightDown = false;
                            ThumbRightLeft = false;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 22500") | state.ToString().Contains("Value: 13500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbRightUp = false;
                            ThumbRightLeft = false;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & (state.ToString().Contains("Value: -1") | state.ToString().Contains("Value: 4500") | state.ToString().Contains("Value: 22500") | state.ToString().Contains("Value: 31500")))
                        {
                            ThumbRightUp = false;
                            ThumbRightRight = false;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 22500"))
                        {
                            ThumbRightDown = true;
                            ThumbRightRight = true;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 31500"))
                        {
                            ThumbRightDown = true;
                            ThumbRightLeft = true;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 4500"))
                        {
                            ThumbRightUp = true;
                            ThumbRightLeft = true;
                        }
                        if (inc == 0 & state.ToString().Contains("PointOfViewControllers0") & state.ToString().Contains("Value: 13500"))
                        {
                            ThumbRightUp = true;
                            ThumbRightRight = true;
                        }
                        if (inc == 0 & state.Offset == JoystickOffset.X)
                            Joystick1AxisX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.Y)
                            Joystick1AxisY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.Z)
                            Joystick1AxisZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.RotationX)
                            Joystick1RotationX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.RotationY)
                            Joystick1RotationY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.RotationZ)
                            Joystick1RotationZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.Sliders0)
                            Joystick1Sliders0 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.Sliders1)
                            Joystick1Sliders1 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers0)
                            Joystick1PointOfViewControllers0 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers1)
                            Joystick1PointOfViewControllers1 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers2)
                            Joystick1PointOfViewControllers2 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.PointOfViewControllers3)
                            Joystick1PointOfViewControllers3 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.VelocityX)
                            Joystick1VelocityX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.VelocityY)
                            Joystick1VelocityY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.VelocityZ)
                            Joystick1VelocityZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityX)
                            Joystick1AngularVelocityX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityY)
                            Joystick1AngularVelocityY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularVelocityZ)
                            Joystick1AngularVelocityZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.VelocitySliders0)
                            Joystick1VelocitySliders0 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.VelocitySliders1)
                            Joystick1VelocitySliders1 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AccelerationX)
                            Joystick1AccelerationX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AccelerationY)
                            Joystick1AccelerationY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AccelerationZ)
                            Joystick1AccelerationZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationX)
                            Joystick1AngularAccelerationX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationY)
                            Joystick1AngularAccelerationY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AngularAccelerationZ)
                            Joystick1AngularAccelerationZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AccelerationSliders0)
                            Joystick1AccelerationSliders0 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.AccelerationSliders1)
                            Joystick1AccelerationSliders1 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.ForceX)
                            Joystick1ForceX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.ForceY)
                            Joystick1ForceY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.ForceZ)
                            Joystick1ForceZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.TorqueX)
                            Joystick1TorqueX = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.TorqueY)
                            Joystick1TorqueY = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.TorqueZ)
                            Joystick1TorqueZ = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.ForceSliders0)
                            Joystick1ForceSliders0 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.ForceSliders1)
                            Joystick1ForceSliders1 = state.Value;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons0 & state.Value == 128)
                            Joystick1Buttons0 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons0 & state.Value == 0)
                            Joystick1Buttons0 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons1 & state.Value == 128)
                            Joystick1Buttons1 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons1 & state.Value == 0)
                            Joystick1Buttons1 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons2 & state.Value == 128)
                            Joystick1Buttons2 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons2 & state.Value == 0)
                            Joystick1Buttons2 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons3 & state.Value == 128)
                            Joystick1Buttons3 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons3 & state.Value == 0)
                            Joystick1Buttons3 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons4 & state.Value == 128)
                            Joystick1Buttons4 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons4 & state.Value == 0)
                            Joystick1Buttons4 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons5 & state.Value == 128)
                            Joystick1Buttons5 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons5 & state.Value == 0)
                            Joystick1Buttons5 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons6 & state.Value == 128)
                            Joystick1Buttons6 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons6 & state.Value == 0)
                            Joystick1Buttons6 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons7 & state.Value == 128)
                            Joystick1Buttons7 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons7 & state.Value == 0)
                            Joystick1Buttons7 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons8 & state.Value == 128)
                            Joystick1Buttons8 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons8 & state.Value == 0)
                            Joystick1Buttons8 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons9 & state.Value == 128)
                            Joystick1Buttons9 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons9 & state.Value == 0)
                            Joystick1Buttons9 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons10 & state.Value == 128)
                            Joystick1Buttons10 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons10 & state.Value == 0)
                            Joystick1Buttons10 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons11 & state.Value == 128)
                            Joystick1Buttons11 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons11 & state.Value == 0)
                            Joystick1Buttons11 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons12 & state.Value == 128)
                            Joystick1Buttons12 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons12 & state.Value == 0)
                            Joystick1Buttons12 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons13 & state.Value == 128)
                            Joystick1Buttons13 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons13 & state.Value == 0)
                            Joystick1Buttons13 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons14 & state.Value == 128)
                            Joystick1Buttons14 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons14 & state.Value == 0)
                            Joystick1Buttons14 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons15 & state.Value == 128)
                            Joystick1Buttons15 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons15 & state.Value == 0)
                            Joystick1Buttons15 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons16 & state.Value == 128)
                            Joystick1Buttons16 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons16 & state.Value == 0)
                            Joystick1Buttons16 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons17 & state.Value == 128)
                            Joystick1Buttons17 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons17 & state.Value == 0)
                            Joystick1Buttons17 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons18 & state.Value == 128)
                            Joystick1Buttons18 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons18 & state.Value == 0)
                            Joystick1Buttons18 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons19 & state.Value == 128)
                            Joystick1Buttons19 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons19 & state.Value == 0)
                            Joystick1Buttons19 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons20 & state.Value == 128)
                            Joystick1Buttons20 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons20 & state.Value == 0)
                            Joystick1Buttons20 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons21 & state.Value == 128)
                            Joystick1Buttons21 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons21 & state.Value == 0)
                            Joystick1Buttons21 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons22 & state.Value == 128)
                            Joystick1Buttons22 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons22 & state.Value == 0)
                            Joystick1Buttons22 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons23 & state.Value == 128)
                            Joystick1Buttons23 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons23 & state.Value == 0)
                            Joystick1Buttons23 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons24 & state.Value == 128)
                            Joystick1Buttons24 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons24 & state.Value == 0)
                            Joystick1Buttons24 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons25 & state.Value == 128)
                            Joystick1Buttons25 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons25 & state.Value == 0)
                            Joystick1Buttons25 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons26 & state.Value == 128)
                            Joystick1Buttons26 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons26 & state.Value == 0)
                            Joystick1Buttons26 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons27 & state.Value == 128)
                            Joystick1Buttons27 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons27 & state.Value == 0)
                            Joystick1Buttons27 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons28 & state.Value == 128)
                            Joystick1Buttons28 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons28 & state.Value == 0)
                            Joystick1Buttons28 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons29 & state.Value == 128)
                            Joystick1Buttons29 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons29 & state.Value == 0)
                            Joystick1Buttons29 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons30 & state.Value == 128)
                            Joystick1Buttons30 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons30 & state.Value == 0)
                            Joystick1Buttons30 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons31 & state.Value == 128)
                            Joystick1Buttons31 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons31 & state.Value == 0)
                            Joystick1Buttons31 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons32 & state.Value == 128)
                            Joystick1Buttons32 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons32 & state.Value == 0)
                            Joystick1Buttons32 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons33 & state.Value == 128)
                            Joystick1Buttons33 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons33 & state.Value == 0)
                            Joystick1Buttons33 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons34 & state.Value == 128)
                            Joystick1Buttons34 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons34 & state.Value == 0)
                            Joystick1Buttons34 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons35 & state.Value == 128)
                            Joystick1Buttons35 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons35 & state.Value == 0)
                            Joystick1Buttons35 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons36 & state.Value == 128)
                            Joystick1Buttons36 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons36 & state.Value == 0)
                            Joystick1Buttons36 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons37 & state.Value == 128)
                            Joystick1Buttons37 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons37 & state.Value == 0)
                            Joystick1Buttons37 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons38 & state.Value == 128)
                            Joystick1Buttons38 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons38 & state.Value == 0)
                            Joystick1Buttons38 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons39 & state.Value == 128)
                            Joystick1Buttons39 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons39 & state.Value == 0)
                            Joystick1Buttons39 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons40 & state.Value == 128)
                            Joystick1Buttons40 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons40 & state.Value == 0)
                            Joystick1Buttons40 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons41 & state.Value == 128)
                            Joystick1Buttons41 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons41 & state.Value == 0)
                            Joystick1Buttons41 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons42 & state.Value == 128)
                            Joystick1Buttons42 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons42 & state.Value == 0)
                            Joystick1Buttons42 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons43 & state.Value == 128)
                            Joystick1Buttons43 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons43 & state.Value == 0)
                            Joystick1Buttons43 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons44 & state.Value == 128)
                            Joystick1Buttons44 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons44 & state.Value == 0)
                            Joystick1Buttons44 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons45 & state.Value == 128)
                            Joystick1Buttons45 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons45 & state.Value == 0)
                            Joystick1Buttons45 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons46 & state.Value == 128)
                            Joystick1Buttons46 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons46 & state.Value == 0)
                            Joystick1Buttons46 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons47 & state.Value == 128)
                            Joystick1Buttons47 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons47 & state.Value == 0)
                            Joystick1Buttons47 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons48 & state.Value == 128)
                            Joystick1Buttons48 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons48 & state.Value == 0)
                            Joystick1Buttons48 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons49 & state.Value == 128)
                            Joystick1Buttons49 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons49 & state.Value == 0)
                            Joystick1Buttons49 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons50 & state.Value == 128)
                            Joystick1Buttons50 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons50 & state.Value == 0)
                            Joystick1Buttons50 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons51 & state.Value == 128)
                            Joystick1Buttons51 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons51 & state.Value == 0)
                            Joystick1Buttons51 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons52 & state.Value == 128)
                            Joystick1Buttons52 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons52 & state.Value == 0)
                            Joystick1Buttons52 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons53 & state.Value == 128)
                            Joystick1Buttons53 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons53 & state.Value == 0)
                            Joystick1Buttons53 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons54 & state.Value == 128)
                            Joystick1Buttons54 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons54 & state.Value == 0)
                            Joystick1Buttons54 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons55 & state.Value == 128)
                            Joystick1Buttons55 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons55 & state.Value == 0)
                            Joystick1Buttons55 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons56 & state.Value == 128)
                            Joystick1Buttons56 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons56 & state.Value == 0)
                            Joystick1Buttons56 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons57 & state.Value == 128)
                            Joystick1Buttons57 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons57 & state.Value == 0)
                            Joystick1Buttons57 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons58 & state.Value == 128)
                            Joystick1Buttons58 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons58 & state.Value == 0)
                            Joystick1Buttons58 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons59 & state.Value == 128)
                            Joystick1Buttons59 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons59 & state.Value == 0)
                            Joystick1Buttons59 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons60 & state.Value == 128)
                            Joystick1Buttons60 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons60 & state.Value == 0)
                            Joystick1Buttons60 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons61 & state.Value == 128)
                            Joystick1Buttons61 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons61 & state.Value == 0)
                            Joystick1Buttons61 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons62 & state.Value == 128)
                            Joystick1Buttons62 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons62 & state.Value == 0)
                            Joystick1Buttons62 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons63 & state.Value == 128)
                            Joystick1Buttons63 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons63 & state.Value == 0)
                            Joystick1Buttons63 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons64 & state.Value == 128)
                            Joystick1Buttons64 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons64 & state.Value == 0)
                            Joystick1Buttons64 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons65 & state.Value == 128)
                            Joystick1Buttons65 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons65 & state.Value == 0)
                            Joystick1Buttons65 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons66 & state.Value == 128)
                            Joystick1Buttons66 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons66 & state.Value == 0)
                            Joystick1Buttons66 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons67 & state.Value == 128)
                            Joystick1Buttons67 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons67 & state.Value == 0)
                            Joystick1Buttons67 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons68 & state.Value == 128)
                            Joystick1Buttons68 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons68 & state.Value == 0)
                            Joystick1Buttons68 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons69 & state.Value == 128)
                            Joystick1Buttons69 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons69 & state.Value == 0)
                            Joystick1Buttons69 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons70 & state.Value == 128)
                            Joystick1Buttons70 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons70 & state.Value == 0)
                            Joystick1Buttons70 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons71 & state.Value == 128)
                            Joystick1Buttons71 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons71 & state.Value == 0)
                            Joystick1Buttons71 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons72 & state.Value == 128)
                            Joystick1Buttons72 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons72 & state.Value == 0)
                            Joystick1Buttons72 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons73 & state.Value == 128)
                            Joystick1Buttons73 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons73 & state.Value == 0)
                            Joystick1Buttons73 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons74 & state.Value == 128)
                            Joystick1Buttons74 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons74 & state.Value == 0)
                            Joystick1Buttons74 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons75 & state.Value == 128)
                            Joystick1Buttons75 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons75 & state.Value == 0)
                            Joystick1Buttons75 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons76 & state.Value == 128)
                            Joystick1Buttons76 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons76 & state.Value == 0)
                            Joystick1Buttons76 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons77 & state.Value == 128)
                            Joystick1Buttons77 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons77 & state.Value == 0)
                            Joystick1Buttons77 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons78 & state.Value == 128)
                            Joystick1Buttons78 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons78 & state.Value == 0)
                            Joystick1Buttons78 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons79 & state.Value == 128)
                            Joystick1Buttons79 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons79 & state.Value == 0)
                            Joystick1Buttons79 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons80 & state.Value == 128)
                            Joystick1Buttons80 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons80 & state.Value == 0)
                            Joystick1Buttons80 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons81 & state.Value == 128)
                            Joystick1Buttons81 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons81 & state.Value == 0)
                            Joystick1Buttons81 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons82 & state.Value == 128)
                            Joystick1Buttons82 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons82 & state.Value == 0)
                            Joystick1Buttons82 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons83 & state.Value == 128)
                            Joystick1Buttons83 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons83 & state.Value == 0)
                            Joystick1Buttons83 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons84 & state.Value == 128)
                            Joystick1Buttons84 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons84 & state.Value == 0)
                            Joystick1Buttons84 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons85 & state.Value == 128)
                            Joystick1Buttons85 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons85 & state.Value == 0)
                            Joystick1Buttons85 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons86 & state.Value == 128)
                            Joystick1Buttons86 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons86 & state.Value == 0)
                            Joystick1Buttons86 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons87 & state.Value == 128)
                            Joystick1Buttons87 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons87 & state.Value == 0)
                            Joystick1Buttons87 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons88 & state.Value == 128)
                            Joystick1Buttons88 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons88 & state.Value == 0)
                            Joystick1Buttons88 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons89 & state.Value == 128)
                            Joystick1Buttons89 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons89 & state.Value == 0)
                            Joystick1Buttons89 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons90 & state.Value == 128)
                            Joystick1Buttons90 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons90 & state.Value == 0)
                            Joystick1Buttons90 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons91 & state.Value == 128)
                            Joystick1Buttons91 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons91 & state.Value == 0)
                            Joystick1Buttons91 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons92 & state.Value == 128)
                            Joystick1Buttons92 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons92 & state.Value == 0)
                            Joystick1Buttons92 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons93 & state.Value == 128)
                            Joystick1Buttons93 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons93 & state.Value == 0)
                            Joystick1Buttons93 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons94 & state.Value == 128)
                            Joystick1Buttons94 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons94 & state.Value == 0)
                            Joystick1Buttons94 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons95 & state.Value == 128)
                            Joystick1Buttons95 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons95 & state.Value == 0)
                            Joystick1Buttons95 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons96 & state.Value == 128)
                            Joystick1Buttons96 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons96 & state.Value == 0)
                            Joystick1Buttons96 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons97 & state.Value == 128)
                            Joystick1Buttons97 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons97 & state.Value == 0)
                            Joystick1Buttons97 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons98 & state.Value == 128)
                            Joystick1Buttons98 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons98 & state.Value == 0)
                            Joystick1Buttons98 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons99 & state.Value == 128)
                            Joystick1Buttons99 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons99 & state.Value == 0)
                            Joystick1Buttons99 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons100 & state.Value == 128)
                            Joystick1Buttons100 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons100 & state.Value == 0)
                            Joystick1Buttons100 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons101 & state.Value == 128)
                            Joystick1Buttons101 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons101 & state.Value == 0)
                            Joystick1Buttons101 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons102 & state.Value == 128)
                            Joystick1Buttons102 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons102 & state.Value == 0)
                            Joystick1Buttons102 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons103 & state.Value == 128)
                            Joystick1Buttons103 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons103 & state.Value == 0)
                            Joystick1Buttons103 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons104 & state.Value == 128)
                            Joystick1Buttons104 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons104 & state.Value == 0)
                            Joystick1Buttons104 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons105 & state.Value == 128)
                            Joystick1Buttons105 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons105 & state.Value == 0)
                            Joystick1Buttons105 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons106 & state.Value == 128)
                            Joystick1Buttons106 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons106 & state.Value == 0)
                            Joystick1Buttons106 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons107 & state.Value == 128)
                            Joystick1Buttons107 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons107 & state.Value == 0)
                            Joystick1Buttons107 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons108 & state.Value == 128)
                            Joystick1Buttons108 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons108 & state.Value == 0)
                            Joystick1Buttons108 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons109 & state.Value == 128)
                            Joystick1Buttons109 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons109 & state.Value == 0)
                            Joystick1Buttons109 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons110 & state.Value == 128)
                            Joystick1Buttons110 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons110 & state.Value == 0)
                            Joystick1Buttons110 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons111 & state.Value == 128)
                            Joystick1Buttons111 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons111 & state.Value == 0)
                            Joystick1Buttons111 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons112 & state.Value == 128)
                            Joystick1Buttons112 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons112 & state.Value == 0)
                            Joystick1Buttons112 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons113 & state.Value == 128)
                            Joystick1Buttons113 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons113 & state.Value == 0)
                            Joystick1Buttons113 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons114 & state.Value == 128)
                            Joystick1Buttons114 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons114 & state.Value == 0)
                            Joystick1Buttons114 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons115 & state.Value == 128)
                            Joystick1Buttons115 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons115 & state.Value == 0)
                            Joystick1Buttons115 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons116 & state.Value == 128)
                            Joystick1Buttons116 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons116 & state.Value == 0)
                            Joystick1Buttons116 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons117 & state.Value == 128)
                            Joystick1Buttons117 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons117 & state.Value == 0)
                            Joystick1Buttons117 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons118 & state.Value == 128)
                            Joystick1Buttons118 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons118 & state.Value == 0)
                            Joystick1Buttons118 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons119 & state.Value == 128)
                            Joystick1Buttons119 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons119 & state.Value == 0)
                            Joystick1Buttons119 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons120 & state.Value == 128)
                            Joystick1Buttons120 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons120 & state.Value == 0)
                            Joystick1Buttons120 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons121 & state.Value == 128)
                            Joystick1Buttons121 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons121 & state.Value == 0)
                            Joystick1Buttons121 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons122 & state.Value == 128)
                            Joystick1Buttons122 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons122 & state.Value == 0)
                            Joystick1Buttons122 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons123 & state.Value == 128)
                            Joystick1Buttons123 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons123 & state.Value == 0)
                            Joystick1Buttons123 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons124 & state.Value == 128)
                            Joystick1Buttons124 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons124 & state.Value == 0)
                            Joystick1Buttons124 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons125 & state.Value == 128)
                            Joystick1Buttons125 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons125 & state.Value == 0)
                            Joystick1Buttons125 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons126 & state.Value == 128)
                            Joystick1Buttons126 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons126 & state.Value == 0)
                            Joystick1Buttons126 = false;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons127 & state.Value == 128)
                            Joystick1Buttons127 = true;
                        if (inc == 0 & state.Offset == JoystickOffset.Buttons127 & state.Value == 0)
                            Joystick1Buttons127 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.X)
                            Joystick2AxisX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.Y)
                            Joystick2AxisY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.Z)
                            Joystick2AxisZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.RotationX)
                            Joystick2RotationX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.RotationY)
                            Joystick2RotationY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.RotationZ)
                            Joystick2RotationZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.Sliders0)
                            Joystick2Sliders0 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.Sliders1)
                            Joystick2Sliders1 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers0)
                            Joystick2PointOfViewControllers0 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers1)
                            Joystick2PointOfViewControllers1 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers2)
                            Joystick2PointOfViewControllers2 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.PointOfViewControllers3)
                            Joystick2PointOfViewControllers3 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.VelocityX)
                            Joystick2VelocityX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.VelocityY)
                            Joystick2VelocityY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.VelocityZ)
                            Joystick2VelocityZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityX)
                            Joystick2AngularVelocityX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityY)
                            Joystick2AngularVelocityY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularVelocityZ)
                            Joystick2AngularVelocityZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.VelocitySliders0)
                            Joystick2VelocitySliders0 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.VelocitySliders1)
                            Joystick2VelocitySliders1 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AccelerationX)
                            Joystick2AccelerationX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AccelerationY)
                            Joystick2AccelerationY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AccelerationZ)
                            Joystick2AccelerationZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationX)
                            Joystick2AngularAccelerationX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationY)
                            Joystick2AngularAccelerationY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AngularAccelerationZ)
                            Joystick2AngularAccelerationZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AccelerationSliders0)
                            Joystick2AccelerationSliders0 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.AccelerationSliders1)
                            Joystick2AccelerationSliders1 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.ForceX)
                            Joystick2ForceX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.ForceY)
                            Joystick2ForceY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.ForceZ)
                            Joystick2ForceZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.TorqueX)
                            Joystick2TorqueX = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.TorqueY)
                            Joystick2TorqueY = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.TorqueZ)
                            Joystick2TorqueZ = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.ForceSliders0)
                            Joystick2ForceSliders0 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.ForceSliders1)
                            Joystick2ForceSliders1 = state.Value;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons0 & state.Value == 128)
                            Joystick2Buttons0 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons0 & state.Value == 0)
                            Joystick2Buttons0 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons1 & state.Value == 128)
                            Joystick2Buttons1 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons1 & state.Value == 0)
                            Joystick2Buttons1 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons2 & state.Value == 128)
                            Joystick2Buttons2 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons2 & state.Value == 0)
                            Joystick2Buttons2 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons3 & state.Value == 128)
                            Joystick2Buttons3 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons3 & state.Value == 0)
                            Joystick2Buttons3 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons4 & state.Value == 128)
                            Joystick2Buttons4 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons4 & state.Value == 0)
                            Joystick2Buttons4 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons5 & state.Value == 128)
                            Joystick2Buttons5 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons5 & state.Value == 0)
                            Joystick2Buttons5 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons6 & state.Value == 128)
                            Joystick2Buttons6 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons6 & state.Value == 0)
                            Joystick2Buttons6 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons7 & state.Value == 128)
                            Joystick2Buttons7 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons7 & state.Value == 0)
                            Joystick2Buttons7 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons8 & state.Value == 128)
                            Joystick2Buttons8 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons8 & state.Value == 0)
                            Joystick2Buttons8 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons9 & state.Value == 128)
                            Joystick2Buttons9 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons9 & state.Value == 0)
                            Joystick2Buttons9 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons10 & state.Value == 128)
                            Joystick2Buttons10 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons10 & state.Value == 0)
                            Joystick2Buttons10 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons11 & state.Value == 128)
                            Joystick2Buttons11 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons11 & state.Value == 0)
                            Joystick2Buttons11 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons12 & state.Value == 128)
                            Joystick2Buttons12 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons12 & state.Value == 0)
                            Joystick2Buttons12 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons13 & state.Value == 128)
                            Joystick2Buttons13 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons13 & state.Value == 0)
                            Joystick2Buttons13 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons14 & state.Value == 128)
                            Joystick2Buttons14 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons14 & state.Value == 0)
                            Joystick2Buttons14 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons15 & state.Value == 128)
                            Joystick2Buttons15 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons15 & state.Value == 0)
                            Joystick2Buttons15 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons16 & state.Value == 128)
                            Joystick2Buttons16 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons16 & state.Value == 0)
                            Joystick2Buttons16 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons17 & state.Value == 128)
                            Joystick2Buttons17 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons17 & state.Value == 0)
                            Joystick2Buttons17 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons18 & state.Value == 128)
                            Joystick2Buttons18 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons18 & state.Value == 0)
                            Joystick2Buttons18 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons19 & state.Value == 128)
                            Joystick2Buttons19 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons19 & state.Value == 0)
                            Joystick2Buttons19 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons20 & state.Value == 128)
                            Joystick2Buttons20 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons20 & state.Value == 0)
                            Joystick2Buttons20 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons21 & state.Value == 128)
                            Joystick2Buttons21 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons21 & state.Value == 0)
                            Joystick2Buttons21 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons22 & state.Value == 128)
                            Joystick2Buttons22 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons22 & state.Value == 0)
                            Joystick2Buttons22 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons23 & state.Value == 128)
                            Joystick2Buttons23 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons23 & state.Value == 0)
                            Joystick2Buttons23 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons24 & state.Value == 128)
                            Joystick2Buttons24 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons24 & state.Value == 0)
                            Joystick2Buttons24 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons25 & state.Value == 128)
                            Joystick2Buttons25 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons25 & state.Value == 0)
                            Joystick2Buttons25 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons26 & state.Value == 128)
                            Joystick2Buttons26 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons26 & state.Value == 0)
                            Joystick2Buttons26 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons27 & state.Value == 128)
                            Joystick2Buttons27 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons27 & state.Value == 0)
                            Joystick2Buttons27 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons28 & state.Value == 128)
                            Joystick2Buttons28 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons28 & state.Value == 0)
                            Joystick2Buttons28 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons29 & state.Value == 128)
                            Joystick2Buttons29 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons29 & state.Value == 0)
                            Joystick2Buttons29 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons30 & state.Value == 128)
                            Joystick2Buttons30 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons30 & state.Value == 0)
                            Joystick2Buttons30 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons31 & state.Value == 128)
                            Joystick2Buttons31 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons31 & state.Value == 0)
                            Joystick2Buttons31 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons32 & state.Value == 128)
                            Joystick2Buttons32 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons32 & state.Value == 0)
                            Joystick2Buttons32 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons33 & state.Value == 128)
                            Joystick2Buttons33 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons33 & state.Value == 0)
                            Joystick2Buttons33 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons34 & state.Value == 128)
                            Joystick2Buttons34 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons34 & state.Value == 0)
                            Joystick2Buttons34 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons35 & state.Value == 128)
                            Joystick2Buttons35 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons35 & state.Value == 0)
                            Joystick2Buttons35 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons36 & state.Value == 128)
                            Joystick2Buttons36 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons36 & state.Value == 0)
                            Joystick2Buttons36 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons37 & state.Value == 128)
                            Joystick2Buttons37 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons37 & state.Value == 0)
                            Joystick2Buttons37 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons38 & state.Value == 128)
                            Joystick2Buttons38 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons38 & state.Value == 0)
                            Joystick2Buttons38 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons39 & state.Value == 128)
                            Joystick2Buttons39 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons39 & state.Value == 0)
                            Joystick2Buttons39 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons40 & state.Value == 128)
                            Joystick2Buttons40 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons40 & state.Value == 0)
                            Joystick2Buttons40 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons41 & state.Value == 128)
                            Joystick2Buttons41 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons41 & state.Value == 0)
                            Joystick2Buttons41 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons42 & state.Value == 128)
                            Joystick2Buttons42 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons42 & state.Value == 0)
                            Joystick2Buttons42 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons43 & state.Value == 128)
                            Joystick2Buttons43 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons43 & state.Value == 0)
                            Joystick2Buttons43 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons44 & state.Value == 128)
                            Joystick2Buttons44 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons44 & state.Value == 0)
                            Joystick2Buttons44 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons45 & state.Value == 128)
                            Joystick2Buttons45 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons45 & state.Value == 0)
                            Joystick2Buttons45 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons46 & state.Value == 128)
                            Joystick2Buttons46 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons46 & state.Value == 0)
                            Joystick2Buttons46 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons47 & state.Value == 128)
                            Joystick2Buttons47 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons47 & state.Value == 0)
                            Joystick2Buttons47 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons48 & state.Value == 128)
                            Joystick2Buttons48 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons48 & state.Value == 0)
                            Joystick2Buttons48 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons49 & state.Value == 128)
                            Joystick2Buttons49 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons49 & state.Value == 0)
                            Joystick2Buttons49 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons50 & state.Value == 128)
                            Joystick2Buttons50 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons50 & state.Value == 0)
                            Joystick2Buttons50 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons51 & state.Value == 128)
                            Joystick2Buttons51 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons51 & state.Value == 0)
                            Joystick2Buttons51 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons52 & state.Value == 128)
                            Joystick2Buttons52 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons52 & state.Value == 0)
                            Joystick2Buttons52 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons53 & state.Value == 128)
                            Joystick2Buttons53 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons53 & state.Value == 0)
                            Joystick2Buttons53 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons54 & state.Value == 128)
                            Joystick2Buttons54 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons54 & state.Value == 0)
                            Joystick2Buttons54 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons55 & state.Value == 128)
                            Joystick2Buttons55 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons55 & state.Value == 0)
                            Joystick2Buttons55 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons56 & state.Value == 128)
                            Joystick2Buttons56 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons56 & state.Value == 0)
                            Joystick2Buttons56 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons57 & state.Value == 128)
                            Joystick2Buttons57 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons57 & state.Value == 0)
                            Joystick2Buttons57 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons58 & state.Value == 128)
                            Joystick2Buttons58 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons58 & state.Value == 0)
                            Joystick2Buttons58 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons59 & state.Value == 128)
                            Joystick2Buttons59 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons59 & state.Value == 0)
                            Joystick2Buttons59 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons60 & state.Value == 128)
                            Joystick2Buttons60 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons60 & state.Value == 0)
                            Joystick2Buttons60 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons61 & state.Value == 128)
                            Joystick2Buttons61 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons61 & state.Value == 0)
                            Joystick2Buttons61 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons62 & state.Value == 128)
                            Joystick2Buttons62 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons62 & state.Value == 0)
                            Joystick2Buttons62 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons63 & state.Value == 128)
                            Joystick2Buttons63 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons63 & state.Value == 0)
                            Joystick2Buttons63 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons64 & state.Value == 128)
                            Joystick2Buttons64 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons64 & state.Value == 0)
                            Joystick2Buttons64 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons65 & state.Value == 128)
                            Joystick2Buttons65 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons65 & state.Value == 0)
                            Joystick2Buttons65 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons66 & state.Value == 128)
                            Joystick2Buttons66 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons66 & state.Value == 0)
                            Joystick2Buttons66 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons67 & state.Value == 128)
                            Joystick2Buttons67 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons67 & state.Value == 0)
                            Joystick2Buttons67 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons68 & state.Value == 128)
                            Joystick2Buttons68 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons68 & state.Value == 0)
                            Joystick2Buttons68 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons69 & state.Value == 128)
                            Joystick2Buttons69 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons69 & state.Value == 0)
                            Joystick2Buttons69 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons70 & state.Value == 128)
                            Joystick2Buttons70 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons70 & state.Value == 0)
                            Joystick2Buttons70 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons71 & state.Value == 128)
                            Joystick2Buttons71 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons71 & state.Value == 0)
                            Joystick2Buttons71 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons72 & state.Value == 128)
                            Joystick2Buttons72 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons72 & state.Value == 0)
                            Joystick2Buttons72 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons73 & state.Value == 128)
                            Joystick2Buttons73 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons73 & state.Value == 0)
                            Joystick2Buttons73 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons74 & state.Value == 128)
                            Joystick2Buttons74 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons74 & state.Value == 0)
                            Joystick2Buttons74 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons75 & state.Value == 128)
                            Joystick2Buttons75 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons75 & state.Value == 0)
                            Joystick2Buttons75 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons76 & state.Value == 128)
                            Joystick2Buttons76 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons76 & state.Value == 0)
                            Joystick2Buttons76 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons77 & state.Value == 128)
                            Joystick2Buttons77 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons77 & state.Value == 0)
                            Joystick2Buttons77 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons78 & state.Value == 128)
                            Joystick2Buttons78 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons78 & state.Value == 0)
                            Joystick2Buttons78 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons79 & state.Value == 128)
                            Joystick2Buttons79 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons79 & state.Value == 0)
                            Joystick2Buttons79 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons80 & state.Value == 128)
                            Joystick2Buttons80 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons80 & state.Value == 0)
                            Joystick2Buttons80 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons81 & state.Value == 128)
                            Joystick2Buttons81 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons81 & state.Value == 0)
                            Joystick2Buttons81 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons82 & state.Value == 128)
                            Joystick2Buttons82 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons82 & state.Value == 0)
                            Joystick2Buttons82 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons83 & state.Value == 128)
                            Joystick2Buttons83 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons83 & state.Value == 0)
                            Joystick2Buttons83 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons84 & state.Value == 128)
                            Joystick2Buttons84 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons84 & state.Value == 0)
                            Joystick2Buttons84 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons85 & state.Value == 128)
                            Joystick2Buttons85 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons85 & state.Value == 0)
                            Joystick2Buttons85 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons86 & state.Value == 128)
                            Joystick2Buttons86 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons86 & state.Value == 0)
                            Joystick2Buttons86 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons87 & state.Value == 128)
                            Joystick2Buttons87 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons87 & state.Value == 0)
                            Joystick2Buttons87 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons88 & state.Value == 128)
                            Joystick2Buttons88 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons88 & state.Value == 0)
                            Joystick2Buttons88 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons89 & state.Value == 128)
                            Joystick2Buttons89 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons89 & state.Value == 0)
                            Joystick2Buttons89 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons90 & state.Value == 128)
                            Joystick2Buttons90 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons90 & state.Value == 0)
                            Joystick2Buttons90 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons91 & state.Value == 128)
                            Joystick2Buttons91 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons91 & state.Value == 0)
                            Joystick2Buttons91 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons92 & state.Value == 128)
                            Joystick2Buttons92 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons92 & state.Value == 0)
                            Joystick2Buttons92 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons93 & state.Value == 128)
                            Joystick2Buttons93 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons93 & state.Value == 0)
                            Joystick2Buttons93 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons94 & state.Value == 128)
                            Joystick2Buttons94 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons94 & state.Value == 0)
                            Joystick2Buttons94 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons95 & state.Value == 128)
                            Joystick2Buttons95 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons95 & state.Value == 0)
                            Joystick2Buttons95 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons96 & state.Value == 128)
                            Joystick2Buttons96 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons96 & state.Value == 0)
                            Joystick2Buttons96 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons97 & state.Value == 128)
                            Joystick2Buttons97 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons97 & state.Value == 0)
                            Joystick2Buttons97 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons98 & state.Value == 128)
                            Joystick2Buttons98 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons98 & state.Value == 0)
                            Joystick2Buttons98 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons99 & state.Value == 128)
                            Joystick2Buttons99 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons99 & state.Value == 0)
                            Joystick2Buttons99 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons100 & state.Value == 128)
                            Joystick2Buttons100 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons100 & state.Value == 0)
                            Joystick2Buttons100 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons101 & state.Value == 128)
                            Joystick2Buttons101 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons101 & state.Value == 0)
                            Joystick2Buttons101 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons102 & state.Value == 128)
                            Joystick2Buttons102 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons102 & state.Value == 0)
                            Joystick2Buttons102 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons103 & state.Value == 128)
                            Joystick2Buttons103 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons103 & state.Value == 0)
                            Joystick2Buttons103 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons104 & state.Value == 128)
                            Joystick2Buttons104 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons104 & state.Value == 0)
                            Joystick2Buttons104 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons105 & state.Value == 128)
                            Joystick2Buttons105 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons105 & state.Value == 0)
                            Joystick2Buttons105 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons106 & state.Value == 128)
                            Joystick2Buttons106 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons106 & state.Value == 0)
                            Joystick2Buttons106 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons107 & state.Value == 128)
                            Joystick2Buttons107 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons107 & state.Value == 0)
                            Joystick2Buttons107 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons108 & state.Value == 128)
                            Joystick2Buttons108 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons108 & state.Value == 0)
                            Joystick2Buttons108 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons109 & state.Value == 128)
                            Joystick2Buttons109 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons109 & state.Value == 0)
                            Joystick2Buttons109 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons110 & state.Value == 128)
                            Joystick2Buttons110 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons110 & state.Value == 0)
                            Joystick2Buttons110 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons111 & state.Value == 128)
                            Joystick2Buttons111 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons111 & state.Value == 0)
                            Joystick2Buttons111 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons112 & state.Value == 128)
                            Joystick2Buttons112 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons112 & state.Value == 0)
                            Joystick2Buttons112 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons113 & state.Value == 128)
                            Joystick2Buttons113 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons113 & state.Value == 0)
                            Joystick2Buttons113 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons114 & state.Value == 128)
                            Joystick2Buttons114 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons114 & state.Value == 0)
                            Joystick2Buttons114 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons115 & state.Value == 128)
                            Joystick2Buttons115 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons115 & state.Value == 0)
                            Joystick2Buttons115 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons116 & state.Value == 128)
                            Joystick2Buttons116 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons116 & state.Value == 0)
                            Joystick2Buttons116 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons117 & state.Value == 128)
                            Joystick2Buttons117 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons117 & state.Value == 0)
                            Joystick2Buttons117 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons118 & state.Value == 128)
                            Joystick2Buttons118 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons118 & state.Value == 0)
                            Joystick2Buttons118 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons119 & state.Value == 128)
                            Joystick2Buttons119 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons119 & state.Value == 0)
                            Joystick2Buttons119 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons120 & state.Value == 128)
                            Joystick2Buttons120 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons120 & state.Value == 0)
                            Joystick2Buttons120 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons121 & state.Value == 128)
                            Joystick2Buttons121 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons121 & state.Value == 0)
                            Joystick2Buttons121 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons122 & state.Value == 128)
                            Joystick2Buttons122 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons122 & state.Value == 0)
                            Joystick2Buttons122 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons123 & state.Value == 128)
                            Joystick2Buttons123 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons123 & state.Value == 0)
                            Joystick2Buttons123 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons124 & state.Value == 128)
                            Joystick2Buttons124 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons124 & state.Value == 0)
                            Joystick2Buttons124 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons125 & state.Value == 128)
                            Joystick2Buttons125 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons125 & state.Value == 0)
                            Joystick2Buttons125 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons126 & state.Value == 128)
                            Joystick2Buttons126 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons126 & state.Value == 0)
                            Joystick2Buttons126 = false;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons127 & state.Value == 128)
                            Joystick2Buttons127 = true;
                        if (inc == 1 & state.Offset == JoystickOffset.Buttons127 & state.Value == 0)
                            Joystick2Buttons127 = false;
                        string data = "inc " + inc.ToString() + Environment.NewLine;
                        data += "state " + state.ToString() + Environment.NewLine;
                        data += "ButtonAPressed " + ButtonAPressed + Environment.NewLine;
                        data += "ButtonBPressed " + ButtonBPressed + Environment.NewLine;
                        data += "ButtonXPressed " + ButtonXPressed + Environment.NewLine;
                        data += "ButtonYPressed " + ButtonYPressed + Environment.NewLine;
                        data += "ButtonDownPressed " + ButtonDownPressed + Environment.NewLine;
                        data += "ButtonUpPressed " + ButtonUpPressed + Environment.NewLine;
                        data += "ButtonLeftPressed " + ButtonLeftPressed + Environment.NewLine;
                        data += "ButtonRightPressed " + ButtonRightPressed + Environment.NewLine;
                        data += "ButtonShoulderLeftPressed " + ButtonShoulderLeftPressed + Environment.NewLine;
                        data += "ButtonShoulderRightPressed " + ButtonShoulderRightPressed + Environment.NewLine;
                        data += "ThumbpadLeftPressed " + ThumbpadLeftPressed + Environment.NewLine;
                        data += "ThumbpadRightPressed " + ThumbpadRightPressed + Environment.NewLine;
                        data += "TriggerLeftPressed " + TriggerLeftPressed + Environment.NewLine;
                        data += "TriggerRightPressed " + TriggerRightPressed + Environment.NewLine;
                        data += "ButtonShoulderLeftLeft " + ButtonShoulderLeftLeft + Environment.NewLine;
                        data += "ButtonShoulderLeftRight " + ButtonShoulderLeftRight + Environment.NewLine;
                        data += "ButtonShoulderRightLeft " + ButtonShoulderRightLeft + Environment.NewLine;
                        data += "ButtonShoulderRightRight " + ButtonShoulderRightRight + Environment.NewLine;
                        data += "ButtonMinus " + ButtonMinus + Environment.NewLine;
                        data += "ButtonPlus " + ButtonPlus + Environment.NewLine;
                        data += "ButtonCapture " + ButtonCapture + Environment.NewLine;
                        data += "ButtonHome " + ButtonHome + Environment.NewLine;
                        data += "ThumbLeftDown " + ThumbLeftDown + Environment.NewLine;
                        data += "ThumbLeftUp " + ThumbLeftUp + Environment.NewLine;
                        data += "ThumbLeftLeft " + ThumbLeftLeft + Environment.NewLine;
                        data += "ThumbLeftRight " + ThumbLeftRight + Environment.NewLine;
                        data += "ThumbRightDown " + ThumbRightDown + Environment.NewLine;
                        data += "ThumbRightUp " + ThumbRightUp + Environment.NewLine;
                        data += "ThumbRightLeft " + ThumbRightLeft + Environment.NewLine;
                        data += "ThumbRightRight " + ThumbRightRight + Environment.NewLine;
                        this.label2.Text = data;
                    }
                    System.Threading.Thread.Sleep(1);
                }
                System.Threading.Thread.Sleep(1);
            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
        }
    }
}
