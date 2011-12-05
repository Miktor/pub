
namespace WorldEditor
{
    class ObjectCounter : Singleton<ObjectCounter>
    {
        int _Cameras = 0;
        public int Cameras { get { return _Cameras++; } }
        int _MogrePanels = 0;
        public int MogrePanels { get { return _MogrePanels++; } }
        int _ControllPoints = 0;
        public int ControllPoints { get { return _ControllPoints++; } }

        private ObjectCounter() { }
    }
}
