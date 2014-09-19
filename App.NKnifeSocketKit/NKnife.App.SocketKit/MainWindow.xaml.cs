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
using System.Windows.Navigation;
using System.Windows.Shapes;
using NKnife.App.SocketKit.Common;
using NKnife.Ioc;
using Xceed.Wpf.AvalonDock.Layout;

namespace NKnife.App.SocketKit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MessageDocumentCollection = DI.Get<ObservableCollection<LayoutDocument>>();
        }

        public ObservableCollection<LayoutDocument> MessageDocumentCollection { get; set; }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ServerCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var firstDocumentPane = _DockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {
                LayoutDocument doc2 = new LayoutDocument();
                doc2.Title = "Socket服务器端";
                doc2.IsActive = true;
                firstDocumentPane.Children.Add(doc2);
            }
        }

        private void ClientCreatorMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            var firstDocumentPane = _DockingManager.Layout.Descendents().OfType<LayoutDocumentPane>().FirstOrDefault();
            if (firstDocumentPane != null)
            {
                LayoutDocument doc2 = new LayoutDocument();
                doc2.Title = "Socket客户端";
                doc2.IsActive = true;
                firstDocumentPane.Children.Add(doc2);
            }
        }

        private void OptionMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("OptionMenuItem_Click");
        }

        private void ParamsViewMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("ParamsViewMenuItem_Click");
        }

        private void PropertiesViewMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("PropertiesViewMenuItem_Click");
        }

        private void LoggerMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("LoggerMenuItem_Click");
        }

        private void AboutMenuItem_Click(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("AboutMenuItem_Click");
        }
    }
}
