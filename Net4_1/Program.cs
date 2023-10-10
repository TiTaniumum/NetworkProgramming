using System.Net;
using System.Net.Sockets;
using System.Text;
namespace Net4_1
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint multiCastEP = new IPEndPoint(IPAddress.Parse("10.13.12.255"), 12345);
            // 224.255.255.255
            string message;
            byte[] buffer;
            while (true)
            {
                message = Console.ReadLine();
                if (message == null || message.Length == 0) continue;
                buffer = Encoding.UTF8.GetBytes(message);
                udpClient.Send(buffer, multiCastEP);
                if (message == "Exit") break;
            }
            udpClient.Close();
            Console.ReadLine();
        }
    }
}