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
    public enum ConfFileIndex { parsec = 0, ParsecExe = 1, ReplicatonClient = 2, ServiceHost = 3 };
    public struct FileStrings
    {
        public string[] ConfPathes;
        public string[] ConfNames;
    }
    public partial class SettingForm : Form
    {
        bool[] ConfExist = new bool[4];
        FileStrings fString = new FileStrings();
        public SettingForm(FileStrings _fstring)
        {
            fString = _fstring;
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            openFolder.ShowNewFolderButton = false;
            CreateFilesCaptions();
            //LoadAdditionOptions();
        }
        private void LoadAdditionOptions()
        {
            propertyGrid1.SelectedObject = Properties.Settings.Default;
        }               
       
        private void CreateFilesCaptions()
        {
            Font myfont;
            SizeF sizeString;
            TextBox text;
            PictureBox pic;
            Button but;
            for (int i = 0; i < fString.ConfPathes.Length; i++)
            {
                pic = new PictureBox();
                pic.Anchor = System.Windows.Forms.AnchorStyles.Top;
                pic.Name = "PictureBox_" + i.ToString();
                pic.Size = new System.Drawing.Size(25, 23);
                pic.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                text = new TextBox();
                text.Anchor = System.Windows.Forms.AnchorStyles.Top;
                text.Name = "TextBox_" + i.ToString();
                text.Text = fString.ConfNames[i];
                text.Multiline = false;
                text.ReadOnly = true;
                myfont = text.Font;
                sizeString = new SizeF(150, 23);
                text.Size = sizeString.ToSize();
                text.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                but = new Button();
                but.Anchor = System.Windows.Forms.AnchorStyles.Top;
                but.Name = "Button_" + i.ToString();
                but.Size = new System.Drawing.Size(25, 23);
                but.Text = "...";
                but.UseVisualStyleBackColor = true;
                but.Click += new System.EventHandler(SetFilePath);
                but.Anchor = AnchorStyles.Top | AnchorStyles.Right;

                text.Location = new System.Drawing.Point(7, 12 + i * 28);
                pic.Location = new System.Drawing.Point(202, 7 + i * 28);
                but.Location = new System.Drawing.Point(233, 7 + i * 28);

                if (File.Exists(fString.ConfPathes[i] + fString.ConfNames[i]))
                {

                    pic.Image = Configer.Properties.Resources.agree1;
                    ConfExist[i] = true;
                }
                else
                {
                    pic.Image = Configer.Properties.Resources.disagree;
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

            if (but.Name.Substring(but.Name.LastIndexOf('_') + 1) != "0")
                openFolder.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            else
                openFolder.SelectedPath = Environment.CurrentDirectory;

            bool endTrying = false;
            while (!endTrying) {
                if (openFolder.ShowDialog() == DialogResult.OK)
                {
                    path = openFolder.SelectedPath + "\\";
                    PictureBox pic = this.MainOptionTab.Controls["PictureBox_" + but.Name.Substring(but.Name.LastIndexOf('_') + 1)] as PictureBox;
                    if (File.Exists(path + fString.ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))]))
                    {
                        pic.Image = Configer.Properties.Resources.agree1;
                        fString.ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = path;
                        ConfExist[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = true;
                        endTrying = true;
                    } else 
                    {
                        pic.Image = Configer.Properties.Resources.disagree;
                        ConfExist[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = false;
                        if (MessageBox.Show("В этой папке нету файла " + fString.ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] + Environment.NewLine +
                            "Попробовать еще раз?", "Поиск " + fString.ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))], MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                            endTrying = true;
                    }
                } else
                    endTrying = true;
            }
        }

        private void Save_Options_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < fString.ConfPathes.Length; i++)
            {
                Properties.Settings.Default.SettingConfPathes[i] = fString.ConfPathes[i]; 
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
