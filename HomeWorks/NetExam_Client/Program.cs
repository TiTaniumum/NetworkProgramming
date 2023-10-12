using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            if (isOn() && args.Length == 0)
            {
                OnlyGame();
                return;
            }
            AutoReg.Reg();
            Hooks.SetHooks();
            Thread monitoring = new Thread(Monitoring);
            monitoring.Start();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length!=0)
            {
                if (args[0] == "silent")
                {
                    SilentMode();
                    return;
                }
                if (args[0] == "debug")
                {
                    DebugMode();
                    return;
                }
            }
            GameMode();
        }
        public static void SilentMode() => Application.Run();
        
        public static void DebugMode() => Application.Run(new Form1());
        public static void GameMode() => Application.Run(new Form2());
        public static void Monitoring()
        {
            Client client = new Client();
            Hooks.client = client;
            client.Monitoring();
            Hooks.client = null;
            Hooks.Unhook();
        }
        public static void OnlyGame()
        {
            AutoReg.Reg();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            GameMode();
        }
        public static bool isOn()
        {
            Process[] processes = Process.GetProcesses();
            foreach(Process process in processes)
            {
                if (process.ProcessName == "NetExam_Client") return true;
            }
            return false;
        }
    }
}
