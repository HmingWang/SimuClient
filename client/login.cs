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
            string sex= (rbman.Checked) ? "1" : "0";
            string cmd = "105:" + tbxName.Text + ";" + sex;
            ClientService.Send(cmd);
            ClientService.FormFactory("chatroom");
            this.Close();
        }

        private void login_Load(object sender, EventArgs e)
        {
            this.btnRandName.PerformClick();
        }

        private void login_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClientService.RemoveForm("login");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] names = {
                "独孤求败",
                "东方不败",
                "善财童子",
                "黑山老妖",
                "东郭先生",
                "小李飞刀",
                "西门吹雪",
                "花满楼",
                "陆小凤",
                "乔峰",
                "南宫飞云",
                "百里屠苏",
                "李逍遥",
                "赵灵儿",
                "林月如",
                "韦小宝",
                "小龙女",
                "郭靖",
                "黄蓉",
                "杨过"
            };
            this.tbxName.Text = names[(new Random()).Next(0, names.Length) % names.Length];
        }
    }
}
