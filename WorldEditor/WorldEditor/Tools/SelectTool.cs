using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mogre;
using WorldEditor.Scene.BaseObjects;

namespace WorldEditor.Tools.MoveTool
{
    class SelectTool : Singleton<SelectTool>, ITool
    {
        public string Name { get; private set; }
        public bool isActive { get; private set; }

        public bool haveButton() 
        {
            if (ControlButton == null)
                return false;
            else return true;
        }        

        public ToolStripButton ControlButton { get; private set; }

        private SelectTool()
        {
            Name = "SelectTool"; 
            ControlButton = new ToolStripButton("Select Tool", global::WorldEditor.Properties.Resources.pointer_black);
        }

        public virtual bool Activate()
        {
            return false;
        }
        public virtual bool DeActivate()
        {
            return false;
        }

        public virtual bool MouseClick(ContiniusMouseEventArgs args)
        {
            RaySceneQueryResult res = ToolController.Instance.CastMouseRay(args);
            if (res.IsEmpty == false)
                if (res[0].movable != null)
                {
                    ISimple obj = res[0].movable.UserObject as ISimple;
                    if (obj != null)
                    {
                        res[0].movable.ParentSceneNode.ShowBoundingBox = true;
                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            else
                return true;
        }

        public virtual bool MouseDragChange(bool isDruging)
        {     
            return true;
        }
        public virtual bool MouseDraging(ContiniusMouseEventArgs args)
        {
            return true;
        }

        public virtual bool MouseMove(ContiniusMouseEventArgs args)
        {
            return true;
        }

        public virtual bool MouseWheel(int ticks)
        {
            return true;
        }
    }
}
