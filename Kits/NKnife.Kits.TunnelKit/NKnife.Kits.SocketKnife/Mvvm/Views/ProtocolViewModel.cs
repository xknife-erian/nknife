﻿using System;
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
        private StringProtocol _SelectedProtocol;

        public ProtocolViewModel()
        {
            Protocols = new ObservableCollection<SimpleProtocol>();
        }

        public ObservableCollection<SimpleProtocol> Protocols { get; set; }

        public StringProtocol SelectedProtocol
        {
            get { return _SelectedProtocol; }
            set
            {
                _SelectedProtocol = value;
                RaisePropertyChanged(() => SelectedProtocol);
            }
        }

        public class SimpleProtocol : ObservableObject
        {
            private string _Command;
            private string _CommandParam;

            public string Command
            {
                get { return _Command; }
                set
                {
                    _Command = value;
                    RaisePropertyChanged(() => Command);
                }
            }

            public string CommandParam
            {
                get { return _CommandParam; }
                set
                {
                    _CommandParam = value;
                    RaisePropertyChanged(() => CommandParam);
                }
            }

            public StringProtocol Protocol { get; set; }
        }
    }
}