using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace XO
{
    public partial class GameXO : Form
    {
        public bool isStartGame = false;  // игра еще не началась
        public bool isStartStep_X = true; // первыми ходят "Крестики"
        public bool isStep_X = true; // текущий ход "Крестиков"

        // время старта новый игры
        DateTime timeStart;

        // для текста игры XO
        Font fontXO = new Font("Arial", 14, FontStyle.Bold);
        Brush brushX = new SolidBrush(Color.Blue); // для X
        Brush brushO = new SolidBrush(Color.Gold); // для O
        Color colorO = Color.Blue; // для X
        Color colorX = Color.Gold; // для O

        // двумерный массив ссылок на объекты типа Panel
        Panel[,] arrPanel; // перемнная класса
        Random random = new Random(DateTime.Now.Millisecond);

        public GameXO()
        {
            InitializeComponent();
            miMain_Restart.Enabled = false;
            Update_MI_StepTurn();
            // Положения панелей на окне
            // 1 2 3   ==>  1;1  1;2  1;3
            // 6 5 4   ==>  2;1  2;2  2;3
            // 9 8 7   ==>  3;1  3;2  3;3
            panel1.Tag = " ;1;1"; panel2.Tag = " ;1;2"; panel3.Tag = " ;1;3";
            panel4.Tag = " ;2;1"; panel5.Tag = " ;2;2"; panel6.Tag = " ;2;3";
            panel7.Tag = " ;3;1"; panel8.Tag = " ;3;2"; panel9.Tag = " ;3;3";

            // двумерный массив ссылок на объекты типа Panel
            Panel[,] tmp = // временная переменная
            {
        { panel1, panel2, panel3 },
        { panel6, panel5, panel4 },
        { panel9, panel8, panel7 },
      };
            arrPanel = tmp;
        }
        private void Update_MI_StepTurn()
        {
            if (isStartStep_X)
            { // первыми ходят "Крестики"
                miGame_Start_X.Checked = true;
                miGame_Start_O.Checked = false;
            }
            else
            { // первыми ходят "Нолики"
                miGame_Start_X.Checked = false;
                miGame_Start_O.Checked = true;
            }
        }

        private void miGame_Start_X_Click(object sender, EventArgs e)
        {
            if (miGame_Start_X.Checked)
            { // инвертируем значение переменной isStartStep_X
                isStartStep_X = !isStartStep_X;
            }
            Update_MI_StepTurn(); // обновляем состояние меню
        }

        private void miGame_Start_O_Click(object sender, EventArgs e)
        {
            if (miGame_Start_O.Checked)
            { // инвертируем значение переменной isStartStep_X
                isStartStep_X = !isStartStep_X;
            }
            Update_MI_StepTurn(); // обновляем состояние меню
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Panel pn = sender as Panel;
            if (pn == null) return;
            // подсказка в статусной строке
            stbStepTurn.Text = isStep_X ? "Ходят X" : "Ходят O";

            // TODO 

            pn.Invalidate(); // инвалидация окна для перепрорисовки
            pn.Update(); // сигнал на обновление окна панели
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Panel pn = sender as Panel;
            if (pn == null) return;
            PointF pt = new PointF(10, 10);

            // центрирование текста на панельке
            SizeF ptSize = e.Graphics.MeasureString("O", fontXO);

            pt.X = (pn.Width - ptSize.Width) / 2;
            pt.Y = (pn.Height - ptSize.Height) / 2;

            if ((pn.Tag as string)[0] == 'X')
            { // вывод X
                Brush brushX = new SolidBrush(colorX);
                e.Graphics.DrawString("X", fontXO, brushX, pt);
            }
            else if ((pn.Tag as string)[0] == 'O')
            { // вывод O
                e.Graphics.DrawString("O", fontXO,
                                 new SolidBrush(colorO), pt);
            }
            else
            { // прорисовка пустого поля
                e.Graphics.Clear(Color.DarkGray);
                e.Graphics.DrawString(" ", fontXO, brushO, pt);
            }
        }

        private void miText_Font_Click(object sender, EventArgs e)
        {
            fontDialog1.Font = fontXO;
            // показать диалог для выбора шрифта
            if (fontDialog1.ShowDialog(this) == DialogResult.OK)
            {
                // сохранить шрифт в объекте GameXO::fontXO
                fontXO = fontDialog1.Font;
            }
            // обновление для всей формы и ее элементов
            Invalidate(true);
            Update();
        }

        private void miText_Color_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                colorX = colorO = colorDialog1.Color;
                // обновление для всей формы и ее элементов
                Invalidate(true);
                Update();
            }
        }

        private void splitContainer2_Panel1_MouseClick(
          object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (isStartGame)
                {
                    contMenu1.Items["pmiRestart"].Enabled = true;
                }
                else
                {
                    contMenu1.Items["pmiRestart"].Enabled = false;
                }
                // вывод контекстного меню по текущим координатам "мыши"
                contMenu1.Show(sender as Control, e.Location);
            }
        }

        private void tmCurTime_Tick(object sender, EventArgs e)
        {
            (sender as System.Windows.Forms.Timer).Enabled = false; // временно выключаем таймер

            stbCurrTime.Text = DateTime.Now.ToShortDateString() +
                        "  " + DateTime.Now.ToShortTimeString();
            (sender as System.Windows.Forms.Timer).Enabled = true; // включаем таймер
        }

        private void miMain_NewGame_Click(object sender, EventArgs e)
        {
            isStartGame = true;

            // включить таймер игры
            tmGame.Enabled = true;
            timeStart = DateTime.Now;

            // прогресс-бар
            pbPastTime.Minimum = 0;
            pbPastTime.Maximum = 60 * 2; // максимум 2 минуты = 120 сек

            // перерисовать форму
            Invalidate(true);
            Update();
        }
        // таймер для текущей игры
        private void tmGame_Tick(object sender, EventArgs e)
        {
            tmGame.Enabled = false;

            stbGameTime.Text
              = (DateTime.Now - timeStart).ToString(@"hh\:mm\:ss");
            pbPastTime.Value = (DateTime.Now - timeStart).Seconds;

            tmGame.Enabled = true;
        }

        TcpListener tcpServer = null;
        TcpClient tcpClient = null;
        string ipServer = "0.0.0.0"; // 127.0.0.1; 192.168.1.60
        int portServer = 4123;
        string UserName = "Noname";
        //
        AutoResetEvent UserStep; // событие хода игрока
        Point MyStep = new Point(); // координаты нашего хода
        Point EnemyStep = new Point(); // координаты хода противника

        private void miStartServer_Click(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(ServerProc, this);
            miConnet.Enabled = false;
            //miStartServer.Enabled = false;
        }

        // поток сервера
        private void ServerProc(object obj)
        {
            GameXO form = obj as GameXO;
            try
            {
                tcpServer = new TcpListener(IPAddress.Parse(ipServer), portServer);
                tcpServer.Start();
                byte[] buffer;
                string msg;
                while (true)
                {
                    TcpClient client = tcpServer.AcceptTcpClient();
                    if (tcpClient !=null)
                    {
                        msg = "ERROR: Client already connected!";
                        NetworkStream ns = client.GetStream();
                        buffer = Encoding.UTF8.GetBytes(msg);
                        ns.Write(buffer, 0, buffer.Length); // send()
                        client.Close();
                        continue;
                    }
                    tcpClient = client;
                    if (random.Next(0, 2)==0)
                    {
                        msg = "OKX";
                        isStartStep_X = false;
                    }
                    else
                    {
                        msg = "OKO";
                        isStartStep_X=true;
                    }
                    buffer = Encoding.UTF8.GetBytes(msg);
                    tcpClient.Client.Send(buffer);
                    ThreadPool.QueueUserWorkItem(ClientProc, this);
                }
            }
            catch (Exception ex)
            { // ошибка при работе сервера
                MessageBox.Show(ex.Message);
            }
        }
        private void ClientProc(object obj)
        {
            isStartGame = true;
            GameXO form = obj as GameXO;
            byte[] buffer = new byte[4*1024];
            byte[] temp;
            try
            {
                form.tcpClient.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, RecieveCallBackProc, form);
                temp = Encoding.UTF8.GetBytes(form.UserName);
                tcpClient.GetStream().Write(temp, 0, temp.Length);

                while (isStartGame)
                {
                    if (UserStep.WaitOne(200) == true)
                    {
                        string message = $"{MyStep.X};{MyStep.Y}";
                        temp = Encoding.UTF8.GetBytes(message);
                        tcpClient.GetStream().Write(temp, 0, temp.Length);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                isStartGame = false;
                form.tcpClient.Close();
                tcpClient = null;
            }
        }

        private void RecieveCallBackProc(IAsyncResult ar)
        {
            byte[] buffer = new byte[4*1024];
            GameXO form = ar.AsyncState as GameXO;
            if (ar.IsCompleted)
            {
                int size = form.tcpClient.Client.EndReceive(ar);
                if (isStartStep_X != isStep_X)
                {

                }
                form.tcpClient.Client.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, RecieveCallBackProc, form);
            }
        }

        private void miConnet_Click(object sender, EventArgs e)
        {
            // запросить имя игрока: UserName
            // запросить адрес сервера: 192.168.1.60; server_XO 

            // Поток для работы игрока с удаленным сервером
            byte[] buffer = new byte[4*1024];
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ipServer, portServer);
                if (tcpClient.Connected)
                {
                    NetworkStream ns = tcpClient.GetStream();
                    int size = ns.Read(buffer, 0, buffer.Length);
                    string msg = Encoding.UTF8.GetString(buffer, 0, size);
                    if (msg.Substring(0, 2) == "OK")
                    {
                        isStartStep_X = (msg[3] =='X');
                        ThreadPool.QueueUserWorkItem(ClientProc, this);
                        miStartServer.Enabled = false;
                        miConnet.Text = "Отключиться";
                        msg = "Соединение с сервером успешное. Игра началась.\r\n";
                    }
                    else
                    {
                        msg = "Ошибка: Сервере отказал в подключении";
                    }
                    MessageBox.Show(msg, "GameInfo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
            // 1 - подключение к серверу игры
            // 2 - отправить имя игрока
            // 3 - получить имя игрока противника
            // 4 - игра в цикле
        }
    } // class GameXO
}
