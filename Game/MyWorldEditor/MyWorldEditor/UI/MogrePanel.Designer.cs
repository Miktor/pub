namespace MyWorldEditor.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MogrePanel));
            this.panel1 = new System.Windows.Forms.Panel();
            this.ViewportTools = new System.Windows.Forms.ToolStrip();
            this.UndoButton = new System.Windows.Forms.ToolStripButton();
            this.ReduButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewPorttype = new System.Windows.Forms.ToolStripSplitButton();
            this.normalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wireFrameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.TringleCount = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.topViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bottomViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leftViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rightViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewportTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(491, 365);
            this.panel1.TabIndex = 0;
            // 
            // ViewportTools
            // 
            this.ViewportTools.Location = new System.Drawing.Point(0, 0);
            this.ViewportTools.Name = "ViewportTools";
            this.ViewportTools.Size = new System.Drawing.Size(497, 25);
            this.ViewportTools.TabIndex = 1;
            this.ViewportTools.Text = "toolStrip1";
            // 
            // UndoButton
            // 
            this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.UndoButton.Image = global::MyWorldEditor.Properties.Resources._112_LeftArrowLong_Blue_48x48_72;
            this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.UndoButton.Name = "UndoButton";
            this.UndoButton.Size = new System.Drawing.Size(23, 22);
            this.UndoButton.Text = "toolStripButton1";
            // 
            // ReduButton
            // 
            this.ReduButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ReduButton.Image = global::MyWorldEditor.Properties.Resources._112_RightArrowLong_Blue_48x48_72;
            this.ReduButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ReduButton.Name = "ReduButton";
            this.ReduButton.Size = new System.Drawing.Size(23, 22);
            this.ReduButton.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ViewPorttype
            // 
            this.ViewPorttype.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ViewPorttype.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.normalToolStripMenuItem,
            this.wireFrameToolStripMenuItem,
            this.pointsToolStripMenuItem});
            this.ViewPorttype.Image = ((System.Drawing.Image)(resources.GetObject("ViewPorttype.Image")));
            this.ViewPorttype.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewPorttype.Name = "ViewPorttype";
            this.ViewPorttype.Size = new System.Drawing.Size(32, 22);
            this.ViewPorttype.Text = "toolStripSplitButton1";
            // 
            // normalToolStripMenuItem
            // 
            this.normalToolStripMenuItem.Name = "normalToolStripMenuItem";
            this.normalToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.normalToolStripMenuItem.Text = "Normal";
            // 
            // wireFrameToolStripMenuItem
            // 
            this.wireFrameToolStripMenuItem.Name = "wireFrameToolStripMenuItem";
            this.wireFrameToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.wireFrameToolStripMenuItem.Text = "WireFrame";
            // 
            // pointsToolStripMenuItem
            // 
            this.pointsToolStripMenuItem.Name = "pointsToolStripMenuItem";
            this.pointsToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.pointsToolStripMenuItem.Text = "Points";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // TringleCount
            // 
            this.TringleCount.Name = "TringleCount";
            this.TringleCount.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // topViewToolStripMenuItem
            // 
            this.topViewToolStripMenuItem.Name = "topViewToolStripMenuItem";
            this.topViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.topViewToolStripMenuItem.Text = "Top View";
            // 
            // bottomViewToolStripMenuItem
            // 
            this.bottomViewToolStripMenuItem.Name = "bottomViewToolStripMenuItem";
            this.bottomViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bottomViewToolStripMenuItem.Text = "Bottom View";
            // 
            // leftViewToolStripMenuItem
            // 
            this.leftViewToolStripMenuItem.Name = "leftViewToolStripMenuItem";
            this.leftViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.leftViewToolStripMenuItem.Text = "Left View";
            // 
            // rightViewToolStripMenuItem
            // 
            this.rightViewToolStripMenuItem.Name = "rightViewToolStripMenuItem";
            this.rightViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rightViewToolStripMenuItem.Text = "Right View";
            // 
            // MogrePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ViewportTools);
            this.Controls.Add(this.panel1);
            this.Name = "MogrePanel";
            this.Size = new System.Drawing.Size(497, 393);
            this.ViewportTools.ResumeLayout(false);
            this.ViewportTools.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip ViewportTools;
        private System.Windows.Forms.ToolStripButton UndoButton;
        private System.Windows.Forms.ToolStripButton ReduButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSplitButton ViewPorttype;
        private System.Windows.Forms.ToolStripMenuItem normalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wireFrameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel TringleCount;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem topViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bottomViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leftViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rightViewToolStripMenuItem;
    }
}
