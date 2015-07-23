using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NKnife.Kits.SocketKnife.StressTest.Codec;

namespace NKnife.Kits.SocketKnife.StressTest.Base
{
    public class SessionWrapper
    {
        public long Id { get; set; }
        public long AddressValue { get; set; }

        public override string ToString()
        {
            return string.Format("Session {0} [{1}]", Id,NangleCodecUtility.ConvertFromIntToFourBytes(AddressValue).ToHexString());
        }
    }
}
