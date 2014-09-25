using System;
using NKnife.Socket.Interfaces;

namespace NKnife.Socket
{
    public class TcpServerKnife : ISocketServerKnife
    {
        public void Bind(string localhost, int port)
        {
            throw new NotImplementedException();
        }

        public ISocketConfig GetConfig { get; private set; }
        public IFilterChain GetFilterChain()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void ReStart()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
