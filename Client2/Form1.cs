using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace Client2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            txtNickName.Text = NickName;
            txtSrvAddr.Text = "127.0.0.1";
            txtSrvPort.Text = "12345";
            messageHandle = Log;
        }

        // сокет клиента для соединения с сервером
        public delegate void MessageHandle(string message);
        private MessageHandle messageHandle;
        private TcpClient sock = null;
        private string NickName = "User1";

        private void Log(string message)
        {
            txtChat.Text += message + "\r\n";
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            { // подлючение к серверу
                sock = new TcpClient();

                sock.BeginConnect(txtSrvAddr.Text, int.Parse(txtSrvPort.Text), ClientConnectProc, this);
                btnConnect.Enabled = false;
                Log("INFO: Connecting to server...");
            }
            else
            { // отключение от сервера
                if (sock!=null && sock.Connected)
                {
                    //sock.Client.Disconnect(true);
                    sock.Close();
                    Log("INFO: Disconnected from server.");
                }
                sock = null;
                btnConnect.Text= "Connect";
            }
        } // void btnConnect_Click()
        private void ClientConnectProc(IAsyncResult ar)
        {
            if (ar.IsCompleted)
            {
                Form1 form = ar.AsyncState as Form1;
                if (form.sock.Connected) { form.sock.EndConnect(ar);
                    form.Invoke((Action)delegate { btnConnect.Text = "Disconnect"; });
                    form.Invoke(messageHandle, "INFO: Connected to Server.");
                }
                else
                {
                    form.Invoke(messageHandle, "FAIL: Connection is failed.");
                }
                form.Invoke((Action)delegate { btnConnect.Enabled = true; });
                //form.sock.Client.BeginReceive(AcyncRecievProc);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (sock == null || !sock.Connected) return;
            if (radioButton1.Checked) {
                string command = "GetListClient";
                byte[] buffer = Encoding.UTF8.GetBytes(command);
                sock.Client.BeginSend(buffer, 0, buffer.Length,SocketFlags.None,AsyncSendProc, this);
            }
            else if (radioButton2.Checked) { }
            else if (radioButton3.Checked) { }
            else if (radioButton4.Checked) { }
            else if (radioButton5.Checked) { }
        }
        private void AsyncSendProc(IAsyncResult ar)
        {
            if (!ar.IsCompleted) return;
            Form1 form = ar.AsyncState as Form1;
            int size = form.sock.Client.EndSend(ar);
        }
    } // class Form1
} // namespace Client2
