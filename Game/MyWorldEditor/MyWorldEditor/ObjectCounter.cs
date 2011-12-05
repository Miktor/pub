using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyWorldEditor
{
    class ObjectCounter : Singleton<ObjectCounter>
    {
        int _Cameras = 0;
        public int Cameras { get { return _Cameras++; } }
        int _MogrePanels = 0;
        public int MogrePanels { get { return _MogrePanels++; } }

        private ObjectCounter()
        {
        }
    }
}
