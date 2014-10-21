namespace SocketKnife.Generic
{
    public abstract class KnifeSocketServerProtocolHandler : KnifeSocketProtocolHandler
    {
        public KnifeSocketSessionMap SessionMap { get; set; }
    }
}