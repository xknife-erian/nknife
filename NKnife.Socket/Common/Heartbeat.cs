using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocketKnife.Common
{
    public class Heartbeat
    {
        public Heartbeat()
            : this("HeartBeater")
        {
        }


        public Heartbeat(string senderDescription)
        {
            RequestOfHeartBeat = Encoding.Default.GetBytes(string.Format("[[SOCKET >>> This is beating of {0} heart.]]", senderDescription));
            ReplyOfHeartBeat = Encoding.Default.GetBytes(string.Format("[[SOCKET >>> The {0} is normal.]]",senderDescription));
        }

        public byte[] RequestOfHeartBeat { get; set; }

        public byte[] ReplyOfHeartBeat { get; set; }
    }
}
