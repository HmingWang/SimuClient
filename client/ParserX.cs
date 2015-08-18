using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    static class ParserX
    {
        private const string cstr_pullplayer = "107";

        static void ParserCmd(string strCmd)
        {
            string[] strvar=strCmd.Split(':');
            switch (strvar[0])
            {
                case cstr_pullplayer:
                    
                    break;
                default:
                    break;
            }
        }
    }
}
