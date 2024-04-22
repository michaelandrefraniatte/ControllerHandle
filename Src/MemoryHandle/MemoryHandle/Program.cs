using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MemoryHandle
{
    internal class Program
    {
        static void OnKeyDown(Keys keyData)
        {
            if (keyData == Keys.F1)
            {
                const string message = "• Author: Michaël André Franiatte.\n\r\n\r• Contact: michael.franiatte@gmail.com.\n\r\n\r• Publisher: https://github.com/michaelandrefraniatte.\n\r\n\r• Copyrights: All rights reserved, no permissions granted.\n\r\n\r• License: Not open source, not free of charge to use.";
                const string caption = "About";
                MessageBox.Show(message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        static void Main(string[] args)
        {
            try
            {
                string filePath = "MemoryHandleTest.exe";
                // read the bytes from the application exe file
                FileStream fs = new FileStream(filePath, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                byte[] bin = br.ReadBytes(Convert.ToInt32(fs.Length));
                fs.Close();
                br.Close();
                // load the bytes into Assembly
                Assembly a = Assembly.Load(bin);
                // search for the Entry Point
                MethodInfo method = a.EntryPoint;
                if (method != null)
                {
                    // create an istance of the Startup form Main method
                    object o = a.CreateInstance(method.Name);
                    // if the method is static as it is in HelloWorld.exe you can use null
                    method.Invoke(null, new object[] { args });
                    // if not static you'll need the object
                    //method.Invoke(o, new object[]{args});
                }
            }
            catch (Exception exception) 
            { 
                Console.WriteLine(exception.ToString());
                Console.ReadLine();
            }
        }
    }
}