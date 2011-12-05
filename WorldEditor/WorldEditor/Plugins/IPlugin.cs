using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldEditor.Plugins
{
    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        System.Windows.Forms.UserControl MainInterface { get; }

        void Initialize();
        void Dispose();
    }

    public interface IPluginHost
    {
        
    }   
}
