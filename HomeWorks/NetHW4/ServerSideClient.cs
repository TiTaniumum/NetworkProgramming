using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetHW4
{
    public class ServerSideClient
    {
        private Socket client;
        private Server server = Server.getInstance();
        public string name;
        public bool IsConnected { get { return client.Connected; } }
        public string RemoteEndPoint { get { return client.RemoteEndPoint.ToString(); } }
        public ServerSideClient(Socket client)
        {
            this.client = client;
            ThreadPool.QueueUserWorkItem(ClientProc);
        }
        private void ClientProc(object obj)
        {
            server.Log(client.RemoteEndPoint.ToString()+" has connected.");

            byte[] buffer = new byte[10*1024];
            string message = string.Empty;

            GetName(buffer);
            server.UpdateListView();

            server.Log(client.RemoteEndPoint.ToString()+" sent his name: " +name);

            while (client.Connected)
            {
                try
                {
                    message = GetMessage(buffer);
                    if (message.Length == 0) break;
                    server.Log(message);
                    HandleRequest(message);
                }catch(Exception ex)
                {
                    server.Log(ex.Message);
                }
            }
            client.Close();
            server.DisconnectClient(this);
        }
        private void GetName(byte[] buffer) => name = GetMessage(buffer);

        private string GetMessage(byte[] buffer)
        {
            int size = client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private void HandleRequest(string message)
        {
            string[] splitedMessage = message.Split(':');
            string command = splitedMessage[0];
            string actualMessage = string.Join(":", splitedMessage, 1, splitedMessage.Length-2);
            string receiver = splitedMessage[splitedMessage.Length-1];
            switch (command)
            {
                case "ALL":
                    server.SendToEveryOne(name, actualMessage);
                    break;
                case "TO":
                    server.SendTo(name, actualMessage, receiver);
                    break;
                case "GET":
                    server.Log(name+" requested client names");
                    Send("CLIENTS:"+server.GetClientNames());
                    break;
            }
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        public void Send(string message) => client.Send(Prepare(message));
    }
}
