using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Net1
{
    internal class MainClass
    {
        static Socket ServerSocket = null;
        public static void Main(string[] args)
        {
            Console.WriteLine("SERVER");
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint srvEP = new IPEndPoint(IPAddress.Any, 12345);
            ServerSocket.Bind(srvEP);
            ServerSocket.Listen(1000);

            Console.WriteLine("Server started on: {0}", srvEP.ToString());

            Socket client = ServerSocket.Accept();

            Console.WriteLine("Client Connected: {0} ==> {1}", client.LocalEndPoint.ToString(), client.RemoteEndPoint.ToString());

            string msg = "Hello from Server!";
            client.Send(Encoding.UTF8.GetBytes(msg));
            byte[] buffer = new byte[1024];
            int size = client.Receive(buffer);
            Console.WriteLine("Recive message from client:[{0}] , message: \'{1}\'", size, Encoding.UTF8.GetString(buffer, 0, size));
            client.Close();
            ServerSocket.Close();
            Console.ReadLine();
        }
    }
}