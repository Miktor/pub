using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;

namespace WorldEditor.UI.Tools
{
    class MoveTool : Singleton<MoveTool>, ITool
    {
        public string Name { get; private set; }
        public bool isActive { get; private set; }

        private MoveTool()
        {

        }

        private RaySceneQuery mRayQ;

        public bool haveButton()
        {
            return false;
        }

        public ToolStripButton ControlButton { get; private set; }

        private MoveTool(string name = "MoveTool")
        {
            Name = name;              
        }

        public bool Activate()
        {
            return false;
        }
        public bool DeActivate()
        {
            return false;
        }

        public bool MouseClick(ContiniusMouseEventArgs args)
        {
            return true;
        }

        public bool MouseDragChange(bool isDruging)
        {
            return true;
        }
        public bool MouseDraging(ContiniusMouseEventArgs args)
        {
            if (ToolController.Instance.SelectedNode == null)
            {
                RaySceneQueryResult obj = ToolController.Instance.CastMouseRay(args);
                if (!obj.IsEmpty && obj[0].movable != null)
                {
                    ToolController.Instance.SelectedNode = obj[0].movable.ParentSceneNode;
                }
            }
            else
            {
                try
                {
                    Vector3 node = (Vector3)ToolController.Instance.ContactPoint(args);
                    ToolController.Instance.SelectedNode.Position = node;                   
                }
                catch(Exception e)
                {
                    ExeptionHandler.Instance.CatchException(e, this);
                }
            }
            return false;
        }

        public bool MouseMove(ContiniusMouseEventArgs args)
        {
            RaySceneQueryResult obj = ToolController.Instance.CastMouseRay(args);
            if (obj.IsEmpty == false && obj[0].movable != null)            
                Cursor.Current = Cursors.Cross;           
            else
                Cursor.Current = Cursors.Arrow;
            return true;
        }

        public bool MouseWheel(int ticks)
        {
            return true;
        }
    }
}
