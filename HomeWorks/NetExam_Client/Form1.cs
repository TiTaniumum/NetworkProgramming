using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetExam_Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Hooks.form = this;
            Client.form = this;
            PC.form = this;
        }
        public void Log(string message)
        {
            textBoxLog.Invoke((Action)delegate
            {
                textBoxLog.AppendText(message);
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Client.form = null;
            Hooks.form = null;
            PC.form = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
