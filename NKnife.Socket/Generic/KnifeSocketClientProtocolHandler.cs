namespace SocketKnife.Generic
{
    public abstract class KnifeSocketClientProtocolHandler : KnifeSocketProtocolHandler
    {
        public abstract KnifeSocketSession Session { get; set; }
    }
}