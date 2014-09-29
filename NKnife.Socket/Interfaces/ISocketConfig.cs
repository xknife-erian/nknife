namespace SocketKnife.Interfaces
{
    public interface ISocketConfig
    {
        int ReadBufferSize { get; set; }

        /// <summary>
        ///     ���հ���С
        /// </summary>
        int MaxBufferSize { get; set; }

        /// <summary>
        ///     ����û�������
        /// </summary>
        int MaxConnectCount { get; set; }

        /// <summary>
        ///     �Ƿ�ر�SOCKET Delay�㷨
        /// </summary>
        bool NoDelay { get; set; }

        /// <summary>
        ///     SOCKET �� ReceiveTimeout����
        /// </summary>
        int ReceiveTimeout { get; set; }

        /// <summary>
        ///     SOCKET �� SendTimeout
        /// </summary>
        int SendTimeout { get; set; }
    }
}