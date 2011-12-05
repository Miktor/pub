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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.form1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.form1BindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.AllValid = new System.Windows.Forms.TabPage();
            this.FullValidOutput = new System.Windows.Forms.TextBox();
            this.HostValid = new System.Windows.Forms.TabPage();
            this.SaveHostState = new System.Windows.Forms.Button();
            this.HostOutput = new System.Windows.Forms.DataGridView();
            this.FileRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PropertyRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueRow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HostErrorOutput = new System.Windows.Forms.TextBox();
            this.IconValid = new System.Windows.Forms.TabPage();
            this.IconsValid = new System.Windows.Forms.TextBox();
            this.AssemblyValid = new System.Windows.Forms.TabPage();
            this.AssemblyValidOutput = new System.Windows.Forms.TextBox();
            this.ReplicOpt = new System.Windows.Forms.TabPage();
            this.SaveReplic = new System.Windows.Forms.Button();
            this.NoLogic = new System.Windows.Forms.RadioButton();
            this.Both = new System.Windows.Forms.RadioButton();
            this.ReplicaClient = new System.Windows.Forms.GroupBox();
            this.CcacheBox = new System.Windows.Forms.CheckBox();
            this.CtransactionBox = new System.Windows.Forms.CheckBox();
            this.Creplica_halBox = new System.Windows.Forms.CheckBox();
            this.Creplica_uiBox = new System.Windows.Forms.CheckBox();
            this.ReplicaHost = new System.Windows.Forms.GroupBox();
            this.StransactionBox = new System.Windows.Forms.CheckBox();
            this.SreplicationBox = new System.Windows.Forms.CheckBox();
            this.ScacheBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).BeginInit();
            this.tabControl2.SuspendLayout();
            this.AllValid.SuspendLayout();
            this.HostValid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HostOutput)).BeginInit();
            this.IconValid.SuspendLayout();
            this.AssemblyValid.SuspendLayout();
            this.ReplicOpt.SuspendLayout();
            this.ReplicaClient.SuspendLayout();
            this.ReplicaHost.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(342, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Настройки";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OpenSettings);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(76, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "Запустить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // form1BindingSource
            // 
            this.form1BindingSource.DataSource = typeof(Configer.Main);
            // 
            // form1BindingSource1
            // 
            this.form1BindingSource1.DataSource = typeof(Configer.Main);
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
            this.tabControl2.Controls.Add(this.ReplicOpt);
            this.tabControl2.Location = new System.Drawing.Point(12, 41);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(528, 632);
            this.tabControl2.TabIndex = 36;
            // 
            // AllValid
            // 
            this.AllValid.Controls.Add(this.FullValidOutput);
            this.AllValid.Location = new System.Drawing.Point(4, 22);
            this.AllValid.Name = "AllValid";
            this.AllValid.Padding = new System.Windows.Forms.Padding(3);
            this.AllValid.Size = new System.Drawing.Size(520, 606);
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
            this.HostValid.Controls.Add(this.SaveHostState);
            this.HostValid.Controls.Add(this.HostOutput);
            this.HostValid.Controls.Add(this.HostErrorOutput);
            this.HostValid.Location = new System.Drawing.Point(4, 22);
            this.HostValid.Name = "HostValid";
            this.HostValid.Size = new System.Drawing.Size(520, 606);
            this.HostValid.TabIndex = 1;
            this.HostValid.Text = "Проверка hosts";
            this.HostValid.UseVisualStyleBackColor = true;
            this.HostValid.Enter += new System.EventHandler(this.HostActivate);
            // 
            // SaveHostState
            // 
            this.SaveHostState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveHostState.Location = new System.Drawing.Point(202, 580);
            this.SaveHostState.Name = "SaveHostState";
            this.SaveHostState.Size = new System.Drawing.Size(75, 23);
            this.SaveHostState.TabIndex = 2;
            this.SaveHostState.Text = "Сохранить";
            this.SaveHostState.UseVisualStyleBackColor = true;
            // 
            // HostOutput
            // 
            this.HostOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HostOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HostOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileRow,
            this.PropertyRow,
            this.ValueRow});
            this.HostOutput.Location = new System.Drawing.Point(0, 0);
            this.HostOutput.Name = "HostOutput";
            this.HostOutput.Size = new System.Drawing.Size(517, 449);
            this.HostOutput.TabIndex = 1;
            // 
            // FileRow
            // 
            this.FileRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FileRow.HeaderText = "Файл";
            this.FileRow.Name = "FileRow";
            this.FileRow.ReadOnly = true;
            // 
            // PropertyRow
            // 
            this.PropertyRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.PropertyRow.HeaderText = "Свойство";
            this.PropertyRow.Name = "PropertyRow";
            this.PropertyRow.ReadOnly = true;
            // 
            // ValueRow
            // 
            this.ValueRow.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ValueRow.HeaderText = "Значение";
            this.ValueRow.Name = "ValueRow";
            // 
            // HostErrorOutput
            // 
            this.HostErrorOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HostErrorOutput.Location = new System.Drawing.Point(0, 455);
            this.HostErrorOutput.Multiline = true;
            this.HostErrorOutput.Name = "HostErrorOutput";
            this.HostErrorOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HostErrorOutput.Size = new System.Drawing.Size(520, 119);
            this.HostErrorOutput.TabIndex = 0;
            // 
            // IconValid
            // 
            this.IconValid.Controls.Add(this.IconsValid);
            this.IconValid.Location = new System.Drawing.Point(4, 22);
            this.IconValid.Name = "IconValid";
            this.IconValid.Size = new System.Drawing.Size(520, 606);
            this.IconValid.TabIndex = 2;
            this.IconValid.Text = "Проверка иконок";
            this.IconValid.UseVisualStyleBackColor = true;
            this.IconValid.Enter += new System.EventHandler(this.IconsActivate);
            // 
            // IconsValid
            // 
            this.IconsValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.IconsValid.Location = new System.Drawing.Point(3, 0);
            this.IconsValid.Multiline = true;
            this.IconsValid.Name = "IconsValid";
            this.IconsValid.ReadOnly = true;
            this.IconsValid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.IconsValid.Size = new System.Drawing.Size(489, 513);
            this.IconsValid.TabIndex = 0;
            // 
            // AssemblyValid
            // 
            this.AssemblyValid.Controls.Add(this.AssemblyValidOutput);
            this.AssemblyValid.Location = new System.Drawing.Point(4, 22);
            this.AssemblyValid.Name = "AssemblyValid";
            this.AssemblyValid.Size = new System.Drawing.Size(520, 606);
            this.AssemblyValid.TabIndex = 0;
            this.AssemblyValid.Text = "Проверка сборки";
            this.AssemblyValid.UseVisualStyleBackColor = true;
            this.AssemblyValid.Enter += new System.EventHandler(AssemblyActivate);
            // 
            // AssemblyValidOutput
            // 
            this.AssemblyValidOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AssemblyValidOutput.Location = new System.Drawing.Point(-4, 0);
            this.AssemblyValidOutput.Multiline = true;
            this.AssemblyValidOutput.Name = "AssemblyValidOutput";
            this.AssemblyValidOutput.ReadOnly = true;
            this.AssemblyValidOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.AssemblyValidOutput.Size = new System.Drawing.Size(524, 606);
            this.AssemblyValidOutput.TabIndex = 0;
            // 
            // ReplicOpt
            // 
            this.ReplicOpt.Controls.Add(this.SaveReplic);
            this.ReplicOpt.Controls.Add(this.NoLogic);
            this.ReplicOpt.Controls.Add(this.Both);
            this.ReplicOpt.Controls.Add(this.ReplicaClient);
            this.ReplicOpt.Controls.Add(this.ReplicaHost);
            this.ReplicOpt.Location = new System.Drawing.Point(4, 22);
            this.ReplicOpt.Name = "ReplicOpt";
            this.ReplicOpt.Size = new System.Drawing.Size(520, 606);
            this.ReplicOpt.TabIndex = 3;
            this.ReplicOpt.Text = "Настройка Replication";
            this.ReplicOpt.UseVisualStyleBackColor = true;
            this.ReplicOpt.Enter += new System.EventHandler(ReplicActiv);
            // 
            // SaveReplic
            // 
            this.SaveReplic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveReplic.Location = new System.Drawing.Point(274, 316);
            this.SaveReplic.Name = "SaveReplic";
            this.SaveReplic.Size = new System.Drawing.Size(200, 23);
            this.SaveReplic.TabIndex = 6;
            this.SaveReplic.Text = "Сохранить";
            this.SaveReplic.UseVisualStyleBackColor = true;
            // 
            // NoLogic
            // 
            this.NoLogic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NoLogic.AutoSize = true;
            this.NoLogic.Location = new System.Drawing.Point(19, 339);
            this.NoLogic.Name = "NoLogic";
            this.NoLogic.Size = new System.Drawing.Size(151, 17);
            this.NoLogic.TabIndex = 5;
            this.NoLogic.Text = "Связываение отключено";
            this.NoLogic.UseVisualStyleBackColor = true;
            // 
            // Both
            // 
            this.Both.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Both.AutoSize = true;
            this.Both.Checked = true;
            this.Both.Location = new System.Drawing.Point(19, 316);
            this.Both.Name = "Both";
            this.Both.Size = new System.Drawing.Size(156, 17);
            this.Both.TabIndex = 2;
            this.Both.TabStop = true;
            this.Both.Text = "Host и Client равноправны";
            this.Both.UseVisualStyleBackColor = true;
            // 
            // ReplicaClient
            // 
            this.ReplicaClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReplicaClient.Controls.Add(this.CcacheBox);
            this.ReplicaClient.Controls.Add(this.CtransactionBox);
            this.ReplicaClient.Controls.Add(this.Creplica_halBox);
            this.ReplicaClient.Controls.Add(this.Creplica_uiBox);
            this.ReplicaClient.Location = new System.Drawing.Point(274, 167);
            this.ReplicaClient.Name = "ReplicaClient";
            this.ReplicaClient.Size = new System.Drawing.Size(200, 127);
            this.ReplicaClient.TabIndex = 1;
            this.ReplicaClient.TabStop = false;
            this.ReplicaClient.Text = "Client";
            // 
            // CcacheBox
            // 
            this.CcacheBox.AutoSize = true;
            this.CcacheBox.Location = new System.Drawing.Point(21, 99);
            this.CcacheBox.Name = "CcacheBox";
            this.CcacheBox.Size = new System.Drawing.Size(46, 17);
            this.CcacheBox.TabIndex = 3;
            this.CcacheBox.Text = "task";
            this.CcacheBox.UseVisualStyleBackColor = true;
            this.CcacheBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // CtransactionBox
            // 
            this.CtransactionBox.AutoSize = true;
            this.CtransactionBox.Location = new System.Drawing.Point(21, 76);
            this.CtransactionBox.Name = "CtransactionBox";
            this.CtransactionBox.Size = new System.Drawing.Size(78, 17);
            this.CtransactionBox.TabIndex = 2;
            this.CtransactionBox.Text = "transaction";
            this.CtransactionBox.UseVisualStyleBackColor = true;
            this.CtransactionBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // Creplica_halBox
            // 
            this.Creplica_halBox.AutoSize = true;
            this.Creplica_halBox.Location = new System.Drawing.Point(21, 53);
            this.Creplica_halBox.Name = "Creplica_halBox";
            this.Creplica_halBox.Size = new System.Drawing.Size(77, 17);
            this.Creplica_halBox.TabIndex = 1;
            this.Creplica_halBox.Text = "replica_hal";
            this.Creplica_halBox.UseVisualStyleBackColor = true;
            this.Creplica_halBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // Creplica_uiBox
            // 
            this.Creplica_uiBox.AutoSize = true;
            this.Creplica_uiBox.Location = new System.Drawing.Point(21, 30);
            this.Creplica_uiBox.Name = "Creplica_uiBox";
            this.Creplica_uiBox.Size = new System.Drawing.Size(71, 17);
            this.Creplica_uiBox.TabIndex = 0;
            this.Creplica_uiBox.Text = "replica_ui";
            this.Creplica_uiBox.UseVisualStyleBackColor = true;
            this.Creplica_uiBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // ReplicaHost
            // 
            this.ReplicaHost.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ReplicaHost.Controls.Add(this.StransactionBox);
            this.ReplicaHost.Controls.Add(this.SreplicationBox);
            this.ReplicaHost.Controls.Add(this.ScacheBox);
            this.ReplicaHost.Location = new System.Drawing.Point(19, 167);
            this.ReplicaHost.Name = "ReplicaHost";
            this.ReplicaHost.Size = new System.Drawing.Size(225, 127);
            this.ReplicaHost.TabIndex = 0;
            this.ReplicaHost.TabStop = false;
            this.ReplicaHost.Text = "Host";
            // 
            // StransactionBox
            // 
            this.StransactionBox.AutoSize = true;
            this.StransactionBox.Location = new System.Drawing.Point(9, 76);
            this.StransactionBox.Name = "StransactionBox";
            this.StransactionBox.Size = new System.Drawing.Size(78, 17);
            this.StransactionBox.TabIndex = 2;
            this.StransactionBox.Text = "transaction";
            this.StransactionBox.UseVisualStyleBackColor = true;
            this.StransactionBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // SreplicationBox
            // 
            this.SreplicationBox.AutoSize = true;
            this.SreplicationBox.Location = new System.Drawing.Point(9, 30);
            this.SreplicationBox.Name = "SreplicationBox";
            this.SreplicationBox.Size = new System.Drawing.Size(74, 17);
            this.SreplicationBox.TabIndex = 1;
            this.SreplicationBox.Text = "replication";
            this.SreplicationBox.UseVisualStyleBackColor = true;
            this.SreplicationBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // ScacheBox
            // 
            this.ScacheBox.AutoSize = true;
            this.ScacheBox.Location = new System.Drawing.Point(9, 99);
            this.ScacheBox.Name = "ScacheBox";
            this.ScacheBox.Size = new System.Drawing.Size(56, 17);
            this.ScacheBox.TabIndex = 0;
            this.ScacheBox.Text = "cache";
            this.ScacheBox.UseVisualStyleBackColor = true;
            this.ScacheBox.CheckedChanged += new System.EventHandler(ChangeReplicState);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 685);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MinimumSize = new System.Drawing.Size(560, 715);
            this.Name = "Main";
            this.Text = "Form1";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.Main_Exit);
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.form1BindingSource1)).EndInit();
            this.tabControl2.ResumeLayout(false);
            this.AllValid.ResumeLayout(false);
            this.AllValid.PerformLayout();
            this.HostValid.ResumeLayout(false);
            this.HostValid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HostOutput)).EndInit();
            this.IconValid.ResumeLayout(false);
            this.IconValid.PerformLayout();
            this.AssemblyValid.ResumeLayout(false);
            this.AssemblyValid.PerformLayout();
            this.ReplicOpt.ResumeLayout(false);
            this.ReplicOpt.PerformLayout();
            this.ReplicaClient.ResumeLayout(false);
            this.ReplicaClient.PerformLayout();
            this.ReplicaHost.ResumeLayout(false);
            this.ReplicaHost.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Windows.Forms.BindingSource form1BindingSource;
        private System.Windows.Forms.BindingSource form1BindingSource1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage AllValid;
        private System.Windows.Forms.TextBox FullValidOutput;
        private System.Windows.Forms.TabPage HostValid;
        private System.Windows.Forms.Button SaveHostState;
        private System.Windows.Forms.DataGridView HostOutput;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn PropertyRow;
        private System.Windows.Forms.DataGridViewTextBoxColumn ValueRow;
        private System.Windows.Forms.TextBox HostErrorOutput;
        private System.Windows.Forms.TabPage IconValid;
        private System.Windows.Forms.TextBox IconsValid;
        private System.Windows.Forms.TabPage AssemblyValid;
        private System.Windows.Forms.TextBox AssemblyValidOutput;
        private System.Windows.Forms.TabPage ReplicOpt;
        private System.Windows.Forms.Button SaveReplic;
        private System.Windows.Forms.RadioButton NoLogic;
        private System.Windows.Forms.RadioButton Both;
        private System.Windows.Forms.GroupBox ReplicaClient;
        private System.Windows.Forms.CheckBox CcacheBox;
        private System.Windows.Forms.CheckBox CtransactionBox;
        private System.Windows.Forms.CheckBox Creplica_halBox;
        private System.Windows.Forms.CheckBox Creplica_uiBox;
        private System.Windows.Forms.GroupBox ReplicaHost;
        private System.Windows.Forms.CheckBox StransactionBox;
        private System.Windows.Forms.CheckBox SreplicationBox;
        private System.Windows.Forms.CheckBox ScacheBox;
    }
}

