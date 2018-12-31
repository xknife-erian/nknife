using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using NKnife.ChannelKnife.Controller;
using ReactiveUI;

namespace NKnife.ChannelKnife.ViewModel
{
    public class PortSelectorDialogViewModel : ReactiveObject
    {
        private int _selectedSerialListIndex = -1;

        public PortSelectorDialogViewModel()
        {
            var controller = new SerialInfoController();
            var map = controller.LocalSerial;
            foreach (var kv in map)
                LocalSerials.Add(new MySerial(kv.Key, kv.Value));

            AcceptButtonEnableCommand = ReactiveCommand.Create(AcceptButtonEnable,
                this.WhenAnyValue(vm => vm.SelectedSerialListIndex).Select(i => i >= 0));
        }

        public List<MySerial> LocalSerials { get; } = new List<MySerial>();

        public int SelectedSerialListIndex
        {
            get => _selectedSerialListIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedSerialListIndex, value);
        }

        public ReactiveCommand<Unit, Unit> AcceptButtonEnableCommand { get; }

        private void AcceptButtonEnable()
        {
            MessageBox.Show($"{_selectedSerialListIndex}");
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