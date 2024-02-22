using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

namespace GeneralGamepadTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool closed = false;
        public bool ControllerButtonAPressed;
        public bool ControllerButtonBPressed;
        public bool ControllerButtonXPressed;
        public bool ControllerButtonYPressed;
        public bool ControllerButtonStartPressed;
        public bool ControllerButtonBackPressed;
        public bool ControllerButtonDownPressed;
        public bool ControllerButtonUpPressed;
        public bool ControllerButtonLeftPressed;
        public bool ControllerButtonRightPressed;
        public bool ControllerButtonShoulderLeftPressed;
        public bool ControllerButtonShoulderRightPressed;
        public bool ControllerThumbpadLeftPressed;
        public bool ControllerThumbpadRightPressed;
        public double ControllerTriggerLeftPosition;
        public double ControllerTriggerRightPosition;
        public double ControllerThumbLeftX;
        public double ControllerThumbLeftY;
        public double ControllerThumbRightX;
        public double ControllerThumbRightY;
        private GamePadState gamepadstate;
        public void Form1_Load(object sender, EventArgs e)
        {
            gamepadstate = GamePad.GetState(0);
            if (gamepadstate.IsConnected)
            {
                Task.Run(() => taskEmulate());
            }
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                gamepadstate = GamePad.GetState(0);
                ControllerButtonAPressed = gamepadstate.Buttons.A == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonBPressed = gamepadstate.Buttons.B == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonXPressed = gamepadstate.Buttons.X == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonYPressed = gamepadstate.Buttons.Y == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonShoulderLeftPressed = gamepadstate.Buttons.LeftShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonShoulderRightPressed = gamepadstate.Buttons.RightShoulder == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonStartPressed = gamepadstate.Buttons.Start == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonBackPressed = gamepadstate.Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerThumbpadLeftPressed = gamepadstate.Buttons.LeftStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerThumbpadRightPressed = gamepadstate.Buttons.RightStick == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonUpPressed = gamepadstate.DPad.Up == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonDownPressed = gamepadstate.DPad.Down == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonLeftPressed = gamepadstate.DPad.Left == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerButtonRightPressed = gamepadstate.DPad.Right == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                ControllerThumbLeftX = gamepadstate.ThumbSticks.Left.X * 32767f;
                ControllerThumbLeftY = gamepadstate.ThumbSticks.Left.Y * 32767f;
                ControllerThumbRightX = gamepadstate.ThumbSticks.Right.X * 32767f;
                ControllerThumbRightY = gamepadstate.ThumbSticks.Right.Y * 32767f;
                ControllerTriggerLeftPosition = gamepadstate.Triggers.Left * 255f;
                ControllerTriggerRightPosition = gamepadstate.Triggers.Right * 255f;
                string str = "ControllerButtonAPressed : " + ControllerButtonAPressed + Environment.NewLine;
                str += "ControllerButtonBPressed : " + ControllerButtonBPressed + Environment.NewLine;
                str += "ControllerButtonXPressed : " + ControllerButtonXPressed + Environment.NewLine;
                str += "ControllerButtonYPressed : " + ControllerButtonYPressed + Environment.NewLine;
                str += "ControllerButtonStartPressed : " + ControllerButtonStartPressed + Environment.NewLine;
                str += "ControllerButtonBackPressed : " + ControllerButtonBackPressed + Environment.NewLine;
                str += "ControllerButtonDownPressed : " + ControllerButtonDownPressed + Environment.NewLine;
                str += "ControllerButtonUpPressed : " + ControllerButtonUpPressed + Environment.NewLine;
                str += "ControllerButtonLeftPressed : " + ControllerButtonLeftPressed + Environment.NewLine;
                str += "ControllerButtonRightPressed : " + ControllerButtonRightPressed + Environment.NewLine;
                str += "ControllerButtonShoulderLeftPressed : " + ControllerButtonShoulderLeftPressed + Environment.NewLine;
                str += "ControllerButtonShoulderRightPressed : " + ControllerButtonShoulderRightPressed + Environment.NewLine;
                str += "ControllerThumbpadLeftPressed : " + ControllerThumbpadLeftPressed + Environment.NewLine;
                str += "ControllerThumbpadRightPressed : " + ControllerThumbpadRightPressed + Environment.NewLine;
                str += "ControllerTriggerLeftPosition : " + ControllerTriggerLeftPosition + Environment.NewLine;
                str += "ControllerTriggerRightPosition : " + ControllerTriggerRightPosition + Environment.NewLine;
                str += "ControllerThumbLeftX : " + ControllerThumbLeftX + Environment.NewLine;
                str += "ControllerThumbLeftY : " + ControllerThumbLeftY + Environment.NewLine;
                str += "ControllerThumbRightX : " + ControllerThumbRightX + Environment.NewLine;
                str += "ControllerThumbRightY : " + ControllerThumbRightY + Environment.NewLine;
                str += Environment.NewLine;
                this.label1.Text = str;
                System.Threading.Thread.Sleep(100);
            }
        }
        public void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closed = true;
        }
    }
}