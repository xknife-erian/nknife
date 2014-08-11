using System.Xml;
using NKnife.Configuring.CoderSetting;
using NKnife.Utility;
using SocketKnife.Interfaces;
using SocketKnife.Interfaces.Sockets;

namespace SocketKnife.Config
{
    public abstract class ServerSetting : ISocketServerSetting
    {
        public string Host { get; private set; }
        public int Port { get; private set; }
        public int MaxBufferSize { get; private set; }
        public int MaxConnectCount { get; private set; }

        /// <summary>
        ///     Gets or sets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public int HeartRange { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }
        public IDatagramCommandParser CommandParser { get; private set; }

    }
}