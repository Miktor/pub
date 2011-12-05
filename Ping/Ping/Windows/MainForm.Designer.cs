namespace Ping
{
    partial class MainForm
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
                if(client!=null)
                    client.Dispose();
                if (iSocket != null)
                    iSocket.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TCPportBox = new System.Windows.Forms.GroupBox();
            this.maxport = new System.Windows.Forms.Label();
            this.TCPmaxPortAdr = new System.Windows.Forms.TextBox();
            this.minPort = new System.Windows.Forms.Label();
            this.TCPminPortAdr = new System.Windows.Forms.TextBox();
            this.programType = new System.Windows.Forms.GroupBox();
            this.optPort = new System.Windows.Forms.TextBox();
            this.clientChek = new System.Windows.Forms.RadioButton();
            this.serverChek = new System.Windows.Forms.RadioButton();
            this.ipList = new IPAddressControlLib.IPAddressControl();
            this.label2 = new System.Windows.Forms.Label();
            this.PingTypeBox = new System.Windows.Forms.GroupBox();
            this.UDPportBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UDPmaxPortAdr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.UDPminPortAdr = new System.Windows.Forms.TextBox();
            this.UDPConnect = new System.Windows.Forms.CheckBox();
            this.TCPConnect = new System.Windows.Forms.CheckBox();
            this.startBTN = new System.Windows.Forms.Button();
            this.stop = new System.Windows.Forms.Button();
            this.clean = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.ShemaCB = new System.Windows.Forms.ComboBox();
            this.DelBut = new System.Windows.Forms.Button();
            this.SaveBut = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveTempleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteTampleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OutputText = new System.Windows.Forms.RichTextBox();
            this.TCPportBox.SuspendLayout();
            this.programType.SuspendLayout();
            this.PingTypeBox.SuspendLayout();
            this.UDPportBox.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TCPportBox
            // 
            this.TCPportBox.BackColor = System.Drawing.SystemColors.Control;
            this.TCPportBox.Controls.Add(this.maxport);
            this.TCPportBox.Controls.Add(this.TCPmaxPortAdr);
            this.TCPportBox.Controls.Add(this.minPort);
            this.TCPportBox.Controls.Add(this.TCPminPortAdr);
            this.TCPportBox.Location = new System.Drawing.Point(59, 19);
            this.TCPportBox.MinimumSize = new System.Drawing.Size(154, 88);
            this.TCPportBox.Name = "TCPportBox";
            this.TCPportBox.Size = new System.Drawing.Size(154, 88);
            this.TCPportBox.TabIndex = 1;
            this.TCPportBox.TabStop = false;
            this.TCPportBox.Text = "Диапозон портов";
            // 
            // maxport
            // 
            this.maxport.AutoSize = true;
            this.maxport.Location = new System.Drawing.Point(9, 55);
            this.maxport.Name = "maxport";
            this.maxport.Size = new System.Drawing.Size(27, 13);
            this.maxport.TabIndex = 7;
            this.maxport.Text = "Max";
            // 
            // TCPmaxPortAdr
            // 
            this.TCPmaxPortAdr.Location = new System.Drawing.Point(39, 52);
            this.TCPmaxPortAdr.Name = "TCPmaxPortAdr";
            this.TCPmaxPortAdr.Size = new System.Drawing.Size(103, 20);
            this.TCPmaxPortAdr.TabIndex = 6;
            this.TCPmaxPortAdr.Text = "65000";
            // 
            // minPort
            // 
            this.minPort.AutoSize = true;
            this.minPort.Location = new System.Drawing.Point(9, 22);
            this.minPort.Name = "minPort";
            this.minPort.Size = new System.Drawing.Size(24, 13);
            this.minPort.TabIndex = 5;
            this.minPort.Text = "Min";
            // 
            // TCPminPortAdr
            // 
            this.TCPminPortAdr.Location = new System.Drawing.Point(39, 19);
            this.TCPminPortAdr.Name = "TCPminPortAdr";
            this.TCPminPortAdr.Size = new System.Drawing.Size(103, 20);
            this.TCPminPortAdr.TabIndex = 5;
            this.TCPminPortAdr.Text = "65000";
            // 
            // programType
            // 
            this.programType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.programType.Controls.Add(this.optPort);
            this.programType.Controls.Add(this.clientChek);
            this.programType.Controls.Add(this.serverChek);
            this.programType.Controls.Add(this.ipList);
            this.programType.Controls.Add(this.label2);
            this.programType.Controls.Add(this.PingTypeBox);
            this.programType.Location = new System.Drawing.Point(12, 56);
            this.programType.Name = "programType";
            this.programType.Size = new System.Drawing.Size(228, 333);
            this.programType.TabIndex = 8;
            this.programType.TabStop = false;
            this.programType.Text = "Тип компьютера";
            // 
            // optPort
            // 
            this.optPort.Location = new System.Drawing.Point(113, 92);
            this.optPort.Name = "optPort";
            this.optPort.Size = new System.Drawing.Size(103, 20);
            this.optPort.TabIndex = 3;
            this.optPort.Text = "10000";
            // 
            // clientChek
            // 
            this.clientChek.AutoSize = true;
            this.clientChek.Location = new System.Drawing.Point(36, 19);
            this.clientChek.Name = "clientChek";
            this.clientChek.Size = new System.Drawing.Size(61, 17);
            this.clientChek.TabIndex = 0;
            this.clientChek.TabStop = true;
            this.clientChek.Text = "Клиент";
            this.clientChek.UseVisualStyleBackColor = true;
            this.clientChek.CheckedChanged += new System.EventHandler(this.clientChek_CheckedChanged);
            // 
            // serverChek
            // 
            this.serverChek.AutoSize = true;
            this.serverChek.Location = new System.Drawing.Point(36, 68);
            this.serverChek.Name = "serverChek";
            this.serverChek.Size = new System.Drawing.Size(62, 17);
            this.serverChek.TabIndex = 1;
            this.serverChek.TabStop = true;
            this.serverChek.Text = "Сервер";
            this.serverChek.UseVisualStyleBackColor = true;
            this.serverChek.CheckedChanged += new System.EventHandler(this.serverChek_CheckedChanged);
            // 
            // ipList
            // 
            this.ipList.AllowInternalTab = false;
            this.ipList.AutoHeight = true;
            this.ipList.BackColor = System.Drawing.SystemColors.Window;
            this.ipList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipList.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipList.Enabled = false;
            this.ipList.Location = new System.Drawing.Point(48, 42);
            this.ipList.Name = "ipList";
            this.ipList.ReadOnly = false;
            this.ipList.Size = new System.Drawing.Size(108, 20);
            this.ipList.TabIndex = 2;
            this.ipList.Text = "192.168.0.100";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(1, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 27);
            this.label2.TabIndex = 20;
            this.label2.Text = "Порт получения\\ отправки настроек";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // PingTypeBox
            // 
            this.PingTypeBox.BackColor = System.Drawing.SystemColors.Control;
            this.PingTypeBox.Controls.Add(this.UDPportBox);
            this.PingTypeBox.Controls.Add(this.UDPConnect);
            this.PingTypeBox.Controls.Add(this.TCPConnect);
            this.PingTypeBox.Controls.Add(this.TCPportBox);
            this.PingTypeBox.Location = new System.Drawing.Point(6, 118);
            this.PingTypeBox.MinimumSize = new System.Drawing.Size(218, 208);
            this.PingTypeBox.Name = "PingTypeBox";
            this.PingTypeBox.Size = new System.Drawing.Size(218, 208);
            this.PingTypeBox.TabIndex = 12;
            this.PingTypeBox.TabStop = false;
            this.PingTypeBox.Text = "Тип Соединения";
            // 
            // UDPportBox
            // 
            this.UDPportBox.BackColor = System.Drawing.SystemColors.Control;
            this.UDPportBox.Controls.Add(this.label1);
            this.UDPportBox.Controls.Add(this.UDPmaxPortAdr);
            this.UDPportBox.Controls.Add(this.label4);
            this.UDPportBox.Controls.Add(this.UDPminPortAdr);
            this.UDPportBox.Enabled = false;
            this.UDPportBox.Location = new System.Drawing.Point(58, 113);
            this.UDPportBox.MinimumSize = new System.Drawing.Size(154, 88);
            this.UDPportBox.Name = "UDPportBox";
            this.UDPportBox.Size = new System.Drawing.Size(154, 88);
            this.UDPportBox.TabIndex = 8;
            this.UDPportBox.TabStop = false;
            this.UDPportBox.Text = "Диапозон портов";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Max";
            // 
            // UDPmaxPortAdr
            // 
            this.UDPmaxPortAdr.Location = new System.Drawing.Point(39, 52);
            this.UDPmaxPortAdr.Name = "UDPmaxPortAdr";
            this.UDPmaxPortAdr.Size = new System.Drawing.Size(103, 20);
            this.UDPmaxPortAdr.TabIndex = 9;
            this.UDPmaxPortAdr.Text = "65000";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Min";
            // 
            // UDPminPortAdr
            // 
            this.UDPminPortAdr.Location = new System.Drawing.Point(39, 19);
            this.UDPminPortAdr.Name = "UDPminPortAdr";
            this.UDPminPortAdr.Size = new System.Drawing.Size(103, 20);
            this.UDPminPortAdr.TabIndex = 8;
            this.UDPminPortAdr.Text = "65000";
            // 
            // UDPConnect
            // 
            this.UDPConnect.AutoSize = true;
            this.UDPConnect.Location = new System.Drawing.Point(6, 148);
            this.UDPConnect.Name = "UDPConnect";
            this.UDPConnect.Size = new System.Drawing.Size(49, 17);
            this.UDPConnect.TabIndex = 7;
            this.UDPConnect.Text = "UDP";
            this.UDPConnect.UseVisualStyleBackColor = true;
            this.UDPConnect.CheckedChanged += new System.EventHandler(this.UDPConnect_CheckedChanged);
            // 
            // TCPConnect
            // 
            this.TCPConnect.AutoSize = true;
            this.TCPConnect.Checked = true;
            this.TCPConnect.CheckState = System.Windows.Forms.CheckState.Checked;
            this.TCPConnect.Location = new System.Drawing.Point(6, 54);
            this.TCPConnect.Name = "TCPConnect";
            this.TCPConnect.Size = new System.Drawing.Size(47, 17);
            this.TCPConnect.TabIndex = 4;
            this.TCPConnect.Text = "TCP";
            this.TCPConnect.UseVisualStyleBackColor = true;
            this.TCPConnect.CheckedChanged += new System.EventHandler(this.TCPConnect_CheckedChanged);
            // 
            // startBTN
            // 
            this.startBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startBTN.Location = new System.Drawing.Point(48, 446);
            this.startBTN.Name = "startBTN";
            this.startBTN.Size = new System.Drawing.Size(70, 23);
            this.startBTN.TabIndex = 10;
            this.startBTN.Text = "Старт";
            this.startBTN.UseVisualStyleBackColor = true;
            this.startBTN.Click += new System.EventHandler(this.startBTN_Click);
            // 
            // stop
            // 
            this.stop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.stop.Enabled = false;
            this.stop.Location = new System.Drawing.Point(147, 446);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(74, 23);
            this.stop.TabIndex = 11;
            this.stop.Text = "Стоп";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // clean
            // 
            this.clean.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clean.Location = new System.Drawing.Point(891, 445);
            this.clean.Name = "clean";
            this.clean.Size = new System.Drawing.Size(75, 23);
            this.clean.TabIndex = 13;
            this.clean.Text = "Очистить";
            this.clean.UseVisualStyleBackColor = true;
            this.clean.Click += new System.EventHandler(this.clean_Click);
            // 
            // save
            // 
            this.save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save.Location = new System.Drawing.Point(810, 445);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 12;
            this.save.Text = "Сохранить";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.saveText_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Чтобы развернуть окно, нажмите на иконку";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Ping";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // ShemaCB
            // 
            this.ShemaCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ShemaCB.FormattingEnabled = true;
            this.ShemaCB.Location = new System.Drawing.Point(12, 27);
            this.ShemaCB.Name = "ShemaCB";
            this.ShemaCB.Size = new System.Drawing.Size(164, 21);
            this.ShemaCB.TabIndex = 22;
            this.ShemaCB.SelectedIndexChanged += new System.EventHandler(this.ShemaCB_SelectedIndexChanged);
            // 
            // DelBut
            // 
            this.DelBut.BackgroundImage = global::Ping.Properties.Resources.del;
            this.DelBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DelBut.Location = new System.Drawing.Point(221, 22);
            this.DelBut.Name = "DelBut";
            this.DelBut.Size = new System.Drawing.Size(32, 28);
            this.DelBut.TabIndex = 24;
            this.DelBut.UseVisualStyleBackColor = true;
            this.DelBut.Click += new System.EventHandler(this.DelBut_Click);
            // 
            // SaveBut
            // 
            this.SaveBut.BackgroundImage = global::Ping.Properties.Resources.save;
            this.SaveBut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SaveBut.Location = new System.Drawing.Point(183, 22);
            this.SaveBut.Name = "SaveBut";
            this.SaveBut.Size = new System.Drawing.Size(32, 28);
            this.SaveBut.TabIndex = 23;
            this.SaveBut.UseVisualStyleBackColor = true;
            this.SaveBut.Click += new System.EventHandler(this.SaveShema_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 24);
            this.menuStrip1.TabIndex = 25;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveReportToolStripMenuItem,
            this.saveTempleToolStripMenuItem,
            this.deleteTampleToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // saveReportToolStripMenuItem
            // 
            this.saveReportToolStripMenuItem.Name = "saveReportToolStripMenuItem";
            this.saveReportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveReportToolStripMenuItem.Text = "Сохранить отчет";
            this.saveReportToolStripMenuItem.Click += new System.EventHandler(this.saveText_Click);
            // 
            // saveTempleToolStripMenuItem
            // 
            this.saveTempleToolStripMenuItem.Name = "saveTempleToolStripMenuItem";
            this.saveTempleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveTempleToolStripMenuItem.Text = "Сохранить шаблон";
            this.saveTempleToolStripMenuItem.Click += new System.EventHandler(this.SaveShema_Click);
            // 
            // deleteTampleToolStripMenuItem
            // 
            this.deleteTampleToolStripMenuItem.Name = "deleteTampleToolStripMenuItem";
            this.deleteTampleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.deleteTampleToolStripMenuItem.Text = "Удалить шаблон";
            this.deleteTampleToolStripMenuItem.Click += new System.EventHandler(this.DelBut_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ExitToolStripMenuItem.Text = "Выход";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.aboutToolStripMenuItem.Text = "О программе";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // OutputText
            // 
            this.OutputText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.OutputText.BackColor = System.Drawing.SystemColors.Window;
            this.OutputText.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OutputText.Location = new System.Drawing.Point(259, 22);
            this.OutputText.Name = "OutputText";
            this.OutputText.ReadOnly = true;
            this.OutputText.Size = new System.Drawing.Size(707, 417);
            this.OutputText.TabIndex = 27;
            this.OutputText.Text = "";
            this.OutputText.WordWrap = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 480);
            this.Controls.Add(this.OutputText);
            this.Controls.Add(this.save);
            this.Controls.Add(this.DelBut);
            this.Controls.Add(this.SaveBut);
            this.Controls.Add(this.ShemaCB);
            this.Controls.Add(this.clean);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.programType);
            this.Controls.Add(this.startBTN);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(674, 467);
            this.Name = "MainForm";
            this.Text = "Ping";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_Deactivate);
            this.TCPportBox.ResumeLayout(false);
            this.TCPportBox.PerformLayout();
            this.programType.ResumeLayout(false);
            this.programType.PerformLayout();
            this.PingTypeBox.ResumeLayout(false);
            this.PingTypeBox.PerformLayout();
            this.UDPportBox.ResumeLayout(false);
            this.UDPportBox.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox TCPportBox;
        private System.Windows.Forms.Label maxport;
        private System.Windows.Forms.TextBox TCPmaxPortAdr;
        private System.Windows.Forms.Label minPort;
        private System.Windows.Forms.TextBox TCPminPortAdr;
        private System.Windows.Forms.GroupBox programType;
        private System.Windows.Forms.RadioButton serverChek;
        private System.Windows.Forms.RadioButton clientChek;
        private System.Windows.Forms.Button startBTN;
        private System.Windows.Forms.GroupBox PingTypeBox;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.GroupBox UDPportBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UDPmaxPortAdr;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox UDPminPortAdr;
        private System.Windows.Forms.CheckBox UDPConnect;
        private System.Windows.Forms.CheckBox TCPConnect;
        private IPAddressControlLib.IPAddressControl ipList;
        private System.Windows.Forms.Button clean;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ComboBox ShemaCB;
        private System.Windows.Forms.Button SaveBut;
        private System.Windows.Forms.Button DelBut;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveTempleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteTampleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TextBox optPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox OutputText;
    }
}

