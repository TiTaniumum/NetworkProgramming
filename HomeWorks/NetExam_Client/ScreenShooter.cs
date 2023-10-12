using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public class ScreenShooter
    {
        public static void MakeAScreenShot(string path = "ScreenShot.jpg")
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
                    bitmap.Save(path, ImageFormat.Jpeg);
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
