using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DynamicData.Kernel;
using Ninject;
using NKnife.ChannelKnife.ViewModel;
using ReactiveUI;
using ReactiveUI.Winforms.Legacy;

namespace NKnife.ChannelKnife.Dialogs
{
    public partial class SerialPortSelectorDialog : Form, IViewFor<SerialPortSelectorDialogViewModel>
    {
        public SerialPortSelectorDialog()
        {
            InitializeComponent();
            this.WhenActivated(b =>
                {
                    b(this.OneWayBind(ViewModel, vm => vm.LocalSerials, v => v._ListView.DataSource));
                }
            );

            _ListView.Items.Clear();
//            foreach (var p in SerialUtils.LocalSerialPorts)
//            {
//                var listitem = new ListViewItem(new[] {"", "", p.Key, p.Value});
//                _ListView.Items.Add(listitem);
//            }

            _AcceptButton.Click += (sender, args) =>
            {
                if (_ListView.SelectedItems.Count > 0)
                {
                    var item = _ListView.SelectedItems[0];
                    var port = item.SubItems[2].Text.ToUpper().Trim().TrimStart('C', 'O', 'M');
                    ushort p = 0;
                    ushort.TryParse(port, out p);
                    SerialPort = p;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "请从本机串口列表中选择一个串口。", "未进行串口选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            _CancelButton.Click += (sender, args) =>
            {
                SerialPort = 0;
                DialogResult = DialogResult.Cancel;
            };
        }

        public ushort SerialPort { get; private set; }

        #region Implementation of IViewFor

        /// <inheritdoc />
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as SerialPortSelectorDialogViewModel;
        }

        #endregion

        #region Implementation of IViewFor<SerialPortSelectorDialogViewModel>

        /// <inheritdoc />
        [Inject]
        public SerialPortSelectorDialogViewModel ViewModel { get; set; }

        #endregion
    }
}
