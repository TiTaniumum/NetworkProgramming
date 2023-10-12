using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetExam_Client
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        int enterCount = 0;
        Random rand = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("YOU WIN!");
        }
        private void ChangeButtonPosition(Button btn)
        {
            Rectangle rect = Bounds;
            int x = rand.Next(0, rect.Width-button1.Width*2);
            int y = rand.Next(0, rect.Height-button1.Height*2);
            btn.Location = new Point(x, y);
            int length = Enum.GetValues(typeof(KnownColor)).Length;
            int n = rand.Next(0, length);
            int j = 0;
            foreach (var i in Enum.GetValues(typeof(KnownColor)))
            {
                if (n == j)
                {
                    btn.BackColor = Color.FromKnownColor((KnownColor)i);
                    break;
                }
                j++;
            }
        }
        private void AddButton()
        {
            Button button = new Button();
            Rectangle rect = Bounds;
            int x = rand.Next(0, rect.Width-button1.Width*2);
            int y = rand.Next(0, rect.Height-button1.Height*2);
            button.Location = new Point(x, y);
            button.Text = GetString();
            button.MouseEnter += button1_MouseEnter;
            Controls.Add(button);
        }
        private string GetString()
        {
            string str = string.Empty;
            int length = rand.Next(1, 10);
            for (int i = 0; i < length; i++)
            {
                str+=(char)rand.Next('A', 'Z');
            }
            return str;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ChangeButtonPosition(sender as Button);
            enterCount++;
            if (enterCount % 10 == 0) AddButton();
        }
    }
}
