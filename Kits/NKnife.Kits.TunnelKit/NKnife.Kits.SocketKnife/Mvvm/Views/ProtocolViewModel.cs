using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using NKnife.Protocol.Generic;

namespace NKnife.Kits.SocketKnife.Mvvm.Views
{
    public class ProtocolViewModel : ViewModelBase
    {
        private StringProtocol _selectedProtocol;

        public ProtocolViewModel()
        {
            Protocols = new ObservableCollection<SimpleProtocol>();
        }

        public ObservableCollection<SimpleProtocol> Protocols { get; set; }

        public StringProtocol SelectedProtocol
        {
            get { return _selectedProtocol; }
            set
            {
                _selectedProtocol = value;
                RaisePropertyChanged(() => SelectedProtocol);
            }
        }

        public class SimpleProtocol : ObservableObject
        {
            private string _command;
            private string _commandParam;

            public string Command
            {
                get { return _command; }
                set
                {
                    _command = value;
                    RaisePropertyChanged(() => Command);
                }
            }

            public string CommandParam
            {
                get { return _commandParam; }
                set
                {
                    _commandParam = value;
                    RaisePropertyChanged(() => CommandParam);
                }
            }

            public StringProtocol Protocol { get; set; }
        }
    }
}
