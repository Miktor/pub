using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Configer
{
    public partial class Main : Form
    {
        SlideComponents MainSlidePanel;
        SlideComponents ParsecSlidePanel;
        string XMLfName =  string.Empty;
        string INIfName =  string.Empty;
        bool[] ConfExist = new bool[4];
        FileStrings fString;
        public Main()
        {
            fString = new FileStrings();
            fString.ConfNames = new string[4] { "parsec.ini", "MDO.Parsec.Win.exe.config", "ParsecReplicatonClient.exe.config", "ParsecServiceHost.exe.config" };
            string programData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            fString.ConfPathes = new string[4] { string.Empty, string.Empty, string.Empty, string.Empty };
            if (Properties.Settings.Default.SettingConfPathes.Count != 4)
            {                
                Properties.Settings.Default.SettingConfPathes.Clear();
                Properties.Settings.Default.SettingConfPathes.AddRange(fString.ConfPathes);
            }
            //Properties.Settings.Default.SettingConfPathes.CopyTo(fString.ConfNames, 0);
            ValidationChekFiles();
            InitializeComponent();
        }      

       
        private void Main_Exit(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < fString.ConfPathes.Length; i++)
            {
                Properties.Settings.Default.SettingConfNames[i] = fString.ConfNames[i];
                Properties.Settings.Default.SettingConfPathes[i] = fString.ConfPathes[i];
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
            }
        }

        private void OpenSettings(object sender, EventArgs e)
        {
            SettingForm SF = new SettingForm(fString);
            SF.ShowDialog();
            SF.Dispose();
            ValidationChekFiles();
        }               

        private void Main_Load(object sender, EventArgs e)
        {
            MainSlidePanel = new SlideComponents(this.MainPanel.Controls, new Control[2] { LabelServiceHost, LabelParsec });
            ParsecSlidePanel = new SlideComponents(PanelParsec.Controls, new Control[2] { LabelTransport, LabelParsecServer });
            this.CalcTimer.Tick += new System.EventHandler(MainSlidePanel.TimerTick);
            this.CalcTimer.Tick += new System.EventHandler(ParsecSlidePanel.TimerTick);
            CalcTimer.Enabled = true;
        }
    }
}
