using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Ping
{
    public partial class MainForm : Form
    {
        private InfoSockets iSocket = null;
        private AClient client = null;
        private Thread MainThread = null;

        public static bool StopAllSockets { get { return _stopAllSockets; } }
        private static bool _stopAllSockets = false;

        public MainForm()
        {             
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.Indent();            
        }

        public RichTextBox Output { get { return OutputText; } set { OutputText = value; } }

        private void startBTN_Click(object sender, EventArgs e)
        {
            _stopAllSockets = false;

            List<int> TCPports = new List<int> { };
            List<int> UDPports = new List<int> { };

            for (int i = System.Convert.ToInt32(TCPminPortAdr.Text); i <= System.Convert.ToInt32(TCPmaxPortAdr.Text); i++)
                TCPports.Add(i);
            for (int i = System.Convert.ToInt32(UDPminPortAdr.Text); i <= System.Convert.ToInt32(UDPmaxPortAdr.Text); i++)
                UDPports.Add(i);

            if (clientChek.Checked)
            {
                startClient(ipList.Text, TCPports, UDPports, System.Convert.ToInt32(optPort.Text));

                stop.Enabled = true;
                startBTN.Enabled = false;
                programType.Enabled = false;
            }
            else if (serverChek.Checked)
            {
                if (!startServer(System.Convert.ToInt32(optPort.Text)))
                    return;
                stop.Enabled = true;
                startBTN.Enabled = false;
                programType.Enabled = false;
            }
            else
                MessageBox.Show((Control)sender, "Выбирети тип работы программы:Сервер или Клиент", "Выберите тип", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        //private void pingPorts(long port1, long port2, string ip)
        //{
        //    System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

        //    string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        //    byte[] buffer = Encoding.ASCII.GetBytes(data);
        //    int timeout = 12000;
        //    try
        //    {
        //        ClientOutput.Text += "IP= " + ip.ToString() + " Ping= " + pingSender.Send(ip, timeout, buffer).RoundtripTime.ToString() + " ms." + Environment.NewLine);
        //    }
        //    catch (Exception e)
        //    {
        //        ClientOutput.Text += "IP= " + ip.ToString() + " Ping не удался" + Environment.NewLine + e.Message;
        //    }
        //}

        private void startClient(string server, List<int> TCPports, List<int> UDPports, int CommandPort)
        {
            if (!TCPConnect.Checked)
                TCPports = new List<int> { };
            if (!UDPConnect.Checked)
                UDPports = new List<int> { };
            
            this.Text = "Клиент, IP " + server;
            notifyIcon1.Text = "Клиент , IP" + server;

            List<MySocket> sockets = new List<MySocket> { };
            foreach (int port in UDPports)
                sockets.Add(new UdpSocket(port, false));
            foreach (int port in TCPports)
                sockets.Add(new TcpSocket(port, false));

            client = new AClient();
            MainThread = new Thread(new ThreadStart(delegate() { client.StartClient(sockets.ToArray(), server, this, CommandPort); }));
            MainThread.Name = "ClientThread";
            MainThread.IsBackground = true;
            MainThread.Start();            
        }

        private bool startServer(int CommandPort)
        {
            IPForm dialog = new IPForm();
            dialog.ShowDialog(this);
            if (dialog.DialogResult != DialogResult.OK)
                return false;

            IPAddress ip = IPAddress.Parse(dialog.SelectedIP);

            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                return false;

            iSocket = new InfoSockets();
            MainThread = new Thread(new ThreadStart(delegate() { iSocket.Start((Int32)CommandPort, ip, this); }));
            MainThread.Name = "ServerThread";
            MainThread.IsBackground = true;
            MainThread.Start();

            this.Text = "Сервер, IP " + dialog.SelectedIP;
            notifyIcon1.Text = "Сервер, IP " + dialog.SelectedIP;
            dialog.Dispose();
            return true;
        }

        private void stop_Click(object sender, EventArgs e)
        {
            Stop();              
        }   

        public void Stop()
        {
            _stopAllSockets = true;

            if (client != null)
                client.Dispose();
            if (iSocket != null)
                iSocket.Dispose();

            //if (MainThread != null)
            //{
            //    MainThread.Abort();
            //    MainThread.Join();
            //}

            stop.Enabled = false;
            startBTN.Enabled = true;
            programType.Enabled = true;
            this.Text = "Ping";
            notifyIcon1.Text = "Ping";
        } 
        
        #region CheckBox & other stuff

        private void MainForm_FormClosing(object sender, EventArgs e)
        {
            Stop();
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Visible = false;
                this.ShowInTaskbar = false;
            }
        }

        private void ShemaCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadShema((sender as ComboBox).SelectedItem.ToString());
        }      

        private void TCPConnect_CheckedChanged(object sender, EventArgs e)
        {
            TCPportBox.Enabled = (sender as CheckBox).Checked;
            startBTN.Enabled = (TCPConnect.Checked || UDPConnect.Checked || serverChek.Checked);
        }

        private void UDPConnect_CheckedChanged(object sender, EventArgs e)
        {
            UDPportBox.Enabled = (sender as CheckBox).Checked;
            startBTN.Enabled = (TCPConnect.Checked || UDPConnect.Checked || serverChek.Checked);
        }

        private void clientChek_CheckedChanged(object sender, EventArgs e)
        {
            ipList.Enabled = (sender as RadioButton).Checked;
            PingTypeBox.Enabled = true;
            startBTN.Enabled = (TCPConnect.Checked || UDPConnect.Checked);
        }

        private void clean_Click(object sender, EventArgs e)
        {
            OutputText.Clear();
        }

        private void saveText_Click(object sender, EventArgs e)
        {            
            StreamWriter sw = null;
            Control aControl = sender as Control;
            try
            {
                if (OutputText.Lines.Length > 0)
                {
                    saveFile.Filter = "TXT (*.txt)|*.txt|All (*.*)|*.*";
                    saveFile.FileName = "ping_save_" + DateTime.Now.ToString().Replace('.', '_').Replace(':', '_').Replace('.', '_').Replace(' ', '_') + ".txt";
                    if (saveFile.ShowDialog(this) == DialogResult.OK)
                    {
                        OutputText.SaveFile(saveFile.FileName, RichTextBoxStreamType.UnicodePlainText);                                             
                    }
                }
                else
                    MessageBox.Show(aControl, "Извините,но нету данных для сохранения в файл", "Нету данных", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            catch
            {
                MessageBox.Show(aControl, "Извините, Но запись не удалась", "Неудача", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();                
                }
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.Activate();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void SaveShema_Click(object sender, EventArgs e)
        {
            InputWin name = new InputWin();
            name.ShowDialog(this);
            if(name.DialogResult==DialogResult.OK)
                if (name.textBox1.Text.Length > 0 && name.textBox1.Text.Contains(";") == false)
                {
                    StringBuilder sb = new StringBuilder(name.textBox1.Text + ";");

                    if (serverChek.Checked)
                        sb.Append('S');
                    else if (clientChek.Checked)
                        sb.Append('C' + ipList.Text);
                    sb.Append(';');

                    if (TCPConnect.Checked)
                        sb.Append("TCP");
                    sb.Append(";");
                    if (UDPConnect.Checked)
                        sb.Append("UDP");
                    sb.Append(";");

                    sb.Append(TCPminPortAdr.Text + ";" + TCPmaxPortAdr.Text + ";" + UDPminPortAdr.Text + ";" + UDPmaxPortAdr.Text + ";" + optPort.Text + ";");
                    // sb.Append(new Guid(sb.ToString()).ToString() + ";");

                    Properties.Settings.Default.shemas.Add(sb.ToString());
                    Properties.Settings.Default.Save();
                    loadSchemas();
                }
                else
                    MessageBox.Show((Control)sender, "Имя шаблона пустое,либо содержит недопустимый символ(\";\")", "Имя шаблона", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private void DelBut_Click(object sender, EventArgs e)
        {
            if (ShemaCB.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали ни одного шаблона чтобы его удалить.", "Шаблон не выбран", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }
            string[] shemas = new string[Properties.Settings.Default.shemas.Count];
            Properties.Settings.Default.shemas.CopyTo(shemas, 0);
            List<string> tmpshemas = new List<string> { };
            for (int i = 0; i < shemas.Length; i++)
                if (!shemas[i].StartsWith(ShemaCB.SelectedItem.ToString()))
                    tmpshemas.Add(shemas[i]);
            Properties.Settings.Default.shemas.Clear();
            Properties.Settings.Default.shemas.AddRange(tmpshemas.ToArray());
            Properties.Settings.Default.Save();
            ShemaCB.Items.Clear();
            loadSchemas();
        }

        private void loadSchemas()
        {
            ShemaCB.Items.Clear();
            string[] shemas = new string[Properties.Settings.Default.shemas.Count];
            Properties.Settings.Default.shemas.CopyTo(shemas, 0);
            foreach (string sh in shemas)
                ShemaCB.Items.Add(sh.Substring(0, sh.IndexOf(";")));
        }

        private void loadShema(string text)
        {
            foreach (string sh in Properties.Settings.Default.shemas)
                if (sh.StartsWith(text))
                {
                    Guid.NewGuid();
                    string[] items = sh.Split(';');
                    if (items[1] == "S")
                        serverChek.Checked = true;
                    else if (items[1][0] == 'C')
                    {
                        clientChek.Checked = true;
                        ipList.Text = items[1].Substring(1);
                    }

                    if (items[2] == "TCP")
                        TCPConnect.Checked = true;
                    else
                        TCPConnect.Checked = false;
                    if (items[3] == "UDP")
                        UDPConnect.Checked = true;
                    else
                        UDPConnect.Checked = false;

                    TCPminPortAdr.Text = items[4];
                    TCPmaxPortAdr.Text = items[5];
                    UDPminPortAdr.Text = items[6];
                    UDPmaxPortAdr.Text = items[7];
                    optPort.Text = items[8];
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadSchemas();
        }        

        private void serverChek_CheckedChanged(object sender, EventArgs e)
        {
            PingTypeBox.Enabled = false;
            startBTN.Enabled = true;
        }         

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog(this);
            about.Dispose();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        #endregion         
    }
}