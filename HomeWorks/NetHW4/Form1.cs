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
            Init();
        }
        private Server server;
        private void Init()
        {
            InitThreadPool();
            InitServer();
            InitServerIPPort();
        }
        private void InitServer() => server = Server.getInstance(this);
        private void InitServerIPPort()
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
            textBoxPort.Text = "63997";
        }
        private void InitThreadPool()
        {
            ThreadPool.SetMinThreads(10, 10);
            ThreadPool.SetMaxThreads(int.MaxValue, int.MaxValue);
        }
        public void buttonStart_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(comboBoxServerIP.SelectedItem.ToString());
            int port = int.Parse(textBoxPort.Text);
            server.Start(ip, port);
        }
        public void buttonClose_Click(object sender, EventArgs e)
        {
            server.Stop();
            Close();
        }
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.Text += message+"\r\n";
            });
        }
        public void UpdateListView(List<ServerSideClient> clients)
        {
            Invoke((Action)delegate
            {
                listViewClients.Items.Clear();
                int id = 0;
                foreach (ServerSideClient item in clients)
                {
                    ListViewItem i = listViewClients.Items.Add(id.ToString());
                    i.SubItems.Add(item.name);
                    i.SubItems.Add(item.RemoteEndPoint.ToString());
                    id++;
                }
                listViewClients.Invalidate(true);
                listViewClients.Update();
            });
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            server.UpdateListView();
        }
    }
}