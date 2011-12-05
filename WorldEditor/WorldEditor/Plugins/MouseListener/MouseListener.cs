using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginBase;
using System.Windows.Forms;

namespace MouseListener
{
    public class MouseListener : IMouseListener
    {
        public IPluginHost Host { get; set; }

        public string Name { get { return "MouseListener"; } }
        public string Description { get { return "MouseListener"; } }
        public string Author { get { return "Towelie"; } }
        public string Version { get { return "1.0.0.0"; } }

        public UserControl MainInterface { get { return null; } }
        public ToolStrip MainToolStrip { get { return null; } }
        public ToolStrip SubToolStrip { get { return null; } }

        public event ContiniusMouseHandler MouseDown;
        public event ContiniusMouseHandler MouseClick;
        public event ContiniusMouseHandler MouseUp;
        public event ContiniusMouseHandler MouseMove;
        public event ContiniusMouseHandler MouseDrag;
        public event ContiniusMouseHandler MouseWheel;

        public ContiniusMouseEventArgs args;

        private IRenderObjectPlugin Parent;
        private bool haveDragged = false;

        public void Initialize()
        {
            args = new ContiniusMouseEventArgs();
            Host.GlobalMouseListener = this;
        }

        public void Dispose()
        {

        }

        public void AddObject(IRenderObjectPlugin win)
        {
            win.MouseUp += new MouseEventHandler(MouseUp_Ewent);
            win.MouseDown += new MouseEventHandler(MouseDown_Ewent);
            win.MouseClick += new MouseEventHandler(MouseClick_Ewent);
            win.MouseMove += new MouseEventHandler(MouseMove_Ewent);
            win.MouseWheel += new MouseEventHandler(MouseWheel_Ewent);
            win.Resize += new EventHandler(updateSize);
            Parent = win;
            updateSize(win, null);
        }

        public void MouseDown_Ewent(object sender, MouseEventArgs evnt)
        {
            haveDragged = false;

            args.isDrag = true;
            args.Button = evnt.Button;

            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            if (MouseDown != null)
                MouseDown(Parent, args);
            
        }

        public void MouseUp_Ewent(object sender, MouseEventArgs evnt)
        {
            args.isDrag = false;
            args.Button = evnt.Button;

            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            if (MouseUp != null)
                MouseUp(Parent, args);
        }

        public void MouseClick_Ewent(object sender, MouseEventArgs evnt)
        {
            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            args.Button = evnt.Button;

            if (haveDragged == false)
            {
                if (MouseClick != null)
                    MouseClick(Parent, args);
            }
        }

        public void MouseMove_Ewent(object sender, MouseEventArgs evnt)
        {
            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            if (MouseMove != null)
                MouseMove(Parent, args);
            if (args.isDrag == true && MouseDrag != null)
            {
                haveDragged = true;     
                MouseDrag(Parent, args);
            }
        }

        public void MouseWheel_Ewent(object sender, MouseEventArgs evnt)
        {
            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            args.Delta = evnt.Delta;
            if (MouseWheel != null)
                MouseWheel(Parent, args);
        }

        public void updateSize(object sender, EventArgs e)
        {
            args.Height = (sender as Control).Height;
            args.Width = (sender as Control).Width;
        }

        //public void Listener_MouseEnter(object sender, System.EventArgs args)
        //{
        //    isMouseOver = true;
        //}

        //public void Listener_MouseLeave(object sender, System.EventArgs args)
        //{
        //    isMouseOver = false;
        //}
    }
}
