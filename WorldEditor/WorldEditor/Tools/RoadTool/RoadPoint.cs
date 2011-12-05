using Mogre;
using PluginBase;
using WorldEditor.Scene.BaseObjects;

namespace WorldEditor.UI.Tools.RoadTool
{

    class PointArms : SimpleObj
    {
        public bool NeedReDrow { get; set; }

        public SimpleObj FirstArm;
        public SimpleObj SecondArm;

        private ManualObject arm;

        public PointArms(ISimple _main, string _name)
            : base(_name, _main)
        {
            FirstArm = new SimpleObj(Name + "_FirstArm", this, 5*Vector3.UNIT_Z);
            FirstArm.AttachEntity("miniCube", Name + "_1Arm", FirstArm, "RoadGuideYellow");
            
            SecondArm = new SimpleObj(Name + "_SecondArm", this, 5*Vector3.NEGATIVE_UNIT_Z);
            SecondArm.AttachEntity("miniCube", Name + "_2Arm", SecondArm, "RoadGuideYellow");      

            arm = new ManualObject(Name + "_ArmEntity");
            arm.Begin("RoadGuideGreen", RenderOperation.OperationTypes.OT_LINE_STRIP);

            arm.Position(FirstArm.Position);
            arm.Position(Vector3.ZERO);
            arm.Position(SecondArm.Position);

            arm.End();
            MainNode.AttachObject(arm);
            Update();
        }       


        public override void ChildUpdated(ISimple sender)
        {
            if (sender == FirstArm)
            {
                Vector3 dir = (sender.Position).NormalisedCopy;
                SecondArm.Position = -dir * SecondArm.Position.Length;
            }
            else if (sender == SecondArm)
            {
                Vector3 dir = (sender.Position).NormalisedCopy;
                FirstArm.Position = -dir * FirstArm.Position.Length;
            }
            else
                return;

            arm.Clear();
            arm.Begin("RoadGuideYellow", RenderOperation.OperationTypes.OT_LINE_STRIP);
            arm.Position(FirstArm.Position);
            arm.Position(Vector3.ZERO);
            arm.Position(SecondArm.Position);
            arm.End();

            Update();
        }
    }

    class ControllPoint : SimpleObj
    {
        public int ArmsCount { get; set; }
        public PointArms HArm;
        public PointArms VArm;      

        public ControllPoint(Vector3 Pos, ISimple owner, string _name)
            : base(_name, owner, Pos)
        {
            AttachEntity("miniCube", this, "RoadGuideRed");            

            HArm = new PointArms(this, Name + "_" + (ArmsCount++).ToString() + "_Arm");
            //VArm = new PointArms(this);
        }

        //public ControllPoint(Vector3 Pos, ISimple owner, string _name, Vector3 _fistArmPos, Vector3 _secondArmPos)
        //    : base(owner, _name)
        //{
        //    Parent = owner;

        //    MainNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("PointNode_" + Road.Instance.itemID.ToString());

        //    Entity ent = OgreRoot.Instance.mSceneMgr.CreateEntity("PointPointer_" + Road.Instance.itemID.ToString(), "miniCube");
        //    MainNode.AttachObject(ent);
        //    Position = Pos;

        //    HArm = new PointArms(this, Name + "_" + (ArmsCount++).ToString() + "_Arm");
        //    //VArm = new PointArms(this);
        //}
       
    }   
}
