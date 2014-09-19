using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput;
using NKnife.Chinese.Ime.HandWritten;
using NKnife.Chinese.TouchInput.Common;
using NKnife.Ioc;

namespace NKnife.Chinese.TouchInput
{
    /// <summary>
    ///     HwAlternatesStrip.xaml 的交互逻辑
    /// </summary>
    public partial class HwAlternatesStrip : Window
    {
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();

        public HwAlternatesStrip()
        {
            InitializeComponent();
            _HwAlternatesListBox.ItemsSource = DI.Get<HwAlternateCollection>();
        }

        /// <summary>
        /// 当有候选词选择完成后发生
        /// </summary>
        public event EventHandler AlternateSelected;

        protected virtual void OnAlternateSelected()
        {
            EventHandler handler = AlternateSelected;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        private void AlternatesListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var word = ((TextBlock) sender).Text;
            ShowWordStrip(word, e);
        }

        private void AlternatesListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
            var word = ((TextBlock)sender).Text;
            _Simulator.Keyboard.TextEntry(word);
            Params.PlayVoice(Properties.Resources.划过);
            OnAlternateSelected();//候选词选择完成的事件。手写的字被选中后，待选将被清空。
        }

        private void ShowWordStrip(String word, MouseButtonEventArgs e)
        {
            var strip = DI.Get<CurrentWordStrip>();
            Point point = e.GetPosition(this);

            strip.Left = Left + point.X - strip.Width/2;
            strip.Top = Top + point.Y - strip.Height - 40;

            strip.UpdateText(word);
            strip.Show();
        }

        private void HideWordStrip()
        {
            var strip = DI.Get<CurrentWordStrip>();
            strip.Hide();
            strip.UpdateText("");
        }
    }
}