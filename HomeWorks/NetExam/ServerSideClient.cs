using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam
{
    public class ServerSideClient
    {
        private Socket client;
        private Server server = Server.getInstance();
        private FormClient form;
        public bool IsConnected { get { return client.Connected; } }
        public string RemoteEndPoint { get { return client.RemoteEndPoint.ToString(); } }
        public ServerSideClient(Socket client)
        {
            this.client = client;
            server.Form.Invoke((Action)delegate
            {
                form = new FormClient();
                form.Owner = server.Form;
                form.Show();
            });
            ThreadPool.QueueUserWorkItem(ClientProc);
        }
        public void Show() => form.Invoke((Action)delegate { form.Show(); });
        private void ClientProc(object obj)
        {
            server.Log(client.RemoteEndPoint.ToString()+" has connected.");

            byte[] buffer = new byte[32*1024];
            string message = string.Empty;

            Thread.Sleep(1000);
            try
            {
                while (client.Connected)
                {
                    message = GetMessage(buffer);
                    if (message.Length == 0) break;
                    HandleRequest(message);
                }
            }
            catch (Exception ex)
            {
                server.Log(ex.Message);
            }
            client?.Close();
            form?.CloseForm();
            server.DisconnectClient(this);
        }
        public void Stop()
        {
            client?.Close();
            client = null;
        }
        private string GetMessage(byte[] buffer)
        {
            int size = client.Receive(buffer);
            return Encoding.UTF8.GetString(buffer, 0, size);
        }
        private void HandleRequest(string message)
        {
            if (message != "*")
            {
                form.Log(message);
                File.AppendAllText("Log.txt", message + "->" +DateTime.Now.ToString("ss:mm:hh")+"\n");
                return;
            }
            form.PlacePicture(Image.FromFile(AcceptPicture()));
        }
        private string AcceptPicture()
        {
            byte[] buffer = new byte[32*1024];
            int size = 0;
            string date = DateTime.Now.ToString("dd-MM-yy ss-mm-hh");
            string path = "ScreenShots\\"+date+".jpg";
            bool isEnd = false;
            client.ReceiveTimeout = 1000;
            using (FileStream fs = File.OpenWrite(path))
            {
                while (!isEnd)
                {
                    try
                    {
                        size = client.Receive(buffer);
                    }
                    catch (Exception) { break; }
                    if (size == 0) break;
                    fs.Write(buffer, 0, size);
                }
            }
            if(client!= null) client.ReceiveTimeout = 0;
            return path;
        }
    }
}
