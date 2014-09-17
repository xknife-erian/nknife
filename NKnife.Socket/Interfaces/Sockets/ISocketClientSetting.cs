﻿namespace SocketKnife.Interfaces.Sockets
{
    public interface ISocketClientSetting
    {
        IDatagramCommandParser CommandParser { get; }

        IDatagramDecoder Decoder { get; }

        IDatagramEncoder Encoder { get; }

        int HeartRange { get; }

        /// <summary>
        ///     Gets 缓存区大小.
        /// </summary>
        int BufferSize { get; }

        /// <summary>
        ///     同步的发送与接收的超时
        /// </summary>
        int Timeout { get; }

        string IpAddress { get; }

        int Port { get; }

        int TalkOneLength { get; }

        /// <summary>
        ///     尝试 Ping 服务器，以判断网络状态
        /// </summary>
        bool EnablePingServer { get; }
    }
}