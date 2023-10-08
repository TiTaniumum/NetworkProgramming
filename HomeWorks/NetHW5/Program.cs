using System;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace NetHW5
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            RockPaperScissors game = new RockPaperScissors();
            game.Start();
        }
    }
    public class RockPaperScissors
    {
        public Server server;
        public Client client;
        public string MeDecision = string.Empty;
        public string EnemyDecision = string.Empty;
        public RockPaperScissors()
        {
            server = new Server(this);
            client = new Client(this);
        }
        public void Start()
        {
            string command = string.Empty;
            while (command != "EXIT")
            {
                PrintMenu();
                command = Console.ReadLine();
                switch (command)
                {
                    case "HOSTGAME": 
                        server.Start();
                        break;
                    case "CONNECT":
                        client.Start();
                        break;
                    case "EXIT": break;
                    default: break;
                }
            }
        }
        private void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Write commands:");
            Console.WriteLine("HOSTGAME");
            Console.WriteLine("CONNECT");
            Console.WriteLine("EXIT");
            Console.WriteLine();
            Console.Write("COMMAND: ");
        }
        public RPSResult GetResult()
        {
            if(MeDecision.Length==EnemyDecision.Length) { return RPSResult.Draw; }
            if(MeDecision == "ROCK")
            {
                if (EnemyDecision == "SCISSOR") return RPSResult.Win;
                return RPSResult.Loose;
            }
            if(MeDecision == "PAPER")
            {
                if (EnemyDecision == "ROCK") return RPSResult.Win;
                return RPSResult.Loose;
            }
            if(MeDecision == "SCISSOR")
            {
                if (EnemyDecision == "PAPER") return RPSResult.Win;
                return RPSResult.Loose;
            }
            return RPSResult.Draw;
        }
        public void PrintResult(string result)
        {
            Console.WriteLine("ROCK!");
            Thread.Sleep(1000);
            Console.WriteLine("PAPER!");
            Thread.Sleep(1000);
            Console.WriteLine("SCISSOR!");
            Thread.Sleep(1000);
            Console.WriteLine(result);
        }
        public void PrintResult()
        {
            Console.WriteLine("ROCK!");
            Thread.Sleep(1000);
            Console.WriteLine("PAPER!");
            Thread.Sleep(1000);
            Console.WriteLine("SCISSOR!");
            Thread.Sleep(1000);
            Console.WriteLine($"You - {MeDecision} : Opponent - {EnemyDecision} : Result - {GetResult()}");
        }
        public string GetResultForOponent()
        {
            RPSResult result = RPSResult.Draw;
            if (GetResult()==RPSResult.Win) result = RPSResult.Loose;
            else if(GetResult()==RPSResult.Loose) result = RPSResult.Win;
            return $"You - {EnemyDecision} : Opponent - {MeDecision} : Result - {result}";
        }
    }
    public enum RPSResult
    {
        Draw=1,
        Win,
        Loose,
    }
    public class Server
    {
        int port = 63997;
        IPAddress ip = IPAddress.Any;
        IPEndPoint ipEP;
        private RockPaperScissors game;
        private ManualResetEvent EnemyDecision = new ManualResetEvent(false);
        private ManualResetEvent MeDesicion = new ManualResetEvent(false);
        public Server(RockPaperScissors game)
        {
            ipEP = new IPEndPoint(ip, port);
            this.game=game;
        }
        Socket server;
        Socket client;
        public void Start()
        {
            StartServer();
            StartGame();
        }
        private void StartGame()
        {
            string command = string.Empty;
            Console.WriteLine("Write: ROCK, PAPER, SCISSOR");
            Console.WriteLine("EXIT to exit");
            while (command!= "EXIT")
            {
                command = Console.ReadLine();
                HandleCommand(command);
            }
        }
        private void HandleCommand(string command)
        {
            switch (command)
            {
                case "ROCK":
                case "PAPER":
                case "SCISSOR":
                    game.MeDecision = command;
                    WaitResult();
                    break;
                case "EXIT":
                    Stop();
                    break;
                default:
                    Console.WriteLine("Wrong input. Try again.");
                    break;
            }
        }
        private void Stop()
        {
            client.Close();
            server.Close();
        }
        private void WaitResult()
        {
            EnemyDecision.WaitOne();
            EnemyDecision.Reset();
            MeDesicion.Set();
            game.PrintResult();
        }
        
        private void StartServer()
        {
            Console.Clear();
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(ipEP);
            server.Listen(1);
            Console.WriteLine("Waiting opponent...");
            client = server.Accept();
            Thread clientThread = new Thread(ClientProc);
            clientThread.IsBackground = true;
            clientThread.Start();
            Console.WriteLine("Opponent found!");
        }
        private void ClientProc()
        {
            byte[] buffer = new byte[10*1024];
            string message = string.Empty;
            while (client.Connected)
            {
                try
                {
                    message = GetMessage(buffer);
                    if (message.Length == 0) break;
                    HandleMessage(message);
                }catch (Exception ex) {
                    Console.WriteLine(ex.ToString());
                }
            }
            client.Close();
        }
        private void HandleMessage(string message)
        {
            game.EnemyDecision = message;
            EnemyDecision.Set();
            MeDesicion.WaitOne();
            MeDesicion.Reset();
            Send(game.GetResultForOponent());
        }
        private string GetMessage(byte[] buffer)
        {
            int size = client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
    public class Client
    {
        int port = 63997;
        IPAddress ip = IPAddress.Loopback;
        RockPaperScissors game;
        Socket client;
        ManualResetEvent waitOpponent = new ManualResetEvent(false);
        public Client(RockPaperScissors game)
        {
            this.game = game;
        }
        public void Start()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client.Connect(ip, port);
            if (!client.Connected)
            {
                Console.WriteLine("Could not connect!");
                return;
            }
            Thread clientThread = new Thread(ClientProc);
            clientThread.IsBackground = true;
            clientThread.Start();
            StartGame();
        }
        private void StartGame()
        {
            string command = string.Empty;
            Console.WriteLine("Write: ROCK, PAPER, SCISSOR");
            Console.WriteLine("EXIT to exit");
            while (command!= "EXIT")
            {
                command = Console.ReadLine();
                HandleCommand(command);
            }
        }
        private void HandleCommand(string command)
        {
            switch (command)
            {
                case "ROCK":
                case "PAPER":
                case "SCISSOR":
                    Send(command);
                    waitOpponent.WaitOne();
                    waitOpponent.Reset();
                    break;
                case "EXIT":
                    Stop();
                    break;
                default:
                    Console.WriteLine("Wrong input. Try again.");
                    break;
            }
        }
        private void Stop() {
            client.Close();
        }
        private void ClientProc()
        {
            byte[] buffer = new byte[10*1024];
            string message = string.Empty;
            while (client.Connected)
            {
                try
                {
                    message = GetMessage(buffer);
                    if (message.Length == 0) break;
                    HandleMessage(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            client.Close();
        }
        private void HandleMessage(string message)
        {
            game.PrintResult(message);
            waitOpponent.Set();
        }
        private string GetMessage(byte[] buffer)
        {
            int size = client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
}