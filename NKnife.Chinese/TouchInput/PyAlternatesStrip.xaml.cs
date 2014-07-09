using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput;
using NKnife.Chinese.Ime.Pinyin;
using NKnife.Chinese.TouchInput.Common;
using NKnife.Ioc;
using NKnife.Wrapper.API;

namespace NKnife.Chinese.TouchInput
{
    /// <summary>
    ///     PyAlternatesStrip.xaml 的交互逻辑
    /// </summary>
    public partial class PyAlternatesStrip : Window
    {
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();


        public PyAlternatesStrip()
        {
            InitializeComponent();
            _AlternatesListBox.ItemsSource = DI.Get<PinyinAlternateCollection>();
            _InputCharListBox.ItemsSource = DI.Get<PinyinSeparatesCollection>();
            _HasLastButton.DataContext = DI.Get<PinyinAlternateCollection>();
            _HasPreviousButton.DataContext = DI.Get<PinyinAlternateCollection>();
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
            API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        private void InputCharListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowWordStrip(((TextBlock) sender).Text, e);
        }

        private void AlternatesListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var word = ((TextBlock) sender).Text;
            ShowWordStrip(word, e);
        }

        private void InputCharListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
        }

        private void AlternatesListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();

            var word = ((TextBlock)sender).Text;
            _Simulator.Keyboard.TextEntry(word);

            Params.PlayVoice(Properties.Resources.划过);
            OnAlternateSelected();//候选词选择完成的事件
        }

        private void HasPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Previous();
        }

        private void HasLastButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Last();
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

        public void BackSpace()
        {
            var sc = DI.Get<PinyinSeparatesCollection>();
            sc.BackSpaceLetter();
        }


    }

}