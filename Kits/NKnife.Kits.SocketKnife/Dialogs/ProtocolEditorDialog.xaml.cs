using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NKnife.Mvvm;
using NKnife.Protocol;
using NKnife.Protocol.Generic;
using NKnife.Protocol.Generic.TextPlain;
using NKnife.Utility;

namespace NKnife.Kits.SocketKnife.Dialogs
{
    /// <summary>
    /// ProtocolEditorDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ProtocolEditorDialog : Window
    {
        private readonly DialogViewModel _ViewModel = new DialogViewModel();

        public ProtocolEditorDialog()
        {
            DataContext = _ViewModel;
            InitializeComponent();

            var packers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IProtocolPacker<>), true);
            foreach (var packer in packers)
            {
                _PackerComboBox.Items.Add(packer);
            }
            _PackerComboBox.SelectedItem = typeof(TextPlainPacker);

            var unpackers = UtilityType.FindTypesByDirectory(AppDomain.CurrentDomain.BaseDirectory, typeof(IProtocolUnPacker<>), true);
            foreach (var unpacker in unpackers)
            {
                _UnPackerComboBox.Items.Add(unpacker);
            }
            _UnPackerComboBox.SelectedItem = typeof(TextPlainUnPacker);
        }

        public StringProtocol CurrentProtocol { get; set; }

        private void AddPair_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new PairDataDialog();
            var dialogResult = dialog.ShowDialog(this);
            if (dialogResult == true)
            {
                _ViewModel.PairDatas.Add(new DialogViewModel.ProtocolPairData(dialog.Key, dialog.Value));
            }
        }

        private void EditPair_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RemovePair_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpPair_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DownPair_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new SingleDataDialog();
            var dialogResult = dialog.ShowDialog(this);
            if (dialogResult == true)
            {
                _ViewModel.Values.Add(dialog.Value);
            }
        }

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Up_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Down_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public class DialogViewModel : NotificationObject
        {
            public DialogViewModel()
            {
                Values = new ObservableCollection<string>();
                PairDatas = new ObservableCollection<ProtocolPairData>();
            }
            public ObservableCollection<string> Values { get; set; }
            public ObservableCollection<ProtocolPairData> PairDatas { get; set; }

            public class ProtocolPairData : NotificationObject
            {
                private string _Key;
                private string _Value;

                public ProtocolPairData()
                {
                    
                }

                public ProtocolPairData(string key, string value)
                {
                    _Key = key;
                    _Value = value;
                }

                public string Key
                {
                    get { return _Key; }
                    set
                    {
                        _Key = value;
                        RaisePropertyChanged(() => Key);
                    }
                }

                public string Value
                {
                    get { return _Value; }
                    set
                    {
                        _Value = value;
                        RaisePropertyChanged(() => Value);
                    }
                }
            }
        }

    }
}
