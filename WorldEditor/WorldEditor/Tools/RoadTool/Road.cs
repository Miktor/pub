using System.Collections.Generic;
using Mogre;

namespace WorldEditor.UI.Tools.RoadTool
{
    class Road : Scene.BaseObjects.SimpleObj
    {
        private int _itemID;
        public int itemID
        {
            get { return _itemID++; }            
        }

        private List<RoadPart> RoadObjects = new List<RoadPart>();

        public RoadPart ActiveRoadObject { get; set; }

        public Road()
            :base("MainRoad")
        {
            _itemID = 0;
            MainNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("RoadNode");          
        }

        public bool AddPoint(Vector3 point)
        {
            if (ActiveRoadObject==null)
            {
                RoadPart newObj = new RoadPart(this, "RoadPart_" + itemID.ToString());
                ActiveRoadObject = newObj;
                RoadObjects.Add(newObj);                
            }

            ActiveRoadObject.AddPoint(point);
            DrawRoad();

            OptionViewer.Instance.Object = ActiveRoadObject;

            RedrowEwent.Instance.AskRedrow();
            return true;
        }

        public void DrawRoad(bool isDebug = true)
        {
            foreach (RoadPart rObj in RoadObjects)
            {
                rObj.DrawRoad();
                //if (isDebug)
                //    rObj.DrawDebug();
                //rObj.ShowDebug(isDebug);
            }            
        }

        override public void ChildUpdated(PluginBase.ISimple sender)
        {
            OgreRoot.Instance.Paint(sender, null);
        }
    }
}
