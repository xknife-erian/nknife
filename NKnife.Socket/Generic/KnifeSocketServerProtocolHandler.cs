namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerProtocolHandler : KnifeSocketProtocolHandler
    {
        public abstract KnifeSocketSessionMap SessionMap { get; set; }
    }
}