using System.Collections.ObjectModel;
using NKnife.Channels.SerialKnife.Services;
using NKnife.IoC;

namespace NKnife.Channels.SerialKnife.Views
{
    public class WorkbenchViewModel
    {
        public SerialChannelService SerialChannelService { get; } = DI.Get<SerialChannelService>();
        public ObservableCollection<SerialPortViewModel> SerialPortViewModels { get; } = new ObservableCollection<SerialPortViewModel>(); 
    }
}
