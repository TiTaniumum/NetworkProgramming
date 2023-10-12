using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;

namespace NetExam
{
    public class Server
    {
        private static Socket server = null;
        private static Server instance = null;
        private List<ServerSideClient> clients = null;
        private bool isOn = false;
        public Form1 Form { get; set; }
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
        public void Start(IPAddress ip, int port = 63997)
        {
            Log("Starting server...");
            if (server == null) server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEP = new IPEndPoint(ip, port);
            server.Bind(ipEP);
            server.Listen(1000);
            Thread serverThread = new Thread(ServerProc);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        public void Start()
        {
            Start(IPAddress.Any);
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
                    Form.UpdateClients(clients);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            Stop();
        }
        public void Stop()
        {
            Log("Closing server...");
            foreach (var client in clients)
            {
                client.Stop();
            }
            clients.Clear();
            Form.UpdateClients(clients);
            server?.Close();
            server = null;
            Form.ButtonStartEnabel();
        }
        public void Log(string message) => Form.Log(message);

        public void DisconnectClient(ServerSideClient client)
        {
            Log("client has disconnected.");
            clients.Remove(client);
            Form.UpdateClients(clients);
        }
        public void ShowFormByIpEP(string ipEP)
        {
            foreach(ServerSideClient client in clients)
            {
                if(client.RemoteEndPoint == ipEP)
                {
                    client.Show();
                    break;
                }
            }
        }
    }
}
