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
    public partial class Form1 : Form
    {
        private Socket socketClient;
        private delegate void dlgtFunc(string s);
        private byte[] obuff;
        private byte[] ibuff;

        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (socketClient!=null&& socketClient.Connected) {
                log("服务器已连接！");
                return;
            }
            string ip = tbxIP.Text;
            string port = tbxPort.Text;
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socketClient.Connect(ip, int.Parse(port));
                log("连接服务器成功：" + ip + port);
                
                Thread t = new Thread(do_read);
                t.IsBackground = true;
                t.Start();
            }
            catch(Exception ex) {
                log("连接服务器失败！错误信息：" + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string comm = tbxCommand.Text;
            try
            {
                List<byte> tmp=new List<byte>(Encoding.Default.GetBytes(tbxCommand.Text));
                tmp.Add(0);
                obuff=tmp.ToArray();
                socketClient.Send(obuff);
                log("<<" + comm);
                tbxCommand.Text = "";

            }
            catch (Exception ex) {
                log("发送消息失败！错误信息：" + ex.Message);
                
            }
        }

        private void log(string str) {
            if (rtbMsg.InvokeRequired) 
            {
                dlgtFunc dlgtLog = new dlgtFunc(log);
                rtbMsg.Invoke(dlgtLog, str);
                return;
            }
            rtbMsg.AppendText(str + Environment.NewLine);
            rtbMsg.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (socketClient!=null&& socketClient.Connected) {
                socketClient.Close();
                log("服务器已断开！");
                return;
            }
        }

        private void do_read()
        {
            //线程
            ibuff=new byte[1024];
            string str;
            log("数据读取开启...");
            try
            {
                while (socketClient.Connected && socketClient.Receive(ibuff) > 0)
                {
                    str=System.Text.Encoding.Default.GetString(ibuff).TrimEnd('\0');
                    log(">>" + str);
                    ibuff = new byte[1024];                    
                }
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
        }

    }
}
