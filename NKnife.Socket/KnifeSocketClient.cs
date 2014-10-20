using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Generic;
using SocketKnife.Interfaces;

namespace SocketKnife
{
    public class KnifeSocketClient : TunnelBase, IKnifeSocketClient
    {
        public override ISocketConfig Config
        {
            get { throw new NotImplementedException(); }
        }

        public override bool Start()
        {
            throw new NotImplementedException();
        }

        public override bool ReStart()
        {
            throw new NotImplementedException();
        }

        public override bool Stop()
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool IsConnectionSuceess { get; private set; }
        public bool ServerStatus { get; private set; }
    }
}
