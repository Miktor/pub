using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WorldEditor
{
    public partial class MainForm : Form
    {
        object SelectedObject { get { return null; } set {  } }

        protected OgreRoot mogreWin;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MogreForm_Disposed(object sender, EventArgs e)
        {

        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            PluginHost.Instance.Initialize(MainPanel);
        }        
    }    
}
