namespace SocketKnife.Interfaces
{
    public interface ISocketClientConfig : ISocketConfig
    {
        /// <summary>
        /// �����ļ��ʱ��
        /// </summary>
        int ReconnectTime { get; set; }
    }
}