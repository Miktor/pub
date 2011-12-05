using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;

namespace MyWorldEditor.UI
{
    class Grid : MyWorldEditor.Singleton<Grid>
    {
        public StaticGeometry GridStatic { get; private set; }

        private Grid()
        {
           
        }

        public void Init()
        {
            GridStatic = OgreRoot.Instance.mSceneMgr.CreateStaticGeometry("ground");

           // Entity ent = Utils.createFackePlane();
            SceneNode node = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("grid");
            node.AttachObject(Utils.createGrid(10));

           // GridStatic.AddEntity(ent, Vector3.ZERO);
            GridStatic.AddSceneNode(node);

            GridStatic.CastShadows = false;

            GridStatic.Build();           
        }
    }
}
