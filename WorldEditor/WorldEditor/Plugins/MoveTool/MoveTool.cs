using System;
using PluginBase;
using System.Reflection;
using System.Windows.Forms;
using Mogre;

namespace MoveTool
{
    public class MoveTool : IControllPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name { get { return "MoveTool"; } }
        public string Description { get { return "MoveTool"; } }
        public string Author { get { return "MoveTool"; } }
        public string Version { get { return "MoveTool"; } }

        ToolStrip mToolStrip = null;

        public UserControl MainInterface { get { return null; } }

        public ToolStrip MainToolStrip { get { return mToolStrip; } }

        public ToolStrip SubToolStrip { get { return null; } }


        public void Initialize()
        {
            ToolStripButton selectBtn = new ToolStripButton();
            selectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            selectBtn.Image = global::MoveTool.Properties.Resources.pointer_black;
            selectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            selectBtn.Name = "selectButton";
            selectBtn.Size = new System.Drawing.Size(23, 22);
            selectBtn.Text = "selectButton";

            ToolStripButton moveBtn = new ToolStripButton();
            moveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            moveBtn.Image = global::MoveTool.Properties.Resources.move_pointer;
            moveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            moveBtn.Name = "moveButton";
            moveBtn.Size = new System.Drawing.Size(23, 22);
            moveBtn.Text = "moveButton";

            mToolStrip = new ToolStrip();
            mToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { selectBtn, moveBtn });
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MouseClick(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            return true;
        }

        public bool MouseDown(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            RaySceneQueryResult obj = sender.Camera.CastMouseRay(args);
            if (!obj.IsEmpty && obj[0].movable != null)
            {
                Host.SelectedObject = obj[0].movable.UserObject as ISimple;
            }
            return true;
        }

        public bool MouseUp(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public bool MouseDraging(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            if (Host.SelectedObject != null)            
            {
                try
                {
                    Vector3 node = (Vector3)sender.Camera.ContactPoint(args);
                    Host.SelectedObject.Position = node;
                }
                catch (Exception e)
                {
                    Host.CatchException(e, this);
                }
            }
            return false;
        }

        public bool MouseMove(IRenderObjectPlugin sender, ContiniusMouseEventArgs args)
        {
            RaySceneQueryResult obj = sender.Camera.CastMouseRay(args);
            if (obj.IsEmpty == false && obj[0].movable != null)
                Cursor.Current = Cursors.Cross;
            else
                Cursor.Current = Cursors.Arrow;
            return true;
        }

        public bool MouseWheel(IRenderObjectPlugin sender, int ticks)
        {
            return true;
        }
    }
}
