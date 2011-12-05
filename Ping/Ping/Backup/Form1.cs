using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Ping
{
    public delegate void outputText(string str);

    public partial class Form1 : Form
    {
        bool serverStarted = false;
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void printText(string str)   { resultView.Text += str; }

        private void startBTN_Click(object sender, EventArgs e)
        {
            List<int> ports = new List<int> { };
            for (int i = System.Convert.ToInt32(minPortAdr.Text); i <= System.Convert.ToInt32(maxPortAdr.Text); i++)
                ports.Add(i);
            if (clientChek.Checked)
            {
                startClient(ipList.SelectedItem.ToString(), ports);
            }
            else if (serverChek.Checked)
                startServer(ports);
            else
                MessageBox.Show("Выбирети тип работы программы:Сервер или Клиент");
        }
        private void pingPorts(long port1, long port2, string ip)
        {
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 12000;
            try
            {
                resultView.Text += "IP= " + ip.ToString() + " Ping= " + pingSender.Send(ip, timeout, buffer).RoundtripTime.ToString() + " ms." + Environment.NewLine;
            }
            catch (Exception e)
            {
                resultView.Text += "IP= " + ip.ToString() + " Ping не удался" + Environment.NewLine + e.Message;
            }
        }
        private void startClient(string server, List<int> ports)
        {
            AClient client = new AClient();
            client.StartClient(ports, server, this, true);
        }
        private void startServer(List<int> ports)
        {
            if (!serverStarted)
            {     
                if (protocolTCP.Checked || protocolUDP.Checked)
                {
                    ProtocolType type;

                    if (protocolTCP.Checked)
                        type = ProtocolType.Tcp;
                    else
                        type = ProtocolType.Udp;

                    AServer server = new AServer();
                    server.Start(ports, type, this);
                }
                else
                    MessageBox.Show("Выберите тип соебинения TCP или UDP");

                //aserv.StartListening(ports, ProtocolType.Tcp);
            }
            else
                MessageBox.Show("Извините,сервер уже запущен");
        }
        private void stop_Click(object sender, EventArgs e)
        {
            //AServer.serverStop();     
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            getNamesOfLiclaComps();
            //Process proc = new Process();
            //proc.StartInfo.WorkingDirectory = Environment.SystemDirectory+"\\System32";
            //proc.StartInfo.FileName = "cmd.exe";
            //proc.StartInfo.Arguments = "/k net view";
            //proc.Start();
            //proc.Dispose();

            IPHostEntry localMachineInfo = Dns.Resolve(Dns.GetHostName());
            foreach (IPAddress ip in localMachineInfo.AddressList)
                ipList.Items.Add(ip);
            ipList.Items.Add(localMachineInfo.HostName);
        }
        private List<string> getNamesOfLiclaComps()
        {

            DirectoryEntry parent = new DirectoryEntry("WinNT:");
            foreach (DirectoryEntry dm in parent.Children)
            {
                DirectoryEntry coParent = new DirectoryEntry("WinNT://" + dm.Name);
                DirectoryEntries dent = coParent.Children;
                dent.SchemaFilter.Add("Computer");
                foreach (DirectoryEntry client in dent)
                {
                    serversList.Items.Add(client.Name);
                }

            }
            //List<string> comps = new List<string> { };
            //System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            //PingOptions options = new PingOptions();
            //options.DontFragment = true;
            //string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            //byte[] buffer = Encoding.ASCII.GetBytes(data);
            //int timeout = 10;
            //string ip = "192.168.0.";
            //for (int i = 0; i < 256; i++)
            //{
            //    PingReply reply = pingSender.Send(ip + i.ToString(), timeout, buffer, options);
            //    if (reply.Status == IPStatus.Success)
            //        comps.Add(ip + i.ToString());
            //}
            //return comps;
        }
    }    
}
