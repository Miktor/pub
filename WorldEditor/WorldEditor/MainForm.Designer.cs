
namespace WorldEditor
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolsStrip = new System.Windows.Forms.ToolStrip();
            this.SelectToolButton = new System.Windows.Forms.ToolStripButton();
            this.MoveToolButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.RoadToolButton = new System.Windows.Forms.ToolStripButton();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.ToolsStrip.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = global::WorldEditor.Properties.Settings.Default.ControlColour;
            this.statusStrip1.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::WorldEditor.Properties.Settings.Default, "ControlColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 736);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1196, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(0, 17);
            // 
            // ToolsStrip
            // 
            this.ToolsStrip.BackColor = global::WorldEditor.Properties.Settings.Default.ControlColour;
            this.ToolsStrip.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::WorldEditor.Properties.Settings.Default, "ControlColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.ToolsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SelectToolButton,
            this.MoveToolButton,
            this.toolStripSeparator1,
            this.RoadToolButton});
            this.ToolsStrip.Location = new System.Drawing.Point(0, 24);
            this.ToolsStrip.Name = "ToolsStrip";
            this.ToolsStrip.Size = new System.Drawing.Size(1196, 25);
            this.ToolsStrip.TabIndex = 1;
            this.ToolsStrip.Text = "toolStrip1";
            // 
            // SelectToolButton
            // 
            this.SelectToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SelectToolButton.Image = ((System.Drawing.Image)(resources.GetObject("SelectToolButton.Image")));
            this.SelectToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SelectToolButton.Name = "SelectToolButton";
            this.SelectToolButton.Size = new System.Drawing.Size(23, 22);
            this.SelectToolButton.Text = "toolStripButton1";
            // 
            // MoveToolButton
            // 
            this.MoveToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MoveToolButton.Image = ((System.Drawing.Image)(resources.GetObject("MoveToolButton.Image")));
            this.MoveToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MoveToolButton.Name = "MoveToolButton";
            this.MoveToolButton.Size = new System.Drawing.Size(23, 22);
            this.MoveToolButton.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // RoadToolButton
            // 
            this.RoadToolButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RoadToolButton.Image = ((System.Drawing.Image)(resources.GetObject("RoadToolButton.Image")));
            this.RoadToolButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RoadToolButton.Name = "RoadToolButton";
            this.RoadToolButton.Size = new System.Drawing.Size(23, 22);
            this.RoadToolButton.Text = "toolStripButton1";
            // 
            // MainMenu
            // 
            this.MainMenu.BackColor = global::WorldEditor.Properties.Settings.Default.ControlColour;
            this.MainMenu.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::WorldEditor.Properties.Settings.Default, "ControlColour", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1196, 24);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = global::WorldEditor.Properties.Settings.Default.MainMenuFile;
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // MainPanel
            // 
            this.MainPanel.Location = new System.Drawing.Point(0, 52);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(1196, 681);
            this.MainPanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = global::WorldEditor.Properties.Settings.Default.MainWindowSize;
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.ToolsStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.DataBindings.Add(new System.Windows.Forms.Binding("ClientSize", global::WorldEditor.Properties.Settings.Default, "MainWindowSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Location = global::WorldEditor.Properties.Settings.Default.MainWindowLocation;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MyWorldEditor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Disposed += new System.EventHandler(this.MogreForm_Disposed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ToolsStrip.ResumeLayout(false);
            this.ToolsStrip.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip ToolsStrip;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripButton SelectToolButton;
        private System.Windows.Forms.ToolStripButton MoveToolButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton RoadToolButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.Panel MainPanel;

    }
}

