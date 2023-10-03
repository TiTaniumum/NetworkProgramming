using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace NetUdpServer
{
    internal class MainClass
    {
        static int port = 12345;
        static Socket socket;
        static byte[] buffer = new byte[64*1024];
        public static void Main(string[] args)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            EndPoint localEP = new IPEndPoint(IPAddress.Any, port);
            EndPoint remoteEP = new IPEndPoint(IPAddress.Any, port);
            socket.Bind(localEP);
            Console.WriteLine($"Start UDP-Server on: {localEP}");
            int size = socket.ReceiveFrom(buffer, ref remoteEP);
            Console.WriteLine($"Receive data from: {remoteEP}");
            Console.WriteLine($"Receive data size: {size}");
            string msg = Encoding.UTF8.GetString(buffer, 0, size);
            Console.WriteLine($"Receive data size: \n{msg}");
            socket.Close();

            UdpClient uClient = new UdpClient();
            uClient.Client.Bind(localEP);
            IPEndPoint IPremoteEP = remoteEP as IPEndPoint;
            using (FileStream fs = File.OpenWrite("result.txt"))
            {
                int i = 0;
                uClient.Client.ReceiveTimeout = 5000;
                while (true)
                {
                    try
                    {
                        buffer = uClient.Receive(ref IPremoteEP);
                    }
                    catch (Exception) { break; }
                    if (buffer == null || buffer.Length == 0) break;
                    fs.Write(buffer, 0, buffer.Length);
                    i++;
                    Console.Write($"{i}: size = {buffer.Length}       \r");
                }
                Console.WriteLine();
                Console.WriteLine($"Recieved {i} packets");
            }
            uClient.Close();
        }
    }
}