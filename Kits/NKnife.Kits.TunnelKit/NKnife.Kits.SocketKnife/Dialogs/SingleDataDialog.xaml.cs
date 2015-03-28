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

namespace NKnife.Kits.SocketKnife.Dialogs
{
    /// <summary>
    /// SingleDataDialog.xaml 的交互逻辑
    /// </summary>
    public partial class SingleDataDialog : Window
    {
        public SingleDataDialog()
        {
            InitializeComponent();
        }

        public string Value
        {
            get { return _ValueTextBox.Text; }
            set { _ValueTextBox.Text = value; }
        }

        private void _ConfirmButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void _CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
