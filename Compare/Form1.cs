using System;
using MDO.Data.SQLite;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Compare
{
    public partial class Form1 : Form
    {
        public delegate void compFunc(string tableName);
        public delegate void modFunc(ListViewItem lvi, bool flag);

        DataSet MainDB = new DataSet("MainSet");
        string selectedTableIndex = null;
        bool sTableChange = true;
        int checkersCounter = 0;
        Dictionary<string, List<int>> flags = new Dictionary<string, List<int>> { };
        List<ListView.ListViewItemCollection> DBsItems = new List<ListView.ListViewItemCollection> { };
        List<ListView.ColumnHeaderCollection> DBsCollumns = new List<ListView.ColumnHeaderCollection> { };           
        public Form1()
        {
            InitializeComponent();
        }

        private void PathSelect(object sender, EventArgs e)
        {
            Button filepath = sender as Button;
            openFile.ShowDialog();
            if (openFile.CheckFileExists)
            {
                if (filepath.Name == "fstFilePathBtn")
                    fstFilePath.Text = openFile.FileName;
                else
                    sndFilePath.Text = openFile.FileName;
            }
        }
        private void CompareBtn_Click(object sender, EventArgs e)
        {
            compareTables();
        }
        private void loadTable(object sender, EventArgs e)
        {
            sTableChange = true;
            ComboBox CB = sender as ComboBox;            
            comparePoles(CB.SelectedItem.ToString());
            listView2.Focus();
        }
        private void chekIgnoreBoxesEdit(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            int i = int.Parse(cb.Name.Substring(8));
            if (cb.Checked)
                flags[comboBox1.Text].Remove(i);
            else
                flags[comboBox1.Text].Add(i);

            comparePoles(comboBox1.Text);
        }
        private void tableSelect(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;
            if (lv.SelectedItems.Count > 0)
            {
                tabControl1.SelectedTab = ValComp;
                comboBox1.SelectedText = lv.SelectedItems[0].Text;
                comparePoles(lv.SelectedItems[0].Text);
            }
        }
        private void autoSizeColumns(ListView.ColumnHeaderCollection dcc)
        {
            foreach (ColumnHeader dc in dcc)
            {
                ColumnHeader tmp1 = dc;
                ColumnHeader tmp2 = dc;
                tmp1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                tmp2.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                if (tmp1.Width > tmp2.Width)
                    dcc[dc.Index].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                else
                    dcc[dc.Index].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
        }
//================================================================================================================================================================
        private void compareTables()
        {
            listView1.Clear();
            listView1.Columns.Add("Первая БД", listView1.Width / 2, HorizontalAlignment.Left);
            listView1.Columns.Add("Вторая БД", listView1.Width / 2, HorizontalAlignment.Left);
            listView1.Groups.Add("main", "Таблицы БДых");
            modFunc modF = new modFunc(itemModyfaer);
            compFunc CollumnFunc = new compFunc(addTable2List);
            if (!MainDB.Tables.Contains("fstDBTablesSchema"))
            {
                MainDB.Tables.Add(loadTableSchema("Data Source=" + fstFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;", null, "Tables", "fstDBTablesSchema"));
                MainDB.Tables.Add(loadTableSchema("Data Source=" + sndFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;", null, "Tables", "sndDBTablesSchema"));
            }

            int counter = LoadList(MainDB.Tables["fstDBTablesSchema"], MainDB.Tables["sndDBTablesSchema"], 2, listView1, "main", CollumnFunc, modF);

            AllResultsout.Text = "Сравнение баз: " + Environment.NewLine +
                fstFilePath.Text.Substring(fstFilePath.Text.LastIndexOf("\\") + 1) + Environment.NewLine +
                sndFilePath.Text.Substring(sndFilePath.Text.LastIndexOf("\\") + 1) + Environment.NewLine + AllResultsout.Text;
            if(counter==0)
                AllResultsout.Text = "Все таблицы в базах данных одинаковы." + Environment.NewLine + AllResultsout.Text;
            else
                AllResultsout.Text = counter.ToString() + " таблиц в базе данных не совпадают." + Environment.NewLine + AllResultsout.Text;
        }
        private void getFullColInfo(string tableName)
        {
            DataTable dt = new DataTable();
            dt = MainDB.Tables["fstDBCollSchema" + tableName];
            listView3.Columns.Add("",0);
            foreach (DataColumn dc in dt.Columns)
                listView3.Columns.Add(dc.ColumnName);

            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem lvi = new ListViewItem();
                foreach (object ob in dr.ItemArray)
                    lvi.SubItems.Add(ob.ToString());
                listView3.Items.Add(lvi);
            }
            autoSizeColumns(listView3.Columns);
            

        }
        private void comparePoles(string tableName)
        {            
            bool flag = true;
            listView2.Clear();
            listView3.Clear();

            getFullColInfo(tableName);

            if (!MainDB.Tables.Contains("fstDBCollSchema" + tableName))
                return;

            for (int i = 0; i < MainDB.Tables["fstDBCollSchema" + tableName].Rows.Count && i < MainDB.Tables["sndDBCollSchema" + tableName].Rows.Count; i++) 
            {
                if (equalArr(MainDB.Tables["fstDBCollSchema" + tableName].Rows[i].ItemArray, MainDB.Tables["sndDBCollSchema" + tableName].Rows[i].ItemArray,null))
                {
                    if (!flags[tableName].Contains(i))
                    {
                        if (i > listView2.Columns.Count / 2)
                            listView2.Columns.Insert(listView2.Columns.Count / 2, MainDB.Tables["fstDBCollSchema" + tableName].Rows[i].ItemArray[3].ToString(), 100);
                        else
                            listView2.Columns.Insert(i, MainDB.Tables["fstDBCollSchema" + tableName].Rows[i].ItemArray[3].ToString(), 100);
                        listView2.Columns.Add(MainDB.Tables["sndDBCollSchema" + tableName].Rows[i].ItemArray[3].ToString(), 100);
                    }
                }
                else
                    flag = false;
            }
            if (flag)
                fillCollumn(listView2.Items, tableName, true);
            autoSizeColumns(listView2.Columns);
        }
        private int LoadList(DataTable fstTable, DataTable sndTable, int NamePole, ListView lv,string group, compFunc del,modFunc mod)
        {
            int counter = 0;
            List<int> nAdded = new List<int> { };

            List<object[]> list = new List<object[]> { };
            for (int j = 0; j < sndTable.Rows.Count; j++)
                list.Add(sndTable.Rows[j].ItemArray);

            for (int i = 0; i < fstTable.Rows.Count && i < sndTable.Rows.Count; i++)
            {
                int tmp = findRowInList(fstTable.Rows[i].ItemArray, list, new List<int> { 4, 5 });
                if (tmp >= 0)
                {
                    ListViewItem lvi = new ListViewItem(fstTable.Rows[i].ItemArray[NamePole].ToString(), lv.Groups[group]);
                    lvi.SubItems.Add(sndTable.Rows[tmp].ItemArray[NamePole].ToString());
                    mod(lvi, true);
                    lv.Items.Add(lvi);
                    try
                    {
                        del(fstTable.Rows[i].ItemArray[NamePole].ToString());
                    }
                    catch { }
                    int c = fillCollumn(new ListView.ListViewItemCollection(listView2), fstTable.Rows[i].ItemArray[NamePole].ToString(), false);
                    if (c > 0)
                    {
                        AllResultsout.Text += "В таблице " + fstTable.Rows[i].ItemArray[NamePole].ToString() + " обнаруженно " + c.ToString() + " ошибок!" + Environment.NewLine;
                        lvi.BackColor = Color.Yellow;
                    }
                    counter++;
                }
                else
                {
                    nAdded.Add(i);
                    counter++;
                }
            }
            lv.Groups[group].Header = "     Ошибок:" + counter.ToString();
            autoSizeColumns(lv.Columns);
            return counter;  
        }
        private int fillCollumn(ListView.ListViewItemCollection items, string tableName,bool type)
        {
            bool flag = true;
            int counter = 0;
            if (type)
                items.Clear();

            List<object[]> fstDBNERows = new List<object[]> { };
            List<object[]> sndDBNERows = new List<object[]> { };
            List<ListViewItem> tItems = new List<ListViewItem> { };
            List<int> sI= new List<int> { };

            if (!flags.ContainsKey(tableName))
                flags.Add(tableName, new List<int> { });

            if (type)
                if (sTableChange)
                {
                    if (selectedTableIndex != null)
                        clearColumnsCheckers();
                    createColumnsCheckers(tableName);
                    selectedTableIndex = comboBox1.SelectedText;
                }

            if (type)
                sI = flags[tableName];        

            SQLiteConnection fstDBConnection = new SQLiteConnection("Data Source=" + fstFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;");
            fstDBConnection.Open();
            SQLiteCommand read = new SQLiteCommand("SELECT * FROM " + tableName);
            read.Connection = fstDBConnection;
            SQLiteDataReader fstDBReader = read.ExecuteReader();

            SQLiteConnection sndDBConnection = new SQLiteConnection("Data Source=" + sndFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;");
            sndDBConnection.Open();
            SQLiteCommand sndRead = new SQLiteCommand("SELECT * FROM " + tableName);
            sndRead.Connection = sndDBConnection;
            SQLiteDataReader sndDBReader = sndRead.ExecuteReader();

            while (sndDBReader.Read())
            {
                object[] tmp = new object[sndDBReader.FieldCount];
                for (int i = 0; i < tmp.Length; i++)
                    if (!sndDBReader.IsDBNull(i))
                        if (sndDBReader.GetFieldType(i) != typeof(DateTime))
                            tmp[i] = sndDBReader.GetValue(i);
                        else
                        {
                            tmp[i] = sndDBReader.GetString(i);
                        }
                    else
                        tmp[i] = "null";
                
                sndDBNERows.Add(tmp);
            }


            while (fstDBReader.Read())
            {
                object[] row = new object[fstDBReader.FieldCount];
                for (int i = 0; i < row.Length; i++)
                    if (!fstDBReader.IsDBNull(i))
                        if (fstDBReader.GetFieldType(i) != typeof(DateTime))
                            row[i] = fstDBReader.GetValue(i);
                        else
                        {
                            row[i] = fstDBReader.GetString(i);
                        }
                    else
                        row[i] = "null";

                if (!addEqRowInList(row, sndDBNERows, tItems, sI, type))
                   fstDBNERows.Add(row);

            }
            for (int i = 0; i < sndDBNERows.Count;i++ )
                if (addEqRowInList(sndDBNERows[i], fstDBNERows, tItems, sI, type))
                    sndDBNERows.RemoveAt(i);
            
            while (fstDBNERows.Count != 0 && sndDBNERows.Count != 0)
            {
                if (fstDBNERows.Count == 0)
                {
                    foreach (object[] row in sndDBNERows)
                        if (type)
                            addRow2List(null, row, tItems, 1, sI, null);
                    sndDBNERows.Clear();
                    counter += sndDBNERows.Count;
                }
                else if (sndDBNERows.Count == 0)
                {
                    foreach (object[] row in fstDBNERows)
                        if (type)
                            addRow2List(row, null, tItems, 1, sI, null);
                    fstDBNERows.Clear();
                    counter += fstDBNERows.Count;
                }
                else
                    for (int i = 0; i < fstDBNERows.Count; i++)
                    {
                        List<int> count = new List<int> { };
                        int pos=-1;
                        List<int> tcount = new List<int> { };
                        for (int j = 0; j < sndDBNERows.Count; j++)
                        {
                            tcount = findEqValInRows(fstDBNERows[i], sndDBNERows[j], sI);
                            if (tcount.Count >= count.Count)
                            {
                                count = tcount;
                                pos = j;
                            }
                        }
                        if (pos >= 0)
                        {
                            if (type)
                                addRow2List(fstDBNERows[i], sndDBNERows[pos], tItems, 2, sI, count);                            
                            fstDBNERows.RemoveAt(i);
                            sndDBNERows.RemoveAt(pos);
                            counter++;
                        }
                    }                   
            }
            listView2.Items.AddRange(tItems.ToArray());
            if(type)
                sTableChange = false;
            return counter;
        }
//================================================================================================================================================================
        private DataTable loadTableSchema(string source, string[] restrictions, string schema, string name)
        {
            SQLiteConnection DBConnection = new SQLiteConnection(source);
            DBConnection.Open();
            DataTable dt = DBConnection.GetSchema(schema, restrictions);
            dt.TableName = name;
            DBConnection.Close();
            return dt;
        }                
//================================================================================================================================================================
        private bool addEqRowInList(object[] row, List<object[]> list1, List<ListViewItem> tItems, List<int> ignore, bool type)
        {
            int fFlag = findRowInList(row, list1, ignore);
            if (fFlag >= 0)
            {
                if (type)
                    addRow2List(row, row, tItems, 0, ignore, null);
                list1.RemoveAt(fFlag);
                return true;
            }
            return false;
        }
        private void addRow2List(object[] row1, object[] row2, List<ListViewItem> tItems, int flag, List<int> sI, List<int> cI)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.UseItemStyleForSubItems = false;
            int j = 0;
            for (int i = 0; i < row1.Length || i < row2.Length; i++)
            {
                if (!sI.Contains(i))
                {
                    if (cI == null || cI.Count == 0)
                        lvi.BackColor = Color.LightGreen;
                    else
                        lvi.BackColor = Color.LightPink;

                    ListViewItem.ListViewSubItem lvsi1 = new ListViewItem.ListViewSubItem();
                    ListViewItem.ListViewSubItem lvsi2 = new ListViewItem.ListViewSubItem();

                    if (flag == 1)                                                       //1 or 2 row null
                    {
                        if (row1 != null && row2 == null)
                            lvsi1.Text = row1[i].ToString();
                        if (row1 == null && row2 != null)
                            lvsi2.Text = row2[i].ToString();
                        lvsi1.BackColor = Color.LightPink;
                        lvsi2.BackColor = Color.LightPink;
                    }
                    else
                    {
                        lvsi1.Text = row1[i].ToString();
                        lvsi2.Text = row2[i].ToString();
                        if (cI == null || !cI.Contains(i))
                            if (flag == 2)
                            {
                                lvsi1.BackColor = Color.Yellow;
                                lvsi2.BackColor = Color.Yellow;
                            }
                            else
                            {
                                lvsi2.BackColor = Color.LightGreen;
                                lvsi1.BackColor = Color.LightGreen;
                            }
                        else
                        {
                            lvsi1.BackColor = Color.LightGreen;
                            lvsi2.BackColor = Color.LightGreen;
                        }
                    }
                    if (j == 0)
                    {
                        lvi.SubItems[0] = lvsi1;
                    }
                    else
                        lvi.SubItems.Insert(j, lvsi1);
                    lvi.SubItems.Add(lvsi2);

                    j++;
                }
            }
            tItems.Add(lvi);
        }
        private int findRowInList(object[] row, List<object[]> list, List<int> ignore)
        {
            bool flag = false;
            for (int i = 0; i < list.Count && flag == false; i++)
            {
                if (equalArr(row, list[i], ignore))
                    return i;
            }
            return -1;
        }
        private bool equalArr(object[] arr1, object[] arr2, List<int> ignore)
        {
            if (arr1.Length == arr2.Length)
            {
                for (int i = 0; i < arr1.Length; i++)
                    if (arr1[i].ToString() != arr2[i].ToString() && !ignore.Contains(i))
                        return false;
            }
            else
                return false;

            return true;
        }
        private List<int> findEqValInRows(object[] row1, object[] row2, List<int> ignore)
        {
            List<int> count = new List<int> { };
            for (int i = 0; i < row1.Length; i++)
                if (row1[i].ToString() == row2[i].ToString() || ignore.Contains(i))
                    count.Add(i);
            return count;
        }       
        private void itemModyfaer(ListViewItem lvi,bool flag)
        {
            if (!flag)
                lvi.BackColor = Color.LightPink;
            else
                lvi.BackColor = Color.LightGreen;
        }
        private void addTable2List(string table)
        {
            comboBox1.Items.Add(table);            
            listView1.Groups.Add(table, "Столбцы таблицы: " + table);

            modFunc modF = new modFunc(itemModyfaer);
            if (!MainDB.Tables.Contains("fstDBCollSchema" + table))
                MainDB.Tables.Add(loadTableSchema("Data Source=" + fstFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;", new string[4] { null, null, table, null }, "Columns", "fstDBCollSchema" + table));

            if (!MainDB.Tables.Contains("sndDBCollSchema" + table))
                MainDB.Tables.Add(loadTableSchema("Data Source=" + sndFilePath.Text + ";" + "FailIfMissing=True;Read Only=True;", new string[4] { null, null, table, null }, "Columns", "sndDBCollSchema" + table));

        }
//================================================================================================================================================================        
        private void createColumnsCheckers(string tableName)
        {
            int collWidth = 0;         

            for (int i = 0; i < listView2.Columns.Count/2; i++)
            {
                CheckBox CollH = new CheckBox();
                CollH.Name = "MyCollH_" + i.ToString();
                CollH.Text = listView2.Columns[i].Text;

                if (flags[tableName].Contains(i))
                    CollH.Checked = false;
                else
                    CollH.Checked = true;

                CollH.CheckedChanged += new EventHandler(chekIgnoreBoxesEdit);
                this.splitContainer1.Panel1.Controls.Add(CollH);
                CollH.Location = new Point(listView2.Location.X + 20 + listView2.Width * i / (listView2.Columns.Count / 2), listView2.Location.Y - 20);
                CollH.Anchor = ( AnchorStyles.Top | AnchorStyles.Left);                
                collWidth += listView2.Columns[i].Width;
            }
            checkersCounter = listView2.Columns.Count / 2;
        }
        private List<int> getNActiveColumns(int count)
        {
            List<int> list = new List<int> { };

            for (int i = 0; i < count-1; i++)
            {
                CheckBox CollH = this.splitContainer1.Panel1.Controls["MyCollH_" + i.ToString()] as CheckBox;
                if (CollH.Checked != true)
                    list.Add(i);
            }
            return list;
        }
        private void clearColumnsCheckers()
        {
            for (int i = 0; i < checkersCounter; i++)
                try
                {
                    this.splitContainer1.Panel1.Controls["MyCollH_" + i.ToString()].Dispose();
                }
                catch { }
            checkersCounter = 0;
        }
        private void formResize(object sender, EventArgs e)
        {
            for (int i = 0; i < checkersCounter; i++)
                try
                {
                    CheckBox tmp = this.splitContainer1.Panel1.Controls["MyCollH_" + i.ToString()] as CheckBox;
                    tmp.Location = new Point(listView2.Location.X + 20 + listView2.Width * i / (listView2.Columns.Count / 2), listView2.Location.Y - 20);
                }
                catch { }
        }
    }
}
