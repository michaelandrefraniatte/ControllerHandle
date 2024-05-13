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
        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(e.KeyData);
        }
        static void OnKeyDown(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public static async Task<String> ShowDialog(string caption, string text, string record, List<string> listrecords)
        {
            return await Task.Run(() =>
            {
                Form prompt = new Form();
                prompt.Width = 280;
                prompt.Height = 120;
                prompt.Text = caption;
                Label textLabel = new Label() { Left = 16, Top = 20, Width = 240, Text = text };
                ComboBox cmbx = new ComboBox() { Left = 112, Top = 44, Width = 140, Text = record };
                foreach (string listrecord in listrecords)
                {
                    cmbx.Items.Add(listrecord);
                }
                Button confirmation = new Button() { Text = "Confirm!", Left = 16, Width = 80, Top = 44, TabIndex = 1, TabStop = true };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(cmbx);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.ShowDialog();
                return string.Format(cmbx.Text);
            });
        }
    }
}