using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam
{
    public partial class FormClient : Form
    {
        public FormClient()
        {
            InitializeComponent();
        }
        public ManualResetEvent isClose = new ManualResetEvent(false);
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.AppendText(message+"\r\n");
            });
        }
        public void PlacePicture(Image img)
        {
            pictureBoxMain.Invoke((Action)delegate
            {
                pictureBoxMain.Image = img;
            });
        }
        public void CloseForm()
        {
            Invoke((Action)delegate
            {
                isClose.Set();
                Close();
            });
        }

        private void FormClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isClose.WaitOne(0))
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
                Hide();
            }
        }
    }
}
