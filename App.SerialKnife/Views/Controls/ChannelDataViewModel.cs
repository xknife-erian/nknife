using System.Collections.ObjectModel;
using NKnife.Channels.SerialKnife.Common;

namespace NKnife.Channels.SerialKnife.Views.Controls
{
    public class ChannelDataViewModel
    {
        public ObservableCollection<ChannelData> Datas { get; } = new ObservableCollection<ChannelData>();
    }
}