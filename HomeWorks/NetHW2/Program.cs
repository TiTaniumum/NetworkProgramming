using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;
using System.Net.Security;
using System.Net.Http.Headers;
using System.Diagnostics;
using NetHW2;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;

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
        static HttpClient client = new HttpClient();
        public static async Task<APIData> GetDataAsync(string path)
        {
            APIData data = null;
            
            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(json);
                data = JsonConvert.DeserializeObject<APIData>(json);
            }
            return data;
        }
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
            server.Close();
            server = null;
            serverThread.Join();
            Console.WriteLine("Server has Stopped!");
        }
        public void ManageServer()
        {
            Console.WriteLine("To Stop Server Write \'STOP\'; To get Info about Server \'INFO\'; \'TEST\' to test output");
            while (isOn)
            {
                switch (Console.ReadLine())
                {
                    case "TEST":
                        Console.WriteLine(GetResponse("010000"));
                        break;
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
        private string GetResponse(string postalCode)
        {
            string uri = $"https://api.post.kz/api/byOldPostcode/{postalCode}?from=0";
            string result = string.Empty;
            Task<APIData> task = GetDataAsync(uri);
            task.Wait();
            Console.WriteLine("Wait completed");
            APIData apiData = task.Result;
            if (apiData == null) {
                Console.WriteLine("Something went wrong!");
                return result;
            }
            Console.WriteLine(apiData.ToString());
            if (apiData.Total == null) Console.WriteLine("Total = null");
            if (apiData.From == null) Console.WriteLine("From = null");
            if(apiData.Data == null)
            {
                Console.WriteLine("Data = null");
                return result;
            }
            foreach (var item in apiData.Data)
            {
                result += item.Address +"\r\n";
            }
            return result;
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
            string message = "[1] - Disconnect, [Any other input] - Send Post code! Input example: \'010000\'";
            Console.WriteLine("Client connected: " + client.LocalEndPoint.ToString() + " => " + client.RemoteEndPoint.ToString());
            Send(message);
            while (client.Connected)
            {
                size = client.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, size);
                Console.WriteLine("Received message " + message + " from " + client.RemoteEndPoint.ToString());
                switch (message)
                {
                    case "1":
                        client.Close();
                        //Send("Disconnecting!");
                        break;
                    case "2":
                        Send("[1] - Disconnect, [Any other input] - Send post code! Input example: '010000'");
                        break;
                    default:
                        Send(GetResponse(message));
                        Console.WriteLine("Sent message to Client");
                        break;
                }
            }
            Console.WriteLine(EP + " Has disconnected!");
            //client.Close();
        }
        private string GetResponse(string postalCode)
        {
            string uri = $"https://api.post.kz/api/byOldPostcode/{postalCode}?from=0";
            string result = string.Empty;
            Task<APIData> task = Server.GetDataAsync(uri);
            task.Wait();
            Console.WriteLine("Wait completed");
            APIData apiData = task.Result;
            if (apiData == null)
            {
                Console.WriteLine("Something went wrong!");
                return result;
            }
            Console.WriteLine(apiData.ToString());
            if (apiData.Total == null) Console.WriteLine("Total = null");
            if (apiData.From == null) Console.WriteLine("From = null");
            if (apiData.Data == null)
            {
                Console.WriteLine("Data = null");
                return result;
            }
            //Console.WriteLine(apiData.Data[0].Address);
            foreach (var item in apiData.Data)
            {
                result += item.Address +"\r\n";
            }
            return result;
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
}