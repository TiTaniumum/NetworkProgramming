using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Remoting;

namespace NetHW2_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            client = new Client(this);
        }
        Client client;
        private void buttonConnect_Click(object sender, EventArgs e)
        {
            client.Connect();
        }
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.Text += message + "\r\n";
            });
        }

        private void buttonRequest_Click(object sender, EventArgs e)
        {
            if (textBoxInput.Text == "1") client.ShutDown();
            client.SendRequest(textBoxInput.Text);
        }

        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            client.ShutDown();
        }
    }
    public class Client
    {
        static Socket client = null;
        IPEndPoint ipEP = null;
        public bool isConnected = false;
        Form1 form = null;
        public Client(Form1 form)
        {
            if (client != null) return;
            Init();
            this.form=form;
        }
        private void Init(int port = 63997)
        {
            if (ipEP == null)
                ipEP = new IPEndPoint(IPAddress.Loopback, 63997);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        public void Connect()
        {
            if (client == null) Init();
            if (client.Connected)
            {
                form.Log("You are already connected!");
                return;
            }
            form.Log("Connecting to server...");
            try
            {
                for (int i = 0; i < 3 && !client.Connected; i++, Thread.Sleep(1000))
                    client.Connect(ipEP);
            }
            catch (Exception ex)
            {
                form.Log($"Failed to connect: {ex.Message}");
                return;
            }

            if (!client.Connected)
            {
                form.Log("Connection failed...");
                return;
            }
            isConnected = true;
            Thread listenerThread = new Thread(ListenProc);
            listenerThread.IsBackground = true;
            listenerThread.Start();
        }

        private void ListenProc(object obj)
        {
            byte[] buffer = new byte[10*1024];
            int size;
            try
            {
                while (isConnected)
                {
                    size = client.Receive(buffer);
                    form.Log("SERVER: " + Encoding.UTF8.GetString(buffer, 0, size));
                }
            }
            catch (Exception ex)
            {
                form.Log(ex.Message);
            }
            finally
            {
                ShutDown();
            }
            form.Log("Disconnected.");
        }
        public void ShutDown()
        {
            if (isConnected)
            {
                Send("1");
                isConnected = false;
                client.Disconnect(false);
                Thread.Sleep(100);
                client?.Close();
                client = null;
            }
        }
        private byte[] Prepare(string message) => Encoding.UTF8.GetBytes(message);
        private void Send(string message) => client.Send(Prepare(message));
        public void SendRequest(string message)
        {
            if (!isConnected)
            {
                form.Log("You are not Connected!");
                return;
            }
            Send(message);
        }
    }
}
