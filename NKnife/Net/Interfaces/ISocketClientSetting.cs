using NKnife.Net.Protocol;

namespace Gean.Net.Interfaces
{
    public interface ISocketClientSetting
    {
        IDatagramCommandParser CommandParser { get; }
        IDatagramDecoder Decoder { get; }
        IDatagramEncoder Encoder { get; }

        int HeartRange { get; }

        /// <summary>
        /// Gets 缓存区大小.
        /// </summary>
        int BufferSize { get; }

        /// <summary>
        /// 同步的发送与接收的超时
        /// </summary>
        int Timeout { get; }

        string IPAddress { get; set; }
        int Port { get; set; }
        int TalkOneLength { get; }

        /// <summary>
        /// 尝试 Ping 服务器，以判断网络状态
        /// </summary>
        bool EnablePingServer { get; set; }
    }
}
