using System;
using System.Net.Sockets;
using System.Collections.Generic;

namespace AsyncServer
{
    public class AsyncUserToken
    {
        Socket m_socket;
        String m_message;
        public AsyncUserToken() : this(null) { }
        public AsyncUserToken(Socket socket) { m_socket = socket; }
        public Socket Socket { get { return m_socket; } set { m_socket = value; } }
        public String Message {
            get { return m_message; }
            set { m_message = value; }
        }

        //public ISession Session { get; set; }         //public Context Context { get; set; }     }
    }
}