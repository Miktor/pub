namespace RenderWindow
{
    partial class MogrePanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ViewPortArea = new System.Windows.Forms.Panel();
            this.ViewportTools = new System.Windows.Forms.ToolStrip();
            this.SideToolStrips = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.MouseCoordsLable = new System.Windows.Forms.ToolStripStatusLabel();
            this.ViewPortArea.SuspendLayout();
            this.SideToolStrips.ContentPanel.SuspendLayout();
            this.SideToolStrips.TopToolStripPanel.SuspendLayout();
            this.SideToolStrips.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ViewPortArea
            // 
            this.ViewPortArea.Controls.Add(this.statusStrip1);
            this.ViewPortArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewPortArea.Location = new System.Drawing.Point(0, 0);
            this.ViewPortArea.Name = "ViewPortArea";
            this.ViewPortArea.Size = new System.Drawing.Size(697, 449);
            this.ViewPortArea.TabIndex = 0;
            // 
            // ViewportTools
            // 
            this.ViewportTools.AllowItemReorder = true;
            this.ViewportTools.Dock = System.Windows.Forms.DockStyle.None;
            this.ViewportTools.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ViewportTools.Location = new System.Drawing.Point(3, 0);
            this.ViewportTools.Name = "ViewportTools";
            this.ViewportTools.Size = new System.Drawing.Size(102, 25);
            this.ViewportTools.TabIndex = 1;
            this.ViewportTools.Text = "toolStrip1";
            // 
            // SideToolStrips
            // 
            // 
            // SideToolStrips.ContentPanel
            // 
            this.SideToolStrips.ContentPanel.Controls.Add(this.ViewPortArea);
            this.SideToolStrips.ContentPanel.Size = new System.Drawing.Size(697, 449);
            this.SideToolStrips.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SideToolStrips.Location = new System.Drawing.Point(0, 0);
            this.SideToolStrips.Name = "SideToolStrips";
            this.SideToolStrips.Size = new System.Drawing.Size(697, 474);
            this.SideToolStrips.TabIndex = 2;
            this.SideToolStrips.Text = "toolStripContainer1";
            // 
            // SideToolStrips.TopToolStripPanel
            // 
            this.SideToolStrips.TopToolStripPanel.Controls.Add(this.ViewportTools);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MouseCoordsLable});
            this.statusStrip1.Location = new System.Drawing.Point(0, 427);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(697, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // MouseCoordsLable
            // 
            this.MouseCoordsLable.Name = "MouseCoordsLable";
            this.MouseCoordsLable.Size = new System.Drawing.Size(117, 17);
            this.MouseCoordsLable.Text = "toolStripStatusLabel1";
            // 
            // MogrePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.SideToolStrips);
            this.Size = new System.Drawing.Size(697, 474);
            this.ViewPortArea.ResumeLayout(false);
            this.ViewPortArea.PerformLayout();
            this.SideToolStrips.ContentPanel.ResumeLayout(false);
            this.SideToolStrips.TopToolStripPanel.ResumeLayout(false);
            this.SideToolStrips.TopToolStripPanel.PerformLayout();
            this.SideToolStrips.ResumeLayout(false);
            this.SideToolStrips.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel ViewPortArea;
        private System.Windows.Forms.ToolStrip ViewportTools;
        private System.Windows.Forms.ToolStripContainer SideToolStrips;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel MouseCoordsLable;
    }
}
