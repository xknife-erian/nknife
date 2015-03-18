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
using GalaSoft.MvvmLight;
using NKnife.IoC;
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

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (CurrentProtocol == null)
                CurrentProtocol = new StringProtocol();
            CurrentProtocol.Command = _CommandTextBox.Text;
            CurrentProtocol.CommandParam = _CommandParamTextBox.Text;
            CurrentProtocol.Infomations.Clear();
            foreach (var data in _ViewModel.PairDatas)
            {
                CurrentProtocol.Infomations.Add(data.Key, data.Value);
            }
            CurrentProtocol.Tags.Clear();
            foreach (var data in _ViewModel.Values)
            {
               CurrentProtocol.Tags.Add(data);
            }

            //TODO:packer unpacker不再是protocol的属性了
//            var packer = (Type)_PackerComboBox.SelectedValue;
//            if (CurrentProtocol.Packer.GetType() != packer)
//                CurrentProtocol.Packer = (StringProtocolPacker)DI.Get(packer);
//            var unPacker = (Type)_UnPackerComboBox.SelectedValue;
//            if (CurrentProtocol.UnPacker.GetType() != unPacker)
//                CurrentProtocol.UnPacker = (StringProtocolUnPacker)DI.Get(unPacker);
            Close();
        }

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

        public class DialogViewModel : ObservableObject
        {
            public DialogViewModel()
            {
                Values = new ObservableCollection<string>();
                PairDatas = new ObservableCollection<ProtocolPairData>();
            }
            public ObservableCollection<string> Values { get; set; }
            public ObservableCollection<ProtocolPairData> PairDatas { get; set; }

            public class ProtocolPairData : ObservableObject
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
