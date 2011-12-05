using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluginBase;
using Mogre;
using System.Windows.Forms;

namespace ScenePlugin
{
    public class BaseScene : IScene
    {
        public IPluginHost Host { get; set; }

        public string Name { get { return "BaseScene"; } }
        public string Description { get { return "BaseScene"; } }
        public string Author { get { return "Towelie"; } }
        public string Version { get { return "1.0.0.0"; } }

        public UserControl MainInterface { get { return null; } }
        public ToolStrip MainToolStrip { get { return null; } }
        public ToolStrip SubToolStrip { get { return null; } }

        public SceneManager Manager { get; set; }
        public Plane FloorPlane { get; set; }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }            
    }
}
