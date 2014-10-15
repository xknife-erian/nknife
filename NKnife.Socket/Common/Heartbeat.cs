using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketKnife.Common
{
    public class Heartbeat
    {
        private byte[] _BeatingOfServerHeart = Encoding.Default.GetBytes(string.Format("[[SOCKET >>> This is beating of server heart.]]"));
        private byte[] _ReplayOfServer = Encoding.Default.GetBytes(string.Format("[[SOCKET >>> The server is normal.]]"));

        private byte[] _BeatingOfClientHeart = Encoding.Default.GetBytes(string.Format("[[SOCKET <<< This is beating of client heart.]]"));
        private byte[] _ReplayOfClient = Encoding.Default.GetBytes(string.Format("[[SOCKET <<< The client is normal.]]"));

        public byte[] BeatingOfServerHeart
        {
            get { return _BeatingOfServerHeart; }
            set { _BeatingOfServerHeart = value; }
        }

        public byte[] ReplayOfServer
        {
            get { return _ReplayOfServer; }
            set { _ReplayOfServer = value; }
        }

        public byte[] BeatingOfClientHeart
        {
            get { return _BeatingOfClientHeart; }
            set { _BeatingOfClientHeart = value; }
        }

        public byte[] ReplayOfClient
        {
            get { return _ReplayOfClient; }
            set { _ReplayOfClient = value; }
        }
    }
}
