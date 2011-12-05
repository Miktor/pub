using System;
using System.Collections.Generic;
using Mogre;
using WorldEditor.Curves;
using WorldEditor.Scene.BaseObjects;

namespace WorldEditor.UI.Tools.RoadTool
{
    public enum CurveType
    {
        ThreePoint = 1,
        FourPoint = 2,
        AllPoints = 3
    }

    class RoadPart : SimpleObj, IRoadObject
    {
        public float Length { get; private set; }

        private List<Curve> RoadPaths = new List<Curve>();       

        public SceneNode RoadDebugNode { get; private set; }
        public ManualObject RoadGuide { get; private set; }
        public ManualObject CurveGuides { get; private set; }
        public ManualObject CurveTangents { get; private set; }

        public ManualObject RoadMesh { get; private set; }

        public RoadPart(SimpleObj _main, string _name)
            : base(_name, _main)
        {       
            RoadDebugNode = MainNode.CreateChildSceneNode(Name + "DebugNode_");

            RoadMesh = new ManualObject(Name + "RoadMesh");
            MainNode.AttachObject(RoadMesh);

            RoadGuide = new ManualObject(Name + "RoadMesh");
            RoadDebugNode.AttachObject(RoadGuide);

            CurveGuides = new ManualObject(Name + "CubucCurveGuides");
            RoadDebugNode.AttachObject(CurveGuides);

            CurveTangents = new ManualObject(Name + "CurveTangents");
            RoadDebugNode.AttachObject(CurveTangents);
        }

        public RoadPart(SimpleObj _main, string _name, SceneNode roadNode, SceneNode debugNode)
            : base(_name, _main)
        {
            Parent = _main;
            Name = _name;           

            MainNode = roadNode;
            RoadDebugNode = debugNode;

            RoadMesh = new ManualObject(Name + "RoadMesh");
            MainNode.AttachObject(RoadMesh);

            RoadGuide = new ManualObject(Name + "RoadMesh");
            RoadDebugNode.AttachObject(RoadGuide);

            CurveGuides = new ManualObject(Name + "CubucCurveGuides");
            RoadDebugNode.AttachObject(CurveGuides);

            CurveTangents = new ManualObject(Name + "CurveTangents");
            RoadDebugNode.AttachObject(CurveTangents);
        }

        #region Interface implementation

        public void AddPoint(Vector3 pos)
        {
            ControllPoint firstpoint;

            if (RoadPaths.Count == 0)
                firstpoint = new ControllPoint(pos - Vector3.NEGATIVE_UNIT_Z * 10, this, Name + "_CPBegin");
            else
                firstpoint = RoadPaths[RoadPaths.Count - 1].EndCP;

            RoadPaths.Add(new Curve(firstpoint, pos, this, Name + "_Curve_" + RoadPaths.Count.ToString()));
        }

        public bool RemovePoint(int n)
        {
            try
            {
                RoadPaths.RemoveAt(n);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //public bool RemovePoint(RoadPoint point)
        //{
        //    return RoadPaths.Remove(point);
        //}

        //public bool RemovePointByName(string name)
        //{
        //    return RemovePoint(FindPointByName(name));
        //}

        //public int FindPointByName(string name)
        //{
        //    for (int n = 0; n < RoadPaths.Count; n++)
        //        if (RoadPaths[n].Name == name)
        //            return n;
        //    return -1;            
        //}

        //public RoadPoint getBeginPoint()
        //{
        //    if (RoadPaths.Count > 0)
        //        return RoadPaths[0];
        //    else
        //        return null;
        //}

        //public RoadPoint getEndPoint()
        //{
        //    if (RoadPaths.Count > 0)
        //        return RoadPaths[0];
        //    else
        //        return null;
        //}

        //public List<RoadPoint> getBorderPoints()
        //{
        //    return new List<RoadPoint> { getBeginPoint(), getEndPoint() };
        //}


        public void DrawRoad(int precision = 15)
        {
            List<Vector3> _points = new List<Vector3>();
            precision *= RoadPaths.Count;

            RoadMesh.Clear();

            RoadMesh.Begin("RoadGray", RenderOperation.OperationTypes.OT_LINE_STRIP);

            Vector3 pos;
            for (int i = 0; i < RoadPaths.Count; i++)
            {
                RoadPaths[i].update();
                for (int n = i == 0 ? 0 : 1; n < RoadPaths[i].Points.Count; n++)
                {
                    pos = RoadPaths[i].Points[n].Position;
                    //backSide = RoadPaths[i].Points[n].Tangent.NormalisedCopy;

                    //RoadMesh.Position(pos - backSide);
                    RoadMesh.Position(pos);
                }
            }
            RoadMesh.End();
            DrawRoadGuide();
        }

        public void DrawRoadGuide(int precision = 15)
        {
            List<Vector3> _points = new List<Vector3>();
            //SceneNode MainNode, pointNode;
            //Entity ent;
            
            //RoadDebugNode.RemoveAndDestroyAllChildren();   

            RoadGuide.Clear();

            RoadGuide.Begin("RoadGuideYellow", RenderOperation.OperationTypes.OT_LINE_STRIP);

            for (int i = 0; i < RoadPaths.Count; i++)
            {
                _points.Clear();
                for (int n = 0; n < RoadPaths[i].Points.Count; n++)
                {
                    RoadGuide.Position(RoadPaths[i].Points[n].Position);
                    //MainNode = RoadDebugNode.CreateChildSceneNode(RoadDebugNode.Name + i.ToString() + "_" + n.ToString(), RoadPaths[i].Points[n].Position); 
                    //ent = OgreRoot.Instance.mSceneMgr.CreateEntity(MainNode.Name + "_Ent", "miniCube");
                    
                    //MainNode.AttachObject(ent);
                    //MainNode.SetScale(0.05f, 0.05f, 0.05f);

                    //pointNode.Position = RoadPaths[i].Points[n].Position;
                }
                //for (int n = 0; n < 4; n++)
                //{
                //    pointNode = MainNode.CreateChildSceneNode("ControllPointNode_" + ObjectCounter.Instance.ControllPoints.ToString(), RoadPaths[i].ControllPoint[n]);
                //    ent = OgreRoot.Instance.mSceneMgr.CreateEntity(pointNode.Name + "_Ent", "miniCube");
                //    ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed"));
                //    pointNode.AttachObject(ent);
                //    pointNode.SetScale(0.1f, 0.1f, 0.1f);
                //}
            }
            RoadGuide.End();        
        }

        public void ShowDebug(bool show)
        {
            RoadDebugNode.SetVisible(show, true);
        }

        public void DrawDebug()
        {
            DrawRoadGuide();
            DrawTangents();
        }

        #endregion

        //private void ChangePointVisiblity(bool visible, int n = 0)
        //{
        //    if (n == 0)            
        //        foreach (RoadPoint point in RoadPaths)
        //        {
        //            point.Visible = visible;
        //        }
        //    else
        //        RoadPaths[n - 1].Visible = visible;
        //}

        private void DrawTangents(int precision = 15)
        {
            Vector3 pos, tangent;
            List<Vector3> _points = new List<Vector3>();

            precision *= RoadPaths.Count;

            CurveTangents.Clear();

            CurveTangents.Begin("RoadGuideGreen", RenderOperation.OperationTypes.OT_LINE_LIST);

            for (int i = 0; i < RoadPaths.Count; i++)
            {
                for (int n = 0; n <= RoadPaths[i].Points.Count; n++)
                {
                    pos = RoadPaths[i].Points[n].Position;
                    tangent = RoadPaths[i].Points[n].Tangent;

                    CurveTangents.Position(pos - tangent);
                    CurveTangents.Position(pos + tangent);
                }
            }
            CurveTangents.End();
        }

        public override void ChildUpdated(PluginBase.ISimple sender)
        {
            DrawRoad();
            Parent.ChildUpdated(sender);
        }
    }
}