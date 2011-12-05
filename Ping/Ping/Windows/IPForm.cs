using System;
using System.Net;
using System.Windows.Forms;

namespace Ping
{
    public partial class IPForm : Form
    {
        public IPForm()
        {
            InitializeComponent();
        }

        public string SelectedIP { get { return comboBox1.SelectedItem.ToString(); } }
        private void Form2_Load(object sender, EventArgs e)
        {
            IPHostEntry localMachineInfo = Dns.GetHostEntry(Dns.GetHostName());    
                       
            foreach (IPAddress ip in localMachineInfo.AddressList)
                comboBox1.Items.Add(ip.ToString());
            comboBox1.SelectedIndex = 0;     
        }

        private void ExitButton(object sender, EventArgs e)
        {         
            this.Close();
        }
    }
}