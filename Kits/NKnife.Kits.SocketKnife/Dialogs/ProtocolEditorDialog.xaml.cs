using System;
using System.Collections.Generic;
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
        public ProtocolEditorDialog()
        {
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
    }
}
