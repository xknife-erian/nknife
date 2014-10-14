using System;

namespace SocketKnife.Interfaces
{
    public interface IProtocolTools : ICloneable
    {
        IProtocolPackager Packager { get; set; }
        IProtocolUnPackager UnPackager { get; set; }
    }
}