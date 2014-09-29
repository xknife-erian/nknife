namespace SocketKnife.Interfaces
{
    public interface ISocketConfig
    {
        void SetReadBufferSize(int size);
        void SetIdleTime(object bothIdle, int i);
    }
}