using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public class Client
    {
        private Socket client;
        private IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 63997);
        public string message = string.Empty;
        public static Form1 form = null;
        ManualResetEvent newConnect = new ManualResetEvent(false);
        public Client()
        {
            Init();
        }
        public void Init() => client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        
        public void Monitoring()
        {
            while (true)
            {
                Connect();
                newConnect.WaitOne();
                newConnect.Reset();
            }
        }
        public void Connect()
        {
            if (client == null) Init();
            while (!client.Connected)
            {
                try
                {
                    client.Connect(serverEP);
                }
                catch (Exception ex) { 
                    
                }
                Thread.Sleep(5000);
            }
            Thread clientThread = new Thread(ClientProc);
            clientThread.IsBackground = true;
            clientThread.Start();
        }
        private void ClientProc()
        {
            Log("Connected....");
            try
            {
                Thread.Sleep(3000);
                //SendScanInfo();
                SendPCInfo();
                while (true)
                {
                    SendMessage();
                    SendScreenShot();
                    Thread.Sleep(10000);
                }
            }catch (Exception ex)
            {

            }
            client.Close();
            client = null;
            newConnect.Set();
        }
        private void SendMessage()
        {
            if (message == string.Empty) return;
            Send(message);
            message = string.Empty;
        }  
        private void SendScreenShot()
        {
            string path = "ScreenShot.jpg";
            ScreenShooter.MakeAScreenShot(path);
            byte[] buffer = File.ReadAllBytes(path);
            Send("*");
            client.Send(buffer);
        }
        private void SendPCInfo()
        {
            Send(PC.GetSystemInformation());
        }
        // ALERT SHIT CODE !!!
        //private void SendScanInfo()
        //{
        //    string[] important = PC.Scan();
        //    if (important == null || important.Length == 0) return;
        //    Send(string.Join("\r\n|", important));
        //}
        public void UppendMessage(string message)=>this.message += message; 
        public void Log(string message) => form?.Log(message);
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        public void Send(string message) => client.Send(Prepare(message));
    }
}
