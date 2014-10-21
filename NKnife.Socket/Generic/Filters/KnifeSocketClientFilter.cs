using System;
using NKnife.Events;
using SocketKnife.Common;
using SocketKnife.Events;
using SocketKnife.Interfaces;

namespace SocketKnife.Generic.Filters
{
    public class KeepAliveClientFilter : KnifeSocketClientFilter
    {
        protected bool _ContinueNextFilter = true;
        public override bool ContinueNextFilter { get { return _ContinueNextFilter; } }

        public override void PrcoessReceiveData(KnifeSocketSession session, byte[] data)
        {
            throw new NotImplementedException();
        }
    }
}