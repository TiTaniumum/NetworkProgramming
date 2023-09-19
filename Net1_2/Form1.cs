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
        }
        private Socket serverSocket = null;
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
                //close
            }
        }
        private void InitSocket()
        {
            int port;
            if (!int.TryParse(textBoxPort.Text, out port) || port <=0 || port>65535)
            {
                MessageBox.Show("Port is not Correct");
                textBoxPort.Focus();
                textBoxPort.SelectAll();
            }
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //serverSocket.Bind();
            //serverSocket.Listen();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
