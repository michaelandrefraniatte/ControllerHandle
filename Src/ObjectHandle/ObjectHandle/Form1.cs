using Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectHandle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int i = 0;
        Class1 obj1 = new Class1();
        Class1 obj2 = new Class1();
        private void button1_Click(object sender, EventArgs e)
        {
            i++;
            obj1.AddToList(i);
            obj2.AddToList(i);
            obj1.AddToStaticList(i);
            obj2.AddToStaticList(i);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            obj1.ShowList();
            obj2.ShowList();
            obj1.ShowStaticList();
            obj2.ShowStaticList();
        }
    }
}