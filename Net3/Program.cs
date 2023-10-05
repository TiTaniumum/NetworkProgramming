using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.IO;


namespace Net3
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            //Uri uri1 = new Uri("http://mail.ru"/*, "index.html"*/);
            Uri uri1 = new Uri("https://titaniumum.github.io/");
            Uri uri2 = new Uri("http://google.com");
            HttpWebRequest request1 = (HttpWebRequest)HttpWebRequest.Create(uri1);
            HttpWebRequest request2 =  HttpWebRequest.CreateHttp(uri2);

            Console.WriteLine("\nЗаголовок Ответа");
            HttpWebResponse response = (HttpWebResponse)request1.GetResponse();
            for (int i = 0; i < response.Headers.Count; i++)
            {
                string key = response.Headers.AllKeys[i];
                string[] vals = response.Headers.GetValues(i);
                string val = string.Join("; ", vals);
                Console.WriteLine("{0} : {1}", key, val);
            }
            Console.ReadLine();
            Console.WriteLine("Заголовок запроса");
            foreach (string key in request1.Headers.AllKeys)
            {
                Console.WriteLine("{0} : {1}", key, request1.Headers.Get(key));
            }
            Console.ReadLine();
            Console.WriteLine("Тело ответа");
            StreamReader sr = new  StreamReader(response.GetResponseStream());
            string body = sr.ReadToEnd();
            Console.WriteLine(body);
            response.Close();
        }
    }
}