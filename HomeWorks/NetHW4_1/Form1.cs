using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetHW4_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        private Client client;
        private List<string> clients = new List<string>();
        public string ClientName { get { return txtNickName.Text; } }
        private void Init()
        {
            client = new Client(this);
            txtNickName.Text = "User1";
            txtSrvAddr.Text = "127.0.0.1";
            txtSrvPort.Text = "63997";
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                client?.Connect();
                btnConnect.Text = "Disconnect";
                return;
            }
            client.Close();
            btnConnect.Text = "Connect";
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!client.IsConnected) return;
            if (txtMsg.Text == string.Empty && !radioButton1.Checked) return;
            if (radioButton1.Checked) client.GetClients();
            if (radioButton2.Checked) client.Send("ALL:"+txtMsg.Text+":ALL");
            if (radioButton3.Checked)
            {
                if (lbClients.SelectedIndex == -1) return;
                client.Send("TO:"+txtMsg.Text+":"+lbClients.Items[lbClients.SelectedIndex]);
            }
        }
        public void Log(string message)
        {
            txtChat.Invoke((Action)delegate
            {
                txtChat.Text += message +"\r\n";
            });
        }
        public (string address, string port) GetAddressPort() => (txtSrvAddr.Text, txtSrvPort.Text);
        public void UpdateClients(string[] clients)
        {
            this.clients.Clear();
            for (int i = 1; i < clients.Length; i++)
            {
                this.clients.Add(clients[i]);
            }
            lbClients.Invoke((Action)delegate
            {
                UpdateClients();
            });
        }
        private void UpdateClients()
        {
            lbClients.Items.Clear();
            foreach (string client in clients)
            {
                lbClients.Items.Add(client);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            client.Close();
            Close();
        }
    }
}