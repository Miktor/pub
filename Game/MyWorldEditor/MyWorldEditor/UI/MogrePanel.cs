using System.Drawing;
using System.Windows.Forms;
using Mogre;

namespace MyWorldEditor.UI
{

    public partial class MogrePanel : UserControl
    {
        #region Events          
        public event KeyEventHandler KeyboardEvent;
        public virtual void KeyboardChanged(KeyEventArgs e)
        {
            if (KeyboardEvent != null)
                KeyboardEvent(this, e);
        }

        private MouseListener mMouseListener;
        #endregion

        public SceneManager mSceneManager;

        public RenderWindow mWindow;

        private Camera mCamera { get; set; }

        private Point lastMouseCoord { get; set; }

        public bool isMouseOver { get; private set; }

        public int ID { get; private set; }

        public MogrePanel()
        {
            InitializeComponent();
            ID = ObjectCounter.Instance.MogrePanels;
        }

        public void init(Form form, SceneManager _manager)
        {
            mSceneManager = _manager;
            try
            {
                setEventHandlers(form);
                createWindow();

                mCamera = new Camera(mSceneManager, mWindow);
                mCamera.setMouseListener(ref mMouseListener);
                mCamera.setKeyboardEvent(ref KeyboardEvent);

                RedrowEWent.Instance.NeedRedrow += new RedrowEWent.RedrowEwentHandler(OgreRoot.Instance.Paint);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void createWindow()
        {
            NameValuePairList misc = new NameValuePairList();
            misc["externalWindowHandle"] = this.panel1.Handle.ToString();
            mWindow = OgreRoot.Instance.root.CreateRenderWindow(this.Name, (uint)this.panel1.Height, (uint)this.panel1.Width, false, misc);
        }  

        private Ray getRayFromCam(MouseEventArgs args)
        {
            return mCamera.getRayFromCam((float)args.X, (float)args.Y);
        }

        #region EventHandlers

        public void setEventHandlers(Form form)
        {
            mMouseListener = new MouseListener(panel1.Height, panel1.Width);

            this.panel1.MouseClick += new MouseEventHandler(mMouseListener.Listener_Update);
            this.panel1.MouseMove += new MouseEventHandler(mMouseListener.Listener_Update);
            form.MouseWheel += new MouseEventHandler(mMouseListener.Listener_Update);

            this.MouseEnter += new System.EventHandler(mMouseListener.Listener_MouseEnter);
            this.MouseLeave += new System.EventHandler(mMouseListener.Listener_MouseLeave);

            this.panel1.Resize += new System.EventHandler(mMouseListener.updateSize);

            form.KeyDown += new KeyEventHandler(Contrl_KeyDown);

            this.panel1.Resize += new System.EventHandler(Contrl_Resize);            
        }       
       

        private void Contrl_KeyDown(object sender, KeyEventArgs key)
        {
            if (isMouseOver == false)
                return;

            KeyboardChanged(key);
        }

        private void Contrl_Resize(object sender, System.EventArgs args)
        {
            mWindow.Resize((uint)this.Width, (uint)this.Height);
        }

        #endregion
        
    }
}
