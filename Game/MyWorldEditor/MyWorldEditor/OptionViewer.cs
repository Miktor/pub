using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MyWorldEditor
{
    class OptionViewer : Singleton<OptionViewer>
    {
        private OptionViewer() { }

        private PropertyGrid panel;
        public PropertyGrid Panel { get { return panel; } }

        public void init(PropertyGrid _panel) { panel = _panel; }
        public object Object { get { return panel.SelectedObject; } set { panel.SelectedObject = value; } }
    }
}
