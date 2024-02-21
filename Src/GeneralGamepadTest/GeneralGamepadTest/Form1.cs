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
        private bool ControllerButtonAPressed;
        private bool ControllerButtonBPressed;
        private bool ControllerButtonXPressed;
        private bool ControllerButtonYPressed;
        private bool ControllerButtonStartPressed;
        private bool ControllerButtonBackPressed;
        private bool ControllerButtonDownPressed;
        private bool ControllerButtonUpPressed;
        private bool ControllerButtonLeftPressed;
        private bool ControllerButtonRightPressed;
        private bool ControllerButtonShoulderLeftPressed;
        private bool ControllerButtonShoulderRightPressed;
        private bool ControllerThumbpadLeftPressed;
        private bool ControllerThumbpadRightPressed;
        private double ControllerTriggerLeftPosition;
        private double ControllerTriggerRightPosition;
        private double ControllerThumbLeftX;
        private double ControllerThumbLeftY;
        private double ControllerThumbRightX;
        private double ControllerThumbRightY;
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
                string data = "";
                data += "ButtonAPressed " + ControllerButtonAPressed + Environment.NewLine;
                data += "ButtonBPressed " + ControllerButtonBPressed + Environment.NewLine;
                data += "ButtonXPressed " + ControllerButtonXPressed + Environment.NewLine;
                data += "ButtonYPressed " + ControllerButtonYPressed + Environment.NewLine;
                data += "ButtonStartPressed " + ControllerButtonStartPressed + Environment.NewLine;
                data += "ButtonBackPressed " + ControllerButtonBackPressed + Environment.NewLine;
                data += "ButtonDownPressed " + ControllerButtonDownPressed + Environment.NewLine;
                data += "ButtonUpPressed " + ControllerButtonUpPressed + Environment.NewLine;
                data += "ButtonLeftPressed " + ControllerButtonLeftPressed + Environment.NewLine;
                data += "ButtonRightPressed " + ControllerButtonRightPressed + Environment.NewLine;
                data += "ButtonShoulderLeftPressed " + ControllerButtonShoulderLeftPressed + Environment.NewLine;
                data += "ButtonShoulderRightPressed " + ControllerButtonShoulderRightPressed + Environment.NewLine;
                data += "ThumbpadLeftPressed " + ControllerThumbpadLeftPressed + Environment.NewLine;
                data += "ThumbpadRightPressed " + ControllerThumbpadRightPressed + Environment.NewLine;
                data += "TriggerLeftPosition " + ControllerTriggerLeftPosition + Environment.NewLine;
                data += "TriggerRightPosition " + ControllerTriggerRightPosition + Environment.NewLine;
                data += "ThumbLeftX " + ControllerThumbLeftX + Environment.NewLine;
                data += "ThumbLeftY " + ControllerThumbLeftY + Environment.NewLine;
                data += "ThumbRightX " + ControllerThumbRightX + Environment.NewLine;
                data += "ThumbRightY " + ControllerThumbRightY + Environment.NewLine;
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