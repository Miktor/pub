using System;
using System.Windows.Forms;
using Mogre;

namespace MyWorldEditor.UI
{
    public enum CameraTypes
    {
        Free, Attached
    }

    class Camera
    {
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
        public int ID { get; private set; }

        public SceneManager mSceneManager { get; set; }

        private SceneNode CamNode;
        public SceneNode TrackingNode { get; set; }

        private Mogre.Camera mCamera;
        private Viewport mViewport;

        public Camera(SceneManager _sMngr, RenderWindow window)
        {
            mSceneManager = _sMngr;

            ID = ObjectCounter.Instance.Cameras;
            _cameraType = CameraTypes.Free;
            CamNode = OgreRoot.Instance.mSceneMgr.RootSceneNode.CreateChildSceneNode("cameraNode_" + ID.ToString());

            mCamera = mSceneManager.CreateCamera("Camera_" + ID.ToString());
            mCamera.Position = Vector3.UNIT_SCALE * 5;
            mCamera.LookAt(Mogre.Vector3.ZERO);
            mCamera.NearClipDistance = 1;

            mViewport = window.AddViewport(mCamera);
            mViewport.BackgroundColour = new ColourValue(0.1f, 0.1f, 0.1f, 1.0f);
        }

        public void setMouseListener(ref MouseListener listener)
        {
            listener.MouseEvent += new ContiniusMouseHandler(MouseMoved);
        }

        public void setKeyboardEvent(ref KeyEventHandler Event)
        {
            Event += new KeyEventHandler(KeyPress);
        }

        public Ray getRayFromCam(float x, float y)
        {
            return mCamera.GetCameraToViewportRay(x / (float)mViewport.ActualWidth, y / (float)mViewport.ActualHeight);
        }

        private void MouseMoved(object sender, ContiniusMouseEventArgs args)
        {
            if (args.Delta !=0)
            {
                mCamera.Move(mCamera.Direction * args.Delta * Properties.Settings.Default.CameraZoomFactor);
                RedrowEWent.Instance.AskRedrow();
            }
            if(args.isDrag)
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
                   
                    RedrowEWent.Instance.AskRedrow();
                }
                else if (args.Button == MouseButtons.Middle)
                {
                    mCamera.Move(new Mogre.Vector3(Properties.Settings.Default.CameraXFactor * args.RelDeltaX, Properties.Settings.Default.CameraYFactor * args.RelDeltaY, 0));
                    mCamera.LookAt(TrackingNode.Position);
                    RedrowEWent.Instance.AskRedrow();
                }
            }
            else            
                if (args.Button == MouseButtons.Right)
                {
                    mCamera.Rotate(Mogre.Vector3.UNIT_Y, new Mogre.Degree(Properties.Settings.Default.CameraXFactor * args.RelDeltaX));
                    mCamera.Pitch(new Mogre.Degree(Properties.Settings.Default.CameraYFactor * args.RelDeltaY));
                    RedrowEWent.Instance.AskRedrow();
                }
                else if (args.Button == MouseButtons.Middle)
                {
                    mCamera.Move(new Mogre.Vector3(Properties.Settings.Default.CameraXFactor * args.RelDeltaX, Properties.Settings.Default.CameraYFactor * args.RelDeltaY, 0));
                    RedrowEWent.Instance.AskRedrow();
                }       
        }

        private void KeyPress(object sender, KeyEventArgs key)
        {
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

            RedrowEWent.Instance.AskRedrow();
        }
    }
}
