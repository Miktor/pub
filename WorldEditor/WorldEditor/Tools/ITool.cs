using PluginBase;

namespace WorldEditor
{
    interface ITool
    {
        string Name { get; }

        bool isActive { get; }
        bool Activate();
        bool DeActivate();

        bool haveButton();

        bool MouseClick(ContiniusMouseEventArgs args);
        bool MouseDown(ContiniusMouseEventArgs args);
        bool MouseUp(ContiniusMouseEventArgs args);

        bool MouseDragChange(bool isDruging);
        bool MouseDraging(ContiniusMouseEventArgs args);

        bool MouseMove(ContiniusMouseEventArgs args);

        bool MouseWheel(int ticks);
    }

    class Tool : ITool
    {
        public string Name { get; private set; }
        public virtual bool isActive { get; private set; }

        public virtual bool haveButton() { return false; }       

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


        public bool MouseDown(ContiniusMouseEventArgs args)
        {
            return true;
        }

        public bool MouseUp(ContiniusMouseEventArgs args)
        {
            return true;
        }
    }
}
