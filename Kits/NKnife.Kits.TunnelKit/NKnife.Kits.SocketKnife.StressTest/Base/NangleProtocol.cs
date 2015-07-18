﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Codec;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class NangleProtocol : BytesProtocol
    {
        protected List<byte> CommandParamList = new List<byte>();
        public NangleProtocol(byte[] command)
            : base("nangle-socket", command)
        {

        }
    }
}
