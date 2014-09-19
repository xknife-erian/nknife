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

            var doc = new LayoutDocument();
            doc.Title = "第一个文档";
            MessageDocumentCollection.Add(doc);
        }

        public ObservableCollection<LayoutDocument> MessageDocumentCollection { get; set; }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
