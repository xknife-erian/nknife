using System;
using System.Reactive.Linq;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using Ninject;
using NKnife.ChannelKnife.ViewModel;
using ReactiveUI;

namespace NKnife.ChannelKnife.Views
{
    public partial class PortSelectorDialog : Form, IViewFor<PortSelectorDialogViewModel>
    {
        public PortSelectorDialog()
        {
            InitializeComponent();
            this.WhenActivated(b =>
            {
                b(this.OneWayBind(ViewModel, vm => vm.LocalSerials, v => v._ListView.DataSource));
                b(this.OneWayBind(ViewModel, vm => vm.SelectedSerialListIndex, v => v._ListView.SelectedIndex));
            });


            _ListView.SelectionChanged += (sender, args) =>
            {
                _AcceptButton.Enabled = _ListView.SelectedIndex >= 0;
            };
            _AcceptButton.Click += (sender, args) =>
            {
                if (_ListView.SelectedItems.Count > 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(this, "请从本机串口列表中选择一个串口。", "未进行串口选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            _CancelButton.Click += (sender, args) =>
            {
                DialogResult = DialogResult.Cancel;
            };
        }

        #region Implementation of IViewFor

        /// <inheritdoc />
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = value as PortSelectorDialogViewModel;
        }

        #endregion

        #region Implementation of IViewFor<PortSelectorDialogViewModel>

        /// <inheritdoc />
        [Inject]
        public PortSelectorDialogViewModel ViewModel { get; set; }

        #endregion
    }
}