using System.Collections.ObjectModel;
using NKnife.IoC;
using SocketKnife.Services;

namespace SocketKnife.Views
{
    public class WorkbenchViewModel
    {
        public SerialChannelService SerialChannelService { get; } = DI.Get<SerialChannelService>();
        public ObservableCollection<SerialPortViewModel> SerialPortViewModels { get; } = new ObservableCollection<SerialPortViewModel>(); 
    }
}
