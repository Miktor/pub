using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using Configer.Properties;

namespace Configer
{
    public partial class Main : Form
    {
        enum ConfFileIndex { parsec = 0, cashe = 1, import = 2, MDOParsecWinexe = 3, MDOParsecWinNotifier = 4, ReplicationClient = 5, ReplicationHost = 6, TaskClient = 7, replication = 8, replicationClient = 9, trasaction = 10 };
        string XMLfName = "";
        string INIfName = "";
        string[] ConfPathes = { "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\" };
        string[] ConfNames = { "parsec.ini", "cache.config", "Import25Util.exe.config", "MDO.Parsec.Win.exe.config", "MDO.Parsec.WinNotifier.exe.config", "ParsecReplicatonClient.exe.config", "ParsecServiceHost.exe.config", "ParsecTaskClient.exe.config", "replication.config", "replication_client.config", "transaction.config" };
        bool[] ConfExist = new bool[11];
        public Main()
        {            
            Properties.Settings.Default.SettingConfNames.CopyTo(ConfNames, 0);
            Properties.Settings.Default.SettingConfPathes.CopyTo(ConfPathes, 0);
            ValidationChekFiles();
            InitializeComponent();
        }

       

       
        private void Main_Exit(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < ConfPathes.Length; i++)
            {
                Properties.Settings.Default.SettingConfNames[i] = ConfNames[i];
                Properties.Settings.Default.SettingConfPathes[i] = ConfPathes[i];
            }
            Properties.Settings.Default.Save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (tabControl2.SelectedTab.TabIndex)
            {
                case 0:
                    FullValidActivate(null, null);
                    break;
                case 1:
                    HostActivate(null, null);
                    break;
                case 2:
                    IconsActivate(null, null);
                    break;
                case 3:
                    AssemblyActivate(null, null);
                    break;
                case 4:
                    ReplicActiv(null, null);
                    break;
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingForm SF = new SettingForm();
            SF.ShowDialog();
            ValidationChekFiles();
        }
    }
}
