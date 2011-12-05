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

namespace Configer
{
    public partial class Main
    {

        //=========================================
        //==== Validation Tab =====
        //=========================================
        private void FullValidActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ParsecExe] &&
                ConfExist[(int)ConfFileIndex.ReplicatonClient] && ConfExist[(int)ConfFileIndex.ServiceHost])
                FullValid();
            else
            {
                if (MessageBox.Show("Извините,некоторые файлы не могут быть открыты,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingForm SF = new SettingForm(fString);
                    SF.ShowDialog();
                }
            }
        }
        //=========================================
        private void HostActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ParsecExe] &&
                ConfExist[(int)ConfFileIndex.ReplicatonClient] && ConfExist[(int)ConfFileIndex.ServiceHost])
            {
                HostErrorOutput.Clear();
                //HostOutput.Rows.Clear();
                HostValidation();
            }
            else
            {
                if (MessageBox.Show("Извините,файла(ов) не существует,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingForm SF = new SettingForm(fString);
                    SF.ShowDialog();
                }
            }
        }
        private void SaveHostState_Click(object sender, EventArgs e)
        {
            //XmlNode oldCd;
            //XmlDocument XMLWriter = new XmlDocument();
            //INIfName = fString.ConfPathes[(int)ConfFileIndex.parsec] + fString.ConfNames[(int)ConfFileIndex.parsec];
            //for (int i = 0; i < 2; i++)
            //    SetValue("Transport", HostOutput.Rows[i].Cells[1].Value.ToString(), HostOutput.Rows[i].Cells[2].Value.ToString());
            //for (int i = 2; i < 10; i += 2)
            //    SetValue("ParsecServer", HostOutput.Rows[i].Cells[1].Value.ToString(), HostOutput.Rows[i].Cells[2].Value.ToString());
            //for (int i = 3; i < 10; i += 2)
            //{
            //    XMLWriter.Load(HostOutput.Rows[i].Cells[0].Value.ToString());
            //    oldCd = XMLWriter.SelectSingleNode(".//channel");

            //    XmlElement newCd = XMLWriter.CreateElement(oldCd.Name);

            //    for (int j = 0; j < oldCd.Attributes.Count; j++)
            //    {
            //        newCd.SetAttribute(oldCd.Attributes[j].Name, oldCd.Attributes[j].Value);
            //    }
            //    newCd.SetAttribute("port", HostOutput.Rows[i].Cells[2].Value.ToString());
            //    oldCd.ParentNode.ReplaceChild(newCd, oldCd);

            //    XMLWriter.Save(HostOutput.Rows[i].Cells[0].Value.ToString());
            //}


        }
        private void lvReDrow(object sendler, InvalidateEventArgs args)
        {
            //int height;
            //foreach (Control c in (sendler as Control).Parent)
            //    if (!c.Name.StartsWith("List"))
            //        height = c.Location.Y + c.Height + 3;
            //    else
            //        height = c.Location.Y + c.Height + 3;
        }
        //=========================================
        private void IconsActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ParsecExe] &&
                ConfExist[(int)ConfFileIndex.ReplicatonClient] && ConfExist[(int)ConfFileIndex.ServiceHost])
            {               
                IconValidation();
            }
            else
            {
                if (MessageBox.Show("Извините,некоторые файлы не найдены,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingForm SF = new SettingForm(fString);
                    SF.ShowDialog();
                }
            }
        }
        //=========================================
        private void AssemblyActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ParsecExe] &&
               ConfExist[(int)ConfFileIndex.ReplicatonClient] && ConfExist[(int)ConfFileIndex.ServiceHost])
            {                
                AssemblyValidation();
            }
            else
            {
                if (MessageBox.Show("Извините,некоторые файлы не найдены,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SettingForm SF = new SettingForm(fString);
                    SF.ShowDialog();
                }
            }
        }
        //=========================================         
        private bool String2Bool(string str)
        {
            if (str == "true")
                return true;
            else
                return false;
        }
        private string Bool2String(bool value)
        {
            if (value)
                return "true";
            else
                return "false";
        }
        //=========================================
        //==== Files =====
        //========================================
        private void ValidationChekFiles()
        {
            for (int i = 0; i < fString.ConfPathes.Length; i++)
            {
                if (File.Exists(fString.ConfPathes[i] + fString.ConfNames[i]))
                {
                    ConfExist[i] = true;
                }
                else
                {
                    ConfExist[i] = false;
                }
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
                fString.ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = tmpfname;
                fString.ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = path;
            }

            PictureBox pic = this.Controls["PictureBox_" + but.Name.Substring(but.Name.LastIndexOf('_') + 1)] as PictureBox;

            if (File.Exists(fString.ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] + fString.ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))]))
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
    }

}