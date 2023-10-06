using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;


namespace NetHW1_1
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            Client client = new Client();
            client.Start();
        }
    }
    public class Client
    {
        static Socket client = null;
        IPEndPoint ipEP = null;
        bool isConnected = false;
        public Client()
        {
            if (client == null) return;
            Init();
        }
        private void Init(int port = 63997)
        {
            if (ipEP == null)
                ipEP = new IPEndPoint(IPAddress.Loopback, 63997);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        private void Connect()
        {
            if (client == null) Init();
            if (client.Connected)
            {
                Console.WriteLine("You are already connected!");
                return;
            }
            Console.WriteLine("Connecting to server...");
            try
            {
                for (int i = 0; i < 3 && !client.Connected; i++, Thread.Sleep(1000))
                    client.Connect(ipEP);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to connect: {ex.Message}");
                return;
            }

            if (!client.Connected)
            {
                Console.WriteLine("Connection failed...");
                return;
            }
            isConnected = true;
            Thread listenerThread = new Thread(ListenProc);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }
        public void Start()
        {
            Connect();
            string message = string.Empty;
            if (client.Connected) Console.WriteLine("Connection successfull!");
            Console.WriteLine("COMMANDS: \'STOP\', \'CONNECT\', \'HELP\';");
            Console.WriteLine("ANY OTHER INPUT WILL BE SENT TO SERVER.");
            while (message != "STOP")
            {
                message = ""+Console.ReadLine();
                switch (message)
                {
                    case "CONNECT":
                        Connect();
                        break;
                    case "stop":
                    case "STOP":
                    case "3":
                        if (isConnected)
                        {
                            Send("3");
                            isConnected = false;
                            client.Disconnect(false);
                            ShutDown();
                        }
                        break;
                    case "?":
                    case "help":
                    case "HELP":
                        Console.WriteLine("COMMANDS: \'STOP\', \'CONNECT\', \'HELP\';");
                        Console.WriteLine("ANY OTHER INPUT WILL BE SENT TO SERVER.");
                        break;
                    default:
                        if (client.Connected) Send(message);
                        break;
                }
            }
            Console.WriteLine("Shutting down client...");
            ShutDown();
        }
        private void ListenProc(object obj)
        {
            byte[] buffer = new byte[1024];
            int size;
            try
            {
                while (isConnected)
                {
                    size = client.Receive(buffer);
                    Console.WriteLine("SERVER: " + Encoding.UTF8.GetString(buffer, 0, size));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                ShutDown();
            }
            Console.WriteLine("Disconnected.");
        }
        private void ShutDown()
        {
            Thread.Sleep(100);
            isConnected = false;
            client?.Close();
            client = null;
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
}