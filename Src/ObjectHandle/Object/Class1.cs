using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Object
{
    public class Class1
    {
        public static List<int> staticlist = new List<int>();
        public List<int> list = new List<int>();
        public void ShowStaticList()
        {
            foreach (int i in staticlist)
            {
                MessageBox.Show("From static list: " + i.ToString());
            }
        }
        public void AddToStaticList(int i)
        {
            staticlist.Add(i);
        }
        public void ShowList()
        {
            foreach (int i in list)
            {
                MessageBox.Show("From list: " + i.ToString());
            }
        }
        public void AddToList(int i)
        {
            list.Add(i);
        }
    }
}