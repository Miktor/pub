namespace Compare
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
            this.fstFilePathBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fstFilePath = new System.Windows.Forms.TextBox();
            this.sndFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sndFilePathBtn = new System.Windows.Forms.Button();
            this.CompareBtn = new System.Windows.Forms.Button();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.dataSet1 = new System.Data.DataSet();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.TableComp = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ValComp = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.AllResults = new System.Windows.Forms.TabPage();
            this.AllResultsout = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.TableComp.SuspendLayout();
            this.ValComp.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.AllResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // fstFilePathBtn
            // 
            this.fstFilePathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fstFilePathBtn.Location = new System.Drawing.Point(741, 12);
            this.fstFilePathBtn.Name = "fstFilePathBtn";
            this.fstFilePathBtn.Size = new System.Drawing.Size(26, 23);
            this.fstFilePathBtn.TabIndex = 0;
            this.fstFilePathBtn.Text = "...";
            this.fstFilePathBtn.UseVisualStyleBackColor = true;
            this.fstFilePathBtn.Click += new System.EventHandler(this.PathSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Первая БД";
            // 
            // fstFilePath
            // 
            this.fstFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fstFilePath.Location = new System.Drawing.Point(82, 14);
            this.fstFilePath.Name = "fstFilePath";
            this.fstFilePath.Size = new System.Drawing.Size(653, 20);
            this.fstFilePath.TabIndex = 2;
            // 
            // sndFilePath
            // 
            this.sndFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.sndFilePath.Location = new System.Drawing.Point(82, 44);
            this.sndFilePath.Name = "sndFilePath";
            this.sndFilePath.Size = new System.Drawing.Size(653, 20);
            this.sndFilePath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Первая БД";
            // 
            // sndFilePathBtn
            // 
            this.sndFilePathBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sndFilePathBtn.Location = new System.Drawing.Point(741, 42);
            this.sndFilePathBtn.Name = "sndFilePathBtn";
            this.sndFilePathBtn.Size = new System.Drawing.Size(26, 23);
            this.sndFilePathBtn.TabIndex = 3;
            this.sndFilePathBtn.Text = "...";
            this.sndFilePathBtn.UseVisualStyleBackColor = true;
            this.sndFilePathBtn.Click += new System.EventHandler(this.PathSelect);
            // 
            // CompareBtn
            // 
            this.CompareBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CompareBtn.Location = new System.Drawing.Point(142, 70);
            this.CompareBtn.Name = "CompareBtn";
            this.CompareBtn.Size = new System.Drawing.Size(487, 23);
            this.CompareBtn.TabIndex = 6;
            this.CompareBtn.Text = "Сравнить";
            this.CompareBtn.UseVisualStyleBackColor = true;
            this.CompareBtn.Click += new System.EventHandler(this.CompareBtn_Click);
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(7, 377);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(824, 90);
            this.textBox1.TabIndex = 8;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.TableComp);
            this.tabControl1.Controls.Add(this.ValComp);
            this.tabControl1.Controls.Add(this.AllResults);
            this.tabControl1.Location = new System.Drawing.Point(4, 99);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(774, 426);
            this.tabControl1.TabIndex = 9;
            // 
            // TableComp
            // 
            this.TableComp.Controls.Add(this.listView1);
            this.TableComp.Controls.Add(this.textBox1);
            this.TableComp.Location = new System.Drawing.Point(4, 22);
            this.TableComp.Name = "TableComp";
            this.TableComp.Padding = new System.Windows.Forms.Padding(3);
            this.TableComp.Size = new System.Drawing.Size(766, 400);
            this.TableComp.TabIndex = 0;
            this.TableComp.Text = "Проверка таблиц";
            this.TableComp.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.AllowColumnReorder = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HoverSelection = true;
            this.listView1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(8, 3);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(818, 368);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 9;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DoubleClick += new System.EventHandler(this.tableSelect);
            // 
            // ValComp
            // 
            this.ValComp.Controls.Add(this.splitContainer1);
            this.ValComp.Location = new System.Drawing.Point(4, 22);
            this.ValComp.Name = "ValComp";
            this.ValComp.Size = new System.Drawing.Size(766, 400);
            this.ValComp.TabIndex = 2;
            this.ValComp.Text = "Проверка значений";
            this.ValComp.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.AllowDrop = true;
            this.splitContainer1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel1.Controls.Add(this.listView2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.listView3);
            this.splitContainer1.Size = new System.Drawing.Size(766, 400);
            this.splitContainer1.SplitterDistance = 272;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 12;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(7, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(402, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.loadTable);
            // 
            // listView2
            // 
            this.listView2.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView2.AllowColumnReorder = true;
            this.listView2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView2.FullRowSelect = true;
            this.listView2.GridLines = true;
            this.listView2.HoverSelection = true;
            this.listView2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.listView2.LabelEdit = true;
            this.listView2.Location = new System.Drawing.Point(3, 53);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(760, 216);
            this.listView2.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView2.TabIndex = 10;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // listView3
            // 
            this.listView3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView3.Location = new System.Drawing.Point(3, 3);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(763, 120);
            this.listView3.TabIndex = 11;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.Details;
            // 
            // AllResults
            // 
            this.AllResults.Controls.Add(this.AllResultsout);
            this.AllResults.Location = new System.Drawing.Point(4, 22);
            this.AllResults.Name = "AllResults";
            this.AllResults.Size = new System.Drawing.Size(766, 400);
            this.AllResults.TabIndex = 3;
            this.AllResults.Text = "Результаты проверки";
            this.AllResults.UseVisualStyleBackColor = true;
            // 
            // AllResultsout
            // 
            this.AllResultsout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.AllResultsout.Location = new System.Drawing.Point(0, 0);
            this.AllResultsout.Multiline = true;
            this.AllResultsout.Name = "AllResultsout";
            this.AllResultsout.Size = new System.Drawing.Size(766, 404);
            this.AllResultsout.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 525);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.CompareBtn);
            this.Controls.Add(this.sndFilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sndFilePathBtn);
            this.Controls.Add(this.fstFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.fstFilePathBtn);
            this.Name = "Form1";
            this.Text = "Compare";
            this.SizeChanged += new System.EventHandler(this.formResize);
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.TableComp.ResumeLayout(false);
            this.TableComp.PerformLayout();
            this.ValComp.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.AllResults.ResumeLayout(false);
            this.AllResults.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fstFilePathBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fstFilePath;
        private System.Windows.Forms.TextBox sndFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sndFilePathBtn;
        private System.Windows.Forms.Button CompareBtn;
        private System.Windows.Forms.OpenFileDialog openFile;
        private System.Data.DataSet dataSet1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage TableComp;
        private System.Windows.Forms.TabPage ValComp;
        private System.Windows.Forms.TabPage AllResults;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox AllResultsout;
    }
}

