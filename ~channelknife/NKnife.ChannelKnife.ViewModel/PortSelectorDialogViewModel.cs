using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using NKnife.ChannelKnife.Model;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class PortSelectorDialogViewModel : ReactiveObject
    {
        private int _selectedSerialListIndex = -1;

        public PortSelectorDialogViewModel()
        {
            var controller = new SerialInfoService();
            var map = controller.LocalSerial;
            foreach (var kv in map)
                LocalSerials.Add(new MySerial(kv.Key, kv.Value));
            
        }

        public List<MySerial> LocalSerials { get; } = new List<MySerial>();

        public int SelectedSerialListIndex
        {
            get => _selectedSerialListIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedSerialListIndex, value);
        }

        public ushort PackagePort()
        {
            var serial = LocalSerials[_selectedSerialListIndex];
            var p = serial.SerialPort.ToUpper().TrimStart('C', 'O', 'M');
            return ushort.Parse(p);
        }

        #region inner class

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

        #endregion
    }
}