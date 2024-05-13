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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace PromptHandle
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }
        public static async Task<String> ShowDialog(string caption, string text, string selStr, string record, List<string> listrecords)
        {
            return await Task.Run(() =>
            {
                Form prompt = new Form();
                prompt.Width = 280;
                prompt.Height = 160;
                prompt.Text = caption;
                Label textLabel = new Label() { Left = 16, Top = 20, Width = 240, Text = text };
                TextBox textBox = new TextBox() { Left = 16, Top = 40, Width = 240, TabIndex = 0, TabStop = true };
                textBox.Text = record;
                Label selLabel = new Label() { Left = 16, Top = 66, Width = 88, Text = selStr };
                ComboBox cmbx = new ComboBox() { Left = 112, Top = 64, Width = 144 };
                cmbx.Text = "";
                foreach (string listrecord in listrecords)
                {
                    cmbx.Items.Add(listrecord);
                }
                Button confirmation = new Button() { Text = "Confirm!", Left = 16, Width = 80, Top = 88, TabIndex = 1, TabStop = true };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(selLabel);
                prompt.Controls.Add(cmbx);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.ShowDialog();
                return string.Format(cmbx.Text != "" ? cmbx.Text : textBox.Text);
            });
        }
    }
}