using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using PluginBase;
using System.Windows.Forms;

namespace CameraPlugin
{
    public class CameraPlugin : ICameraPlugin
    {
        #region Plugin
        public IPluginHost Host { get; set; }

        public IRenderObjectPlugin Parent { get; private set; }

        public string Name { get { return "CameraPlugin"; } }
        public string Description { get { return "CameraPlugin"; } }
        public string Author { get { return "CameraPlugin"; } }
        public string Version { get { return "CameraPlugin"; } }
        #endregion

        #region UI
        public UserControl MainInterface { get { return null; } }
        public ToolStrip MainToolStrip { get { return null; } }
        public ToolStrip SubToolStrip { get { return null; } }

        private ToolStrip CameraToolStrip = null;
        private ToolStripSplitButton CameraTools = new ToolStripSplitButton();
        private ToolStripMenuItem topViewToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem bottomViewToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem leftViewToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem rightViewToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem frontViewToolStripMenuItem = new ToolStripMenuItem();
        private ToolStripMenuItem backViewToolStripMenuItem = new ToolStripMenuItem();
        #endregion

        private CameraTypes _cameraType;
        public CameraTypes CameraType
        {
            get { return _cameraType; }
            set
            {
                if (value == CameraTypes.Attached)
                    if (TrackingNode == null)
                        throw new ArgumentNullException("TrackingNode", "TrackingNode have not seted yet");
                    else
                        mCamera.SetAutoTracking(true, TrackingNode);
                else
                {
                    mCamera.SetAutoTracking(false);
                    TrackingNode = null;
                }
                _cameraType = value;
            }
        }

        public float Distance { get; set; } 

        public SceneManager mSceneManager { get; set; }

        public SceneNode TrackingNode { get; set; }

        private Mogre.Camera mCamera;
        private Viewport mViewport;
        private RaySceneQuery rQuery;
        
        public void Initialize()
        {
            _cameraType = CameraTypes.Free;     
        }

        public void SetParent(IRenderObjectPlugin _parent)
        {
            _cameraType = CameraTypes.Free; 

            Parent = _parent;
            mSceneManager = Host.ActiveScene.Manager;

            mCamera = mSceneManager.CreateCamera(_parent.Name + "_Camera");
            mCamera.Position = Vector3.UNIT_SCALE * 5;
            mCamera.LookAt(Mogre.Vector3.ZERO);
            mCamera.NearClipDistance = 1;

            mViewport = Parent.mWindow.AddViewport(mCamera);
            mViewport.BackgroundColour = new ColourValue(0.1f, 0.1f, 0.1f, 1.0f);

            rQuery = mSceneManager.CreateRayQuery(new Ray(), Mogre.SceneManager.ENTITY_TYPE_MASK);
            rQuery.SetSortByDistance(true, 5);

            Host.GlobalMouseListener.MouseMove += new ContiniusMouseHandler(MouseMove);
            Host.GlobalMouseListener.MouseWheel += new ContiniusMouseHandler(MouseWheel);

            CreateButton();
            Parent.AddToTopStrip(CameraToolStrip);
        }

        private void CreateButton()
        {
            CameraToolStrip = new ToolStrip();

            CameraToolStrip.Name = "CameraToolStrip";
            CameraToolStrip.Size = new System.Drawing.Size(102, 25);
            CameraToolStrip.Text = "CameraToolStrip";

            CameraTools = new System.Windows.Forms.ToolStripSplitButton();
            // 
            // topViewToolStripMenuItem
            // 
            this.topViewToolStripMenuItem.Name = "topViewToolStripMenuItem";
            this.topViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.topViewToolStripMenuItem.Tag = (int)SideView.Top;
            this.topViewToolStripMenuItem.Text = "Top View";
            this.topViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // bottomViewToolStripMenuItem
            // 
            this.bottomViewToolStripMenuItem.Name = "bottomViewToolStripMenuItem";
            this.bottomViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.bottomViewToolStripMenuItem.Text = "Bottom View";
            this.bottomViewToolStripMenuItem.Tag = (int)SideView.Bottom;
            this.bottomViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // leftViewToolStripMenuItem
            // 
            this.leftViewToolStripMenuItem.Name = "leftViewToolStripMenuItem";
            this.leftViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.leftViewToolStripMenuItem.Text = "Left View";
            this.leftViewToolStripMenuItem.Tag = (int)SideView.Left;
            this.leftViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // rightViewToolStripMenuItem
            // 
            this.rightViewToolStripMenuItem.Name = "rightViewToolStripMenuItem";
            this.rightViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.rightViewToolStripMenuItem.Text = "Right View";
            this.rightViewToolStripMenuItem.Tag = (int)SideView.Right;
            this.rightViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // frontViewToolStripMenuItem
            // 
            this.frontViewToolStripMenuItem.Name = "frontViewToolStripMenuItem";
            this.frontViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.frontViewToolStripMenuItem.Text = "Front View";
            this.frontViewToolStripMenuItem.Tag = (int)SideView.Front;
            this.frontViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);
            // 
            // backViewToolStripMenuItem
            // 
            this.backViewToolStripMenuItem.Name = "backViewToolStripMenuItem";
            this.backViewToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.backViewToolStripMenuItem.Text = "Back View";
            this.backViewToolStripMenuItem.Tag = (int)SideView.Back;
            this.backViewToolStripMenuItem.Click += new System.EventHandler(this.ViewToolStripMenuItem_Click);

            CameraTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            CameraTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topViewToolStripMenuItem,
            this.bottomViewToolStripMenuItem,
            this.leftViewToolStripMenuItem,
            this.rightViewToolStripMenuItem,
            this.frontViewToolStripMenuItem,
            this.backViewToolStripMenuItem});
            //CameraTools.Image = ((System.Drawing.Image)(resources.GetObject("CameraViewSide.Image")));
            CameraTools.ImageTransparentColor = System.Drawing.Color.Magenta;
            CameraTools.Name = "CameraViewSide";
            CameraTools.Size = new System.Drawing.Size(32, 22);
            CameraTools.Text = "toolStripSplitButton1";

            CameraToolStrip.Items.Add(CameraTools);
        }        
  
        public void Dispose()
        {
            DeleteButton();

            Host.GlobalMouseListener.MouseMove -= new ContiniusMouseHandler(MouseMove);
            Host.GlobalMouseListener.MouseWheel -= new ContiniusMouseHandler(MouseWheel);

            mViewport.Dispose();
            mViewport = null;

            mSceneManager.DestroyCamera(mCamera);
            mCamera = null;
        }

        private void DeleteButton()
        {
            CameraTools.Dispose();
        }

        public void setViewType(int polly)
        {
            mCamera.PolygonMode = (PolygonMode)polly;
        }

        public void setSideView(int side)
        {
            if (_cameraType == CameraTypes.Attached)
            {
                switch (side)
                {
                    case (int)SideView.Top:
                        break;
                    case (int)SideView.Bottom:
                        break;
                    case (int)SideView.Left:
                        break;
                    case (int)SideView.Right:
                        break;
                    case (int)SideView.Front:
                        break;
                    case (int)SideView.Back:
                        break;
                }
            }
            else
            {
                switch (side)
                {
                    case (int)SideView.Top:
                        mCamera.Position = Vector3.UNIT_Y * Properties.Settings.Default.DefaultDistance;
                        break;
                    case (int)SideView.Bottom:
                        mCamera.Position = Vector3.NEGATIVE_UNIT_Y * Properties.Settings.Default.DefaultDistance;
                        break;
                    case (int)SideView.Left:
                        mCamera.Position = Vector3.NEGATIVE_UNIT_Z * Properties.Settings.Default.DefaultDistance;
                        break;
                    case (int)SideView.Right:
                        mCamera.Position = Vector3.UNIT_Z * Properties.Settings.Default.DefaultDistance;
                        break;
                    case (int)SideView.Front:
                        mCamera.Position = Vector3.UNIT_X * Properties.Settings.Default.DefaultDistance;
                        break;
                    case (int)SideView.Back:
                        mCamera.Position = Vector3.NEGATIVE_UNIT_X * Properties.Settings.Default.DefaultDistance;
                        break;
                }
                mCamera.LookAt(Vector3.ZERO);
            }
            Host.AskRedrow(this);
        }

        private void ViewToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            setSideView((int)(sender as ToolStripItem).Tag);
        }

        public void setKeyboardEvent(ref KeyEventHandler Event)
        {
            Event += new KeyEventHandler(KeyPress);
        }

        public Ray CameraRay(float x, float y)
        {
            return mCamera.GetCameraToViewportRay(x / (float)mViewport.ActualWidth, y / (float)mViewport.ActualHeight);
        }

        public RaySceneQueryResult CastMouseRay(ContiniusMouseEventArgs args)
        {
            rQuery.Ray = CameraRay(args.X, args.Y);
            return rQuery.Execute();
        }

        public object ContactPoint(ContiniusMouseEventArgs args)
        {
            RaySceneQueryResult res;

            Ray ray = CameraRay(args.X, args.Y);
            if (!(res = CastMouseRay(args)).IsEmpty)
            {
                Pair<bool, float> pres = ray.Intersects(res[0].movable.BoundingBox);
                if (pres.first)
                    return ray.GetPoint(pres.second);
            }
            else
            {
                Pair<bool, float> pres = ray.Intersects(Host.ActiveScene.FloorPlane);
                if (pres.first)
                    return ray.GetPoint(pres.second);  
            }
            return null;
        }

        private bool MouseWheel(object sender, ContiniusMouseEventArgs args)
        {
            if (sender as IRenderObjectPlugin != Parent)
                return true;

            mCamera.Move(mCamera.Direction * args.Delta * Properties.Settings.Default.ZoomFactor);
            Host.AskRedrow(this);
            return true;
        }

        private bool MouseMove(object sender, ContiniusMouseEventArgs args)
        {
            if (sender as IRenderObjectPlugin != Parent)
                return true;

            if (args.isDrag == true)
                if (CameraType == CameraTypes.Attached)
                {
                    if (args.Button == MouseButtons.Right)
                    {
                        float cosL = Mogre.Math.Cos(args.RelDeltaX); float sinL = Mogre.Math.Sin(args.RelDeltaX);
                        float cosB = Mogre.Math.Cos(args.RelDeltaY); float sinB = Mogre.Math.Sin(args.RelDeltaY);

                        Mogre.Matrix3 rot = new Mogre.Matrix3(
                            cosL * cosB, -sinL, cosL * sinB,
                            sinL * cosB, cosL , sinL * sinB,
                            -sinB      , 0    , cosB);

                        Vector3 pos = mCamera.Position = TrackingNode.Position;
                        pos = rot * pos;

                        mCamera.Position = TrackingNode.Position + pos;
                   
                        Host.AskRedrow(this);
                    }
                    else if (args.Button == MouseButtons.Middle)
                    {
                        mCamera.Move(new Mogre.Vector3(Properties.Settings.Default.XFactor * args.RelDeltaX, Properties.Settings.Default.YFactor * args.RelDeltaY, 0));
                        mCamera.LookAt(TrackingNode.Position);
                        Host.AskRedrow(this);
                    }
                }
                else            
                    if (args.Button == MouseButtons.Right)
                    {
                        mCamera.Rotate(Mogre.Vector3.UNIT_Y, new Mogre.Degree(Properties.Settings.Default.XFactor * args.RelDeltaX));
                        mCamera.Pitch(new Mogre.Degree(Properties.Settings.Default.YFactor * args.RelDeltaY));
                        Host.AskRedrow(this);
                    }
                    else if (args.Button == MouseButtons.Middle)
                    {
                        mCamera.Move(new Mogre.Vector3(Properties.Settings.Default.XFactor * args.RelDeltaX, Properties.Settings.Default.YFactor * args.RelDeltaY, 0));
                        Host.AskRedrow(this);
                    }
            return true;
        }

        private void KeyPress(object sender, KeyEventArgs key)
        {
            if (sender as IRenderObjectPlugin != Parent)
                return;

            if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up)
                mCamera.Move(mCamera.Direction);
            else if (key.KeyCode == Keys.S || key.KeyCode == Keys.Down)
                mCamera.Move(-mCamera.Direction);
            else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
                mCamera.Move(-mCamera.DerivedRight);
            else if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right)
                mCamera.Move(mCamera.DerivedRight);
            else if (key.KeyCode == Keys.Space)
                mCamera.Move(mCamera.DerivedUp);
            else if (key.KeyCode == Keys.Q)
                mCamera.PolygonMode = PolygonMode.PM_WIREFRAME;
            else
                return;

            if (CameraType == CameraTypes.Attached)
                mCamera.LookAt(TrackingNode.Position);

            Host.AskRedrow(this);
        }
    }
}
