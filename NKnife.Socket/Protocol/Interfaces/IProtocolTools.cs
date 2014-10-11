namespace SocketKnife.Protocol.Interfaces
{
    public interface IProtocolTools
    {
        IProtocolHead Head { get; }
        IProtocolTail Tail { get; }
        IProtocolPackager Packager { get; }
        IProtocolUnPackager UnPackager { get; }
        IProtocolFamily Family { get; }
    }
}