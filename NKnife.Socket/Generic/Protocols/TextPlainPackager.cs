using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Protocol;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Protocols
{
    public class TextPlainPackager : IProtocolPackager
    {
        public short Version {
            get { return 1; }
        }
        public string Combine(IProtocolContent c)
        {
            var command = string.Format("{0}|{1}|", c.Command, c.CommandParam);
            var sb = new StringBuilder(command);
            foreach (var info in c.Infomations)
            {
                sb.Append(info.Value).Append('|');
            }
            return sb.ToString();
        }
    }
}
