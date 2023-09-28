namespace XO
{
  partial class GameXO
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.miMain_Main = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_NewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_Restart = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_Pause = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.miMain_Stat = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miMain_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_Load = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miMain_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_Game = new System.Windows.Forms.ToolStripMenuItem();
            this.miGame_Start_X = new System.Windows.Forms.ToolStripMenuItem();
            this.miGame_Start_O = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miGame_Text = new System.Windows.Forms.ToolStripMenuItem();
            this.miText_Font = new System.Windows.Forms.ToolStripMenuItem();
            this.miText_Color = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miStartServer = new System.Windows.Forms.ToolStripMenuItem();
            this.miConnet = new System.Windows.Forms.ToolStripMenuItem();
            this.miMain_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp_About = new System.Windows.Forms.ToolStripMenuItem();
            this.miHelp_ContHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.stbStepTurn = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbPastTime = new System.Windows.Forms.ToolStripProgressBar();
            this.stbGameTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbCurrTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tmCurTime = new System.Windows.Forms.Timer(this.components);
            this.tmGame = new System.Windows.Forms.Timer(this.components);
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.contMenu1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pmiRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.pmiPause = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.pmiColor = new System.Windows.Forms.ToolStripMenuItem();
            this.pmiFont = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.SuspendLayout();
            this.contMenu1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMain_Main,
            this.miMain_Game,
            this.miMain_Help});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(784, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // miMain_Main
            // 
            this.miMain_Main.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMain_NewGame,
            this.miMain_Restart,
            this.miMain_Pause,
            this.toolStripMenuItem1,
            this.miMain_Stat,
            this.toolStripSeparator1,
            this.miMain_Save,
            this.miMain_Load,
            this.toolStripMenuItem2,
            this.miMain_Close});
            this.miMain_Main.Name = "miMain_Main";
            this.miMain_Main.Size = new System.Drawing.Size(64, 20);
            this.miMain_Main.Text = "&Главное";
            // 
            // miMain_NewGame
            // 
            this.miMain_NewGame.Name = "miMain_NewGame";
            this.miMain_NewGame.Size = new System.Drawing.Size(180, 22);
            this.miMain_NewGame.Text = "&Новая игра";
            this.miMain_NewGame.Click += new System.EventHandler(this.miMain_NewGame_Click);
            // 
            // miMain_Restart
            // 
            this.miMain_Restart.Name = "miMain_Restart";
            this.miMain_Restart.Size = new System.Drawing.Size(180, 22);
            this.miMain_Restart.Text = "Пе&резапуск игры";
            this.miMain_Restart.ToolTipText = "Перезапуск игры";
            // 
            // miMain_Pause
            // 
            this.miMain_Pause.Name = "miMain_Pause";
            this.miMain_Pause.Size = new System.Drawing.Size(180, 22);
            this.miMain_Pause.Text = "&Пауза";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // miMain_Stat
            // 
            this.miMain_Stat.Name = "miMain_Stat";
            this.miMain_Stat.Size = new System.Drawing.Size(180, 22);
            this.miMain_Stat.Text = "С&татистика игр";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // miMain_Save
            // 
            this.miMain_Save.Name = "miMain_Save";
            this.miMain_Save.Size = new System.Drawing.Size(180, 22);
            this.miMain_Save.Text = "&Сохранить игру";
            // 
            // miMain_Load
            // 
            this.miMain_Load.Name = "miMain_Load";
            this.miMain_Load.Size = new System.Drawing.Size(180, 22);
            this.miMain_Load.Text = "&Загрузить игру";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // miMain_Close
            // 
            this.miMain_Close.Name = "miMain_Close";
            this.miMain_Close.Size = new System.Drawing.Size(180, 22);
            this.miMain_Close.Text = "З&акрыть";
            // 
            // miMain_Game
            // 
            this.miMain_Game.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGame_Start_X,
            this.miGame_Start_O,
            this.toolStripMenuItem3,
            this.miGame_Text,
            this.toolStripMenuItem4,
            this.miStartServer,
            this.miConnet});
            this.miMain_Game.Name = "miMain_Game";
            this.miMain_Game.Size = new System.Drawing.Size(46, 20);
            this.miMain_Game.Text = "&Игра";
            // 
            // miGame_Start_X
            // 
            this.miGame_Start_X.CheckOnClick = true;
            this.miGame_Start_X.Name = "miGame_Start_X";
            this.miGame_Start_X.Size = new System.Drawing.Size(212, 22);
            this.miGame_Start_X.Text = "Начинают &X";
            this.miGame_Start_X.Click += new System.EventHandler(this.miGame_Start_X_Click);
            // 
            // miGame_Start_O
            // 
            this.miGame_Start_O.CheckOnClick = true;
            this.miGame_Start_O.Name = "miGame_Start_O";
            this.miGame_Start_O.Size = new System.Drawing.Size(212, 22);
            this.miGame_Start_O.Text = "Начинают &O";
            this.miGame_Start_O.Click += new System.EventHandler(this.miGame_Start_O_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(209, 6);
            // 
            // miGame_Text
            // 
            this.miGame_Text.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miText_Font,
            this.miText_Color});
            this.miGame_Text.Name = "miGame_Text";
            this.miGame_Text.Size = new System.Drawing.Size(212, 22);
            this.miGame_Text.Text = "Вид текста...";
            // 
            // miText_Font
            // 
            this.miText_Font.Name = "miText_Font";
            this.miText_Font.Size = new System.Drawing.Size(150, 22);
            this.miText_Font.Text = "Шрифт текста";
            this.miText_Font.Click += new System.EventHandler(this.miText_Font_Click);
            // 
            // miText_Color
            // 
            this.miText_Color.Name = "miText_Color";
            this.miText_Color.Size = new System.Drawing.Size(150, 22);
            this.miText_Color.Text = "Цвет текста";
            this.miText_Color.Click += new System.EventHandler(this.miText_Color_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(209, 6);
            // 
            // miStartServer
            // 
            this.miStartServer.Name = "miStartServer";
            this.miStartServer.Size = new System.Drawing.Size(212, 22);
            this.miStartServer.Text = "Запустить сервер";
            this.miStartServer.Click += new System.EventHandler(this.miStartServer_Click);
            // 
            // miConnet
            // 
            this.miConnet.Name = "miConnet";
            this.miConnet.Size = new System.Drawing.Size(212, 22);
            this.miConnet.Text = "Подключиться к серверу";
            this.miConnet.Click += new System.EventHandler(this.miConnet_Click);
            // 
            // miMain_Help
            // 
            this.miMain_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miHelp_About,
            this.miHelp_ContHelp});
            this.miMain_Help.Name = "miMain_Help";
            this.miMain_Help.Size = new System.Drawing.Size(68, 20);
            this.miMain_Help.Text = "Помощь";
            // 
            // miHelp_About
            // 
            this.miHelp_About.Name = "miHelp_About";
            this.miHelp_About.Size = new System.Drawing.Size(192, 22);
            this.miHelp_About.Text = "Об авторах и игре";
            // 
            // miHelp_ContHelp
            // 
            this.miHelp_ContHelp.Name = "miHelp_ContHelp";
            this.miHelp_ContHelp.Size = new System.Drawing.Size(192, 22);
            this.miHelp_ContHelp.Text = "Контекстная помощь";
            // 
            // statusStrip1
            // 
            this.statusStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stbStepTurn,
            this.pbPastTime,
            this.stbGameTime,
            this.stbCurrTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 539);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(784, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // stbStepTurn
            // 
            this.stbStepTurn.AutoSize = false;
            this.stbStepTurn.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.stbStepTurn.Name = "stbStepTurn";
            this.stbStepTurn.Size = new System.Drawing.Size(205, 17);
            this.stbStepTurn.Spring = true;
            this.stbStepTurn.ToolTipText = "Чей ход";
            // 
            // pbPastTime
            // 
            this.pbPastTime.AutoSize = false;
            this.pbPastTime.Name = "pbPastTime";
            this.pbPastTime.Size = new System.Drawing.Size(150, 16);
            this.pbPastTime.ToolTipText = "Оставшееся время игры";
            // 
            // stbGameTime
            // 
            this.stbGameTime.AutoSize = false;
            this.stbGameTime.Name = "stbGameTime";
            this.stbGameTime.Size = new System.Drawing.Size(205, 17);
            this.stbGameTime.Spring = true;
            this.stbGameTime.ToolTipText = "Время текущей игры";
            // 
            // stbCurrTime
            // 
            this.stbCurrTime.AutoSize = false;
            this.stbCurrTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.stbCurrTime.Name = "stbCurrTime";
            this.stbCurrTime.Size = new System.Drawing.Size(205, 17);
            this.stbCurrTime.Spring = true;
            this.stbCurrTime.ToolTipText = "Текущее время";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel9);
            this.splitContainer1.Panel1.Controls.Add(this.panel7);
            this.splitContainer1.Panel1.Controls.Add(this.panel8);
            this.splitContainer1.Panel1.Controls.Add(this.panel4);
            this.splitContainer1.Panel1.Controls.Add(this.panel5);
            this.splitContainer1.Panel1.Controls.Add(this.panel6);
            this.splitContainer1.Panel1.Controls.Add(this.panel3);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer2_Panel1_MouseClick);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(784, 515);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Location = new System.Drawing.Point(89, 261);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(100, 100);
            this.panel7.TabIndex = 8;
            this.panel7.Click += new System.EventHandler(this.panel1_Click);
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Location = new System.Drawing.Point(195, 261);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(100, 100);
            this.panel8.TabIndex = 7;
            this.panel8.Click += new System.EventHandler(this.panel1_Click);
            this.panel8.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Location = new System.Drawing.Point(301, 261);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(100, 100);
            this.panel9.TabIndex = 6;
            this.panel9.Click += new System.EventHandler(this.panel1_Click);
            this.panel9.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Location = new System.Drawing.Point(89, 155);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(100, 100);
            this.panel4.TabIndex = 5;
            this.panel4.Click += new System.EventHandler(this.panel1_Click);
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Location = new System.Drawing.Point(195, 155);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(100, 100);
            this.panel5.TabIndex = 4;
            this.panel5.Click += new System.EventHandler(this.panel1_Click);
            this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Location = new System.Drawing.Point(301, 155);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(100, 100);
            this.panel6.TabIndex = 3;
            this.panel6.Click += new System.EventHandler(this.panel1_Click);
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Location = new System.Drawing.Point(301, 49);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(100, 100);
            this.panel3.TabIndex = 2;
            this.panel3.Click += new System.EventHandler(this.panel1_Click);
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(195, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(100, 100);
            this.panel2.TabIndex = 1;
            this.panel2.Click += new System.EventHandler(this.panel1_Click);
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(89, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 100);
            this.panel1.TabIndex = 0;
            this.panel1.Tag = "\" ;1;1\"";
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer2_Panel1_MouseClick);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.splitContainer2_Panel1_MouseClick);
            this.splitContainer2.Size = new System.Drawing.Size(290, 515);
            this.splitContainer2.SplitterDistance = 281;
            this.splitContainer2.TabIndex = 0;
            // 
            // tmCurTime
            // 
            this.tmCurTime.Enabled = true;
            this.tmCurTime.Tick += new System.EventHandler(this.tmCurTime_Tick);
            // 
            // tmGame
            // 
            this.tmGame.Tick += new System.EventHandler(this.tmGame_Tick);
            // 
            // fontDialog1
            // 
            this.fontDialog1.ShowApply = true;
            // 
            // contMenu1
            // 
            this.contMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pmiRestart,
            this.pmiPause,
            this.toolStripSeparator2,
            this.pmiColor,
            this.pmiFont});
            this.contMenu1.Name = "contMenu1";
            this.contMenu1.Size = new System.Drawing.Size(155, 98);
            // 
            // pmiRestart
            // 
            this.pmiRestart.Name = "pmiRestart";
            this.pmiRestart.Size = new System.Drawing.Size(154, 22);
            this.pmiRestart.Text = "Начать заново";
            // 
            // pmiPause
            // 
            this.pmiPause.Name = "pmiPause";
            this.pmiPause.Size = new System.Drawing.Size(154, 22);
            this.pmiPause.Text = "Пауза";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(151, 6);
            // 
            // pmiColor
            // 
            this.pmiColor.Name = "pmiColor";
            this.pmiColor.Size = new System.Drawing.Size(154, 22);
            this.pmiColor.Text = "Цвет текста";
            this.pmiColor.Click += new System.EventHandler(this.miText_Color_Click);
            // 
            // pmiFont
            // 
            this.pmiFont.Name = "pmiFont";
            this.pmiFont.Size = new System.Drawing.Size(154, 22);
            this.pmiFont.Text = "Шрифт";
            this.pmiFont.Click += new System.EventHandler(this.miText_Font_Click);
            // 
            // GameXO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameXO";
            this.Text = "Игра \"Крестики-Нолики\"";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.contMenu1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem miMain_Main;
    private System.Windows.Forms.ToolStripMenuItem miMain_NewGame;
    private System.Windows.Forms.ToolStripMenuItem miMain_Restart;
    private System.Windows.Forms.ToolStripMenuItem miMain_Pause;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem miMain_Stat;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem miMain_Save;
    private System.Windows.Forms.ToolStripMenuItem miMain_Load;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripMenuItem miMain_Close;
    private System.Windows.Forms.ToolStripMenuItem miMain_Game;
    private System.Windows.Forms.ToolStripMenuItem miMain_Help;
    private System.Windows.Forms.ToolStripMenuItem miGame_Start_X;
    private System.Windows.Forms.ToolStripMenuItem miGame_Start_O;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripMenuItem miGame_Text;
    private System.Windows.Forms.ToolStripMenuItem miText_Font;
    private System.Windows.Forms.ToolStripMenuItem miText_Color;
    private System.Windows.Forms.ToolStripMenuItem miHelp_About;
    private System.Windows.Forms.ToolStripMenuItem miHelp_ContHelp;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel stbGameTime;
    private System.Windows.Forms.ToolStripStatusLabel stbStepTurn;
    private System.Windows.Forms.ToolStripStatusLabel stbCurrTime;
    private System.Windows.Forms.ToolStripProgressBar pbPastTime;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.SplitContainer splitContainer2;
    private System.Windows.Forms.Panel panel7;
    private System.Windows.Forms.Panel panel8;
    private System.Windows.Forms.Panel panel9;
    private System.Windows.Forms.Panel panel4;
    private System.Windows.Forms.Panel panel5;
    private System.Windows.Forms.Panel panel6;
    private System.Windows.Forms.Panel panel3;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Timer tmCurTime;
    private System.Windows.Forms.Timer tmGame;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.ColorDialog colorDialog1;
    private System.Windows.Forms.ContextMenuStrip contMenu1;
    private System.Windows.Forms.ToolStripMenuItem pmiRestart;
    private System.Windows.Forms.ToolStripMenuItem pmiPause;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripMenuItem pmiColor;
    private System.Windows.Forms.ToolStripMenuItem pmiFont;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem miStartServer;
    private System.Windows.Forms.ToolStripMenuItem miConnet;
  }
}

