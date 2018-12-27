using System.Collections.ObjectModel;
using NKnife.ChannelKnife.Controller;
using NKnife.IoC;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class WorkbenchViewModel : ReactiveObject
    {
        public SerialChannelService SerialChannelService { get; } = DI.Get<SerialChannelService>();
        public ObservableCollection<SerialPortViewModel> SerialPortViewModels { get; } = new ObservableCollection<SerialPortViewModel>(); 
    }
}
