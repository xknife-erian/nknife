using System;
using Ninject;
using SocketKnife.Interfaces.Sockets;

namespace NKnife.Socket.StarterKit.Base
{
    internal class SocketOption : ISocketClientSetting
    {
        private readonly string _BufferSize;
        private readonly string _EnablePingServer;
        private readonly string _HeartRange;
        private readonly string _IPAddress;
        private readonly string _Port;
        private readonly string _TalkOneLength;
        private readonly string _Timeout;

        [Inject]
        public SocketOption(
            [Named("BufferSize")] string buffersize,
            [Named("EnablePingServer")] string enablePingServer,
            [Named("HeartRange")] string heartRange,
            [Named("IPAddress")] string ipAddress,
            [Named("Port")] string port,
            [Named("TalkOneLength")] string talkOneLength,
            [Named("Timeout")] string timeout)
        {
            _BufferSize = buffersize;
            _EnablePingServer = enablePingServer;
            _HeartRange = heartRange;
            _IPAddress = ipAddress;
            _Port = port;
            _TalkOneLength = talkOneLength;
            _Timeout = timeout;
        }

        public IDatagramCommandParser CommandParser { get; private set; }
        public IDatagramDecoder Decoder { get; private set; }
        public IDatagramEncoder Encoder { get; private set; }

        /// <summary>
        ///     Gets or sets 服务器IP地址
        /// </summary>
        /// <value>The IP address.</value>
        public virtual string IPAddress
        {
            get { return _IPAddress; }
        }

        /// <summary>
        ///     Gets or sets 服务器的端口
        /// </summary>
        /// <value>The port.</value>
        public virtual int Port
        {
            get { return int.Parse(_Port); }
        }

        public int TalkOneLength
        {
            get { return int.Parse(_TalkOneLength); }
        }

        public bool EnablePingServer
        {
            get { return bool.Parse(_EnablePingServer); }
        }

        /// <summary>
        ///     Gets 心跳间隔.
        /// </summary>
        /// <value>The heart range.</value>
        public virtual int HeartRange
        {
            get { return int.Parse(_HeartRange); }
        }

        /// <summary>
        ///     Gets 缓存区大小.
        /// </summary>
        public virtual int BufferSize
        {
            get { return int.Parse(_BufferSize); }
        }

        /// <summary>
        ///     同步的发送与接收的超时
        /// </summary>
        public virtual int Timeout
        {
            get { return int.Parse(_Timeout); }
        }
    }
}