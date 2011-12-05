using System;
using MDO.Data.SQLite;
using System.Collections.Specialized;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Windows.Forms;

namespace TreeCreator
{

    public partial class Form1 : Form
    {
        enum ConnFildsIndex { PathToFile = 0, DictPolename = 1, GuIDPoleName = 2 };
        SQLiteConnection DBConnection = null;
        SQLiteDataReader DBReader = null;
        SQLiteDataReader DBTransReader = null;
        string[] ConnOptionStr = new string[4];

        public Form1(string[] arg)
        {
            InitializeComponent();
            //this.Icon = new System.Drawing.Icon("icon.ico");
            if (arg.Length>=1 && arg[0] == "?")
                MessageBox.Show("1-ый параметр-Имя файля Базы Данных \n2-ой параметр-Имя таблицы словоря \n3-ий параметр-Имя таблицы GuID ");
            else if (arg.Length != 3 && arg.Length >= 1)
                MessageBox.Show("Внимание!! \nВВеденые арументы неверны! \n1-ый параметр-Имя файля Базы Данных \n2-ой параметр-Имя таблицы словоря \n3-ий параметр-Имя таблицы GuID ");
            else if(arg.Length==3)
            {
                ConnOptionStr = arg;

                this.FilePath.Text = ConnOptionStr[(int)ConnFildsIndex.PathToFile];
                this.GuIDColumnName.Text = ConnOptionStr[(int)ConnFildsIndex.GuIDPoleName];
                this.DictColumnName.Text = ConnOptionStr[(int)ConnFildsIndex.DictPolename];

                MainCikle();
            }
            else if (arg.Length >= 1 & arg.Length != 3)
            {
                MessageBox.Show("неправильные параметры командной строки");
                this.FilePath.Text = "parsec3.dictionary.dat";
            }
            else
            {
                this.FilePath.Text = "parsec3.dictionary.dat";
            }
        }
        private void MainCikle()
        {
            treeView.Nodes.Clear();
            try
            {
                OpenDB();
                FindRoots();

                for (int i = 0; i < treeView.Nodes.Count; i++)
                    FindChilds(this.treeView.Nodes[i], DBReader, this.treeView.Nodes[i].Name);

                CloseDB();
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void FindRoots()
        {
            SQLiteCommand read = null;

            read = new SQLiteCommand("SELECT * FROM " + ConnOptionStr[(int)ConnFildsIndex.GuIDPoleName]);
            read.Connection = DBConnection;
            DBReader = read.ExecuteReader();

            while (DBReader.Read())
            {
                if (DBReader.GetGuid(2) == DBReader.GetGuid(3))
                {
                    this.treeView.Nodes.Add(DBReader.GetGuid(3).ToString(), FindNormalName(DBReader.GetGuid(3), DBTransReader));
                }
            }

        }
        private void FindChilds(TreeNode treeView, SQLiteDataReader DBReader, string CName)
        {
            SQLiteCommand read = null;

            read = new SQLiteCommand("SELECT * FROM " + ConnOptionStr[(int)ConnFildsIndex.GuIDPoleName]);
            read.Connection = DBConnection;
            DBReader = read.ExecuteReader();

            while (DBReader.Read())
            {
                if (DBReader.GetGuid(2).ToString() == CName && DBReader.GetGuid(3).ToString() != CName)
                    treeView.Nodes.Add(DBReader.GetGuid(3).ToString(), FindNormalName(DBReader.GetGuid(3), DBTransReader));
            }


            for (int i = 0; i < treeView.Nodes.Count; i++)
            {
                FindChilds(treeView.Nodes[i], DBReader, treeView.Nodes[i].Name);
            }
        }
        private string FindNormalName(Guid NodeGuid, SQLiteDataReader DBTransReader)
        {
            SQLiteCommand read = null;
            bool flag = false;
            string NName = null;

            read = new SQLiteCommand("SELECT * FROM " + ConnOptionStr[(int)ConnFildsIndex.DictPolename]);
            read.Connection = DBConnection;
            DBTransReader = read.ExecuteReader();

            while (flag == false && DBTransReader.Read())
            {
                if (DBTransReader.GetGuid(2) == NodeGuid)
                {
                    NName = DBTransReader.GetString(3);
                    flag = true;
                }
            }
            if (flag == true)
                return NName;
            else
                return NodeGuid.ToString();

        }
        private void OpenDB()
        {
            try
            {
                ConnOptionStr[(int)ConnFildsIndex.PathToFile] = FilePath.Text;
                DBConnection = new SQLiteConnection("Data Source=" + ConnOptionStr[(int)ConnFildsIndex.PathToFile] + ";"+"FailIfMissing=True;Read Only=True;");
                DBConnection.Open();
            }
            catch (Exception ex)
            { }
        }
        private void CloseDB()
        {
            try
            {
                DBConnection.Close();
            }
            catch
            {
                MessageBox.Show("Неудалось закрыть БД");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnOptionStr[(int)ConnFildsIndex.PathToFile] = this.FilePath.Text;
                ConnOptionStr[(int)ConnFildsIndex.GuIDPoleName] = this.GuIDColumnName.SelectedItem.ToString();
                ConnOptionStr[(int)ConnFildsIndex.DictPolename] = this.DictColumnName.SelectedItem.ToString();
                MainCikle();
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
        }
        private void GetDBObjectNames()
        {
            DataTable schema = null;

            GuIDColumnName.Items.Clear();
            GuIDColumnName.SelectedItem = null;
            DictColumnName.Items.Clear();
            DictColumnName.SelectedItem = null;
            
            try
            {
                OpenDB();                
                schema = DBConnection.GetSchema("Tables");
                CloseDB();
                foreach (DataRow row in schema.Rows)
                {
                    GuIDColumnName.Items.Add(row["TABLE_NAME"].ToString());
                    DictColumnName.Items.Add(row["TABLE_NAME"].ToString());
                }
                MessageBox.Show("Поиск прошел успешно,выберите желаемые таблицы в списках");
            }
            catch (Exception ex)
            {
                if(ex.Message == "Operation is not valid due to the current state of the object.")
                    MessageBox.Show("База не найдена!");
                MessageBox.Show(ex.ToString());
            }
            
        }
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileBD.ShowDialog();
            if (OpenFileBD.CheckPathExists == true)
            {
                ConnOptionStr[(int)ConnFildsIndex.PathToFile] = OpenFileBD.FileName;
                this.FilePath.Text = ConnOptionStr[(int)ConnFildsIndex.PathToFile];
            }
            else
                MessageBox.Show("Файла не существует!");
            GetDBObjectNames();
        }
        private void FindTables_Click(object sender, EventArgs e)
        {
            ConnOptionStr[(int)ConnFildsIndex.PathToFile] = this.FilePath.Text;
            GetDBObjectNames();
        } 
    }
}
