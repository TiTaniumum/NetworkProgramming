using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace NetHW6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            server = new Server(this);
        }
        Server server;
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.Text+=message+"\r\n";
            });
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            server.Start();
            buttonStart.Enabled = false;
        }
    }
    public class Server
    {
        UdpClient server;
        IPEndPoint ipEP = new IPEndPoint(IPAddress.Any, 63997);
        IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
        Form1 form;
        public Server(Form1 form)
        {
            server = new UdpClient();
            server.Client.Bind(ipEP);
            this.form=form;
        }
        public void Start()
        {
            Thread serverThread = new Thread(Proc);
            serverThread.IsBackground = true;
            serverThread.Start();
        }
        private void Proc()
        {
            byte[] buffer;
            form.Log("Listening...");
            while (true)
            {
                using (FileStream fs = File.OpenWrite("result.jpg"))
                {
                    int i = 0;
                    server.Client.ReceiveTimeout = 0;
                    buffer = server.Receive(ref remoteEP);
                    fs.Write(buffer, 0, buffer.Length);
                    server.Client.ReceiveTimeout = 3000;
                    while (true)
                    {
                        try
                        {
                            buffer = server.Receive(ref remoteEP);
                        }
                        catch (Exception) { break; }
                        if (buffer == null || buffer.Length == 0) break;
                        fs.Write(buffer, 0, buffer.Length);
                        i++;
                        form.Log($"{i}: size = {buffer.Length}       \r");
                    }
                    form.Log("");
                    form.Log($"Recieved {i} packets");
                }
            }
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
                    bitmap.Save("test.png", ImageFormat.Png);
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
