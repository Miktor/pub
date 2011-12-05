namespace Parsec.Configuration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.saveFile = new System.Windows.Forms.SaveFileDialog();
            this.wizard1 = new Gui.Wizard.Wizard();
            this.WorkPage = new Gui.Wizard.WizardPage();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.Section = new System.Windows.Forms.Label();
            this.ProcessTabText = new System.Windows.Forms.TextBox();
            this.ProcessTabLable = new System.Windows.Forms.Label();
            this.ProcessTabHeader = new Gui.Wizard.Header();
            this.ProcessTabBar = new System.Windows.Forms.ProgressBar();
            this.LicenseTab = new Gui.Wizard.WizardPage();
            this.LicenseTabText = new System.Windows.Forms.RichTextBox();
            this.LicenceCheck = new System.Windows.Forms.CheckBox();
            this.LicenseTabHeader = new Gui.Wizard.Header();
            this.WelcomePage = new Gui.Wizard.WizardPage();
            this.infoPage1 = new Gui.Wizard.InfoPage();
            this.FinishPage = new Gui.Wizard.WizardPage();
            this.FinishTab = new Gui.Wizard.InfoPage();
            this.ResultPage = new Gui.Wizard.WizardPage();
            this.EMailLink = new System.Windows.Forms.LinkLabel();
            this.SaveRTLabel = new System.Windows.Forms.Label();
            this.SaveRT = new System.Windows.Forms.RadioButton();
            this.MaiToRB = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SendTabHeader = new Gui.Wizard.Header();
            this.wizard1.SuspendLayout();
            this.WorkPage.SuspendLayout();
            this.LicenseTab.SuspendLayout();
            this.WelcomePage.SuspendLayout();
            this.FinishPage.SuspendLayout();
            this.ResultPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // saveFile
            // 
            this.saveFile.FileName = "ParsecConfig.zip";
            this.saveFile.Filter = "Zip archive|*.zip";
            // 
            // wizard1
            // 
            this.wizard1.Controls.Add(this.FinishPage);
            this.wizard1.Controls.Add(this.ResultPage);
            this.wizard1.Controls.Add(this.WorkPage);
            this.wizard1.Controls.Add(this.LicenseTab);
            this.wizard1.Controls.Add(this.WelcomePage);
            this.wizard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizard1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wizard1.Location = new System.Drawing.Point(0, 0);
            this.wizard1.Name = "wizard1";
            this.wizard1.Pages.AddRange(new Gui.Wizard.WizardPage[] {
            this.WelcomePage,
            this.LicenseTab,
            this.WorkPage,
            this.ResultPage,
            this.FinishPage});
            this.wizard1.Size = new System.Drawing.Size(594, 432);
            this.wizard1.TabIndex = 4;
            // 
            // WorkPage
            // 
            this.WorkPage.Controls.Add(this.comboBox1);
            this.WorkPage.Controls.Add(this.Section);
            this.WorkPage.Controls.Add(this.ProcessTabText);
            this.WorkPage.Controls.Add(this.ProcessTabLable);
            this.WorkPage.Controls.Add(this.ProcessTabHeader);
            this.WorkPage.Controls.Add(this.ProcessTabBar);
            this.WorkPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WorkPage.IsFinishPage = false;
            this.WorkPage.Location = new System.Drawing.Point(0, 0);
            this.WorkPage.Name = "WorkPage";
            this.WorkPage.Size = new System.Drawing.Size(594, 384);
            this.WorkPage.TabIndex = 3;
            this.WorkPage.ShowFromNext += new System.EventHandler(this.WorkPage_ShowFromNext);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(82, 112);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 5;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Section
            // 
            this.Section.AutoSize = true;
            this.Section.Location = new System.Drawing.Point(9, 115);
            this.Section.Name = "Section";
            this.Section.Size = new System.Drawing.Size(67, 13);
            this.Section.TabIndex = 4;
            this.Section.Text = "SectionLable";
            // 
            // ProcessTabText
            // 
            this.ProcessTabText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessTabText.BackColor = System.Drawing.SystemColors.Window;
            this.ProcessTabText.Location = new System.Drawing.Point(12, 139);
            this.ProcessTabText.Multiline = true;
            this.ProcessTabText.Name = "ProcessTabText";
            this.ProcessTabText.ReadOnly = true;
            this.ProcessTabText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ProcessTabText.Size = new System.Drawing.Size(570, 242);
            this.ProcessTabText.TabIndex = 3;
            // 
            // ProcessTabLable
            // 
            this.ProcessTabLable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessTabLable.AutoSize = true;
            this.ProcessTabLable.Location = new System.Drawing.Point(12, 67);
            this.ProcessTabLable.Name = "ProcessTabLable";
            this.ProcessTabLable.Size = new System.Drawing.Size(44, 13);
            this.ProcessTabLable.TabIndex = 2;
            this.ProcessTabLable.Text = "Process";
            // 
            // ProcessTabHeader
            // 
            this.ProcessTabHeader.BackColor = System.Drawing.SystemColors.Control;
            this.ProcessTabHeader.CausesValidation = false;
            this.ProcessTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Description", global::Parsec.Configuration.Properties.Settings.Default, "ProcessTabDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProcessTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Title", global::Parsec.Configuration.Properties.Settings.Default, "ProcessTabTitle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ProcessTabHeader.Description = global::Parsec.Configuration.Properties.Settings.Default.ProcessTabDescription;
            this.ProcessTabHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.ProcessTabHeader.Image = ((System.Drawing.Image)(resources.GetObject("ProcessTabHeader.Image")));
            this.ProcessTabHeader.Location = new System.Drawing.Point(0, 0);
            this.ProcessTabHeader.Name = "ProcessTabHeader";
            this.ProcessTabHeader.Size = new System.Drawing.Size(594, 64);
            this.ProcessTabHeader.TabIndex = 1;
            this.ProcessTabHeader.Title = global::Parsec.Configuration.Properties.Settings.Default.ProcessTabTitle;
            // 
            // ProcessTabBar
            // 
            this.ProcessTabBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessTabBar.Location = new System.Drawing.Point(12, 83);
            this.ProcessTabBar.Name = "ProcessTabBar";
            this.ProcessTabBar.Size = new System.Drawing.Size(570, 23);
            this.ProcessTabBar.TabIndex = 0;
            // 
            // LicenseTab
            // 
            this.LicenseTab.Controls.Add(this.LicenseTabText);
            this.LicenseTab.Controls.Add(this.LicenceCheck);
            this.LicenseTab.Controls.Add(this.LicenseTabHeader);
            this.LicenseTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LicenseTab.IsFinishPage = false;
            this.LicenseTab.Location = new System.Drawing.Point(0, 0);
            this.LicenseTab.Name = "LicenseTab";
            this.LicenseTab.Size = new System.Drawing.Size(594, 384);
            this.LicenseTab.TabIndex = 2;
            this.LicenseTab.ShowFromNext += new System.EventHandler(this.CheckBoxChanged);
            // 
            // LicenseTabText
            // 
            this.LicenseTabText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LicenseTabText.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "DescriptionFullText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LicenseTabText.Location = new System.Drawing.Point(12, 70);
            this.LicenseTabText.Name = "LicenseTabText";
            this.LicenseTabText.Size = new System.Drawing.Size(570, 288);
            this.LicenseTabText.TabIndex = 3;
            this.LicenseTabText.Text = global::Parsec.Configuration.Properties.Settings.Default.DescriptionFullText;
            // 
            // LicenceCheck
            // 
            this.LicenceCheck.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.LicenceCheck.AutoSize = true;
            this.LicenceCheck.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "DescriptionAgreeText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LicenceCheck.Location = new System.Drawing.Point(12, 364);
            this.LicenceCheck.Name = "LicenceCheck";
            this.LicenceCheck.Size = new System.Drawing.Size(61, 17);
            this.LicenceCheck.TabIndex = 2;
            this.LicenceCheck.Text = global::Parsec.Configuration.Properties.Settings.Default.DescriptionAgreeText;
            this.LicenceCheck.UseVisualStyleBackColor = true;
            this.LicenceCheck.CheckedChanged += new System.EventHandler(this.CheckBoxChanged);
            // 
            // LicenseTabHeader
            // 
            this.LicenseTabHeader.BackColor = System.Drawing.SystemColors.Control;
            this.LicenseTabHeader.CausesValidation = false;
            this.LicenseTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Description", global::Parsec.Configuration.Properties.Settings.Default, "DescriptionTab_Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LicenseTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Title", global::Parsec.Configuration.Properties.Settings.Default, "DescriptionTab_Tile", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.LicenseTabHeader.Description = global::Parsec.Configuration.Properties.Settings.Default.DescriptionTab_Description;
            this.LicenseTabHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.LicenseTabHeader.Image = ((System.Drawing.Image)(resources.GetObject("LicenseTabHeader.Image")));
            this.LicenseTabHeader.Location = new System.Drawing.Point(0, 0);
            this.LicenseTabHeader.Name = "LicenseTabHeader";
            this.LicenseTabHeader.Size = new System.Drawing.Size(594, 64);
            this.LicenseTabHeader.TabIndex = 0;
            this.LicenseTabHeader.Title = global::Parsec.Configuration.Properties.Settings.Default.DescriptionTab_Tile;
            // 
            // WelcomePage
            // 
            this.WelcomePage.Controls.Add(this.infoPage1);
            this.WelcomePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WelcomePage.IsFinishPage = false;
            this.WelcomePage.Location = new System.Drawing.Point(0, 0);
            this.WelcomePage.Name = "WelcomePage";
            this.WelcomePage.Size = new System.Drawing.Size(594, 384);
            this.WelcomePage.TabIndex = 1;
            // 
            // infoPage1
            // 
            this.infoPage1.BackColor = System.Drawing.Color.White;
            this.infoPage1.DataBindings.Add(new System.Windows.Forms.Binding("PageTitle", global::Parsec.Configuration.Properties.Settings.Default, "IntroTab_Title", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.infoPage1.DataBindings.Add(new System.Windows.Forms.Binding("PageText", global::Parsec.Configuration.Properties.Settings.Default, "IntroTab_Text", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.infoPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.infoPage1.Image = ((System.Drawing.Image)(resources.GetObject("infoPage1.Image")));
            this.infoPage1.Location = new System.Drawing.Point(0, 0);
            this.infoPage1.Name = "infoPage1";
            this.infoPage1.PageText = global::Parsec.Configuration.Properties.Settings.Default.IntroTab_Text;
            this.infoPage1.PageTitle = global::Parsec.Configuration.Properties.Settings.Default.IntroTab_Title;
            this.infoPage1.Size = new System.Drawing.Size(594, 384);
            this.infoPage1.TabIndex = 0;
            // 
            // FinishPage
            // 
            this.FinishPage.Controls.Add(this.FinishTab);
            this.FinishPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinishPage.IsFinishPage = true;
            this.FinishPage.Location = new System.Drawing.Point(0, 0);
            this.FinishPage.Name = "FinishPage";
            this.FinishPage.Size = new System.Drawing.Size(594, 384);
            this.FinishPage.TabIndex = 5;
            this.FinishPage.ShowFromNext += new System.EventHandler(this.FinishPage_ShowFromNext);
            // 
            // FinishTab
            // 
            this.FinishTab.BackColor = System.Drawing.Color.White;
            this.FinishTab.DataBindings.Add(new System.Windows.Forms.Binding("PageText", global::Parsec.Configuration.Properties.Settings.Default, "FinishTabText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FinishTab.DataBindings.Add(new System.Windows.Forms.Binding("PageTitle", global::Parsec.Configuration.Properties.Settings.Default, "FinishTabTitle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FinishTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FinishTab.Image = ((System.Drawing.Image)(resources.GetObject("FinishTab.Image")));
            this.FinishTab.Location = new System.Drawing.Point(0, 0);
            this.FinishTab.Name = "FinishTab";
            this.FinishTab.PageText = global::Parsec.Configuration.Properties.Settings.Default.FinishTabText;
            this.FinishTab.PageTitle = global::Parsec.Configuration.Properties.Settings.Default.FinishTabTitle;
            this.FinishTab.Size = new System.Drawing.Size(594, 384);
            this.FinishTab.TabIndex = 0;
            // 
            // ResultPage
            // 
            this.ResultPage.Controls.Add(this.EMailLink);
            this.ResultPage.Controls.Add(this.SaveRTLabel);
            this.ResultPage.Controls.Add(this.SaveRT);
            this.ResultPage.Controls.Add(this.MaiToRB);
            this.ResultPage.Controls.Add(this.label1);
            this.ResultPage.Controls.Add(this.SendTabHeader);
            this.ResultPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultPage.IsFinishPage = false;
            this.ResultPage.Location = new System.Drawing.Point(0, 0);
            this.ResultPage.Name = "ResultPage";
            this.ResultPage.Size = new System.Drawing.Size(594, 384);
            this.ResultPage.TabIndex = 4;
            // 
            // EMailLink
            // 
            this.EMailLink.AutoSize = true;
            this.EMailLink.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "mailtoLink", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.EMailLink.Location = new System.Drawing.Point(62, 236);
            this.EMailLink.Name = "EMailLink";
            this.EMailLink.Size = new System.Drawing.Size(86, 18);
            this.EMailLink.TabIndex = 8;
            this.EMailLink.TabStop = true;
            this.EMailLink.Text = global::Parsec.Configuration.Properties.Settings.Default.mailtoLink;
            this.EMailLink.UseCompatibleTextRendering = true;
            this.EMailLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EMailLink_LinkClicked);
            // 
            // SaveRTLabel
            // 
            this.SaveRTLabel.AutoSize = true;
            this.SaveRTLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "Use", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SaveRTLabel.Location = new System.Drawing.Point(62, 212);
            this.SaveRTLabel.Name = "SaveRTLabel";
            this.SaveRTLabel.Size = new System.Drawing.Size(453, 18);
            this.SaveRTLabel.TabIndex = 6;
            this.SaveRTLabel.Text = global::Parsec.Configuration.Properties.Settings.Default.Use;
            this.SaveRTLabel.UseCompatibleTextRendering = true;
            // 
            // SaveRT
            // 
            this.SaveRT.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "SaveRarRT", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SaveRT.Location = new System.Drawing.Point(15, 176);
            this.SaveRT.Name = "SaveRT";
            this.SaveRT.Size = new System.Drawing.Size(579, 33);
            this.SaveRT.TabIndex = 5;
            this.SaveRT.Text = global::Parsec.Configuration.Properties.Settings.Default.SaveRarRT;
            this.SaveRT.UseVisualStyleBackColor = true;
            this.SaveRT.CheckedChanged += new System.EventHandler(this.SaveRT_CheckedChanged);
            // 
            // MaiToRB
            // 
            this.MaiToRB.AutoSize = true;
            this.MaiToRB.Checked = true;
            this.MaiToRB.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "MilSendRBText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MaiToRB.Location = new System.Drawing.Point(15, 136);
            this.MaiToRB.Name = "MaiToRB";
            this.MaiToRB.Size = new System.Drawing.Size(168, 17);
            this.MaiToRB.TabIndex = 4;
            this.MaiToRB.TabStop = true;
            this.MaiToRB.Text = global::Parsec.Configuration.Properties.Settings.Default.MilSendRBText;
            this.MaiToRB.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "MailTabText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.label1.Location = new System.Drawing.Point(12, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(570, 22);
            this.label1.TabIndex = 3;
            this.label1.Text = global::Parsec.Configuration.Properties.Settings.Default.MailTabText;
            // 
            // SendTabHeader
            // 
            this.SendTabHeader.BackColor = System.Drawing.SystemColors.Control;
            this.SendTabHeader.CausesValidation = false;
            this.SendTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Title", global::Parsec.Configuration.Properties.Settings.Default, "SendTabTitle", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SendTabHeader.DataBindings.Add(new System.Windows.Forms.Binding("Description", global::Parsec.Configuration.Properties.Settings.Default, "SendTabDescription", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.SendTabHeader.Description = global::Parsec.Configuration.Properties.Settings.Default.SendTabDescription;
            this.SendTabHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.SendTabHeader.Image = ((System.Drawing.Image)(resources.GetObject("SendTabHeader.Image")));
            this.SendTabHeader.Location = new System.Drawing.Point(0, 0);
            this.SendTabHeader.Name = "SendTabHeader";
            this.SendTabHeader.Size = new System.Drawing.Size(594, 64);
            this.SendTabHeader.TabIndex = 1;
            this.SendTabHeader.Title = global::Parsec.Configuration.Properties.Settings.Default.SendTabTitle;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 432);
            this.Controls.Add(this.wizard1);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::Parsec.Configuration.Properties.Settings.Default, "WindowHeader", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("MaximumSize", global::Parsec.Configuration.Properties.Settings.Default, "FixedSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new System.Windows.Forms.Binding("MinimumSize", global::Parsec.Configuration.Properties.Settings.Default, "FixedSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = global::Parsec.Configuration.Properties.Settings.Default.FixedSize;
            this.MinimizeBox = false;
            this.MinimumSize = global::Parsec.Configuration.Properties.Settings.Default.FixedSize;
            this.Name = "MainForm";
            this.Text = global::Parsec.Configuration.Properties.Settings.Default.WindowHeader;
            this.wizard1.ResumeLayout(false);
            this.WorkPage.ResumeLayout(false);
            this.WorkPage.PerformLayout();
            this.LicenseTab.ResumeLayout(false);
            this.LicenseTab.PerformLayout();
            this.WelcomePage.ResumeLayout(false);
            this.FinishPage.ResumeLayout(false);
            this.ResultPage.ResumeLayout(false);
            this.ResultPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Gui.Wizard.Wizard wizard1;
        private Gui.Wizard.WizardPage FinishPage;
        private Gui.Wizard.WizardPage ResultPage;
        private Gui.Wizard.WizardPage WorkPage;
        private Gui.Wizard.WizardPage LicenseTab;
        private Gui.Wizard.WizardPage WelcomePage;
        private Gui.Wizard.InfoPage infoPage1;
        private System.Windows.Forms.RichTextBox LicenseTabText;
        private System.Windows.Forms.CheckBox LicenceCheck;
        private Gui.Wizard.Header LicenseTabHeader;
        private Gui.Wizard.InfoPage FinishTab;
        private System.Windows.Forms.Label ProcessTabLable;
        private Gui.Wizard.Header ProcessTabHeader;
        private System.Windows.Forms.ProgressBar ProcessTabBar;
        private System.Windows.Forms.TextBox ProcessTabText;
        private Gui.Wizard.Header SendTabHeader;
        private System.Windows.Forms.SaveFileDialog saveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton SaveRT;
        private System.Windows.Forms.RadioButton MaiToRB;
        private System.Windows.Forms.Label SaveRTLabel;
        private System.Windows.Forms.LinkLabel EMailLink;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label Section;

    }
}

