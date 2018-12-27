using System.Collections.ObjectModel;
using NKnife.ChannelKnife.ViewModel.Common;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class ChannelDataViewModel : ReactiveObject
    {
        public ObservableCollection<ChannelData> Datas { get; } = new ObservableCollection<ChannelData>();
    }
}