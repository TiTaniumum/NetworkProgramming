using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetHW4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            server = Server.getInstance(this);
        }
        Server server;
        public void buttonStart_Click(object sender,EventArgs e)
        {
            // TODO: start server.
        }
        public void buttonClose_Click(object sender,EventArgs e)
        {
            // TODO: stop Server, close Form.
        }
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.Text += message+"\r\n";
            });
        }
    }

    public class Server
    {
        private static Socket server = null;
        private static Server instance = null;
        private List<ServerSideClient> clients = null;
        private bool isOn = false;
        Form1 Form { get; set; }
        private Server(Form1 form)
        {
            this.Form=form;
            if (server == null) server = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
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
            while (isOn)
            {
                Socket client = server.Accept();
            }
        }
        public void Stop()
        {

        }
        public void Log(string message)
        {
            Form.Log(message);
        }
    }
    public class ServerSideClient
    {
        private Socket client;
        private Server server = Server.getInstance();
        public ServerSideClient(Socket client)
        {
            this.client = client;
            ThreadPool.QueueUserWorkItem(ClientProc);
        }
        private void ClientProc(object obj)
        {
            byte[] buffer = new byte[10*1024];
            string message = string.Empty;
            
            while (client.Connected)
            {
                message = GetMessage(buffer);
                server.Log(message);
                HandleRequest(message);
            }
        }
        private string GetMessage(byte[] buffer)
        {
            int size = client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private void HandleRequest(string message)
        {
            string command = message.Split(':')[0];
            string actualMessage = message.Split(':')[1];
            switch (command)
            {
                case "ALL":
                    //SendToEveryone(actualMessage);
                    break;
            }
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
    }
}