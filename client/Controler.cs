using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace client
{
    public partial class Controler : Form
    {

        private delegate void dlgtFunc(string s);


        public Controler()
        {
            InitializeComponent();
            ClientService.setLogContainer(this.GetLogContainer());
        }

        public RichTextBox GetLogContainer()
        {
            return this.rtbMsg;
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            string ip = tbxIP.Text;
            string port = tbxPort.Text;
            ClientService.Connect(ip, port);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string comm = tbxCommand.Text;
            ClientService.Send(comm);

        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            ClientService.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            (new Controler()).Show();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new login()).Show();
        }
    }
}
