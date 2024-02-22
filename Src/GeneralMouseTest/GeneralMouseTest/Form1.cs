using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;

namespace GeneralMouseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private bool closed = false;
        private MouseState mousestate;
        public bool MouseButtons0;
        public bool MouseButtons1;
        public bool MouseButtons2;
        public bool MouseButtons3;
        public bool MouseButtons4;
        public int MouseAxisX;
        public int MouseAxisY;
        public int MouseAxisZ;
        public void Form1_Load(object sender, EventArgs e)
        {
            mousestate = Mouse.GetState();
            Task.Run(() => taskEmulate());
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                mousestate = Mouse.GetState();
                MouseButtons0 = mousestate.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                MouseButtons1 = mousestate.RightButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                MouseButtons2 = mousestate.MiddleButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                MouseButtons3 = mousestate.XButton1 == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                MouseButtons4 = mousestate.XButton2 == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                MouseAxisX = mousestate.X;
                MouseAxisY = mousestate.Y;
                MouseAxisZ = mousestate.ScrollWheelValue;
                string str = "MouseAxisX : " + MouseAxisX + Environment.NewLine;
                str += "MouseAxisY : " + MouseAxisY + Environment.NewLine;
                str += "MouseAxisZ : " + MouseAxisZ + Environment.NewLine;
                str += "MouseButtons0 : " + MouseButtons0 + Environment.NewLine;
                str += "MouseButtons1 : " + MouseButtons1 + Environment.NewLine;
                str += "MouseButtons2 : " + MouseButtons2 + Environment.NewLine;
                str += "MouseButtons3 : " + MouseButtons3 + Environment.NewLine;
                str += "MouseButtons4 : " + MouseButtons4 + Environment.NewLine;
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