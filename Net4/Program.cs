using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Net4
{
    internal class MianClass
    {
        static void Main(string[] args)
        {
            //FtpWebRequest ftpWebReq = (FtpWebRequest)FtpWebRequest.Create("ftp://ftp.gnu.org");
            //IPEndPoint ipEP = new IPEndPoint(IPAddress.Any, 12345);
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint remoteEP = new IPEndPoint(0,0);
            byte[] buffer;
            while (true)
            {
                buffer = udpClient.Receive(ref remoteEP);
                string str = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"{remoteEP}: {str}");
                if (str == "Exit") break;
            }
            Console.WriteLine("EXITed");
            udpClient.Close();
            Console.ReadLine();
        }
    }
}