﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace client
{
    class ClientService
    {
        private Socket socketClient;
        private delegate void dlgtFunc(string s);
        private RichTextBox rtbMsg;
        private byte[] obuff;
        private byte[] ibuff;

        public ClientService(RichTextBox rtb) { this.rtbMsg = rtb; }

        public void Connect(string strIP,string strPort)
        {
            if (socketClient != null && socketClient.Connected)
            {
                log("服务器已连接！");
                return;
            }
            string ip = strIP;
            string port = strPort;
            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socketClient.Connect(ip, int.Parse(port));
                log("连接服务器成功：" + ip + port);

                Thread t = new Thread(do_read);
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception ex)
            {
                log("连接服务器失败！错误信息：" + ex.Message);
            }
        }

        public void log(string str)
        {
            if (rtbMsg.InvokeRequired)
            {
                dlgtFunc dlgtLog = new dlgtFunc(log);
                rtbMsg.Invoke(dlgtLog, str);
                return;
            }
            rtbMsg.AppendText(str + Environment.NewLine);
            rtbMsg.ScrollToCaret();
        }

        public void do_read()
        {
            //线程
            ibuff = new byte[1024];
            string str;
            log("数据读取开启...");
            try
            {
                while (socketClient.Connected && socketClient.Receive(ibuff) > 0)
                {
                    str = System.Text.Encoding.Default.GetString(ibuff).TrimEnd('\0');
                    log(">>>" + str);
                    ibuff = new byte[1024];
                }
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
        }

        public void Send(string cmd)
        {
            string comm = cmd;
            try
            {
                List<byte> tmp = new List<byte>(Encoding.Default.GetBytes(cmd));
                tmp.Add(0);
                obuff = tmp.ToArray();
                socketClient.Send(obuff);
                log("<<<" + comm);

            }
            catch (Exception ex)
            {
                log("发送消息失败！错误信息：" + ex.Message);

            }
        }

        public void Close()
        {
            if (socketClient != null && socketClient.Connected)
            {
                socketClient.Close();
                log("服务器已断开！");
                return;
            }
        }
    }
}