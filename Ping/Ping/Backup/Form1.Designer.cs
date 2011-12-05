namespace Ping
{
    partial class Form1
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
            this.resultView = new System.Windows.Forms.TextBox();
            this.portBox = new System.Windows.Forms.GroupBox();
            this.maxport = new System.Windows.Forms.Label();
            this.maxPortAdr = new System.Windows.Forms.TextBox();
            this.minPort = new System.Windows.Forms.Label();
            this.minPortAdr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.programType = new System.Windows.Forms.GroupBox();
            this.serverChek = new System.Windows.Forms.RadioButton();
            this.clientChek = new System.Windows.Forms.RadioButton();
            this.startBTN = new System.Windows.Forms.Button();
            this.PingTypeBox = new System.Windows.Forms.GroupBox();
            this.protocolUDP = new System.Windows.Forms.RadioButton();
            this.protocolTCP = new System.Windows.Forms.RadioButton();
            this.pingProgress = new System.Windows.Forms.ProgressBar();
            this.stop = new System.Windows.Forms.Button();
            this.ipList = new System.Windows.Forms.ComboBox();
            this.portBox.SuspendLayout();
            this.programType.SuspendLayout();
            this.PingTypeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // resultView
            // 
            this.resultView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resultView.Location = new System.Drawing.Point(186, 29);
            this.resultView.Multiline = true;
            this.resultView.Name = "resultView";
            this.resultView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.resultView.Size = new System.Drawing.Size(246, 463);
            this.resultView.TabIndex = 0;
            // 
            // portBox
            // 
            this.portBox.Controls.Add(this.maxport);
            this.portBox.Controls.Add(this.maxPortAdr);
            this.portBox.Controls.Add(this.minPort);
            this.portBox.Controls.Add(this.minPortAdr);
            this.portBox.Location = new System.Drawing.Point(12, 79);
            this.portBox.Name = "portBox";
            this.portBox.Size = new System.Drawing.Size(154, 88);
            this.portBox.TabIndex = 1;
            this.portBox.TabStop = false;
            this.portBox.Text = "Диапозон портов";
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
            // maxPortAdr
            // 
            this.maxPortAdr.Location = new System.Drawing.Point(39, 52);
            this.maxPortAdr.Name = "maxPortAdr";
            this.maxPortAdr.Size = new System.Drawing.Size(103, 20);
            this.maxPortAdr.TabIndex = 6;
            this.maxPortAdr.Text = "65010";
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
            // minPortAdr
            // 
            this.minPortAdr.Location = new System.Drawing.Point(39, 19);
            this.minPortAdr.Name = "minPortAdr";
            this.minPortAdr.Size = new System.Drawing.Size(103, 20);
            this.minPortAdr.TabIndex = 4;
            this.minPortAdr.Text = "65000";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "IP";
            // 
            // programType
            // 
            this.programType.Controls.Add(this.serverChek);
            this.programType.Controls.Add(this.clientChek);
            this.programType.Location = new System.Drawing.Point(12, 173);
            this.programType.Name = "programType";
            this.programType.Size = new System.Drawing.Size(154, 44);
            this.programType.TabIndex = 8;
            this.programType.TabStop = false;
            this.programType.Text = "Тип компьютера";
            // 
            // serverChek
            // 
            this.serverChek.AutoSize = true;
            this.serverChek.Location = new System.Drawing.Point(80, 19);
            this.serverChek.Name = "serverChek";
            this.serverChek.Size = new System.Drawing.Size(62, 17);
            this.serverChek.TabIndex = 1;
            this.serverChek.TabStop = true;
            this.serverChek.Text = "Сервер";
            this.serverChek.UseVisualStyleBackColor = true;
            // 
            // clientChek
            // 
            this.clientChek.AutoSize = true;
            this.clientChek.Location = new System.Drawing.Point(12, 19);
            this.clientChek.Name = "clientChek";
            this.clientChek.Size = new System.Drawing.Size(61, 17);
            this.clientChek.TabIndex = 0;
            this.clientChek.TabStop = true;
            this.clientChek.Text = "Клиент";
            this.clientChek.UseVisualStyleBackColor = true;
            // 
            // startBTN
            // 
            this.startBTN.Location = new System.Drawing.Point(15, 223);
            this.startBTN.Name = "startBTN";
            this.startBTN.Size = new System.Drawing.Size(70, 23);
            this.startBTN.TabIndex = 9;
            this.startBTN.Text = "Старт";
            this.startBTN.UseVisualStyleBackColor = true;
            this.startBTN.Click += new System.EventHandler(this.startBTN_Click);
            // 
            // PingTypeBox
            // 
            this.PingTypeBox.Controls.Add(this.protocolUDP);
            this.PingTypeBox.Controls.Add(this.protocolTCP);
            this.PingTypeBox.Location = new System.Drawing.Point(12, 29);
            this.PingTypeBox.Name = "PingTypeBox";
            this.PingTypeBox.Size = new System.Drawing.Size(154, 44);
            this.PingTypeBox.TabIndex = 12;
            this.PingTypeBox.TabStop = false;
            this.PingTypeBox.Text = "Тип Соединения";
            // 
            // protocolUDP
            // 
            this.protocolUDP.AutoSize = true;
            this.protocolUDP.Location = new System.Drawing.Point(78, 19);
            this.protocolUDP.Name = "protocolUDP";
            this.protocolUDP.Size = new System.Drawing.Size(48, 17);
            this.protocolUDP.TabIndex = 1;
            this.protocolUDP.Text = "UDP";
            this.protocolUDP.UseVisualStyleBackColor = true;
            // 
            // protocolTCP
            // 
            this.protocolTCP.AutoSize = true;
            this.protocolTCP.Checked = true;
            this.protocolTCP.Location = new System.Drawing.Point(12, 19);
            this.protocolTCP.Name = "protocolTCP";
            this.protocolTCP.Size = new System.Drawing.Size(46, 17);
            this.protocolTCP.TabIndex = 0;
            this.protocolTCP.TabStop = true;
            this.protocolTCP.Text = "TCP";
            this.protocolTCP.UseVisualStyleBackColor = true;
            // 
            // pingProgress
            // 
            this.pingProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pingProgress.Location = new System.Drawing.Point(186, 3);
            this.pingProgress.Name = "pingProgress";
            this.pingProgress.Size = new System.Drawing.Size(246, 20);
            this.pingProgress.Step = 1;
            this.pingProgress.TabIndex = 13;
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(92, 223);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(74, 23);
            this.stop.TabIndex = 14;
            this.stop.Text = "Стоп";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.Click += new System.EventHandler(this.stop_Click);
            // 
            // ipList
            // 
            this.ipList.FormattingEnabled = true;
            this.ipList.Location = new System.Drawing.Point(35, 2);
            this.ipList.Name = "ipList";
            this.ipList.Size = new System.Drawing.Size(131, 21);
            this.ipList.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 500);
            this.Controls.Add(this.ipList);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.pingProgress);
            this.Controls.Add(this.PingTypeBox);
            this.Controls.Add(this.startBTN);
            this.Controls.Add(this.programType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portBox);
            this.Controls.Add(this.resultView);
            this.Name = "Form1";
            this.Text = "ping";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.portBox.ResumeLayout(false);
            this.portBox.PerformLayout();
            this.programType.ResumeLayout(false);
            this.programType.PerformLayout();
            this.PingTypeBox.ResumeLayout(false);
            this.PingTypeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox portBox;
        private System.Windows.Forms.Label maxport;
        private System.Windows.Forms.TextBox maxPortAdr;
        private System.Windows.Forms.Label minPort;
        private System.Windows.Forms.TextBox minPortAdr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox programType;
        private System.Windows.Forms.RadioButton serverChek;
        private System.Windows.Forms.RadioButton clientChek;
        private System.Windows.Forms.Button startBTN;
        private System.Windows.Forms.GroupBox PingTypeBox;
        private System.Windows.Forms.RadioButton protocolUDP;
        private System.Windows.Forms.RadioButton protocolTCP;
        private System.Windows.Forms.ProgressBar pingProgress;
        private System.Windows.Forms.Button stop;
        public System.Windows.Forms.TextBox resultView;
        private System.Windows.Forms.ComboBox ipList;
    }
}

