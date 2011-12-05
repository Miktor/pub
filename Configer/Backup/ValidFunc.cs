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

namespace Configer
{
    public partial class Main : Form
    {
        bool FullValidWas = false;
        private void FullValid()
        {
            if (ConfExist[(int)ConfFileIndex.parsec] && ConfExist[(int)ConfFileIndex.ReplicationHost] &&
                ConfExist[(int)ConfFileIndex.replication] && ConfExist[(int)ConfFileIndex.cashe] &&
                ConfExist[(int)ConfFileIndex.trasaction] && ConfExist[(int)ConfFileIndex.MDOParsecWinexe] &&
                ConfExist[(int)ConfFileIndex.ReplicationHost] && ConfExist[(int)ConfFileIndex.ReplicationClient])
            {
                int i = 0;

                FullValidOutput.Text = "Результаты полной проверки.." + System.Environment.NewLine;

                i = HostValidation();
                if (i == 0)
                    FullValidOutput.Text += "При проверке хостов ошибок не обнаружено" + System.Environment.NewLine;
                else
                    FullValidOutput.Text += "При проверке хостов обнаружено " + i.ToString() + " ошибки(ок)" + System.Environment.NewLine;

                i = IconValidation();
                if (i == 0)
                    FullValidOutput.Text += "При проверке иконок ошибок не обнаружено" + System.Environment.NewLine;
                else
                    FullValidOutput.Text += "При проверке иконок обнаружено " + i.ToString() + " ошибки(ок)" + System.Environment.NewLine;

                i = AssemblyValidation();
                if (i == 0)
                    FullValidOutput.Text += "При проверке сборки ошибок не обнаружено" + System.Environment.NewLine;
                else
                    FullValidOutput.Text += "При проверке сборки обнаружено " + i.ToString() + " ошибки(ок)" + System.Environment.NewLine;

                FullValidOutput.Text += "Подробные отчеты смотрите в соответствующих вкладках";
                FullValidWas = true;
            }
        }
        private int HostValidation()
        {
            string[,] PortsValue = new string[6, 3];
            int count = 0;

            INIfName = ConfPathes[(int)ConfFileIndex.parsec] + ConfNames[(int)ConfFileIndex.parsec];

            PortsValue[0, 0] = "ServerPort";
            PortsValue[1, 0] = "LocalPort";
            PortsValue[2, 0] = "Host";
            PortsValue[3, 0] = "ReplicaHost";
            PortsValue[4, 0] = "CacheHost";
            PortsValue[5, 0] = "TransactionHost";

            PortsValue[0, 1] = GetValue("Transport", "ServerPort").ToString();
            PortsValue[1, 1] = GetValue("Transport", "LocalPort").ToString();
            PortsValue[2, 1] = GetValue("ParsecServer", "Host").ToString();
            PortsValue[3, 1] = GetValue("ParsecServer", "ReplicaHost").ToString();
            PortsValue[4, 1] = GetValue("ParsecServer", "CacheHost").ToString();
            PortsValue[5, 1] = GetValue("ParsecServer", "TransactionHost").ToString();
            for (int i = 0; i < 6; i++)
            {
                HostOutput.Rows.Add(INIfName, PortsValue[i, 0], PortsValue[i, 1]);
                if (i > 1)
                {
                    switch (i)
                    {
                        case 2:
                            XMLfName = ConfPathes[(int)ConfFileIndex.ReplicationHost] + ConfNames[(int)ConfFileIndex.ReplicationHost];
                            break;
                        case 3:
                            XMLfName = ConfPathes[(int)ConfFileIndex.replication] + ConfNames[(int)ConfFileIndex.replication];
                            break;
                        case 4:
                            XMLfName = ConfPathes[(int)ConfFileIndex.cashe] + ConfNames[(int)ConfFileIndex.cashe];
                            break;
                        case 5:
                            XMLfName = ConfPathes[(int)ConfFileIndex.trasaction] + ConfNames[(int)ConfFileIndex.trasaction];
                            break;
                    }
                    HostOutput.Rows.Add(XMLfName, PortsValue[i, 0], GetAtribute("channel", "port"));

                    if (GetAtribute("channel", "port") != PortsValue[i, 1].Substring(PortsValue[i, 1].Length - 6, 5))
                    {
                        HostErrorOutput.Text += "Несовпадение портов!" + System.Environment.NewLine +
                            INIfName + PortsValue[i, 0] + " = " + PortsValue[i, 1] + System.Environment.NewLine +
                            XMLfName + PortsValue[i, 0].ToString() + GetAtribute("channel", "port") + System.Environment.NewLine;
                        count++;
                    }
                }
            }
            return count;
        }
        private int IconValidation()
        {
            bool flag = false;
            int FileMissing = 0;
            string ImageBPath = "";
            string output = "";
            XmlTextReader XMLrdr = new XmlTextReader(ConfPathes[(int)ConfFileIndex.MDOParsecWinexe] + ConfNames[(int)ConfFileIndex.MDOParsecWinexe]);
            XMLrdr = FindNodeByName("imagesConfig", XMLrdr);

            ImageBPath = XMLrdr.GetAttribute(0);
            output = "Начальный путь = " + ImageBPath + System.Environment.NewLine;
            while (XMLrdr.Read())
            {
                if (XMLrdr.NodeType == XmlNodeType.Element && XMLrdr.Name == "image")
                {
                    output += XMLrdr.GetAttribute(0) + System.Environment.NewLine;
                    while (XMLrdr.Read() && flag == false)
                    {
                        if (XMLrdr.NodeType == XmlNodeType.Element)
                        {
                            if (XMLrdr.Name == "add")
                            {
                                if (File.Exists(ImageBPath + "\\" + XMLrdr.GetAttribute(2)))
                                    output += "Файл присутствует ";
                                else
                                {
                                    output += "ФАЙЛА НЕТ!! ";
                                    FileMissing++;
                                }
                                output += "\t" + "width=" + XMLrdr.GetAttribute(0) + " height=" + XMLrdr.GetAttribute(1) + " path=" + XMLrdr.GetAttribute(2) + System.Environment.NewLine;
                            }
                        }
                        if (XMLrdr.NodeType == XmlNodeType.EndElement)
                            break;
                    }
                }
            }
            XMLrdr.Close();
            IconsValid.Text = output + "Всего отсутствует файлов :" + FileMissing + System.Environment.NewLine + IconsValid.Text;
            return FileMissing;
        }
        private int AssemblyValidation()
        {
            Assembly tryAsm;
            bool flag = false;
            int FileMissing = 0;

            XmlTextReader XMLrdr = new XmlTextReader(ConfPathes[(int)ConfFileIndex.MDOParsecWinexe] + ConfNames[(int)ConfFileIndex.MDOParsecWinexe]);
            XMLrdr = FindNodeByName("toolsTypes", XMLrdr);

            while (XMLrdr.Read() && flag == false)
            {
                if (XMLrdr.NodeType == XmlNodeType.Element)
                {
                    try
                    {
                        tryAsm = Assembly.Load(XMLrdr.GetAttribute(0));
                        AssemblyValidOutput.Text += "Сборка присутствует " + "\t" + XMLrdr.GetAttribute(0) + System.Environment.NewLine;
                    }
                    catch
                    {
                        AssemblyValidOutput.Text += "СБОРКИ НЕТ!! " + "\t" + XMLrdr.GetAttribute(0) + System.Environment.NewLine;
                        FileMissing++;
                    }
                }
                if (XMLrdr.NodeType == XmlNodeType.EndElement)
                    flag = true;
            }
            AssemblyValidOutput.Text = "Всего отсутствует сборок :" + FileMissing.ToString() + System.Environment.NewLine + AssemblyValidOutput.Text;
            return FileMissing;
        }
        private void ReplicValidation()
        {
            XmlTextReader XMLrdr = new XmlTextReader(ConfPathes[(int)ConfFileIndex.ReplicationHost] + ConfNames[(int)ConfFileIndex.ReplicationHost]);
            XMLrdr = FindNodeByName("extentions", XMLrdr);

            while (XMLrdr.Read())
            {
                if (XMLrdr.NodeType == XmlNodeType.Element && XMLrdr.Name == "service")
                {
                    switch (XMLrdr.GetAttribute("name"))
                    {
                        case "cache":
                            {
                                ScacheBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                        case "replication":
                            {
                                SreplicationBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                        case "transaction":
                            {
                                StransactionBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                    }
                }
            }

            XMLrdr = new XmlTextReader(ConfPathes[(int)ConfFileIndex.ReplicationClient] + ConfNames[(int)ConfFileIndex.ReplicationClient]);
            XMLrdr = FindNodeByName("extentions", XMLrdr);

            while (XMLrdr.Read())
            {
                if (XMLrdr.NodeType == XmlNodeType.Element && XMLrdr.Name == "service")
                {
                    switch (XMLrdr.GetAttribute("name"))
                    {
                        case "replica_ui":
                            {
                                Creplica_uiBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                        case "replica_hal":
                            {
                                Creplica_halBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                        case "task":
                            {
                                CcacheBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                        case "transaction":
                            {
                                CtransactionBox.Checked = String2Bool(XMLrdr.GetAttribute("enable"));
                                break;
                            }
                    }
                }
            }
            XMLrdr.Close();

        }
    }
}