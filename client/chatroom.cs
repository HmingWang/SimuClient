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
    public partial class chatroom : Form
    {
        public chatroom()
        {
            InitializeComponent();
        }

        public void AddPlayer(string name)
        {
            this.lbxPlayerList.Items.Add(name);
            this.lblCount.Text = this.lbxPlayerList.Items.Count.ToString();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            
        }

        private void chatroom_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClientService.RemoveForm("chatroom");
        }

        private void chatroom_Load(object sender, EventArgs e)
        {
            ClientService.Send("107");
        }
    }
}
