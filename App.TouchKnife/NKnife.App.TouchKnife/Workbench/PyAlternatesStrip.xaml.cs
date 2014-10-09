using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput;
using NKnife.App.TouchKnife.Common;
using NKnife.App.TouchKnife.Common.Pinyin;
using NKnife.App.TouchKnife.Properties;
using NKnife.IoC;

namespace NKnife.App.TouchKnife.Workbench
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
        ///     当有候选词选择完成后发生
        /// </summary>
        public event EventHandler<AlternateSelectedEventArgs> AlternateSelected;

        protected virtual void OnAlternateSelected(AlternateSelectedEventArgs e)
        {
            EventHandler<AlternateSelectedEventArgs> handler = AlternateSelected;
            if (handler != null)
                handler(this, e);
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        private void InputCharListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowWordStrip(((TextBlock) sender).Text, e);
        }

        private void AlternatesListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string word = ((TextBlock) sender).Text;
            ShowWordStrip(word, e);
        }

        private void InputCharListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
        }

        private void AlternatesListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();

            string word = ((TextBlock) sender).Text;
            _Simulator.Keyboard.TextEntry(word);

            DI.Get<Params>().PlayVoice(OwnResources.划过);
            var rs = DI.Get<PinyinAlternateCollection>().CallNextAlternateGroup();
            OnAlternateSelected(new AlternateSelectedEventArgs(rs, word)); //候选词选择完成的事件
        }

        private void HasPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Previous();
        }

        private void HasLastButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Next();
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

        public bool BackSpace()
        {
            var sc = DI.Get<PinyinSeparatesCollection>();
            return sc.BackSpaceLetter();
        }
    }

    public class AlternateSelectedEventArgs : EventArgs
    {
        public AlternateSelectedEventArgs(bool hasAlternate, string selectedWord)
        {
            Word = selectedWord;
            HasAlternate = hasAlternate;
        }

        public string Word { get; set; }
        public bool HasAlternate { get; set; }
    }
}