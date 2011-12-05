using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace Configer
{
    public partial class SettingForm : Form
    {
        enum ConfFileIndex { parsec = 0, cashe = 1, import = 2, MDOParsecWinexe = 3, MDOParsecWinNotifier = 4, ReplicationClient = 5, ReplicationHost = 6, TaskClient = 7, replication = 8, replicationClient = 9, trasaction = 10 };
        string[] ConfPathes = { "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\", "..\\..\\todo\\" };
        string[] ConfNames = { "parsec.ini", "cache.config", "Import25Util.exe.config", "MDO.Parsec.Win.exe.config", "MDO.Parsec.WinNotifier.exe.config", "ParsecReplicatonClient.exe.config", "ParsecServiceHost.exe.config", "ParsecTaskClient.exe.config", "replication.config", "replication_client.config", "transaction.config" };
        bool[] ConfExist = new bool[11];

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {

            LoadOptions();
            LoadAdditionOptions();
        }
        private void LoadAdditionOptions()
        {
            propertyGrid1.SelectedObject = Properties.Settings.Default;
        }
        private void LoadOptions()
        {
            Properties.Settings.Default.SettingConfNames.CopyTo(ConfNames, 0);
            Properties.Settings.Default.SettingConfPathes.CopyTo(ConfPathes, 0);
            CreateFilesCaptions();
        }
        private void CreateFilesCaptions()
        {
            Font myfont;
            SizeF sizeString;
            TextBox text;
            PictureBox pic;
            Button but;
            for (int i = 0; i < ConfPathes.Length; i++)
            {
                pic = new PictureBox();
                pic.Anchor = System.Windows.Forms.AnchorStyles.Top;
                pic.Name = "PictureBox_" + i.ToString();
                pic.Size = new System.Drawing.Size(25, 23);

                text = new TextBox();
                text.Anchor = System.Windows.Forms.AnchorStyles.Top;
                text.Name = "TextBox_" + i.ToString();
                text.Text = ConfNames[i];
                text.Multiline = false;
                text.ReadOnly = true;
                myfont = text.Font;
                sizeString = new SizeF(150, 23);
                text.Size = sizeString.ToSize();
                //text.Select(0, 0);

                but = new Button();
                but.Anchor = System.Windows.Forms.AnchorStyles.Top;
                but.Name = "Button_" + i.ToString();
                but.Size = new System.Drawing.Size(25, 23);
                but.Text = "...";
                but.UseVisualStyleBackColor = true;
                but.Click += new System.EventHandler(SetFilePath);

                if (i < ConfPathes.Length / 2)
                {
                    text.Location = new System.Drawing.Point(7, 12 + i * 28);
                    pic.Location = new System.Drawing.Point(202, 7 + i * 28);
                    but.Location = new System.Drawing.Point(233, 7 + i * 28);
                }
                else
                {
                    text.Location = new System.Drawing.Point(263, 12 + (i - ConfPathes.Length / 2) * 28);
                    pic.Location = new System.Drawing.Point(457, 7 + (i - ConfPathes.Length / 2) * 28);
                    but.Location = new System.Drawing.Point(488, 7 + (i - ConfPathes.Length / 2) * 28);
                }

                if (File.Exists(ConfPathes[i] + ConfNames[i]))
                {
                    pic.Image = new Bitmap("agree.gif");
                    ConfExist[i] = true;
                }
                else
                {
                    pic.Image = new Bitmap("disagree.gif");
                    ConfExist[i] = false;
                }
                this.MainOptionTab.Controls.Add(pic);
                this.MainOptionTab.Controls.Add(text);
                this.MainOptionTab.Controls.Add(but);
            }
        }
        private void SetFilePath(object sender, EventArgs tmp)
        {
            Button but = sender as Button;
            string path = "";
            string tmpfname = "";

            openFile.ShowDialog();
            if (openFile.ValidateNames)
            {
                path = openFile.FileName;
                tmpfname = path.Substring(path.LastIndexOf('\\') + 1);
                path = path.Substring(0, path.LastIndexOf('\\') + 1);
                ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = tmpfname;
                ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = path;
            }

            PictureBox pic = this.MainOptionTab.Controls["PictureBox_" + but.Name.Substring(but.Name.LastIndexOf('_') + 1)] as PictureBox;

            if (File.Exists(ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] + ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))]))
            {
                pic.ImageLocation = "agree.gif";
                ConfExist[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = true;
            }
            else
            {
                pic.ImageLocation = "disagree.gif";
                ConfExist[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = false;
            }
        }

        private void Save_Options_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ConfPathes.Length; i++)
            {
                Properties.Settings.Default.SettingConfNames[i] = ConfNames[i];
                Properties.Settings.Default.SettingConfPathes[i] = ConfPathes[i];
            }
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void AdditionalOptionTabActivate(object sender, EventArgs e)
        {
            if(MessageBox.Show("Внимание" + System.Environment.NewLine + "Изменение этих параметров может превести к неправильной работе праграммы","Внимание",MessageBoxButtons.YesNo)!=DialogResult.Yes)
            {
                Optiontabs.SelectedIndex = 0;
            }
        }

        private void Cancel_Otions_Click(object sender, EventArgs e){ this.Close(); }
    }
}
