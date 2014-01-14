﻿using System;
using Gean.Net.Protocol;

namespace Gean.Net.Interfaces
{
    public interface ISocketServerSetting
    {
        IDatagramCommandParser CommandParser { get; }
        IDatagramDecoder Decoder { get; }
        IDatagramEncoder Encoder { get; }
        int HeartRange { get; }
        string Host { get; }
        int MaxBufferSize { get; }
        int MaxConnectCount { get; }
        int Port { get; }
    }
}