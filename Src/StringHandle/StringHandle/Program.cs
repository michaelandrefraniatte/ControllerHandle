using System;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Management;
using System.Windows.Forms;

namespace StringHandle
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
        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);
        private static Type program;
        private static object obj;
        private static Assembly assembly;
        private static System.CodeDom.Compiler.CompilerResults results;
        private static Microsoft.CSharp.CSharpCodeProvider provider;
        private static System.CodeDom.Compiler.CompilerParameters parameters;
        private static string code = @"
                using System;
                using System.Globalization;
                using System.IO;
                using System.Numerics;
                using System.Runtime.InteropServices;
                using System.Threading;
                using System.Threading.Tasks;
                using System.Windows;
                using System.Windows.Forms;
                using System.Reflection;
                namespace StringToCode
                {
                    public class FooClass 
                    { 
                        [DllImport(""winmm.dll"", EntryPoint = ""timeBeginPeriod"")]
                        private static extern uint TimeBeginPeriod(uint ms);
                        [DllImport(""winmm.dll"", EntryPoint = ""timeEndPeriod"")]
                        private static extern uint TimeEndPeriod(uint ms);
                        [DllImport(""ntdll.dll"", EntryPoint = ""NtSetTimerResolution"")]
                        private static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
                        private static uint CurrentResolution = 0;
                        public void Load()
                        {
                            TimeBeginPeriod(1);
                            NtSetTimerResolution(1, true, ref CurrentResolution);
                            Task.Run(() => Start());
                        }
                        private void Start()
                        {
                            DooClass dooclass = new DooClass();
                        }
                        public void Close()
                        {
                        }
                    }
                    public class DooClass 
                    { 
                        public DooClass()
                        {
                        }
                    }
                }";
        static void Main(string[] args)
        {
            if (GetMAC() == "D88083575B35" & GetUniqueID() == "44F54C10BFEBFBFF000806C132444335-3631-3735-4332-323736314435")
            {
                handler = new ConsoleEventDelegate(ConsoleEventCallback);
                SetConsoleCtrlHandler(handler, true);
                parameters = new System.CodeDom.Compiler.CompilerParameters();
                parameters.GenerateExecutable = false;
                parameters.GenerateInMemory = true;
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                provider = new Microsoft.CSharp.CSharpCodeProvider();
                results = provider.CompileAssemblyFromSource(parameters, code);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (System.CodeDom.Compiler.CompilerError error in results.Errors)
                    {
                        sb.AppendLine(String.Format("Error ({0}) : {1}", error.ErrorNumber, error.ErrorText));
                    }
                    MessageBox.Show("Script Error :\n\r" + sb.ToString());
                    return;
                }
                assembly = results.CompiledAssembly;
                program = assembly.GetType("StringToCode.FooClass");
                obj = Activator.CreateInstance(program);
                program.InvokeMember("Load", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { });
                Console.Read();
            }
        }
        static string GetMAC()
        {
            String firstMacAddress = System.Net.NetworkInformation.NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && nic.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            return firstMacAddress;
        }
        public static string GetUniqueID()
        {
            try
            {
                string cpuInfo = string.Empty;
                ManagementClass mc = new ManagementClass("win32_processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
                string drive = "C";
                ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
                dsk.Get();
                string volumeSerial = dsk["VolumeSerialNumber"].ToString();
                string uuidInfo = string.Empty;
                ManagementClass mcu = new ManagementClass("Win32_ComputerSystemProduct");
                ManagementObjectCollection mocu = mcu.GetInstances();
                foreach (ManagementObject mou in mocu)
                {
                    uuidInfo = mou.Properties["UUID"].Value.ToString();
                    break;
                }
                if (volumeSerial != null & volumeSerial != "" & cpuInfo != null & cpuInfo != "" & uuidInfo != null & uuidInfo != "")
                    return volumeSerial + cpuInfo + uuidInfo;
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Thread close;
                close = new Thread(new ThreadStart(threadclose));
                close.Start();
            }
            return false;
        }
        static void threadclose()
        {
            program.InvokeMember("Close", BindingFlags.Default | BindingFlags.InvokeMethod, null, obj, new object[] { });
        }
    }
}