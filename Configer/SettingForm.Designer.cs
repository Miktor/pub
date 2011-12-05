namespace Configer
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.Optiontabs = new System.Windows.Forms.TabControl();
            this.MainOptionTab = new System.Windows.Forms.TabPage();
            this.AdditionalOptionTab = new System.Windows.Forms.TabPage();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.Save_Options = new System.Windows.Forms.Button();
            this.Cancel_Otions = new System.Windows.Forms.Button();
            this.openFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.Optiontabs.SuspendLayout();
            this.AdditionalOptionTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // Optiontabs
            // 
            this.Optiontabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Optiontabs.Controls.Add(this.MainOptionTab);
            this.Optiontabs.Controls.Add(this.AdditionalOptionTab);
            this.Optiontabs.Location = new System.Drawing.Point(12, 12);
            this.Optiontabs.Name = "Optiontabs";
            this.Optiontabs.SelectedIndex = 0;
            this.Optiontabs.Size = new System.Drawing.Size(287, 148);
            this.Optiontabs.TabIndex = 0;
            // 
            // MainOptionTab
            // 
            this.MainOptionTab.Location = new System.Drawing.Point(4, 22);
            this.MainOptionTab.Name = "MainOptionTab";
            this.MainOptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.MainOptionTab.Size = new System.Drawing.Size(279, 122);
            this.MainOptionTab.TabIndex = 0;
            this.MainOptionTab.Text = "Основные";
            this.MainOptionTab.UseVisualStyleBackColor = true;
            // 
            // AdditionalOptionTab
            // 
            this.AdditionalOptionTab.Controls.Add(this.propertyGrid1);
            this.AdditionalOptionTab.Location = new System.Drawing.Point(4, 22);
            this.AdditionalOptionTab.Name = "AdditionalOptionTab";
            this.AdditionalOptionTab.Padding = new System.Windows.Forms.Padding(3);
            this.AdditionalOptionTab.Size = new System.Drawing.Size(279, 122);
            this.AdditionalOptionTab.TabIndex = 1;
            this.AdditionalOptionTab.Text = "Дополнительные";
            this.AdditionalOptionTab.UseVisualStyleBackColor = true;
            this.AdditionalOptionTab.Enter += new System.EventHandler(this.AdditionalOptionTabActivate);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 3);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(273, 116);
            this.propertyGrid1.TabIndex = 0;
            // 
            // Save_Options
            // 
            this.Save_Options.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Save_Options.Location = new System.Drawing.Point(57, 162);
            this.Save_Options.Name = "Save_Options";
            this.Save_Options.Size = new System.Drawing.Size(75, 23);
            this.Save_Options.TabIndex = 1;
            this.Save_Options.Text = "Сохранить";
            this.Save_Options.UseVisualStyleBackColor = true;
            this.Save_Options.Click += new System.EventHandler(this.Save_Options_Click);
            // 
            // Cancel_Otions
            // 
            this.Cancel_Otions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_Otions.Location = new System.Drawing.Point(157, 162);
            this.Cancel_Otions.Name = "Cancel_Otions";
            this.Cancel_Otions.Size = new System.Drawing.Size(75, 23);
            this.Cancel_Otions.TabIndex = 2;
            this.Cancel_Otions.Text = "Отмена";
            this.Cancel_Otions.UseVisualStyleBackColor = true;
            this.Cancel_Otions.Click += new System.EventHandler(this.Cancel_Otions_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(311, 191);
            this.Controls.Add(this.Cancel_Otions);
            this.Controls.Add(this.Save_Options);
            this.Controls.Add(this.Optiontabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.Text = "Настройки";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.Optiontabs.ResumeLayout(false);
            this.AdditionalOptionTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Optiontabs;
        private System.Windows.Forms.TabPage AdditionalOptionTab;
        private System.Windows.Forms.Button Save_Options;
        private System.Windows.Forms.Button Cancel_Otions;
        private System.Windows.Forms.TabPage MainOptionTab;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.FolderBrowserDialog openFolder;
    }
}