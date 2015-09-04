using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    static class ParserX
    {
        private const string cstr_pullplayer = "201";
        private const string cstr_pullchat = "202";

        public static void ParserCmd(string strCmd)
        {
            string[] strvar=strCmd.Split(':');
            switch (strvar[0])
            {
                case cstr_pullplayer:
                    string playerlist = strvar[1];
                    ((chatroom)ClientService.GetFormByName("chatroom")).SetPlayerList(playerlist);                        
                    break;
                case cstr_pullchat:
                    ((chatroom)ClientService.GetFormByName("chatroom")).Chat(strvar[1]);
                    break;
                default:
   
                    break;
            }
        }
    }
}
