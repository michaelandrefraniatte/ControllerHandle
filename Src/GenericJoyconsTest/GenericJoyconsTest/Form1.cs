using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX.DirectInput;
namespace GenericJoyconsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public delegate void ButtonPressedDelegate(object sender, int ButtonNumber);
        public event ButtonPressedDelegate ButtonPressed;
        List<DeviceInstance> directInputList = new List<DeviceInstance>();
        DirectInput directInput = new DirectInput();
        List<SlimDX.DirectInput.Joystick> gamepads = new List<Joystick>();
        SlimDX.DirectInput.JoystickState state;
        private static bool closed = false;
        private void Form1_Shown(object sender, EventArgs e)
        {
            directInputList.Clear();
            directInputList.AddRange(directInput.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AllDevices));
            gamepads.Clear();
            foreach (var device in directInputList)
            {
                gamepads.Add(new SlimDX.DirectInput.Joystick(directInput, directInputList[0].InstanceGuid));
            }
            this.label1.Text = gamepads.Count.ToString();
            timer1.Interval = 100;
            timer1.Tick += (obj, eventargs) =>
            {
                string data = "";
                try
                {
                    foreach (var gamepad in gamepads)
                    {
                        if (gamepad.Acquire().IsFailure)
                        {
                            data += "acquire failed" + Environment.NewLine;
                            continue;
                        }
                        if (gamepad.Poll().IsFailure)
                        {
                            data += "poll failed" + Environment.NewLine;
                            continue;
                        }
                        if (SlimDX.Result.Last.IsFailure)
                        {
                            data += "last failed" + Environment.NewLine;
                            continue;
                        }
                        state = gamepad.GetCurrentState();
                        bool[] buttons = state.GetButtons();
                        for (int i = 0; i < buttons.Length; i++)
                        {
                            if (buttons[i])
                            {
                                if (ButtonPressed != null)
                                {
                                    ButtonPressed(gamepad, i);
                                    data += "ok" + Environment.NewLine;
                                }
                            }
                        }
                        data += state.GetButtons().ToString() + Environment.NewLine;
                        data += state.GetSliders().ToString() + Environment.NewLine;
                        data += state.GetPointOfViewControllers().ToString() + Environment.NewLine;
                        gamepad.Unacquire();
                    }
                }
                catch { }
                label2.Text = data;
            };
            timer1.Enabled = true;
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
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            closed = true;
        }
    }
}