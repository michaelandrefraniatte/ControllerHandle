using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Gaming.Input;
using Windows.Xbox.Input;

namespace GameInputSharp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent(); 
            var controllers = RawGameController.RawGameControllers;
            foreach (var item in controllers)
            {
                var name = item.DisplayName;
            }
        }
    }
}