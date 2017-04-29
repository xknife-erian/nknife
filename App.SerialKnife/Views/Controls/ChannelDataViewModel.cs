using System.Collections.ObjectModel;
using SocketKnife.Common;

namespace SocketKnife.Views.Controls
{
    public class ChannelDataViewModel
    {
        public ObservableCollection<ChannelData> Datas { get; } = new ObservableCollection<ChannelData>();
    }
}