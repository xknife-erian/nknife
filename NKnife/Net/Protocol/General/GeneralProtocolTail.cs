﻿using System.Text;

namespace Gean.Net.Protocol.General
{
    public class GeneralProtocolTail : IProtocolTail
    {
        #region IProtocolTail Members

        public byte[] Tail
        {
            get { return Encoding.UTF8.GetBytes("ö"); }
        }

        #endregion
    }
}