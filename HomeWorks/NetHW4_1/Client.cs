using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetHW4_1
{
    public class Client
    {
        private TcpClient client;
        private Form1 form;
        public string Name { get { return form.ClientName; } }
        public bool IsConnected { get { return client.Connected; } }
        public Client(Form1 form)
        {
            this.form = form;
            client = new TcpClient();
        }
        public void Connect()
        {
            if (client.Connected) return;
            form.Log("Connecting....");
            (string ip, string port) = form.GetAddressPort();
            try
            {
                int p = int.Parse(port);
                client.Connect(ip, p);
                Send(Name);
                Thread clientThread = new Thread(ClientProc);
                clientThread.IsBackground = true;
                clientThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ClientProc()
        {
            form.Log("Connected.");
            byte[] buffer = new byte[10*1024];
            string message = string.Empty;
            try
            {
                while (client.Connected)
                {
                    message = GetMessage(buffer);
                    HandleMessage(message);
                }
            }
            catch (Exception ex)
            {
                form.Log(ex.Message);
            }
            Close();
        }
        private string GetMessage(byte[] buffer)
        {
            int size = client.Client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private void HandleMessage(string message)
        {
            string[] separatedMessage = message.Split(':');
            string command = separatedMessage[0];
            string actualMessage = separatedMessage[1];
            switch (command)
            {
                case "MESSAGE":
                    form.Log(actualMessage);
                    break;
                case "CLIENTS":
                    form.UpdateClients(separatedMessage);
                    break;
            }
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        public void Send(string message) => client.Client.Send(Prepare(message));
        public void GetClients()=>Send("GET: : ");
        public void Close()
        {
            form.Log("Disconnecting...");
            client.Close();
            client = new TcpClient();
        }
    }
}
