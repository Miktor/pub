using Mogre;
using System;
using System.Windows.Forms;

namespace PluginBase
{
    public delegate bool ContiniusMouseHandler(IRenderObjectPlugin sender, ContiniusMouseEventArgs e);  

    public interface IPlugin      
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        System.Windows.Forms.UserControl MainInterface { get; }
        System.Windows.Forms.ToolStrip MainToolStrip { get; }
        System.Windows.Forms.ToolStrip SubToolStrip { get; }

        void Initialize();
        void Dispose();
    }

    public interface IRenderObjectPlugin : IPlugin
    {
        IScene Scene { get; }
        ICameraPlugin Camera { get; }

        event System.Windows.Forms.MouseEventHandler MouseDown;
        event System.Windows.Forms.MouseEventHandler MouseClick;
        event System.Windows.Forms.MouseEventHandler MouseUp;
        event System.Windows.Forms.MouseEventHandler MouseMove;
        event System.Windows.Forms.MouseEventHandler MouseWheel;
        event System.EventHandler Resize;

        Mogre.RenderWindow mWindow { get; }  

        void AddToTopStrip(System.Windows.Forms.ToolStrip item);      
    }

    public interface IControllPlugin : IPlugin
    {
       
    }

    public interface ICameraPlugin : IControllPlugin
    {
        IRenderObjectPlugin Parent { get; } 

        CameraTypes CameraType { get; set; }
        SceneNode TrackingNode { get; set; }

        void SetParent(IRenderObjectPlugin _parent);

        void setViewType(int polly);
        void setSideView(int side);

        Ray CameraRay(float x, float y);
        RaySceneQueryResult CastMouseRay(ContiniusMouseEventArgs args);
        object ContactPoint(ContiniusMouseEventArgs args);
    }

    public interface IMouseListener : IPlugin
    {
        event ContiniusMouseHandler MouseDown;
        event ContiniusMouseHandler MouseClick;
        event ContiniusMouseHandler MouseUp;
        event ContiniusMouseHandler MouseMove;
        event ContiniusMouseHandler MouseWheel;
        event ContiniusMouseHandler MouseDrag;

        void AddObject(IRenderObjectPlugin obj);

        void MouseClick_Ewent(object sender, System.Windows.Forms.MouseEventArgs evnt);
        void MouseDown_Ewent(object sender, System.Windows.Forms.MouseEventArgs evnt);
        void MouseMove_Ewent(object sender, System.Windows.Forms.MouseEventArgs evnt);
        void MouseUp_Ewent(object sender, System.Windows.Forms.MouseEventArgs evnt);
        void MouseWheel_Ewent(object sender, System.Windows.Forms.MouseEventArgs evnt);
        void updateSize(object sender, EventArgs e);
    }

    public interface IPluginHost
    {
        Root ORoot { get; }
        IScene ActiveScene { get; }
        ISimple SelectedObject { get; set; }

        IMouseListener GlobalMouseListener { get; set; }

        IPlugin GetCustomPlugin(string name);
        void AddPlugin(string FileName);

        void CatchException(System.Exception e, object sender);
        void AskRedrow(IPlugin sender);

        Control MainControl { get; }
    }

    public class IScene
    {
        public SceneManager Manager { get; set; }
        public Plane FloorPlane { get; set; }
    }
}
