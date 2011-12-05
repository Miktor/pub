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
        public string GetAtribute(string element, string atr)
        {
            XmlTextReader XMLrdr = new XmlTextReader(XMLfName);
            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.Load(XMLrdr);
            XMLrdr.Close();

            XmlNode CurNode;

            CurNode = XMLdoc.SelectSingleNode("//" + element);

            atr = CurNode.Attributes[atr].Value;

            return atr;

        }

        public void ReplaceNode(List<string> AtrValue, List<string> AtrNames, string name, string path, string fName)
        {
            XmlTextReader XMLrdr = new XmlTextReader(fName);
            XmlDocument XMLwriter = new XmlDocument();
            XMLwriter.Load(XMLrdr);
            XMLrdr.Close();

            XmlNode oldCd;

            oldCd = XMLwriter.SelectSingleNode(path);

            XmlElement newCd = XMLwriter.CreateElement(name);

            for (int i = 0; i < AtrNames.Count; i++)
            {
                newCd.SetAttribute(AtrNames.ElementAt(i), AtrValue.ElementAt(i));
            }

            oldCd.ParentNode.ReplaceChild(newCd, oldCd);

            XMLwriter.Save(fName);
        }

        public XmlTextReader FindNodeByName(string name, XmlTextReader XMLrdr)
        {
            XMLrdr.ResetState();
            while (XMLrdr.Read())
            {
                if (XMLrdr.NodeType == XmlNodeType.Element && XMLrdr.Name == name)
                {
                    return XMLrdr;
                }
            }
            return null;
        }

        public XmlNode FindNodeWithAtr(string element, string atr, string atrValue)
        {
            string adr = "//" + element;
            string tmpValue =  string.Empty;

            XmlTextReader XMLrdr = new XmlTextReader(XMLfName);
            XmlDocument XMLdoc = new XmlDocument();
            XMLdoc.Load(XMLrdr);
            XMLrdr.Close();
            XmlNode CurNode = null;

            while (atrValue != tmpValue)
            {
                CurNode = XMLdoc.SelectSingleNode("//" + element);
                tmpValue = CurNode.Attributes[atr].Value;
            }

            return CurNode;
        }

        public List<string[]> GetAtributes(string file, string xPath, string[] atr)
        {
            List<string[]> atrs = new List<string[]> { };
            if (atr.Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                try
                {
                    doc.Load(file);

                    XPathNavigator nav = doc.CreateNavigator();
                    XPathNodeIterator it = (XPathNodeIterator)nav.Evaluate(xPath);

                    while (it.MoveNext())
                    {
                        List<string> atrValues = new List<string> { };

                        foreach (string str in atr)
                            atrValues.Add(it.Current.GetAttribute(str,  string.Empty));

                        atrs.Add(atrValues.ToArray());                            
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }                
            }
            return atrs;                    
        }
    }    
}