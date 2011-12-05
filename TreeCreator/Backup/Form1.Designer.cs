namespace TreeCreator
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
            this.RunButton = new System.Windows.Forms.Button();
            this.treeView = new System.Windows.Forms.TreeView();
            this.OpenFileBD = new System.Windows.Forms.OpenFileDialog();
            this.PathButton = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.GuIDColumnName = new System.Windows.Forms.ComboBox();
            this.DictColumnName = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FindTables = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RunButton.Location = new System.Drawing.Point(2, 592);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(291, 23);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Сделать дерево";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // treeView
            // 
            this.treeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.treeView.Location = new System.Drawing.Point(5, 8);
            this.treeView.Name = "treeView";
            this.treeView.Size = new System.Drawing.Size(291, 468);
            this.treeView.TabIndex = 2;
            // 
            // OpenFileBD
            // 
            this.OpenFileBD.FileName = "OpenFileBD";
            // 
            // PathButton
            // 
            this.PathButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.PathButton.Location = new System.Drawing.Point(271, 480);
            this.PathButton.Name = "PathButton";
            this.PathButton.Size = new System.Drawing.Size(25, 23);
            this.PathButton.TabIndex = 8;
            this.PathButton.Text = "...";
            this.PathButton.UseVisualStyleBackColor = true;
            this.PathButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // FilePath
            // 
            this.FilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FilePath.Location = new System.Drawing.Point(109, 482);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(154, 20);
            this.FilePath.TabIndex = 9;
            // 
            // GuIDColumnName
            // 
            this.GuIDColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GuIDColumnName.FormattingEnabled = true;
            this.GuIDColumnName.Location = new System.Drawing.Point(109, 538);
            this.GuIDColumnName.Name = "GuIDColumnName";
            this.GuIDColumnName.Size = new System.Drawing.Size(184, 21);
            this.GuIDColumnName.TabIndex = 15;
            // 
            // DictColumnName
            // 
            this.DictColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DictColumnName.FormattingEnabled = true;
            this.DictColumnName.Location = new System.Drawing.Point(109, 565);
            this.DictColumnName.Name = "DictColumnName";
            this.DictColumnName.Size = new System.Drawing.Size(184, 21);
            this.DictColumnName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 485);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Путь к файлу";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 541);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Таблица GuID";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 568);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Таблица словаря";
            // 
            // FindTables
            // 
            this.FindTables.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FindTables.Location = new System.Drawing.Point(5, 508);
            this.FindTables.Name = "FindTables";
            this.FindTables.Size = new System.Drawing.Size(288, 23);
            this.FindTables.TabIndex = 20;
            this.FindTables.Text = "Найти таблицы в бд";
            this.FindTables.UseVisualStyleBackColor = true;
            this.FindTables.Click += new System.EventHandler(this.FindTables_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 625);
            this.Controls.Add(this.FindTables);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DictColumnName);
            this.Controls.Add(this.GuIDColumnName);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.PathButton);
            this.Controls.Add(this.treeView);
            this.Controls.Add(this.RunButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "TreeCreator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.OpenFileDialog OpenFileBD;
        private System.Windows.Forms.Button PathButton;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.ComboBox GuIDColumnName;
        private System.Windows.Forms.ComboBox DictColumnName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button FindTables;
    }
}

