using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            server = Server.getInstance(this);
            InitDir();
        }
        Server server;
        private void InitDir()
        { 
            if (!Directory.Exists("ScreenShots")) Directory.CreateDirectory("ScreenShots");
        }
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.AppendText(message+"\r\n");
            });
        }
        public void UpdateClients(List<ServerSideClient> clients)
        {
            listBoxClients.Invoke((Action)delegate
            {
                listBoxClients.Items.Clear();
                foreach (ServerSideClient client in clients)
                {
                    listBoxClients.Items.Add(client.RemoteEndPoint);
                }
            });
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            buttonStart.Enabled = false; 
            server.Start();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (listBoxClients.SelectedIndex == -1) return;
            server.ShowFormByIpEP(listBoxClients.Items[listBoxClients.SelectedIndex].ToString());
        }
        public void ButtonStartEnabel() => buttonStart.Invoke((Action)delegate { buttonStart.Enabled = true; }) ;

        private void buttonClose_Click(object sender, EventArgs e)
        {
            server.Stop();
            Close();
        }
    }
}
