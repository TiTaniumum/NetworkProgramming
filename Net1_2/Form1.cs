using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Net1_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitList();
            InitPoolThread();
            MessageHandle = SaveToLog;
        }
        private Socket serverSocket = null;
        public delegate void dSaveToLog(string msg);
        public dSaveToLog MessageHandle;
        private List<ClientParam> Clients = new List<ClientParam>();
        private void InitPoolThread()
        {
            ThreadPool.SetMinThreads(10, 10);
            ThreadPool.SetMaxThreads(int.MaxValue, int.MaxValue);
        }
        private void InitList()
        {
            string compName = Dns.GetHostName();
            Text = "Server: " + compName;
            IPAddress[] adressess = Dns.GetHostAddresses(compName);
            comboBoxServerIP.Items.Add(IPAddress.Any.ToString());
            comboBoxServerIP.Items.Add(IPAddress.Loopback.ToString());
            foreach (IPAddress address in adressess)
            {
                comboBoxServerIP.Items.Add(address.ToString());
            }
            comboBoxServerIP.SelectedIndex = 0;
            textBoxPort.Text = "12345";
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Start")
            {
                InitSocket();
                //start
            }
            else
            {
                serverSocket.Close();
                buttonStart.Text = "Start";
                //close
            }
        }
        private void InitSocket()
        {
            int port;
            if (!int.TryParse(textBoxPort.Text, out port) || port <=0 || port>65535)
            {
                textBoxLog.Text+="ERROR: Port is not Correct!\r\n";
                //MessageBox.Show("Port is not Correct");
                textBoxPort.Focus();
                textBoxPort.SelectAll();
                return;
            }
            IPAddress ip;
            if (!IPAddress.TryParse(comboBoxServerIP.Text, out ip))
            {
                textBoxLog.Text+="ERROR: IP is not correct";
                comboBoxServerIP.Focus();
                comboBoxServerIP.SelectAll();
                return;
            }
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, port));
            serverSocket.Listen(100);

            //ThreadPool.QueueUserWorkItem();
            buttonStart.Text = "Stop";
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ServerProc(object obj)
        {
            Form1 form = obj as Form1;
            form.Invoke(MessageHandle, "Server started");
            while (true)
            {
                try
                {
                    form.Invoke(MessageHandle, "Waiting next client...");
                    Socket client = form.serverSocket.Accept();
                    //client param

                    ClientParam param = new ClientParam
                    {
                        client = client,
                        form = form,
                    };
                    string message = $"Client {client.RemoteEndPoint} is connected";
                    form.Invoke(MessageHandle, message);
                    //ThreadPool.QueueUserWorkItem(ClientProc, param);
                }
                catch (Exception ex)
                {
                    form.Invoke(MessageHandle, ex.Message);
                    break;
                }//try
            }//while

        }
        private void ClientProc(object obj)
        {
            ClientParam par = (ClientParam)obj;
            try
            {
                //1 hello
                par.form.Invoke(MessageHandle, "Client Thread is runing, sending \'Hello!\'");
                string msg = "Hello!";
                par.client.Send(Encoding.UTF8.GetBytes(msg));
                //2 getname
                byte[] buffer = new byte[4*1024];
                int size = par.client.Receive(buffer);
                par.name = Encoding.UTF8.GetString(buffer, 0, size);
                string message = $"Client: {par.name}";
                par.form.Invoke(MessageHandle, message);
                lock (Clients)
                {
                    Clients.Add(par);
                }
                //3 comands
                while (true)
                {
                    size = par.client.Receive(buffer);
                    if (size<=0)
                    {
                        break; // client disconected;
                    }
                    string command = Encoding.UTF8.GetString(buffer, 0, size);
                    message = $"\'{par.name}\' sent command: \'{command}\'";
                    par.form.Invoke(MessageHandle, message);
                    switch (command)
                    {
                        case "GetListClient":
                            GetListClient(par);
                            break;
                        case "SendMessageAll":
                            SendMessageAll(par, buffer);
                            break;
                        case "SendMessageTo":
                            SendMessageTo(par, buffer);
                            break;
                        default:
                            message = $"Incorrect comand \'{command}\' from client {par.name}!";
                            par.form.Invoke(MessageHandle, message);
                            break;
                    }
                }


            }
            catch (Exception ex)
            {
                par.form.Invoke(MessageHandle, ex.Message);
            }
            finally
            {
                string message = $"User {par.name} disconnected!";
                par.form.Invoke(MessageHandle, message);
                par.client.Close();
                //delete client from list
                lock (Clients)
                {
                    Clients.Remove(par);
                }
            }
        }
        private void SendMessageTo(ClientParam par, byte[]buffer)
        {
            int size = par.client.Receive(buffer);
            string to = Encoding.UTF8.GetString(buffer, 0, size);
            int size2 = par.client.Receive(buffer);
            string res = "Error";
            for (int i = 0; i < Clients.Count; i++)
            {
                if (Clients[i].name == to && Clients[i].client.Connected)
                {
                    Clients[i].client.Send(buffer,0,size2, SocketFlags.None);
                    res = "OK";
                }
            }
            par.client.Send(Encoding.UTF8.GetBytes(res));
            string message = $"client {par.name} sent message to {to} ; result: {res}";
            par.form.Invoke(MessageHandle, message);
        }
        private void SendMessageAll(ClientParam par, byte[] buffer)
        {
            int size = par.client.Receive(buffer);
            //string str = Encoding.UTF8.GetString(buffer, 0, size);
            int cnt = 0;
            foreach (ClientParam c in Clients)
            {
                if (c.client.Connected)
                {
                    c.client.Send(buffer, 0, size,SocketFlags.None);
                    cnt++;
                }
            }
            par.client.Send(Encoding.UTF8.GetBytes(cnt.ToString()));
            string message = $"client {par.name} sent message to everyone";
            par.form.Invoke(MessageHandle, message);
        }
        private void GetListClient(ClientParam par)
        {
            string[] names = new string[Clients.Count];
            lock (Clients)
            {
                for (int i = 0; i < Clients.Count; i++)
                {
                    names[i] = Clients[i].name;
                }
            }
            string str = string.Join(";", names);
            par.client.Send(Encoding.UTF8.GetBytes(str));
            string message = $"client {par.name} requested client list";
            par.form.Invoke(MessageHandle, message);
        }
        public void SaveToLog(string msg)
        {
            textBoxLog.Text += msg+ "\r\n";
        }
        public struct ClientParam
        {
            public string name;
            public Socket client;
            public Form1 form;
            public ClientParam(string name, Socket client, Form1 form)
            {
                this.name = name;
                this.client = client;
                this.form = form;
            }
        }
    }
}
