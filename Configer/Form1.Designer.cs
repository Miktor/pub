namespace Configer
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.AllValid = new System.Windows.Forms.TabPage();
            this.FullValidOutput = new System.Windows.Forms.TextBox();
            this.HostValid = new System.Windows.Forms.TabPage();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LabelParsec = new System.Windows.Forms.Label();
            this.PanelParsec = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.LabelParsecServer = new System.Windows.Forms.Label();
            this.ListParsecServer = new System.Windows.Forms.ListView();
            this.panel6 = new System.Windows.Forms.Panel();
            this.LabelTransport = new System.Windows.Forms.Label();
            this.ListTransport = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LabelServiceHost = new System.Windows.Forms.Label();
            this.ListParsecServiceHost = new System.Windows.Forms.ListView();
            this.SaveHostState = new System.Windows.Forms.Button();
            this.IconValid = new System.Windows.Forms.TabPage();
            this.ListIconsCompare = new System.Windows.Forms.ListView();
            this.AssemblyValid = new System.Windows.Forms.TabPage();
            this.ListAssemblyCompare = new System.Windows.Forms.ListView();
            this.HostErrorOutput = new System.Windows.Forms.TextBox();
            this.CalcTimer = new System.Windows.Forms.Timer(this.components);
            this.Help = new System.Windows.Forms.HelpProvider();
            this.tabControl2.SuspendLayout();
            this.AllValid.SuspendLayout();
            this.HostValid.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PanelParsec.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel2.SuspendLayout();
            this.IconValid.SuspendLayout();
            this.AssemblyValid.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(334, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Настройки";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenSettings);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(76, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "Запустить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl2.Controls.Add(this.AllValid);
            this.tabControl2.Controls.Add(this.HostValid);
            this.tabControl2.Controls.Add(this.IconValid);
            this.tabControl2.Controls.Add(this.AssemblyValid);
            this.tabControl2.Location = new System.Drawing.Point(12, 41);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(520, 633);
            this.tabControl2.TabIndex = 36;
            // 
            // AllValid
            // 
            this.AllValid.Controls.Add(this.FullValidOutput);
            this.AllValid.Location = new System.Drawing.Point(4, 22);
            this.AllValid.Name = "AllValid";
            this.AllValid.Padding = new System.Windows.Forms.Padding(3);
            this.AllValid.Size = new System.Drawing.Size(512, 607);
            this.AllValid.TabIndex = 0;
            this.AllValid.Text = "Полная проверка";
            this.AllValid.UseVisualStyleBackColor = true;
            this.AllValid.Enter += new System.EventHandler(this.FullValidActivate);
            // 
            // FullValidOutput
            // 
            this.FullValidOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FullValidOutput.Location = new System.Drawing.Point(0, 0);
            this.FullValidOutput.Multiline = true;
            this.FullValidOutput.Name = "FullValidOutput";
            this.FullValidOutput.ReadOnly = true;
            this.FullValidOutput.Size = new System.Drawing.Size(520, 606);
            this.FullValidOutput.TabIndex = 0;
            // 
            // HostValid
            // 
            this.HostValid.Controls.Add(this.MainPanel);
            this.HostValid.Controls.Add(this.SaveHostState);
            this.HostValid.Location = new System.Drawing.Point(4, 22);
            this.HostValid.Name = "HostValid";
            this.HostValid.Size = new System.Drawing.Size(512, 607);
            this.HostValid.TabIndex = 1;
            this.HostValid.Text = "Проверка hosts";
            this.HostValid.UseVisualStyleBackColor = true;
            this.HostValid.Enter += new System.EventHandler(this.HostActivate);
            // 
            // MainPanel
            // 
            this.MainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPanel.BackColor = System.Drawing.Color.Gainsboro;
            this.MainPanel.Controls.Add(this.panel3);
            this.MainPanel.Controls.Add(this.panel2);
            this.Help.SetHelpKeyword(this.MainPanel, "Slide Panel");
            this.Help.SetHelpString(this.MainPanel, "Click on the desired label to push the results");
            this.MainPanel.Location = new System.Drawing.Point(3, 3);
            this.MainPanel.Name = "MainPanel";
            this.Help.SetShowHelp(this.MainPanel, true);
            this.MainPanel.Size = new System.Drawing.Size(506, 572);
            this.MainPanel.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.LightGray;
            this.panel3.Controls.Add(this.LabelParsec);
            this.panel3.Controls.Add(this.PanelParsec);
            this.Help.SetHelpKeyword(this.panel3, "Slide Panel");
            this.Help.SetHelpString(this.panel3, "Click on the desired label to push the results");
            this.panel3.Location = new System.Drawing.Point(6, 45);
            this.panel3.Name = "panel3";
            this.Help.SetShowHelp(this.panel3, true);
            this.panel3.Size = new System.Drawing.Size(494, 40);
            this.panel3.TabIndex = 2;
            // 
            // LabelParsec
            // 
            this.LabelParsec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelParsec.BackColor = System.Drawing.Color.LawnGreen;
            this.LabelParsec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelParsec.Font = new System.Drawing.Font("Modern No. 20", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Help.SetHelpKeyword(this.LabelParsec, "Slide Panel");
            this.Help.SetHelpString(this.LabelParsec, "Click on the desired label to push the results");
            this.LabelParsec.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelParsec.Location = new System.Drawing.Point(7, 9);
            this.LabelParsec.Name = "LabelParsec";
            this.Help.SetShowHelp(this.LabelParsec, true);
            this.LabelParsec.Size = new System.Drawing.Size(478, 23);
            this.LabelParsec.TabIndex = 1;
            this.LabelParsec.Text = "Parces";
            this.LabelParsec.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PanelParsec
            // 
            this.PanelParsec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelParsec.BackColor = System.Drawing.Color.Silver;
            this.PanelParsec.Controls.Add(this.panel5);
            this.PanelParsec.Controls.Add(this.panel6);
            this.PanelParsec.Location = new System.Drawing.Point(4, 39);
            this.PanelParsec.Name = "PanelParsec";
            this.PanelParsec.Size = new System.Drawing.Size(487, 0);
            this.PanelParsec.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.DarkGray;
            this.panel5.Controls.Add(this.LabelParsecServer);
            this.panel5.Controls.Add(this.ListParsecServer);
            this.Help.SetHelpKeyword(this.panel5, "Slide Panel");
            this.Help.SetHelpString(this.panel5, "Click on the desired label to push the results");
            this.panel5.Location = new System.Drawing.Point(3, 40);
            this.panel5.Name = "panel5";
            this.Help.SetShowHelp(this.panel5, true);
            this.panel5.Size = new System.Drawing.Size(481, 32);
            this.panel5.TabIndex = 2;
            // 
            // LabelParsecServer
            // 
            this.LabelParsecServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelParsecServer.BackColor = System.Drawing.Color.LawnGreen;
            this.LabelParsecServer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelParsecServer.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Help.SetHelpKeyword(this.LabelParsecServer, "Slide Panel");
            this.Help.SetHelpString(this.LabelParsecServer, "Click on the desired label to push the results");
            this.LabelParsecServer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelParsecServer.Location = new System.Drawing.Point(3, 5);
            this.LabelParsecServer.Name = "LabelParsecServer";
            this.Help.SetShowHelp(this.LabelParsecServer, true);
            this.LabelParsecServer.Size = new System.Drawing.Size(475, 22);
            this.LabelParsecServer.TabIndex = 4;
            this.LabelParsecServer.Text = "ParsecServer";
            this.LabelParsecServer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListParsecServer
            // 
            this.ListParsecServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Help.SetHelpKeyword(this.ListParsecServer, "Slide Panel");
            this.Help.SetHelpString(this.ListParsecServer, "Click on the desired label to push the results");
            this.ListParsecServer.Location = new System.Drawing.Point(3, 30);
            this.ListParsecServer.Name = "ListParsecServer";
            this.Help.SetShowHelp(this.ListParsecServer, true);
            this.ListParsecServer.Size = new System.Drawing.Size(475, 0);
            this.ListParsecServer.TabIndex = 3;
            this.ListParsecServer.UseCompatibleStateImageBehavior = false;
            this.ListParsecServer.View = System.Windows.Forms.View.Details;
            this.ListParsecServer.Invalidated += new System.Windows.Forms.InvalidateEventHandler(this.lvReDrow);
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.DarkGray;
            this.panel6.Controls.Add(this.LabelTransport);
            this.panel6.Controls.Add(this.ListTransport);
            this.Help.SetHelpKeyword(this.panel6, "Slide Panel");
            this.Help.SetHelpString(this.panel6, "Click on the desired label to push the results");
            this.panel6.Location = new System.Drawing.Point(3, 3);
            this.panel6.Name = "panel6";
            this.Help.SetShowHelp(this.panel6, true);
            this.panel6.Size = new System.Drawing.Size(481, 31);
            this.panel6.TabIndex = 1;
            // 
            // LabelTransport
            // 
            this.LabelTransport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelTransport.BackColor = System.Drawing.Color.LawnGreen;
            this.LabelTransport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelTransport.Font = new System.Drawing.Font("Lucida Console", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Help.SetHelpKeyword(this.LabelTransport, "Slide Panel");
            this.Help.SetHelpString(this.LabelTransport, "Click on the desired label to push the results");
            this.LabelTransport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelTransport.Location = new System.Drawing.Point(3, 5);
            this.LabelTransport.Name = "LabelTransport";
            this.Help.SetShowHelp(this.LabelTransport, true);
            this.LabelTransport.Size = new System.Drawing.Size(475, 22);
            this.LabelTransport.TabIndex = 4;
            this.LabelTransport.Text = "Transport";
            this.LabelTransport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListTransport
            // 
            this.ListTransport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Help.SetHelpKeyword(this.ListTransport, "Slide Panel");
            this.Help.SetHelpString(this.ListTransport, "Click on the desired label to push the results");
            this.ListTransport.Location = new System.Drawing.Point(3, 30);
            this.ListTransport.Name = "ListTransport";
            this.Help.SetShowHelp(this.ListTransport, true);
            this.ListTransport.Size = new System.Drawing.Size(475, 0);
            this.ListTransport.TabIndex = 3;
            this.ListTransport.UseCompatibleStateImageBehavior = false;
            this.ListTransport.View = System.Windows.Forms.View.Details;
            this.ListTransport.Invalidated += new System.Windows.Forms.InvalidateEventHandler(this.lvReDrow);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.LightGray;
            this.panel2.Controls.Add(this.LabelServiceHost);
            this.panel2.Controls.Add(this.ListParsecServiceHost);
            this.Help.SetHelpKeyword(this.panel2, "Slide Panel");
            this.Help.SetHelpString(this.panel2, "Click on the desired label to push the results");
            this.panel2.Location = new System.Drawing.Point(6, 3);
            this.panel2.Name = "panel2";
            this.Help.SetShowHelp(this.panel2, true);
            this.panel2.Size = new System.Drawing.Size(494, 36);
            this.panel2.TabIndex = 1;
            // 
            // LabelServiceHost
            // 
            this.LabelServiceHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelServiceHost.BackColor = System.Drawing.Color.LawnGreen;
            this.LabelServiceHost.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelServiceHost.Font = new System.Drawing.Font("Modern No. 20", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Help.SetHelpKeyword(this.LabelServiceHost, "Slide Panel");
            this.Help.SetHelpString(this.LabelServiceHost, "Click on the desired label to push the results");
            this.LabelServiceHost.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelServiceHost.Location = new System.Drawing.Point(10, 5);
            this.LabelServiceHost.Name = "LabelServiceHost";
            this.Help.SetShowHelp(this.LabelServiceHost, true);
            this.LabelServiceHost.Size = new System.Drawing.Size(475, 23);
            this.LabelServiceHost.TabIndex = 4;
            this.LabelServiceHost.Text = "ParsecServiceHost";
            this.LabelServiceHost.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ListParsecServiceHost
            // 
            this.ListParsecServiceHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListParsecServiceHost.Location = new System.Drawing.Point(10, 35);
            this.ListParsecServiceHost.Name = "ListParsecServiceHost";
            this.ListParsecServiceHost.Size = new System.Drawing.Size(475, 0);
            this.ListParsecServiceHost.TabIndex = 3;
            this.ListParsecServiceHost.UseCompatibleStateImageBehavior = false;
            this.ListParsecServiceHost.View = System.Windows.Forms.View.Details;
            this.ListParsecServiceHost.Invalidated += new System.Windows.Forms.InvalidateEventHandler(this.lvReDrow);
            // 
            // SaveHostState
            // 
            this.SaveHostState.AccessibleRole = System.Windows.Forms.AccessibleRole.ButtonMenu;
            this.SaveHostState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveHostState.Location = new System.Drawing.Point(202, 581);
            this.SaveHostState.Name = "SaveHostState";
            this.SaveHostState.Size = new System.Drawing.Size(67, 23);
            this.SaveHostState.TabIndex = 3;
            this.SaveHostState.Text = "Сохранить";
            this.SaveHostState.UseVisualStyleBackColor = true;
            // 
            // IconValid
            // 
            this.IconValid.Controls.Add(this.ListIconsCompare);
            this.IconValid.Location = new System.Drawing.Point(4, 22);
            this.IconValid.Name = "IconValid";
            this.IconValid.Size = new System.Drawing.Size(512, 607);
            this.IconValid.TabIndex = 2;
            this.IconValid.Text = "Проверка иконок";
            this.IconValid.UseVisualStyleBackColor = true;
            this.IconValid.Enter += new System.EventHandler(this.IconsActivate);
            // 
            // ListIconsCompare
            // 
            this.ListIconsCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListIconsCompare.Location = new System.Drawing.Point(0, 0);
            this.ListIconsCompare.Name = "ListIconsCompare";
            this.ListIconsCompare.Size = new System.Drawing.Size(512, 611);
            this.ListIconsCompare.TabIndex = 0;
            this.ListIconsCompare.UseCompatibleStateImageBehavior = false;
            this.ListIconsCompare.View = System.Windows.Forms.View.Details;
            // 
            // AssemblyValid
            // 
            this.AssemblyValid.Controls.Add(this.ListAssemblyCompare);
            this.AssemblyValid.Location = new System.Drawing.Point(4, 22);
            this.AssemblyValid.Name = "AssemblyValid";
            this.AssemblyValid.Size = new System.Drawing.Size(512, 607);
            this.AssemblyValid.TabIndex = 0;
            this.AssemblyValid.Text = "Проверка сборки";
            this.AssemblyValid.UseVisualStyleBackColor = true;
            this.AssemblyValid.Enter += new System.EventHandler(this.AssemblyActivate);
            // 
            // ListAssemblyCompare
            // 
            this.ListAssemblyCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ListAssemblyCompare.Location = new System.Drawing.Point(0, 0);
            this.ListAssemblyCompare.Name = "ListAssemblyCompare";
            this.ListAssemblyCompare.Size = new System.Drawing.Size(512, 607);
            this.ListAssemblyCompare.TabIndex = 0;
            this.ListAssemblyCompare.UseCompatibleStateImageBehavior = false;
            this.ListAssemblyCompare.View = System.Windows.Forms.View.Details;
            // 
            // HostErrorOutput
            // 
            this.HostErrorOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HostErrorOutput.Location = new System.Drawing.Point(542, 465);
            this.HostErrorOutput.Multiline = true;
            this.HostErrorOutput.Name = "HostErrorOutput";
            this.HostErrorOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HostErrorOutput.Size = new System.Drawing.Size(282, 119);
            this.HostErrorOutput.TabIndex = 0;
            // 
            // CalcTimer
            // 
            this.CalcTimer.Interval = 10;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 686);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.HostErrorOutput);
            this.Controls.Add(this.button1);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "Configer";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Exit);
            this.Load += new System.EventHandler(this.Main_Load);
            this.tabControl2.ResumeLayout(false);
            this.AllValid.ResumeLayout(false);
            this.AllValid.PerformLayout();
            this.HostValid.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.PanelParsec.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.IconValid.ResumeLayout(false);
            this.AssemblyValid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage AllValid;
        private System.Windows.Forms.TextBox FullValidOutput;
        private System.Windows.Forms.TabPage HostValid;
        private System.Windows.Forms.Button SaveHostState;
        private System.Windows.Forms.TabPage IconValid;
        private System.Windows.Forms.TabPage AssemblyValid;
        private System.Windows.Forms.Timer CalcTimer;
        public System.Windows.Forms.TextBox HostErrorOutput;
        public System.Windows.Forms.ListView ListParsecServiceHost;
        public System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Panel MainPanel;
        public System.Windows.Forms.Panel panel5;
        public System.Windows.Forms.ListView ListParsecServer;
        public System.Windows.Forms.Panel panel6;
        public System.Windows.Forms.ListView ListTransport;
        public System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.Label LabelParsec;
        public System.Windows.Forms.Label LabelParsecServer;
        public System.Windows.Forms.Label LabelTransport;
        public System.Windows.Forms.Label LabelServiceHost;
        public System.Windows.Forms.Panel PanelParsec;
        private System.Windows.Forms.HelpProvider Help;
        private System.Windows.Forms.ListView ListIconsCompare;
        private System.Windows.Forms.ListView ListAssemblyCompare;
    }
}

