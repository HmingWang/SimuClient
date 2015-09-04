using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AsyncService;

namespace client
{
    public partial class main : Form
    {

        private delegate void dlgtFunc(string s);


        public main()
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
            //Service srv = new Service(1, 1024);
            //srv.StartClinet(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1986));
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
            (new main()).Show();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new login()).Show();
        }

        private void Controler_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
