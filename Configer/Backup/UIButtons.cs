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
    public partial class Main : Form
    {
    
        //=========================================
        //==== Validation Tab =====
        //=========================================
        private void FullValidActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ReplicationHost] &&
                ConfExist[(int)ConfFileIndex.replication] && ConfExist[(int)ConfFileIndex.cashe] &&
                ConfExist[(int)ConfFileIndex.trasaction] && ConfExist[(int)ConfFileIndex.MDOParsecWinexe] &&
                ConfExist[(int)ConfFileIndex.ReplicationHost] && ConfExist[(int)ConfFileIndex.ReplicationClient])
                FullValid();
            else
            {
                if(MessageBox.Show("Извините,некоторые файлы не могут быть открыты,открыть окно настроек?.","Файлов нет!",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {            
                    SettingForm SF = new SettingForm();
                    SF.ShowDialog();
                }
            }
        }
        //=========================================
        private void HostActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ReplicationHost] &&
                ConfExist[(int)ConfFileIndex.replication] && ConfExist[(int)ConfFileIndex.cashe] &&
                ConfExist[(int)ConfFileIndex.trasaction])
            {
                HostErrorOutput.Clear();
                HostOutput.Rows.Clear();
                HostValidation();
            }
            else
            {
                if (MessageBox.Show("Извините,файл " + ConfNames[(int)ConfFileIndex.ReplicationHost] + " не существует,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {            
                    SettingForm SF = new SettingForm();
                    SF.ShowDialog();
                }
            }
        }        
        private void SaveHostState_Click(object sender, EventArgs e)
        {
            XmlNode oldCd;
            XmlDocument XMLWriter= new XmlDocument();
            INIfName = ConfPathes[(int)ConfFileIndex.parsec] + ConfNames[(int)ConfFileIndex.parsec];
            for (int i = 0; i < 2; i++)
                SetValue("Transport", HostOutput.Rows[i].Cells[1].Value.ToString(), HostOutput.Rows[i].Cells[2].Value.ToString());
            for (int i = 2; i < 10; i += 2)
                SetValue("ParsecServer", HostOutput.Rows[i].Cells[1].Value.ToString(), HostOutput.Rows[i].Cells[2].Value.ToString());
            for (int i = 3; i < 10; i += 2)
            {
                XMLWriter.Load(HostOutput.Rows[i].Cells[0].Value.ToString());
                oldCd=XMLWriter.SelectSingleNode(".//channel");

                XmlElement newCd = XMLWriter.CreateElement(oldCd.Name);

                for (int j = 0; j < oldCd.Attributes.Count; j++)
                {
                    newCd.SetAttribute(oldCd.Attributes[j].Name, oldCd.Attributes[j].Value);
                }
                newCd.SetAttribute("port", HostOutput.Rows[i].Cells[2].Value.ToString());
                oldCd.ParentNode.ReplaceChild(newCd, oldCd);

                XMLWriter.Save(HostOutput.Rows[i].Cells[0].Value.ToString());
            }


        }
        //=========================================
        private void IconsActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.MDOParsecWinexe])
            {
                IconsValid.Clear();
                IconValidation();
            }
            else
            {
                if(MessageBox.Show("Извините,файл "+ConfNames[(int)ConfFileIndex.MDOParsecWinexe]+" не существует,открыть окно настроек?.","Файлов нет!",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {            
                    SettingForm SF = new SettingForm();
                    SF.ShowDialog();
                }
            }
        }
        //=========================================
        private void AssemblyActivate(object sender, EventArgs e)
        {
            if (ConfExist[(int)ConfFileIndex.MDOParsecWinexe])
            {
                AssemblyValidOutput.Clear();
                AssemblyValidation();
            }
            else
            {
                if (MessageBox.Show("Извините,файл " + ConfNames[(int)ConfFileIndex.MDOParsecWinexe] + " не существует,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {            
                    SettingForm SF = new SettingForm();
                    SF.ShowDialog();
                }
            }
        }
        //=========================================         
        private void SaveReplicationState(object sender, EventArgs e)
        {
            List<string> AtrNames = new List<string>();
            List<string> AtrValue = new List<string>();
            CheckBox ReplicOption;
            
            XmlDocument XMLwriter = new XmlDocument();
            XMLwriter.Load(ConfPathes[(int)ConfFileIndex.ReplicationHost] + ConfNames[(int)ConfFileIndex.ReplicationHost]);

            XmlNode oldCd;
            XmlElement newElem;

            oldCd = XMLwriter.SelectSingleNode("configuration/extentions");

            for (int i = 0; i < oldCd.ChildNodes.Count; i++)
            {
                newElem = XMLwriter.CreateElement(oldCd.ChildNodes[i].Name);

                for (int j = 0; j < oldCd.ChildNodes[i].Attributes.Count; j++)
                    newElem.SetAttribute(oldCd.ChildNodes[i].Attributes[j].Name, oldCd.ChildNodes[i].Attributes[j].Value);

                if (newElem.Attributes["name"].Value != "task")
                    ReplicOption = ReplicaHost.Controls["S" + newElem.Attributes["name"].Value + "Box"] as CheckBox;
                else
                    ReplicOption = ScacheBox;
                newElem.Attributes["enable"].Value = Bool2String(ReplicOption.Checked);
                oldCd.ReplaceChild(newElem, oldCd.ChildNodes[i]);
            }
            XMLwriter.Save(ConfPathes[(int)ConfFileIndex.ReplicationHost] + ConfNames[(int)ConfFileIndex.ReplicationHost]);

            XMLwriter.Load(ConfPathes[(int)ConfFileIndex.ReplicationClient] + ConfNames[(int)ConfFileIndex.ReplicationClient]);
            oldCd = XMLwriter.SelectSingleNode("configuration/extentions");

            for (int i = 0; i < oldCd.ChildNodes.Count; i++)
            {
                newElem = XMLwriter.CreateElement(oldCd.ChildNodes[i].Name);

                for (int j = 0; j < oldCd.ChildNodes[i].Attributes.Count; j++)
                    newElem.SetAttribute(oldCd.ChildNodes[i].Attributes[j].Name, oldCd.ChildNodes[i].Attributes[j].Value);

                if (newElem.Attributes["name"].Value != "task")
                    ReplicOption = ReplicaClient.Controls["C" + newElem.Attributes["name"].Value + "Box"] as CheckBox;
                else
                    ReplicOption = CcacheBox;
                newElem.Attributes["enable"].Value = Bool2String(ReplicOption.Checked);
                oldCd.ReplaceChild(newElem, oldCd.ChildNodes[i]);
            }

            XMLwriter.Save(ConfPathes[(int)ConfFileIndex.ReplicationClient] + ConfNames[(int)ConfFileIndex.ReplicationClient]);
        }       
        private void ChangeReplicState(object sender, EventArgs e)
        {
            CheckBox chek = sender as CheckBox;
            CheckBox childrens;
            string Name;

            if (!NoLogic.Checked)
            {
                Name = chek.Name.Substring(1);
                if (chek.Name.StartsWith("S"))
                {
                    if (Name.StartsWith("replica"))
                    {
                        if (chek.Checked != (Creplica_uiBox.Checked || Creplica_halBox.Checked))
                        {
                            Creplica_uiBox.Checked = chek.Checked;
                            Creplica_halBox.Checked = chek.Checked;
                        }
                    }
                    else
                    {
                        childrens = ReplicaClient.Controls["C" + Name] as CheckBox;
                        childrens.Checked = chek.Checked;
                    }
                }
                else if (chek.Name.StartsWith("C"))
                {
                    if (Name.StartsWith("replica"))
                    {
                        SreplicationBox.Checked = Creplica_uiBox.Checked || Creplica_halBox.Checked;
                    }
                    else
                    {
                        childrens = ReplicaHost.Controls["S" + Name] as CheckBox;
                        childrens.Checked = chek.Checked;
                    }
                }

            }

        }
        private void ReplicActiv(object sender, EventArgs e)
        {
            if(ConfExist[(int)ConfFileIndex.ReplicationHost]&&ConfExist[(int)ConfFileIndex.ReplicationClient])
                ReplicValidation();
            else
            {
                if (MessageBox.Show("Извините,некоторые файлы не могут быть открыты,открыть окно настроек?.", "Файлов нет!", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {            
                    SettingForm SF = new SettingForm();
                    SF.ShowDialog();
                }
            }
        }

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
            for (int i = 0; i < ConfPathes.Length; i++)
            {
                if (File.Exists(ConfPathes[i] + ConfNames[i]))
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
            string tmpfname="";

            openFile.ShowDialog();
            if (openFile.ValidateNames)
            {
                path = openFile.FileName;
                tmpfname = path.Substring(path.LastIndexOf('\\')+1);
                path = path.Substring(0,path.LastIndexOf('\\')+1);
                ConfNames[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = tmpfname;
                ConfPathes[Convert.ToInt32(but.Name.Substring(but.Name.LastIndexOf('_') + 1))] = path;
            }

            PictureBox pic = this.Controls["PictureBox_" + but.Name.Substring(but.Name.LastIndexOf('_') + 1)] as PictureBox;

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
    }

}