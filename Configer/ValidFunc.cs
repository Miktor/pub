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
        public struct ListItem
        {
            string name;
            bool compared;
            public ListItem(string _name, bool flag)
            {
                name = _name;
                compared = flag;                
            }
        }
        private void FullValid()
        {
           
            int i = 0;

            FullValidOutput.Text = "Результаты полной проверки.." + System.Environment.NewLine;

            //i = HostValidation();
            //if (i == 0)
            //    FullValidOutput.Text += "При проверке хостов ошибок не обнаружено" + System.Environment.NewLine;
            //else
            //    FullValidOutput.Text += "При проверке хостов обнаружено " + i.ToString() + " ошибки(ок)" + System.Environment.NewLine;

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
            
        }
        private Dictionary<string, ListItem> AddDataToListFromXML(string file, ListView lv)
        {
            Dictionary<string, ListItem> d = new Dictionary<string, ListItem>();
            foreach (string[] str in GetAtributes(file, "//channel[@ref and @port]", new string[2] { "ref", "port" }))
            {
                d.Add(str[0], new ListItem(str[1], true));
                lv.Items.Add(new ListViewItem(str));
            }
            return d;
        }
        private Dictionary<string, ListItem> AddDataToListFromINI(string file, string section, ListView lv)
        {
            INIfName = file;
            Dictionary<string, ListItem> d = new Dictionary<string, ListItem>();

            string[] sectionsKeys = GetEntryNames(section);
            for (int i = 0; i < sectionsKeys.Length; i++)
            {
                d.Add(sectionsKeys[i], new ListItem(GetValue(section, sectionsKeys[i]).ToString(), true));
                lv.Items.Add(new ListViewItem(new string[] { sectionsKeys[i], GetValue(section, sectionsKeys[i]).ToString() }));
            }

            return d;
        }
        private string GetvalueByCompArgs(string[] args)
        {
            switch (args[0])
            {
                case "pTransport":
                    INIfName = fString.ConfPathes[(int)ConfFileIndex.parsec] + fString.ConfNames[(int)ConfFileIndex.parsec];
                    return GetValue("Transport", args[1]).ToString();                    
                case "pParsecServer":
                    INIfName = fString.ConfPathes[(int)ConfFileIndex.parsec] + fString.ConfNames[(int)ConfFileIndex.parsec];
                    return GetValue("ParsecServer", args[1]).ToString();
                case "ParsecServiceHost":
                    return GetAtributes(fString.ConfPathes[(int)ConfFileIndex.ServiceHost] + fString.ConfNames[(int)ConfFileIndex.ServiceHost],
                        "//channel[@ref=\"" + args[1] + "\"]", new string[1] { "port" }).ElementAt(0)[0];                   
            }
            return "null";
        }
        private string GetArgs(string row)
        {
            int pos = 0;
            string key = string.Empty;
            string[] comandOperators = new string[] { string.Empty, string.Empty };

            if (row.IndexOf("Last") > 0)
            {
                key = "Last";
                pos = row.IndexOf("Last");
            }
            else if (row.IndexOf("Fist") > 0)
            {
                key = "Fist";
                pos = row.IndexOf("Fist");
            }

            if (pos > 0)
            {
                comandOperators = row.Substring(pos + 4).Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                row = row.Substring(0, pos - 1);
            }

            string[] arg = row.Split(new char[] { ':' });
            string val = GetvalueByCompArgs(arg);
            int poss=-1;
            int length = val.Length;

            if (key == "Last")
            {
                poss = val.LastIndexOf(comandOperators[1]);
                length = val.LastIndexOf(comandOperators[0]) - poss-1;
            }
            else if (key == "Fist")
            {
                poss = val.IndexOf(comandOperators[0]);
                length = val.IndexOf(comandOperators[1]) - poss-1;
            }
            val = val.Substring(poss + 1, length);
            return val;

        }
        private bool traseRow(string str)
        {
            string[] parts = str.Split(new string[] { " = " }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (GetArgs(parts[0]) == GetArgs(parts[1]))
                return true;
            else return false;
        }
        private int HostValidation()
        {
            ListParsecServer.Clear();
            ListParsecServiceHost.Clear();
            ListTransport.Clear();

            ListParsecServer.Columns.Add("Поле", ListParsecServer.Width / 2);
            ListParsecServer.Columns.Add("Значение", ListParsecServer.Width / 2);
            ListParsecServiceHost.Columns.Add("Поле", ListParsecServiceHost.Width / 2);
            ListParsecServiceHost.Columns.Add("Значение", ListParsecServiceHost.Width / 2);
            ListTransport.Columns.Add("Поле", ListTransport.Width / 2);
            ListTransport.Columns.Add("Значение", ListTransport.Width / 2);

            Dictionary<string, ListItem> ServiceHostValues = AddDataToListFromXML(fString.ConfPathes[(int)ConfFileIndex.ServiceHost] + fString.ConfNames[(int)ConfFileIndex.ServiceHost], ListParsecServiceHost);
            Dictionary<string, ListItem> ParsecTransportValues = AddDataToListFromINI(fString.ConfPathes[(int)ConfFileIndex.parsec] + fString.ConfNames[(int)ConfFileIndex.parsec], "Transport", ListTransport);
            Dictionary<string, ListItem> ParsecServerValues = AddDataToListFromINI(fString.ConfPathes[(int)ConfFileIndex.parsec] + fString.ConfNames[(int)ConfFileIndex.parsec], "ParsecServer", ListParsecServer);

            string[] compareStrings = new string[3] { string.Empty, string.Empty, string.Empty };
            Configer.Properties.Settings.Default.CompareStrings.CopyTo(compareStrings,0);


            if (!traseRow(compareStrings[0]))
            {
                LabelParsec.BackColor = Color.LightPink;
                LabelTransport.BackColor = Color.LightPink;
                LabelParsecServer.BackColor = Color.LightPink;
                PaintRowInList(ListTransport.Items, "ServerPath", Color.LightPink);
                PaintRowInList(ListParsecServiceHost.Items, "ServerAddress", Color.LightPink);                
            }
            else
            {
                LabelParsec.BackColor = Color.LawnGreen;
                LabelTransport.BackColor = Color.LawnGreen;
                LabelParsecServer.BackColor = Color.LawnGreen;
                PaintRowInList(ListTransport.Items, "ServerPath", Color.LawnGreen);
                PaintRowInList(ListParsecServiceHost.Items, "ServerAddress", Color.LawnGreen);  
            }
            if (!traseRow(compareStrings[1]))
            {
                LabelParsec.BackColor = Color.LightPink;
                LabelTransport.BackColor = Color.LightPink;
                PaintRowInList(ListTransport.Items, "ServerPort",Color.LightPink);
                PaintRowInList(ListTransport.Items, "LocalPort", Color.LightPink);
            }
            else
            {
                LabelParsec.BackColor = Color.LawnGreen;
                LabelTransport.BackColor = Color.LawnGreen;
                PaintRowInList(ListTransport.Items, "ServerPort", Color.LawnGreen);
                PaintRowInList(ListTransport.Items, "LocalPort",  Color.LawnGreen);
            }
            if (!traseRow(compareStrings[2]))
            {
                LabelParsec.BackColor = Color.LightPink;
                LabelParsecServer.BackColor = Color.LightPink;
                LabelServiceHost.BackColor = Color.LightPink;
                PaintRowInList(ListParsecServiceHost.Items, "authChannel", Color.LightPink);
                PaintRowInList(ListParsecServer.Items, "ServerEntry", Color.LightPink);
            }
            else
            {
                LabelParsec.BackColor = Color.LawnGreen;
                LabelParsecServer.BackColor = Color.LawnGreen;
                LabelServiceHost.BackColor = Color.LawnGreen;
                PaintRowInList(ListParsecServiceHost.Items, "authChannel", Color.LawnGreen);
                PaintRowInList(ListParsecServer.Items, "ServerEntry", Color.LawnGreen);
            }

                  
            return 0;
        }

        private void PaintRowInList(ListView.ListViewItemCollection lvic, string name, Color color)
        {
            foreach (ListViewItem lvi in lvic)
                if (lvi.Text == name)
                    lvi.BackColor = color;
        }
        private int IconValidation()
        {            
            bool flag = false;
            int FileMissing = 0;
            string ImageBPath = "";
            string output =  string.Empty;
            XmlTextReader XMLrdr = new XmlTextReader(fString.ConfPathes[(int)ConfFileIndex.ParsecExe] + fString.ConfNames[(int)ConfFileIndex.ParsecExe]);
            XMLrdr = FindNodeByName("imagesConfig", XMLrdr);
            ImageBPath = XMLrdr.GetAttribute(0);            

            ListIconsCompare.Clear();
            ListIconsCompare.Columns.Add("Сборка", 2 * ListIconsCompare.Width / 3);
            ListIconsCompare.Columns.Add("Присутствует", ListIconsCompare.Width / 3);
            ListViewGroup lvg = ListIconsCompare.Groups.Add("baze", "Начальный путь = " + ImageBPath);
            ListIconsCompare.Groups.Add(lvg);

            while (XMLrdr.Read())
            {
                if (XMLrdr.NodeType == XmlNodeType.Element && XMLrdr.Name == "image")
                {
                    ListViewItem lvi = new ListViewItem(XMLrdr.GetAttribute(0) + System.Environment.NewLine, lvg);                    
                    while (XMLrdr.Read() && flag == false)
                    {
                        if (XMLrdr.NodeType == XmlNodeType.Element)
                        {
                            if (XMLrdr.Name == "add")
                            {
                                if (File.Exists(ImageBPath + "\\" + XMLrdr.GetAttribute(2)))
                                {
                                    lvi.SubItems.Add("ДА");
                                    lvi.BackColor = Color.LawnGreen;
                                }
                                else
                                {
                                    lvi.SubItems.Add("НЕТ");
                                    lvi.BackColor = Color.LightPink;
                                    FileMissing++;
                                }
                                //output += "\t" + "width=" + XMLrdr.GetAttribute(0) + " height=" + XMLrdr.GetAttribute(1) + " path=" + XMLrdr.GetAttribute(2) + System.Environment.NewLine;
                            }
                        }
                        if (XMLrdr.NodeType == XmlNodeType.EndElement)
                            break;
                    }
                    ListIconsCompare.Items.Add(lvi);
                }
            }
            lvg.Header = "Всего отсутствует иконок :" + FileMissing.ToString();
            XMLrdr.Close();            
            return FileMissing;
        }
        private int AssemblyValidation()
        {
            Assembly tryAsm;
            bool flag = false;
            string APath = string.Empty;
            int FileMissing = 0;

            XmlTextReader XMLrdr = new XmlTextReader(fString.ConfPathes[(int)ConfFileIndex.ParsecExe] + fString.ConfNames[(int)ConfFileIndex.ParsecExe]);
            XMLrdr = FindNodeByName("probing", XMLrdr);
            APath = XMLrdr.GetAttribute(0);
            XMLrdr.Close();
            XMLrdr = new XmlTextReader(fString.ConfPathes[(int)ConfFileIndex.ParsecExe] + fString.ConfNames[(int)ConfFileIndex.ParsecExe]);
            XMLrdr = FindNodeByName("toolsTypes", XMLrdr);

            
            ListAssemblyCompare.Clear();
            ListAssemblyCompare.Columns.Add("Сборка", 2 * ListIconsCompare.Width / 3);
            ListAssemblyCompare.Columns.Add("Присутствует", ListIconsCompare.Width / 3);
            ListViewGroup lvg = ListIconsCompare.Groups.Add("baze", "Сборки");
            ListAssemblyCompare.Groups.Add(lvg);

            while (XMLrdr.Read() && flag == false)
            {
                if (XMLrdr.NodeType == XmlNodeType.Element)
                {
                    ListViewItem lvi = new ListViewItem(XMLrdr.GetAttribute(0), lvg);
                    Assembly asm;                    
                    try
                    {                       
                        lvi.BackColor = Color.LawnGreen;
                        lvi.SubItems.Add("ДА");                        
                    }
                    catch
                    {
                        lvi.SubItems.Add("НЕТ");                        
                        lvi.BackColor = Color.LightPink;
                        FileMissing++;
                    }
                    lvi = ListAssemblyCompare.Items.Add(lvi);   
                }
                if (XMLrdr.NodeType == XmlNodeType.EndElement)
                    flag = true;  
            }
            lvg.Header = "Всего отсутствует сборок :" + FileMissing.ToString();
            return FileMissing;
        }    
    }
}