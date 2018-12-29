using System.Collections.Generic;
using DynamicData;
using NKnife.ChannelKnife.Controller;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class SerialPortSelectorDialogViewModel : ReactiveObject
    {
        public SerialPortSelectorDialogViewModel()
        {
            var controller = new SerialInfoController();
            LocalSerials = controller.LocalSerial;
        }

        public Dictionary<string, string> LocalSerials { get; }


    }
}