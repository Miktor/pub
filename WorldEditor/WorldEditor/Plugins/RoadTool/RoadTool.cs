using System;
using System.Windows.Forms;
using Mogre;
using PluginBase;

namespace RoadTool
{
    public class RoadTool : IControllPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name { get { return "RoadTool"; } }
        public string Description { get { return "RoadTool"; } }
        public string Author { get { return "RoadTool"; } }
        public string Version { get { return "RoadTool"; } }

        ToolStrip mToolStrip = null;

        public UserControl MainInterface { get { return null; } }
        public ToolStrip MainToolStrip { get { return mToolStrip; } }
        public ToolStrip SubToolStrip { get { return null; } }


        public void Initialize()
        {
            ToolStripButton AddPoint = new ToolStripButton();
            AddPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            AddPoint.Image = global::RoadTool.Properties.Resources.AddPointIcon;
            AddPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            AddPoint.Name = "RoadTool";
            AddPoint.Size = new System.Drawing.Size(23, 22);
            AddPoint.Text = "RoadTool";

            mToolStrip = new ToolStrip();
            mToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { AddPoint });
        }

        public void InitEventHandlers()
        {
            Host.GlobalMouseListener.MouseClick += new ContiniusMouseHandler(MouseClick);
        }

        public void Dispose()
        {
            Host.GlobalMouseListener.MouseClick -= new ContiniusMouseHandler(MouseClick);
        }

        public bool MouseClick(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            return true;
        }       
    }
}
