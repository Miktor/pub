using System;
using System.Collections.Generic;
using Mogre;

namespace MyWorldEditor
{
    static class Binomial
    {
        private static List<int[]> coeficients = new List<int[]> { new int[] { }, new int[] { }, new int[] { }, new int[] { }, new int[3] { 1, 4, 6 }, new int[3] { 1, 5, 10 } };

        private static int addCoef(int n, int k, int coef)
        {
            if (getCoef(n, k) != 0)
                return getCoef(n, k);

            if (coeficients.Count <= n)
                coeficients.Add(new int[(n - n % 2) / 2 + 1]);

            if ((n - n % 2) / 2 >= k)
                coeficients[n][k] = coef;
            else
                coeficients[n][n - k] = coef;
            return coef;
        }

        private static int getCoef(int n, int k)
        {
            if (k == 0 || k == n)
                return 1;
            if (k == 1 || k == n - 1)
                return n;
            else if (coeficients.Count > n)
                if ((n - n % 2) / 2 >= k)
                    return coeficients[n][k];
                else
                    return coeficients[n][n - k];
            else
                return 0;
        }

        public static int BinomialCoefficient(int n, int k)
        {
            if (getCoef(n, k) != 0)
                return getCoef(n, k);
            else
                return addCoef(n - 1, k, BinomialCoefficient(n - 1, k)) + addCoef(n - 1, k - 1, BinomialCoefficient(n - 1, k - 1));
        }
    }

    class CurvePoint : SceneNode.Listener
    {
        public SceneNode MainNode { get; private set; }
        public SceneNode FirstArmNode { get; private set; }
        public SceneNode SecondArmNode { get; private set; }

        public Vector3 MainPosition { get { return MainNode.Position;} set { MainNode.Position = value; NeedReDrow = true; } }
        public Vector3 FirstArmPosition { get { return FirstArmNode.Position; } set { FirstArmNode.Position = value; NeedReDrow = true; } }
        public Vector3 SecondArmPosition { get { return SecondArmNode.Position; } set { SecondArmNode.Position = value; NeedReDrow = true; } }

        private ManualObject arm;

        public float FirstArm { get; set; }
        public float SecondArm { get; set; }

        public bool Visible { set { MainNode.SetVisible(value); } }

        public bool NeedReDrow { get; set; }

        public int ID { get; private set; }
        public IRoadObject Owner { get; private set; }

        public CurvePoint(Vector3 Pos, int id, IRoadObject owner)
        {
            Owner = owner;
            ID = id;

            MainNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString());
            FirstArmNode = MainNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString() + "_FirstArm");
            SecondArmNode = MainNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString() + "_SecondArm");

            Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString(), "miniCube");
            ent.SetMaterial(MaterialManager.Singleton.GetByName("RoadGuideRed", ResourceGroupManager.DEFAULT_RESOURCE_GROUP_NAME));
            MainNode.AttachObject(ent);
            MainPosition = Pos;

            ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString() + "_FirstArm", "miniCube");
            FirstArmNode.AttachObject(ent);
            FirstArmPosition = Vector3.UNIT_Z * 2;
            FirstArm = 2;
            FirstArmNode.SetListener(this);

            ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString() + "_SecondArm", "miniCube");
            SecondArmNode.AttachObject(ent);
            SecondArmPosition = Vector3.NEGATIVE_UNIT_Z * 2;
            SecondArm = 2;
            SecondArmNode.SetListener(this);

            arm = new ManualObject("Arm_" + Owner.ID.ToString() + "_" + ID.ToString());            
            MainNode.AttachObject(arm);
        }

        public CurvePoint(Vector3 Pos, int id, IRoadObject owner, Vector3 _fistArmPos, Vector3 _secondArmPos)
        {
            Owner = owner;
            ID = id;

            MainNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString());
            FirstArmNode = MainNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString() + "_FirstArm");
            SecondArmNode = MainNode.CreateChildSceneNode("PointNode_" + Owner.ID.ToString() + "_" + ID.ToString() + "_SecondArm");

            Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString(), "miniCube");
            MainNode.AttachObject(ent);
            MainPosition = Pos;

            ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString() + "_FirstArm", "miniCube");
            SecondArmNode.AttachObject(ent);
            FirstArmPosition = _fistArmPos;

            ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Owner.ID.ToString() + "_" + ID.ToString() + "_SecondArm", "miniCube");
            MainNode.AttachObject(ent);
            SecondArmPosition = _secondArmPos;
        }

        public override void NodeUpdated(Node param1)
        {
            if (param1 == FirstArmNode)
            {
                Vector3 dir = (param1.Position).NormalisedCopy;
                SecondArmPosition = -dir * SecondArmPosition.Length;
            }
            if (param1 == SecondArmNode)
            {
                Vector3 dir = (param1.Position).NormalisedCopy;
                FirstArmPosition = -dir * FirstArmPosition.Length;               
            }

            arm.Clear();
            arm.Begin("RoadGuideYellow",RenderOperation.OperationTypes.OT_LINE_LIST);
            arm.Position(FirstArmPosition);
            arm.Position(SecondArmPosition);
            arm.End();
      
            NeedReDrow = true;
        }
    }   


    class Road : Singleton<Road>
    {
        public int itemID { get; private set; }

        private List<IRoadObject> RoadObjects = new List<IRoadObject>();

        public IRoadObject ActiveRoadObject { get; set; }

        public SceneNode RoadNode { get; private set; }        

        private Road()
        {
            RoadNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("RoadNode");          
        }

        public bool AddPoint(Vector3 point)
        {
            if (ActiveRoadObject==null)
            {
                IRoadObject newObj = new RoadPart(itemID++);
                ActiveRoadObject = newObj;
                RoadObjects.Add(newObj);                
            }

            ActiveRoadObject.AddPoint(point);
            DrawRoad(10);                
            
            OptionViewer.Instance.Object = ActiveRoadObject.getEndPoint();

            RedrowEWent.Instance.AskRedrow();
            return true;
        }

        public void DrawRoad(int precision = 15, bool isDebug = true)
        {
            foreach (IRoadObject rObj in RoadObjects)
            {
                rObj.DrawRoad(precision);
                //if (isDebug)
                //    rObj.DrawDebug();
                //rObj.ShowDebug(isDebug);
            }
            RedrowEWent.Instance.AskRedrow();
        }
    }
}
