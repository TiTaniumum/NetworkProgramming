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

            ////-- ---- --   - -- -- -- - - -

            string fileName = @"C:\Users\elemanovt\source\repos\NetworkProgramming\UdpClient\file.txt";
            int blockSize = 1024;
            FileInfo file = new FileInfo(fileName);
            int cntSend = (int)(file.Length/blockSize);
            int remSend = (int)(file.Length%blockSize);
            UdpClient uClient = new UdpClient();
            IPEndPoint srvEP = new IPEndPoint(server, port);
            byte[] buf = new byte[blockSize];
            int startIndex = 0;
            using (FileStream fs = File.OpenRead(fileName))
            {
                for (int i = 0; i < cntSend; i++)
                {
                    fs.Read(buf, 0, blockSize);
                    uClient.Send(buf, blockSize, srvEP);
                    startIndex+=blockSize;
                    float proc = (float)i/(float)cntSend *100;                   
                    Console.Write($"{i}/{cntSend} = {proc:f1}%             \r");
                    Thread.Sleep(1);
                }
                if (remSend>0)
                {
                    fs.Read(buf, startIndex, remSend);
                    uClient.Send(buf, blockSize, srvEP);
                }
                Console.WriteLine();
            }
            uClient.Close();
            Console.ReadLine();
        }
    }
}