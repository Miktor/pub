using System.Drawing;
using System.Windows.Forms;
using Mogre;
using PluginBase;

namespace RenderWindow
{

    public partial class MogrePanel : UserControl, IRenderObjectPlugin
    {
        #region Events          
        public event KeyEventHandler KeyboardEvent;

        public IMouseListener mMouseListener { get; set; }

        public virtual void KeyboardChanged(KeyEventArgs e)
        {
            if (KeyboardEvent != null)
                KeyboardEvent(this, e);
        }
        #endregion

        public SceneManager mSceneManager;

        public Mogre.RenderWindow mWindow { get; private set; }
        public ICameraPlugin Camera { get; set; }
        public bool isMouseOver { get; private set; }
        public IPluginHost Host { get; set; }
        public IScene Scene { get; private set; }

        public string Name { get { return "MogrePanel"; } }
        public string Description { get { return "MogrePanel"; } }
        public string Author { get { return "Towelie"; } }
        public string Version { get { return "1.0.0.0"; } }

        public UserControl MainInterface { get { return this; } }
        public ToolStrip MainToolStrip { get { return null; } }
        public ToolStrip SubToolStrip { get { return null; } }

        public event System.Windows.Forms.MouseEventHandler MouseDown { add { ViewPortArea.MouseDown += value; } remove { ViewPortArea.MouseDown -= value; } }
        public event System.Windows.Forms.MouseEventHandler MouseUp { add { ViewPortArea.MouseUp += value; } remove { ViewPortArea.MouseUp -= value; } }
        public event System.Windows.Forms.MouseEventHandler MouseMove { add { ViewPortArea.MouseMove += value; } remove { ViewPortArea.MouseMove -= value; } }
        public event System.Windows.Forms.MouseEventHandler MouseClick { add { ViewPortArea.MouseClick += value; } remove { ViewPortArea.MouseClick -= value; } }

        public MogrePanel()
        {
            InitializeComponent();            
        }

        public void Initialize()
        {
            mSceneManager = Host.ActiveScene.Manager;
            Scene = Host.ActiveScene;
            mMouseListener = Host.GlobalMouseListener;

            try
            {
                Host.MainControl.Controls.Add(MainInterface);
                Host.AddPlugin(@"Plugins\CameraPlugin.dll");
                createWindow();
                this.Dock = DockStyle.Fill;

                Host.GlobalMouseListener.AddObject(this);
                this.Resize += new System.EventHandler(Contrl_Resize); 

                Camera = Host.GetCustomPlugin("Camera") as ICameraPlugin;
                Camera.SetParent(this);
                Host.GlobalMouseListener.MouseMove += new ContiniusMouseHandler(MouseMove_Ewent);
            }
            catch (System.Exception ex)
            {
                Host.CatchException(ex, this);
            }
        }

        private void createWindow()
        {
            NameValuePairList misc = new NameValuePairList();
            misc["externalWindowHandle"] = this.ViewPortArea.Handle.ToString();
            mWindow = Host.ORoot.CreateRenderWindow(this.Name, (uint)this.ViewPortArea.Height, (uint)this.ViewPortArea.Width, false, misc);
        }  

        private Ray getRayFromCam(MouseEventArgs args)
        {
            return Camera.CameraRay((float)args.X, (float)args.Y);
        }

        #region EventHandlers         

        public void AddToTopStrip(ToolStrip item)
        {
            SideToolStrips.TopToolStripPanel.Controls.Add(item);
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

        private void PolygonModeMenuItem_Click(object sender, System.EventArgs e)
        {
            Camera.setViewType((int)(sender as ToolStripItem).Tag);
        }

        private bool MouseMove_Ewent(object sender, ContiniusMouseEventArgs e)
        {
            MouseCoordsLable.Text = e.X + " : " + e.Y;
            return true;
        }
       
    }
}
