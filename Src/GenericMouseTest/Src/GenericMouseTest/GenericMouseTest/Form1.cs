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
namespace GenericMouseTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private static Mouse[] mouse = new Mouse[] { null, null, null, null };
        private static int mnum = 0;
        private static bool closed;
        public static bool MouseButtons0;
        public static bool MouseButtons1;
        public static bool MouseButtons2;
        public static bool MouseButtons3;
        public static bool MouseButtons4;
        public static bool MouseButtons5;
        public static bool MouseButtons6;
        public static bool MouseButtons7;
        public static int MouseAxisX;
        public static int MouseAxisY;
        public static int MouseAxisZ;
        private void Form1_Shown(object sender, EventArgs e)
        {
            DirectInput directInput = new DirectInput();
            Guid[] mouseGuid = new Guid[] { Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty };
            foreach (var deviceInstance in directInput.GetDevices(DeviceType.Mouse, DeviceEnumerationFlags.AllDevices))
            {
                mouseGuid[mnum] = deviceInstance.InstanceGuid;
                mnum++;
                if (mnum > 1)
                    break;
            }
            if (mouseGuid[0] == Guid.Empty)
            {
                this.label1.Text = "No mouse found." + Environment.NewLine;
            }
            else
            {
                this.label1.Text = "";
                for (int inc = 0; inc < mnum; inc++)
                {
                    mouse[inc] = new Mouse(directInput);
                    this.label1.Text += "Found mouse with GUID: " + mouseGuid[inc] + Environment.NewLine;
                    var allEffects = mouse[inc].GetEffects();
                    foreach (var effectInfo in allEffects)
                        this.label1.Text += "Effect available:" + effectInfo.Name + Environment.NewLine;
                    mouse[inc].Properties.BufferSize = 128;
                    mouse[inc].Acquire();
                }
                Task.Run(() => taskEmulate());
            }
        }
        private void taskEmulate()
        {
            while (!closed)
            {
                for (int inc = 0; inc < mnum; inc++)
                {
                    mouse[inc].Poll();
                    var datas = mouse[inc].GetBufferedData();
                    foreach (var state in datas)
                    {
                        if (inc == 0 & state.Offset == MouseOffset.X)
                            MouseAxisX = state.Value;
                        if (inc == 0 & state.Offset == MouseOffset.Y)
                            MouseAxisY = state.Value;
                        if (inc == 0 & state.Offset == MouseOffset.Z)
                            MouseAxisZ = state.Value;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons0 & state.Value == 128)
                            MouseButtons0 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons0 & state.Value == 0)
                            MouseButtons0 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons1 & state.Value == 128)
                            MouseButtons1 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons1 & state.Value == 0)
                            MouseButtons1 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons2 & state.Value == 128)
                            MouseButtons2 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons2 & state.Value == 0)
                            MouseButtons2 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons3 & state.Value == 128)
                            MouseButtons3 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons3 & state.Value == 0)
                            MouseButtons3 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons4 & state.Value == 128)
                            MouseButtons4 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons4 & state.Value == 0)
                            MouseButtons4 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons5 & state.Value == 128)
                            MouseButtons5 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons5 & state.Value == 0)
                            MouseButtons5 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons6 & state.Value == 128)
                            MouseButtons6 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons6 & state.Value == 0)
                            MouseButtons6 = false;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons7 & state.Value == 128)
                            MouseButtons7 = true;
                        if (inc == 0 & state.Offset == MouseOffset.Buttons7 & state.Value == 0)
                            MouseButtons7 = false;
                        string data = "number " + inc.ToString() + Environment.NewLine;
                        data += "state " + state.ToString() + Environment.NewLine;
                        data += "MouseAxisX " + MouseAxisX + Environment.NewLine;
                        data += "MouseAxisY " + MouseAxisY + Environment.NewLine;
                        data += "MouseAxisZ " + MouseAxisZ + Environment.NewLine;
                        data += "MouseButtons0 " + MouseButtons0 + Environment.NewLine;
                        data += "MouseButtons1 " + MouseButtons1 + Environment.NewLine;
                        data += "MouseButtons2 " + MouseButtons2 + Environment.NewLine;
                        data += "MouseButtons3 " + MouseButtons3 + Environment.NewLine;
                        data += "MouseButtons4 " + MouseButtons4 + Environment.NewLine;
                        data += "MouseButtons5 " + MouseButtons5 + Environment.NewLine;
                        data += "MouseButtons6 " + MouseButtons6 + Environment.NewLine;
                        data += "MouseButtons7 " + MouseButtons7 + Environment.NewLine;
                        this.label2.Text = data;
                        MouseAxisX = 0;
                        MouseAxisY = 0;
                        MouseAxisZ = 0;
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
