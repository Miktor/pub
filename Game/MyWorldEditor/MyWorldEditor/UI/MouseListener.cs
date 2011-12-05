using System;
using System.Windows.Forms;

namespace MyWorldEditor.UI
{
    public delegate void ContiniusMouseHandler(object sender, ContiniusMouseEventArgs e);

    public struct ContiniusMouseEventArgs
    {
        public float Height;
        public float Width;

        public float X;
        public float Y;

        public float relX
        {
            get
            {
                return X / Width;
            }
            set
            {
                X = value * Width;
            }
        }
        public float relY
        {
            get
            {
                return Y / Height;
            }
            set
            {
                Y = value * Height;
            }
        }

        public bool isDrag;

        public float _lastX, _lastY;

        public float RelDeltaX
        {
            get
            {
                return (X - _lastX) / Width;
            }
        }
        public float RelDeltaY
        {
            get
            {
                return (Y - _lastY) / Height;
            }
        }

        public float AbsDeltaX
        {
            get
            {
                return (X - _lastX);
            }
        }
        public float AbsDeltaY
        {
            get
            {
                return (Y - _lastY);
            }
        }

        public int Delta;
        public MouseButtons Button;
    }

    public class MouseListener
    {
        public event ContiniusMouseHandler MouseEvent;

        public ContiniusMouseEventArgs args;      

        private bool isMouseOver;

        public MouseListener(float _height, float _width)
        {
            args = new ContiniusMouseEventArgs();

            args.Height = _height;
            args.Width = _width;
        }

        public void Listener_Update(object sender, MouseEventArgs evnt)
        {
            args.isDrag = (args.Button == evnt.Button) && ((args.Y != (float)evnt.Y) || (args.X != (float)evnt.X));

            args._lastX = args.X;
            args.X = (float)evnt.X;

            args._lastY = args.Y;
            args.Y = (float)evnt.Y;

            args.Delta = evnt.Delta;            
            args.Button = evnt.Button;

            if (MouseEvent != null)
                MouseEvent(this, args);
        }

        public void updateSize(object sender, EventArgs e)
        {
            args.Height = (sender as Control).Height;
            args.Width = (sender as Control).Width;
        }

        public void Listener_MouseEnter(object sender, System.EventArgs args)
        {
            isMouseOver = true;
        }

        public void Listener_MouseLeave(object sender, System.EventArgs args)
        {
            isMouseOver = false;
        }
    }
}
