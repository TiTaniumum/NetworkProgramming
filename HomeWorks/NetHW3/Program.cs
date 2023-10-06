using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Diagnostics;

namespace NetHW1
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            PrepareThreads();
            Server server = new Server();
            server.StartServer();
            server.ManageServer();
        }
        public static void PrepareThreads()
        {
            ThreadPool.SetMaxThreads(100, 100);
            ThreadPool.SetMinThreads(100, 100);
        }
    }
    public class Server
    {
        static Socket server = null;
        static bool isOn = false;
        Thread serverThread = null;
        public Server()
        {
            if (server != null) return;
            Init();
        }
        private void Init(int port = 63997)
        {
            IPEndPoint ipEP = new IPEndPoint(IPAddress.Any, port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipEP);
        }
        public void StartServer()
        {
            Console.WriteLine("Starting Server....");
            if (server == null) Init();
            if (isOn)
            {
                Console.WriteLine("Server is already running!");
                return;
            }
            isOn = true;
            serverThread = new Thread(ServerProc);
            serverThread.IsBackground = true;
            serverThread.Start(server);
        }
        public void StopServer()
        {
            Console.WriteLine("Shutting down server....");
            isOn = false;
            //Я хотел прервать Accept() что бы выключить сервер. но этот метод уже не поддерживается.
            //serverThread.Abort();
            server.Close();
            server = null;
            serverThread.Join();
            Console.WriteLine("Server has Stopped!");
        }
        public void ManageServer()
        {
            Console.WriteLine("To Stop Server Write \'STOP\'; To get Info about Server \'INFO\'");
            while (isOn)
            {
                switch (Console.ReadLine())
                {
                    case "stop":
                    case "STOP":
                        StopServer();
                        break;
                    case "INFO":
                    case "info":
                        Console.WriteLine("SERVER INFO:");
                        Console.WriteLine("Connected: " + server.Connected.ToString());
                        Console.WriteLine("Address family: "+server.AddressFamily.ToString());
                        Console.WriteLine("Remote end point: "+server.RemoteEndPoint?.ToString());
                        Console.WriteLine("Socket type: "+server.SocketType.ToString());
                        Console.WriteLine("Protocol type: "+server.ProtocolType.ToString());
                        break;
                    case "?":
                    case "help":
                    case "HELP":
                    default:
                        Console.WriteLine("To Stop Server Write \'STOP\'; To get Info about Server 'INFO'");
                        break;
                }
            }
        }

        private void ServerProc(object serverObj)
        {
            Socket server = serverObj as Socket;
            server.Listen(1000);
            Console.WriteLine("Server: " + server.LocalEndPoint.ToString());
            Console.WriteLine("Starting to Listen...");
            try
            {
                while (isOn)
                {
                    Socket client = server.Accept();
                    Console.WriteLine("Client Accepted");
                    ServerSideClient serverSideClient = new ServerSideClient(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
            finally
            {
                isOn = false;
            }
            Console.WriteLine("Server is Shutting Down");
        }
    }
    public class ServerSideClient
    {
        Socket client;
        public ServerSideClient(Socket client)
        {
            this.client = client;
            ThreadPool.QueueUserWorkItem(ClientProc, client);
        }
        private void ClientProc(object clientObj)
        {
            Socket client = clientObj as Socket;
            string EP = client.RemoteEndPoint.ToString();
            byte[] buffer = new byte[1024];
            int size;
            string message = "[1] - EURO-USD, [2] - USD-EURO, [3] - Disconnect, [4] - Info";
            Console.WriteLine("Client connected: " + client.LocalEndPoint.ToString() + " => " + client.RemoteEndPoint.ToString());
            client.Send(Encoding.UTF8.GetBytes(message));
            while (client.Connected)
            {
                size = client.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, size);
                Console.WriteLine("Received message " + message + " from " + client.RemoteEndPoint.ToString());
                switch (message)
                {
                    case "1":
                        message = "1 eur - 1.05 usd";
                        Send(message);
                        break;
                    case "2":
                        message = "1 usd - 0.95 eur";
                        Send(message);
                        break;
                    case "3":
                        client.Close();
                        //Send("Disconnecting!");
                        break;
                    case "4":
                        Send("[1] - EURO-USD, [2] - USD-EURO, [3] - Disconnect, [4] - Info");
                        break;
                    default:
                        Send("Wrong input!");
                        break;
                }
            }
            Console.WriteLine(EP + " Has disconnected!");
            //client.Close();
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
}