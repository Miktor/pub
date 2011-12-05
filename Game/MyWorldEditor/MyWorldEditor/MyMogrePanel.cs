//using System.Drawing;
//using System.Windows.Forms;
//using System.ComponentModel;
//using Mogre;

//namespace MyWorldEditor
//{
//    public partial class RenderObject
//    {
//        static int count;

//        public MyEvents myEvent;
//        public SceneManager mSceneManager;
//        public Control mContrl;
//        public RenderWindow mWindow;
//        public Camera mCamera;
//        public Viewport mViewport;

//        RaySceneQuery myRayQ;
//        PlaneBoundedVolumeList mVolList;

//        private Point lastMouseCoord;
//        private bool mouseDragging;
//        private bool mouseMiddleDragging;
//        private bool mouseOver;

//        // охуитительный faq http://www.rsdn.ru/article/dotnet/PropertyGridFAQ.xml
//        [DisplayName("Ogre Window")]
//        [Description("App Main Class")]
//        [Category("1. Main Properties")]

//        public Camera Camera
//        {
//            get { return mCamera; }
//            set { mCamera = value; }
//        }
//        public Viewport Viewport
//        {
//            get { return mViewport; }
//            set { mViewport = value; }
//        }

//        public RenderObject(Control _mContrl)
//        {
//            myEvent = new MyEvents();
//            mContrl = _mContrl;

//            mVolList = new PlaneBoundedVolumeList();
//            //myRayQ = OgreRoot.Instance.mSceneMgr.CreatePlaneBoundedVolumeQuery(mVolList, Mogre.SceneManager.WORLD_GEOMETRY_TYPE_MASK);
//            myRayQ = OgreRoot.Instance.mSceneMgr.CreateRayQuery(new Ray(), Mogre.SceneManager.ENTITY_TYPE_MASK);            
//        }

//        #region Initialize

//        public void init(Form form, Vector3 camPos)
//        {
//            try
//            {            
//                createWindow();
//                createCamera("baseCamera_" + count++, camPos);
//                createViewPort();
//                InitInput(form);
                
//                //mCamera.PolygonMode = PolygonMode.PM_WIREFRAME;

//                mContrl.Resize +=new System.EventHandler(mContrl_Resize);
//            }
//            catch (System.Exception ex)
//            {
//                MessageBox.Show(ex.ToString());
//            }
//        }

//        public void createWindow()
//        {
//            NameValuePairList misc = new NameValuePairList();
//            misc["externalWindowHandle"] = mContrl.Handle.ToString();
//            mWindow = OgreRoot.Instance.root.CreateRenderWindow(mContrl.Name, (uint)mContrl.Height, (uint)mContrl.Width, false, misc);
//        }

//        public void createCamera(string name, Vector3 pos)
//        {
//            mCamera = OgreRoot.Instance.mSceneMgr.CreateCamera(name);
//            mCamera.Position = pos;
//            mCamera.LookAt(Mogre.Vector3.ZERO);
//            mCamera.NearClipDistance = 1; 
//        }

//        public void createViewPort()
//        {
//            mViewport = mWindow.AddViewport(mCamera);
//            mViewport.BackgroundColour = new ColourValue(0.1f, 0.1f, 0.1f, 1.0f);            
//        }

//        #endregion

//        #region Input

//        private Ray getRayFromCam(MouseEventArgs args)
//        {
//            return mCamera.GetCameraToViewportRay((float)args.X / (float)mContrl.Width, (float)args.Y / (float)mContrl.Height);
//        }
//        public void InitInput(Form form)
//        {
//            mContrl.MouseClick += new MouseEventHandler(MogrePanel_MouseDown);
//            mContrl.MouseMove += new MouseEventHandler(MogrePanel_MouseMove);

//            mContrl.MouseEnter += new System.EventHandler(mContrl_MouseLeave);
//            mContrl.MouseLeave += new System.EventHandler(mContrl_MouseLeave);

//            form.KeyDown += new KeyEventHandler(MogrePanel_KeyDown);
//            form.MouseWheel += new MouseEventHandler(mContrl_MouseWheel);
//        }    
       

//        private void mContrl_MouseWheel(object sender, MouseEventArgs args)
//        {
//            if (mouseOver == false)
//            {
//                RayCaster.Instance.StopDrag();
//                return;
//            }
//            mCamera.Move(mCamera.Direction * args.Delta / 10);
//            myEvent.Redrow();
//        }

//        private void MogrePanel_MouseMove(object sender, MouseEventArgs args)
//        {
//            mouseOver = true;

//            if (args.Button == MouseButtons.Left)
//            {
//                RayCaster.Instance.Drag(getRayFromCam(args));
//            }
//            else if (args.Button == MouseButtons.Right)
//            {
//                mCamera.Rotate(Mogre.Vector3.UNIT_Y, new Mogre.Degree(args.X - lastMouseCoord.X));
//                mCamera.Pitch(new Mogre.Degree(args.Y - lastMouseCoord.Y));
//                myEvent.Redrow();
//            }
//            else if (args.Button == MouseButtons.Middle)
//            {
//                mCamera.Move(-(new Mogre.Vector3(args.X - lastMouseCoord.X, args.Y - lastMouseCoord.Y, 0)));
//                myEvent.Redrow();
//            }
//            else
//                RayCaster.Instance.StopDrag();
//            lastMouseCoord = args.Location;
            
//        }

//        private void MogrePanel_MouseDown(object sender, MouseEventArgs args)
//        {
//            mouseOver = true;

//            if (args.Button == MouseButtons.Left) 
//                RayCaster.Instance.createRoadPoint(getRayFromCam(args));
//            else
//                RayCaster.Instance.StopDrag();
//            lastMouseCoord = args.Location;
//        }

//        private void mContrl_MouseEnter(object sender, System.EventArgs args)
//        {
//            mouseOver = true;
//        }

//        private void mContrl_MouseLeave(object sender, System.EventArgs args)
//        {
//            mouseOver = false;
//        }

//        private void MogrePanel_KeyDown(object sender, KeyEventArgs key)
//        {
//            if (mouseOver == false)
//                return;

//            if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up)
//                mCamera.Move(mCamera.Direction);
//            else if (key.KeyCode == Keys.S || key.KeyCode == Keys.Down)
//                mCamera.Move(-mCamera.Direction);
//            else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
//                mCamera.Move(-mCamera.DerivedRight);
//            else if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right)
//                mCamera.Move(mCamera.DerivedRight);
//            else if (key.KeyCode == Keys.Space)
//                mCamera.Move(mCamera.DerivedUp);
//            else if (key.KeyCode == Keys.Q)
//                mCamera.PolygonMode = PolygonMode.PM_WIREFRAME;
//            else
//                return;


//            myEvent.Redrow();

//        }

//        private void mContrl_Resize(object sender, System.EventArgs args)
//        {
//            Control cntr = sender as Control;
//            Point p = cntr.PointToClient((sender as Control).Location);

//            mWindow.Resize((uint)mContrl.Width, (uint)mContrl.Height);
//        }
//        #endregion
//    }
//}
