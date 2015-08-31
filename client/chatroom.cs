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

        public void SetPlayerList(string names)
        {
     
            this.Invoke(new MethodInvoker(delegate ()
            {
                int count = 0;
                
                foreach (string p in names.Split(';'))
                {
                    if (p.Length == 0) continue;
                    this.lbxPlayerList.Items.Add(p/*.Split(',')[0]*/);
                    count++;
                }
                this.lblCount.Text = count.ToString();
            }));

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
