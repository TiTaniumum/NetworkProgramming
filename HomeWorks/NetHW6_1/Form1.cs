using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetHW6_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Client client = new Client();
        private void button1_Click(object sender, EventArgs e)
        {
            client.Send();
        }
    }

    public class Client
    {
        IPAddress serverIP = IPAddress.Loopback;
        int port = 63997;
        public Client()
        {

        }
        public void Send()
        {
            ScreenShooter.MakeAScreenShot();


            string fileName = "ScreenShot.jpg";
            int blockSize = 32*1024;
            FileInfo file = new FileInfo(fileName);
            int cntSend = (int)(file.Length/blockSize);
            int remSend = (int)(file.Length%blockSize);
            UdpClient uClient = new UdpClient();
            IPEndPoint srvEP = new IPEndPoint(serverIP, port);
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
                    //Console.Write($"{i}/{cntSend} = {proc:f1}%             \r");
                    Thread.Sleep(1);
                }
                if (remSend>0)
                {
                    //MessageBox.Show(file.Length.ToString() + " " + startIndex +" "+remSend);
                    fs.Read(buf, 0, remSend);
                    uClient.Send(buf, remSend, srvEP);
                }
                //Console.WriteLine();
            }
            uClient.Close();
            //Console.ReadLine();
        }
    }


    public class ScreenShooter
    {
        public static void MakeAScreenShot()
        {
            try
            {
                Rectangle bounds = Screen.GetBounds(Point.Empty);
                //Rectangle bounds = Screen.AllScreens[0].Bounds;

                var sf = GetWindowsScreenScalingFactor(false);
                Size sz = bounds.Size;
                sz.Width = (int)(sz.Width*sf);
                sz.Height = (int)(sz.Height*sf);

                using (Bitmap bitmap = new Bitmap(sz.Width, sz.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(Point.Empty, Point.Empty, sz);
                    }
                    bitmap.Save("ScreenShot.jpg", ImageFormat.Jpeg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117
        }
        // этот метод определяет масштаб экрана. у меня он 125% из-за чего скриншот получялся не полный. поэтому поискал как бы найти масштаб экрана
        public static double GetWindowsScreenScalingFactor(bool percentage = true)
        {

            Graphics GraphicsObject = Graphics.FromHwnd(IntPtr.Zero);

            IntPtr DeviceContextHandle = GraphicsObject.GetHdc();

            int LogicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(DeviceContextHandle, (int)DeviceCap.DESKTOPVERTRES);

            double ScreenScalingFactor = Math.Round(PhysicalScreenHeight / (double)LogicalScreenHeight, 2);

            if (percentage)
            {
                ScreenScalingFactor *= 100.0;
            }

            GraphicsObject.ReleaseHdc(DeviceContextHandle);
            GraphicsObject.Dispose();

            return ScreenScalingFactor;
        }
    }
}
