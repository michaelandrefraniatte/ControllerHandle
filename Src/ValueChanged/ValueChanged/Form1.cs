using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace ValueChanged
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public valuechanged ValueChanged = new valuechanged();
        private void button1_Click(object sender, EventArgs e)
        {
            double number = Convert.ToSingle(textBox1.Text);
            ValueChanged[0] = number; // Output
            MessageBox.Show(valuechanged._ValueChanged[0].ToString()); // Input
        }
    }
    public class valuechanged
    {
        public static double[] _valuechanged = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public static double[] _ValueChanged = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
        public double this[int index]
        {
            get { return _ValueChanged[index]; }
            set
            {
                if (_valuechanged[index] != value)
                    _ValueChanged[index] = value - _valuechanged[index];
                else
                    _ValueChanged[index] = 0;
                _valuechanged[index] = value;
            }
        }
    }
}