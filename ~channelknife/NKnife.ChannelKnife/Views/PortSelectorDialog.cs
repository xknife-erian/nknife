using System;
using System.Reactive.Linq;
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
                b(this.BindCommand(ViewModel, vm => vm.AcceptButtonEnableCommand, v => v._AcceptButton));
            });

            var serialSelectionChanged = Observable.FromEventPattern<EventHandler, EventArgs>(
                handler => handler.Invoke,
                add => _ListView.SelectionChanged += add,
                remove => _ListView.SelectionChanged -= remove);

            serialSelectionChanged.Subscribe(c => ViewModel.SelectedSerialListIndex = _ListView.SelectedIndex);

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