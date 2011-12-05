using Mogre;

namespace WorldEditor.Curves
{
    class Point
    {
        public Vector3 Position { get; set; }
        public Vector3 Tangent { get; set; }
        int t { get; set; }

        public SceneNode MainNode;

        //public Point(Vector3 _pos) { Position = _pos;}
        public Point(Vector3 _pos, Vector3 _tanget) 
        { 
            Position = _pos; 
            Tangent = _tanget;           
        }

    }
}
