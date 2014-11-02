namespace SocketKnife.Interfaces
{
    public interface ISocketClientConfig : ISocketConfig
    {
        /// <summary>
        /// 重连的间隔时间
        /// </summary>
        int ReconnectTime { get; set; }
    }
}