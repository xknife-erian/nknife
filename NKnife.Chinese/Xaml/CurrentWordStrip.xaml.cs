using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NKnife.Ioc;
using NKnife.TouchInput.Common;
using NKnife.Wrapper.API;

namespace NKnife.TouchInput.Xaml
{
    /// <summary>
    /// CurrentWordStrip.xaml 的交互逻辑
    /// </summary>
    public partial class CurrentWordStrip : Window
    {
        public CurrentWordStrip()
        {
            InitializeComponent();
        }

        public void UpdateText(string currentWord)
        {
            DI.Get<Kernal>().CurrentWord = currentWord;
            _TextBlock.Text = DI.Get<Kernal>().CurrentWord;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }
    }
}
