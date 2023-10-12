using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public class AutoReg
    {
        public static void Reg()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            Registry.SetValue(key.Name, "NetExam", path+" silent");
        }
    }
}
