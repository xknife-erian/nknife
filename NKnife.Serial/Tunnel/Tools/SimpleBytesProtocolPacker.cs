﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ninject.Infrastructure.Language;
using NKnife.Protocol.Generic;

namespace SerialKnife.Tunnel.Tools
{
    public class SimpleBytesProtocolPacker : BytesProtocolPacker
    {
        public override byte[] Combine(BytesProtocol content)
        {
            int len = content.Command.Length;
            if (content.CommandParam != null)
            {
                len += content.CommandParam.Length;
            }
            var result = new byte[len];
            Array.Copy(content.Command,0,result,0,content.Command.Length);
            if (content.CommandParam != null)
            {
                Array.Copy(content.CommandParam, 0, result, content.Command.Length,content.CommandParam.Length);
            }
            return result;
        }
    }
}