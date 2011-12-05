using Mogre;

namespace PluginBase
{
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
        public System.Windows.Forms.MouseButtons Button;
    }

    public enum CameraTypes
    {
        Free, Attached
    }

    public enum SideView
    {
        Top = 1, Bottom = 2,
        Left = 3, Right = 4,
        Front = 5, Back = 6
    }

    public interface ISimple
    {
        string Name { get; }
        ISimple Parent { get; }

        SceneNode MainNode { get; }

        Vector3 Position { get; set; }
        Vector3 GlobalPosition { get; set; }
        //Quaternion Rotation { get; set; }

        void Select();
        void UnSelect();

        void Update();
        void ChildUpdated(ISimple sender);
    }
}
