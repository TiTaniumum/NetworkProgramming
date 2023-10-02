using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace NetUdpClient
{
    internal class MainClass
    {
        static IPAddress server = IPAddress.Parse("127.0.0.1");
        static int port = 12345;
        static Socket socket;
        static void Main(string[] args)
        {
            Console.ReadLine();
            Console.ReadLine();
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            string msg = "Hello! This is client!";
            int size = socket.SendTo(Encoding.UTF8.GetBytes(msg), new IPEndPoint(server, port));
            Console.WriteLine("Send message to UDP-Server");
            Console.WriteLine($"Send size {size} bytes");
            socket.Close();
            string fileName = "";
            int blockSize = 1024;
            FileInfo file = new FileInfo(fileName);
            int cntSend = (int)(file.Length/blockSize);
            int remSend = (int)(file.Length%blockSize);
            UdpClient uClient = new UdpClient();
            EndPoint srvEP = new IPEndPoint(server, port);
            char[] buf = new char[blockSize];
            int startIndex = 0;
            Convert.ToByte();
            using (StreamReader sr = new StreamReader(fileName))
            {
                sr.ReadBlock(buf,startIndex, blockSize);
                uClient.Send();
            }
        }
    }
}