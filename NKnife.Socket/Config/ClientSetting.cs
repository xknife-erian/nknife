using System.Net;
using System.Xml;
using NKnife.Configuring.CoderSetting;
using NKnife.Utility;
using SocketKnife.Interfaces;

namespace SocketKnife.Config
{
    public abstract class ClientSetting : ISocketClientSetting
    {
        /// <summary>
        ///     Gets or sets 服务器IP地址
        /// </summary>
        /// <value>The IP address.</value>
        public virtual string IPAddress { get; set; }

        /// <summary>
        ///     Gets or sets 服务器的端口
        /// </summary>
        /// <value>The port.</value>
        public virtual int Port { get; set; }

        public int TalkOneLength { get; private set; }

        public bool EnablePingServer { get; set; }

        /// <summary>
        ///     Gets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public virtual int HeartRange { get; protected set; }

        /// <summary>
        ///     Gets 缓存区大小.
        /// </summary>
        public virtual int BufferSize { get; protected set; }

        /// <summary>
        ///     同步的发送与接收的超时
        /// </summary>
        public virtual int Timeout { get; protected set; }

        public IDatagramDecoder Decoder { get; protected set; }
        public IDatagramEncoder Encoder { get; protected set; }
        public IDatagramCommandParser CommandParser { get; protected set; }

    }
}