using System;
using Mogre;

namespace WorldEditor
{
    //класс всякой поеботы, он типо статик, его ничен е нужно объявлять и т.п. пишеш Utils.<Funcname>
    //но е увлекайся, обычно статики юзают для всякого говна типо конвертировани, 
    public partial class Utils : Singleton<Utils>
    {
        private Utils() { }
        public static ManualObject createGrid(int dim)
        {
            float minX = - 10 * dim;
            float minZ = - 10 * dim;

            ManualObject mygrid = new ManualObject("MyGrid");
            mygrid.Begin("", RenderOperation.OperationTypes.OT_LINE_LIST);

            for (int i = 0; i <= 2 * dim; i++)
            {
                mygrid.Position(minX + 10 * i, 1, minZ);
                mygrid.Position(minX + 10 * i, 1, -minZ);

                mygrid.Position(minX, 1, minZ + 10 * i);
                mygrid.Position(-minX, 1, minZ + 10 * i);
            }

            mygrid.End();

            return mygrid;
        }

        public Entity createFackePlane()
        {
            try
            {
                Plane plane = new Plane(Vector3.UNIT_Y, 1);

                MeshManager.Singleton.CreatePlane("floorPlane", Mogre.ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME, plane, 450.0f, 450.0f, 10, 10, true, 1, 50.0f, 50.0f, Vector3.UNIT_Z);
                Entity planeEnt = OgreRoot.Instance.mSceneMgr.CreateEntity("planeEnty", "floorPlane", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME);

                return planeEnt;
            }
            catch (Exception e)
            {
                LogManager.Singleton.DefaultLog.LogMessage(e.ToString());
                return null;
            }
        }
     
    }

}