using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX.XInput;
namespace GenericGamepadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Controller[] controller = new Controller[] { null, null, null, null };
        public static int xnum;
        private static State state;
        private static bool closed;
        public static bool Controller1ButtonAPressed, Controller2ButtonAPressed, Controller3ButtonAPressed, Controller4ButtonAPressed;
        public static bool Controller1ButtonBPressed, Controller2ButtonBPressed, Controller3ButtonBPressed, Controller4ButtonBPressed;
        public static bool Controller1ButtonXPressed, Controller2ButtonXPressed, Controller3ButtonXPressed, Controller4ButtonXPressed;
        public static bool Controller1ButtonYPressed, Controller2ButtonYPressed, Controller3ButtonYPressed, Controller4ButtonYPressed;
        public static bool Controller1ButtonStartPressed, Controller2ButtonStartPressed, Controller3ButtonStartPressed, Controller4ButtonStartPressed;
        public static bool Controller1ButtonBackPressed, Controller2ButtonBackPressed, Controller3ButtonBackPressed, Controller4ButtonBackPressed;
        public static bool Controller1ButtonDownPressed, Controller2ButtonDownPressed, Controller3ButtonDownPressed, Controller4ButtonDownPressed;
        public static bool Controller1ButtonUpPressed, Controller2ButtonUpPressed, Controller3ButtonUpPressed, Controller4ButtonUpPressed;
        public static bool Controller1ButtonLeftPressed, Controller2ButtonLeftPressed, Controller3ButtonLeftPressed, Controller4ButtonLeftPressed;
        public static bool Controller1ButtonRightPressed, Controller2ButtonRightPressed, Controller3ButtonRightPressed, Controller4ButtonRightPressed;
        public static bool Controller1ButtonShoulderLeftPressed, Controller2ButtonShoulderLeftPressed, Controller3ButtonShoulderLeftPressed, Controller4ButtonShoulderLeftPressed;
        public static bool Controller1ButtonShoulderRightPressed, Controller2ButtonShoulderRightPressed, Controller3ButtonShoulderRightPressed, Controller4ButtonShoulderRightPressed;
        public static bool Controller1ThumbpadLeftPressed, Controller2ThumbpadLeftPressed, Controller3ThumbpadLeftPressed, Controller4ThumbpadLeftPressed;
        public static bool Controller1ThumbpadRightPressed, Controller2ThumbpadRightPressed, Controller3ThumbpadRightPressed, Controller4ThumbpadRightPressed;
        public static double Controller1TriggerLeftPosition, Controller2TriggerLeftPosition, Controller3TriggerLeftPosition, Controller4TriggerLeftPosition;
        public static double Controller1TriggerRightPosition, Controller2TriggerRightPosition, Controller3TriggerRightPosition, Controller4TriggerRightPosition;
        public static double Controller1ThumbLeftX, Controller2ThumbLeftX, Controller3ThumbLeftX, Controller4ThumbLeftX;
        public static double Controller1ThumbLeftY, Controller2ThumbLeftY, Controller3ThumbLeftY, Controller4ThumbLeftY;
        public static double Controller1ThumbRightX, Controller2ThumbRightX, Controller3ThumbRightX, Controller4ThumbRightX;
        public static double Controller1ThumbRightY, Controller2ThumbRightY, Controller3ThumbRightY, Controller4ThumbRightY;

        private void Form1_Shown(object sender, EventArgs e)
        {
            var controllers = new[] { new Controller(UserIndex.One), new Controller(UserIndex.Two), new Controller(UserIndex.Three), new Controller(UserIndex.Four) };
            xnum = 0;
            foreach (var selectControler in controllers)
            {
                if (selectControler.IsConnected)
                {
                    controller[xnum] = selectControler;
                    xnum++;
                    if (xnum > 4)
                    {
                        break;
                    }
                }
            }
            if (controller[0] == null)
            {
                this.label1.Text = "No XInput controller installed" + Environment.NewLine;
            }
            else
            {
                this.label1.Text = "Found a XInput controller available" + Environment.NewLine;
                Task.Run(() => taskEmulate());
            }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e.KeyData);
        }
        private void OnKeyDown(System.Windows.Forms.Keys keyData)
        {
            if (keyData == System.Windows.Forms.Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (keyData == System.Windows.Forms.Keys.Escape)
            {
                this.Close();
            }
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                for (int inc = 0; inc < xnum; inc++)
                {
                    state = controller[inc].GetState();
                    if (inc == 0)
                    {
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A))
                            Controller1ButtonAPressed = true;
                        else
                            Controller1ButtonAPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B))
                            Controller1ButtonBPressed = true;
                        else
                            Controller1ButtonBPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X))
                            Controller1ButtonXPressed = true;
                        else
                            Controller1ButtonXPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y))
                            Controller1ButtonYPressed = true;
                        else
                            Controller1ButtonYPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start))
                            Controller1ButtonStartPressed = true;
                        else
                            Controller1ButtonStartPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back))
                            Controller1ButtonBackPressed = true;
                        else
                            Controller1ButtonBackPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
                            Controller1ButtonDownPressed = true;
                        else
                            Controller1ButtonDownPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
                            Controller1ButtonUpPressed = true;
                        else
                            Controller1ButtonUpPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
                            Controller1ButtonLeftPressed = true;
                        else
                            Controller1ButtonLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
                            Controller1ButtonRightPressed = true;
                        else
                            Controller1ButtonRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
                            Controller1ButtonShoulderLeftPressed = true;
                        else
                            Controller1ButtonShoulderLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
                            Controller1ButtonShoulderRightPressed = true;
                        else
                            Controller1ButtonShoulderRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb))
                            Controller1ThumbpadLeftPressed = true;
                        else
                            Controller1ThumbpadLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightThumb))
                            Controller1ThumbpadRightPressed = true;
                        else
                            Controller1ThumbpadRightPressed = false;
                        Controller1TriggerLeftPosition = state.Gamepad.LeftTrigger;
                        Controller1TriggerRightPosition = state.Gamepad.RightTrigger;
                        Controller1ThumbLeftX = state.Gamepad.LeftThumbX;
                        Controller1ThumbLeftY = state.Gamepad.LeftThumbY;
                        Controller1ThumbRightX = state.Gamepad.RightThumbX;
                        Controller1ThumbRightY = state.Gamepad.RightThumbY;
                    }
                    if (inc == 1)
                    {
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A))
                            Controller2ButtonAPressed = true;
                        else
                            Controller2ButtonAPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B))
                            Controller2ButtonBPressed = true;
                        else
                            Controller2ButtonBPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X))
                            Controller2ButtonXPressed = true;
                        else
                            Controller2ButtonXPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y))
                            Controller2ButtonYPressed = true;
                        else
                            Controller2ButtonYPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start))
                            Controller2ButtonStartPressed = true;
                        else
                            Controller2ButtonStartPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back))
                            Controller2ButtonBackPressed = true;
                        else
                            Controller2ButtonBackPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
                            Controller2ButtonDownPressed = true;
                        else
                            Controller2ButtonDownPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
                            Controller2ButtonUpPressed = true;
                        else
                            Controller2ButtonUpPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
                            Controller2ButtonLeftPressed = true;
                        else
                            Controller2ButtonLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
                            Controller2ButtonRightPressed = true;
                        else
                            Controller2ButtonRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
                            Controller2ButtonShoulderLeftPressed = true;
                        else
                            Controller2ButtonShoulderLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
                            Controller2ButtonShoulderRightPressed = true;
                        else
                            Controller2ButtonShoulderRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb))
                            Controller2ThumbpadLeftPressed = true;
                        else
                            Controller2ThumbpadLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightThumb))
                            Controller2ThumbpadRightPressed = true;
                        else
                            Controller2ThumbpadRightPressed = false;
                        Controller2TriggerLeftPosition = state.Gamepad.LeftTrigger;
                        Controller2TriggerRightPosition = state.Gamepad.RightTrigger;
                        Controller2ThumbLeftX = state.Gamepad.LeftThumbX;
                        Controller2ThumbLeftY = state.Gamepad.LeftThumbY;
                        Controller2ThumbRightX = state.Gamepad.RightThumbX;
                        Controller2ThumbRightY = state.Gamepad.RightThumbY;
                    }
                    if (inc == 2)
                    {
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A))
                            Controller3ButtonAPressed = true;
                        else
                            Controller3ButtonAPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B))
                            Controller3ButtonBPressed = true;
                        else
                            Controller3ButtonBPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X))
                            Controller3ButtonXPressed = true;
                        else
                            Controller3ButtonXPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y))
                            Controller3ButtonYPressed = true;
                        else
                            Controller3ButtonYPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start))
                            Controller3ButtonStartPressed = true;
                        else
                            Controller3ButtonStartPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back))
                            Controller3ButtonBackPressed = true;
                        else
                            Controller3ButtonBackPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
                            Controller3ButtonDownPressed = true;
                        else
                            Controller3ButtonDownPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
                            Controller3ButtonUpPressed = true;
                        else
                            Controller3ButtonUpPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
                            Controller3ButtonLeftPressed = true;
                        else
                            Controller3ButtonLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
                            Controller3ButtonRightPressed = true;
                        else
                            Controller3ButtonRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
                            Controller3ButtonShoulderLeftPressed = true;
                        else
                            Controller3ButtonShoulderLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
                            Controller3ButtonShoulderRightPressed = true;
                        else
                            Controller3ButtonShoulderRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb))
                            Controller3ThumbpadLeftPressed = true;
                        else
                            Controller3ThumbpadLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightThumb))
                            Controller3ThumbpadRightPressed = true;
                        else
                            Controller3ThumbpadRightPressed = false;
                        Controller3TriggerLeftPosition = state.Gamepad.LeftTrigger;
                        Controller3TriggerRightPosition = state.Gamepad.RightTrigger;
                        Controller3ThumbLeftX = state.Gamepad.LeftThumbX;
                        Controller3ThumbLeftY = state.Gamepad.LeftThumbY;
                        Controller3ThumbRightX = state.Gamepad.RightThumbX;
                        Controller3ThumbRightY = state.Gamepad.RightThumbY;
                    }
                    if (inc == 3)
                    {
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A))
                            Controller4ButtonAPressed = true;
                        else
                            Controller4ButtonAPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B))
                            Controller4ButtonBPressed = true;
                        else
                            Controller4ButtonBPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X))
                            Controller4ButtonXPressed = true;
                        else
                            Controller4ButtonXPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y))
                            Controller4ButtonYPressed = true;
                        else
                            Controller4ButtonYPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start))
                            Controller4ButtonStartPressed = true;
                        else
                            Controller4ButtonStartPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back))
                            Controller4ButtonBackPressed = true;
                        else
                            Controller4ButtonBackPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown))
                            Controller4ButtonDownPressed = true;
                        else
                            Controller4ButtonDownPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp))
                            Controller4ButtonUpPressed = true;
                        else
                            Controller4ButtonUpPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft))
                            Controller4ButtonLeftPressed = true;
                        else
                            Controller4ButtonLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight))
                            Controller4ButtonRightPressed = true;
                        else
                            Controller4ButtonRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder))
                            Controller4ButtonShoulderLeftPressed = true;
                        else
                            Controller4ButtonShoulderLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder))
                            Controller4ButtonShoulderRightPressed = true;
                        else
                            Controller4ButtonShoulderRightPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftThumb))
                            Controller4ThumbpadLeftPressed = true;
                        else
                            Controller4ThumbpadLeftPressed = false;
                        if (state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightThumb))
                            Controller4ThumbpadRightPressed = true;
                        else
                            Controller4ThumbpadRightPressed = false;
                        Controller4TriggerLeftPosition = state.Gamepad.LeftTrigger;
                        Controller4TriggerRightPosition = state.Gamepad.RightTrigger;
                        Controller4ThumbLeftX = state.Gamepad.LeftThumbX;
                        Controller4ThumbLeftY = state.Gamepad.LeftThumbY;
                        Controller4ThumbRightX = state.Gamepad.RightThumbX;
                        Controller4ThumbRightY = state.Gamepad.RightThumbY;
                    }
                    string data = "";
                    data += "ButtonAPressed " + Controller1ButtonAPressed + Environment.NewLine;
                    data += "ButtonBPressed " + Controller1ButtonBPressed + Environment.NewLine;
                    data += "ButtonXPressed " + Controller1ButtonXPressed + Environment.NewLine;
                    data += "ButtonYPressed " + Controller1ButtonYPressed + Environment.NewLine;
                    data += "ButtonStartPressed " + Controller1ButtonStartPressed + Environment.NewLine;
                    data += "ButtonBackPressed " + Controller1ButtonBackPressed + Environment.NewLine;
                    data += "ButtonDownPressed " + Controller1ButtonDownPressed + Environment.NewLine;
                    data += "ButtonUpPressed " + Controller1ButtonUpPressed + Environment.NewLine;
                    data += "ButtonLeftPressed " + Controller1ButtonLeftPressed + Environment.NewLine;
                    data += "ButtonRightPressed " + Controller1ButtonRightPressed + Environment.NewLine;
                    data += "ButtonShoulderLeftPressed " + Controller1ButtonShoulderLeftPressed + Environment.NewLine;
                    data += "ButtonShoulderRightPressed " + Controller1ButtonShoulderRightPressed + Environment.NewLine;
                    data += "ThumbpadLeftPressed " + Controller1ThumbpadLeftPressed + Environment.NewLine;
                    data += "ThumbpadRightPressed " + Controller1ThumbpadRightPressed + Environment.NewLine;
                    data += "TriggerLeftPosition " + Controller1TriggerLeftPosition + Environment.NewLine;
                    data += "TriggerRightPosition " + Controller1TriggerRightPosition + Environment.NewLine;
                    data += "ThumbLeftX " + Controller1ThumbLeftX + Environment.NewLine;
                    data += "ThumbLeftY " + Controller1ThumbLeftY + Environment.NewLine;
                    data += "ThumbRightX " + Controller1ThumbRightX + Environment.NewLine;
                    data += "ThumbRightY " + Controller1ThumbRightY + Environment.NewLine;
                    data += "state " + state.Gamepad.Buttons.ToString();
                    this.label2.Text = data;
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
