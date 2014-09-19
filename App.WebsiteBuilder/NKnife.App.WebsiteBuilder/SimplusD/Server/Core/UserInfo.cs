using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Jeelu.SimplusD.Server
{
    public class UserInfo
    {
        private string _userName;
        public string UserName
        {
            get { return _userName; }
        }

        private TcpClient _client;
        public TcpClient Client
        {
            get { return _client; }
        }

        public UserInfo(string userName, TcpClient tcpClient)
        {
            this._userName = userName;
            this._client = tcpClient;
        }
    }
}
