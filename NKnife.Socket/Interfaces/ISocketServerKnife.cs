namespace NKnife.Socket.Interfaces
{
    public interface ISocketServerKnife
    {
        void Bind(string localhost, int port);
        ISocketConfig GetConfig { get; }
        IFilterChain GetFilterChain();
        void Start();
        void ReStart();
        void Stop();
    }
}