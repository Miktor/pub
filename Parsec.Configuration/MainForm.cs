using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Parsec.Configuration
{
    public partial class MainForm : Form
    {
        Gatherer gatherer = new Gatherer();        
        
        public MainForm()
        {
            InitializeComponent();
            gatherer.WorkDone += new Gatherer.EventHandler(gatherer_WorkDone);
            gatherer.ProgressChanged += new Gatherer.ProgressChangedEventHandler(gatherer_ProgressChanged);            
        }   

        private void gatherer_ProgressChanged(object ob, ProgressChangedEventArgs args)
        {
            if (InvokeRequired) 
            {
                this.Invoke((MethodInvoker)delegate() { this.gatherer_ProgressChanged(ob, args); });
                return;
            }

            ProcessTabBar.Value = args.value;
            ProcessTabLable.Text = args.capture;
            updateCombooBoxitems();
        }
        
        private void gatherer_WorkDone(object ob, EventArgs args)
        {
            if (InvokeRequired)
            {

                this.Invoke((MethodInvoker)delegate() { this.gatherer_WorkDone(ob, args); });
                return;
            }
            if(ProcessTabText.Text.Length == 0)
                ProcessTabText.Text = "Work Done. Select some field in the ComboBox to view it.";
            wizard1.NextEnabled = true;
            wizard1.BackEnabled = true;
            updateCombooBoxitems();
        }

        private void updateCombooBoxitems()
        {
            foreach (KeyValuePair<string, StringBuilder> log in gatherer.InfoStrings)
            {
                if (!comboBox1.Items.Contains(log.Key))
                    comboBox1.Items.Add(log.Key);
            }
        }

        private void CheckBoxChanged(object sender, EventArgs e)
        {
            wizard1.NextEnabled = LicenceCheck.Checked;
        }

        private void WorkPage_ShowFromNext(object sender, EventArgs e)
        {
            //Gatherer gatherer = new Gatherer();            
            try
            {
                comboBox1.Items.Clear();
                ProcessTabText.Clear();
                Thread MainThread = new Thread(new ThreadStart(delegate() { gatherer.TakeInfos(); }));
                MainThread.Name = "WorkThread";
                MainThread.IsBackground = true;
                MainThread.Start();
                wizard1.NextEnabled = false;
                wizard1.BackEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveResToFile();            
        }

        private void SaveResToFile()
        {
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    FileStream file = File.OpenRead(Compress.PathToFile);
                    FileStream copyTo = File.Create(saveFile.FileName);
                    file.CopyTo(copyTo);
                    copyTo.Close();
                    file.Close();
                    if (MessageBox.Show(Properties.Settings.Default.openForWatch, Properties.Settings.Default.OpenFile, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                        System.Diagnostics.Process.Start(saveFile.FileName);
                    saveFile.Dispose();
                }
                catch
                {
                    if (MessageBox.Show(Properties.Settings.Default.FileOpenEror, Properties.Settings.Default.SavinError, MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                        saveBtn_Click(this, new EventArgs());
                }
            }
        }
        private void sendMail()
        {
            int result;
            try
            {

                Mail message = new Mail();
                message.AddRecipientTo(Properties.Settings.Default.MailAdr);
                message.AddAttachment(Compress.PathToFile);

                if ((result = message.SendMailPopup(Properties.Settings.Default.MailSubject, Properties.Settings.Default.MailBody)) != 0)
                    if (MessageBox.Show(this,
                    Properties.Settings.Default.MailErrors[result], Properties.Settings.Default.MailSendingError,
                    MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                        sendMail();
            }
            catch (Exception e)
            {
                if (MessageBox.Show(this,
                e.ToString(), Properties.Settings.Default.MailSendingError,
                MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1) == DialogResult.Retry)
                    sendMail();
            }
        }
        private void mailtoBtn_Click(object sender, EventArgs e)
        {
            sendMail();
        }

        private void SaveRT_CheckedChanged(object sender, EventArgs e)
        {
            wizard1.NextEnabled = true;
        }       

         private void FinishPage_ShowFromNext(object sender, EventArgs e)
        {
            if (SaveRT.Checked)
                SaveResToFile();
            else
                sendMail();
        }

         private void EMailLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
         {
             string mail = "mailto:" + Properties.Settings.Default.MailAdr + "?subject=" + Properties.Settings.Default.MailSubject + "&body=" + Properties.Settings.Default.MailBody;
             System.Diagnostics.Process.Start(mail);
         }

         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
         {
             ComboBox comboBox = (ComboBox)sender;
             string selected = (string)comboBox.SelectedItem;

             ProcessTabText.Text = gatherer.InfoStrings[selected].ToString();
         }        
    }        
}
