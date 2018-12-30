using System.Collections.Generic;
using NKnife.ChannelKnife.Controller;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class PortSelectorDialogViewModel : ReactiveObject
    {
        public PortSelectorDialogViewModel()
        {
            var controller = new SerialInfoController();
            var map = controller.LocalSerial;
            foreach (var kv in map)
                LocalSerials.Add(new MySerial(kv.Key, kv.Value));
        }

        public List<MySerial> LocalSerials { get; } = new List<MySerial>();

        public struct MySerial
        {
            public MySerial(string port, string description)
            {
                SerialPort = port;
                Description = description;
            }

            public string SerialPort { get; set; }
            public string Description { get; set; }
        }
    }
}