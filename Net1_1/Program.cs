using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Net1_1
{
    internal class MainClass
    {
        public static Socket socket = null;
        public static void Main(string[] args)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint srvEP = new IPEndPoint(ip, 12345);
            socket.Connect(srvEP);
            if (socket.Connected)
            {
                Console.WriteLine("Connected to server: {0} ==> {1}",
                  socket.LocalEndPoint.ToString(), socket.RemoteEndPoint.ToString());

                // Получение данных от сервера
                byte[] buffer = new byte[1024];
                int size = socket.Receive(buffer);
                string msg = Encoding.UTF8.GetString(buffer, 0, size);
                Console.WriteLine("Receive message from server: [{0}] {1}", size, msg);

                // Посылка данных на сервер
                msg = "Hello from Client!";
                socket.Send(Encoding.UTF8.GetBytes(msg));
                Console.WriteLine("Send message to server");

                Console.ReadLine();

                // Отключиться от сервера
                socket.Disconnect(false);

                // Закрыть сокет
                socket.Close();
            }
        }
    }
}