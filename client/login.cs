using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ClientService.isConnected())
            {
                ClientService.Connect("127.0.0.1", "1986");
            }
            string cmd = "105:" + tbxName.Text + ";" + rbman.Checked.ToString();
            ClientService.Send(cmd);

            ClientService.FormFactory("chatroom");
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
    }
}
