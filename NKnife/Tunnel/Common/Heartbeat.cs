using System.Text;

namespace NKnife.Tunnel.Common
{
    public class Heartbeat
    {
        public Heartbeat()
            : this("HeartBeater")
        {
        }


        public Heartbeat(string senderDescription)
        {
            RequestOfHeartBeat = Encoding.Default.GetBytes(string.Format("[[This is beating of {0} heart.]]", senderDescription));
            ReplyOfHeartBeat = Encoding.Default.GetBytes(string.Format("[[The {0} is normal.]]",senderDescription));
        }

        public byte[] RequestOfHeartBeat { get; set; }

        public byte[] ReplyOfHeartBeat { get; set; }
    }
}
