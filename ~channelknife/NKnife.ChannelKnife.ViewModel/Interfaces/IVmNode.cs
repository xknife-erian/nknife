using System;

namespace NKnife.ChannelKnife.ViewModel.Interfaces
{
    public interface IVmNode
    {
        Guid Id { get; }
        IVmManager Manager { get; set; }
        bool InSingleton { get; }
    }
}