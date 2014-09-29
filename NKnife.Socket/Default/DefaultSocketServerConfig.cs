﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocketKnife.Interfaces;

namespace SocketKnife.Default
{
    public class DefaultSocketServerConfig : ISocketServerConfig
    {
        public void Initialize(int receiveTimeout, int sendTimeout, int maxBufferSize, int maxConnectCount, int readBufferSize)
        {
            ReadBufferSize = readBufferSize;
            MaxBufferSize = maxBufferSize;
            MaxConnectCount = maxConnectCount;
            ReceiveTimeout = receiveTimeout;
            SendTimeout = sendTimeout;
        }

        public int ReadBufferSize { get; set; }
        public int MaxBufferSize { get; set; }
        public int MaxConnectCount { get; set; }
        public int ReceiveTimeout { get; set; }
        public int SendTimeout { get; set; }
    }
}
