using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Management.Instrumentation;
using Mogre;

namespace MyWorldEditor
{
    public partial class MainForm : Form
    {
        object SelectedObject { get { return null; } set {  } }

        protected OgreRoot mogreWin;

        List<UI.MogrePanel> mRenderingObjects = new List<UI.MogrePanel>();

        public MainForm()
        {
            InitializeComponent();
            mRenderingObjects.Add(MainWindow);
        }

        private void MogreForm_Disposed(object sender, EventArgs e)
        {
            mogreWin.Dispose();
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                OgreRoot.Instance.InitMogre();
                this.Paint += new System.Windows.Forms.PaintEventHandler(OgreRoot.Instance.Paint);
                UI.Grid.Instance.Init();
                MainWindow.init(this, OgreRoot.Instance.mSceneMgr);
   
                this.Refresh();
                OgreRoot.Instance.loadMaterials();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }        
    }    
}
