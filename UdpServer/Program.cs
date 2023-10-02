using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace UdpServer
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
        }
    }
}