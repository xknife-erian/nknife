using System;
using System.Windows;
using System.Windows.Interop;
using NKnife.Ioc;
using NKnife.Wrapper.API;

namespace NKnife.Chinese.TouchInput
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
