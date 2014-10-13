using System;

namespace SocketKnife.Interfaces
{
    public interface IProtocolTools : ICloneable
    {
        IProtocolHead Head { get; }
        IProtocolTail Tail { get; }
        IProtocolPackager Packager { get; }
        IProtocolUnPackager UnPackager { get; }
    }
}