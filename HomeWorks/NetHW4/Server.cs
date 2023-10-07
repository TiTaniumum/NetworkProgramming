using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Deployment.Application;

namespace NetHW4
{
    public class Server
    {
        private static Socket server = null;
        private static Server instance = null;
        private List<ServerSideClient> clients = null;
        private bool isOn = false;
        private Form1 Form { get; set; }
        private Server(Form1 form)
        {
            this.Form=form;
            if (server == null) server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (clients == null) clients = new List<ServerSideClient>();
        }
        public static Server getInstance(Form1 form)
        {
            if (instance == null) instance = new Server(form);
            else instance.Form=form;
            return instance;
        }
        public static Server getInstance()
        {
            return instance;
        }
        public void Start(IPAddress ip, int port)
        {
            Log("Starting server...");
            IPEndPoint ipEP = new IPEndPoint(ip, port);
            server.Bind(ipEP);
            server.Listen(1000);
            Thread serverThread = new Thread(ServerProc);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void ServerProc()
        {
            isOn = true;
            Log("Server listening...");
            try
            {
                while (isOn)
                {
                    Socket client = server.Accept();
                    clients.Add(new ServerSideClient(client));
                    UpdateListView();
                }
            }catch(Exception ex)
            {
            }
        }
        public void Stop()
        {
            Log("Closing server...");
            isOn= false;
            server.Close();
        }
        public void Log(string message) => Form.Log(message);

        public void SendToEveryOne(string sender, string message)
        {
            Log(sender+" sent message to everyone");
            foreach (ServerSideClient client in clients)
            {
                if (client.IsConnected) client.Send("MESSAGE:"+sender+"- "+message);
            }
        }
        public void SendTo(string sender, string message, string receiver)
        {
            Log(sender+" sent message to "+ receiver);
            foreach (ServerSideClient client in clients)
            {
                if (client.name == receiver) client.Send("MESSAGE:"+sender+"- "+message);
            }
        }
        public string GetClientNames()
        {
            string names = string.Empty;
            foreach (ServerSideClient client in clients)
            {
                names += client.name+":";
            }
            if (names != string.Empty && names[names.Length-1]==':')
                names = names.Substring(0, names.Length-1);
            return names;
        }
        public void DisconnectClient(ServerSideClient client)
        {
            Log(client.name+" has disconnected.");
            clients.Remove(client);
            UpdateListView();
        }
        public void UpdateListView()
        {
            Form.UpdateListView(clients);
        }
    }
}
