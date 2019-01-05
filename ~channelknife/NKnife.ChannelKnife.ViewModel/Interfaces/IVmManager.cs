using System;
using System.Collections.Generic;

namespace NKnife.ChannelKnife.ViewModel.Interfaces
{
    public interface IVmManager
    {
        Dictionary<Guid, IVmNode> Nodes { get; }
    }
}