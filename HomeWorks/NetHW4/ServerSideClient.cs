using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

            server.Log(client.RemoteEndPoint.ToString()+" sent his name: " +name);

            while (client.Connected)
            {
                message = GetMessage(buffer);
                server.Log(message);
                HandleRequest(message);
            }
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
            string actualMessage = splitedMessage[1];
            string receiver = splitedMessage[2];
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
