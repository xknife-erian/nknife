using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput;
using NKnife.Ioc;
using NKnife.TouchInput.Common;
using NKnife.TouchInput.Common.PinyinIme;
using NKnife.Wrapper.API;
using Button = System.Windows.Controls.Button;

namespace NKnife.TouchInput.Xaml
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TouchInputWindow : Window
    {
        private readonly AlternatesStrip _AlternatesStrip = DI.Get<AlternatesStrip>();
        private readonly TouchInputPanelParams _PanelParams = DI.Get<TouchInputPanelParams>();
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();

        /// <summary>
        ///     候选词词框是否可用（显示）
        /// </summary>
        private bool _AlternatesStripEnable;

        private TouchInputPanelParams.InputMode _InputMode = TouchInputPanelParams.InputMode.Pinyin;

        public TouchInputWindow()
        {
            InitializeComponent();
            Topmost = true;
            ShowInTaskbar = false;
            _HandWriteGrid.Visibility = Visibility.Hidden;
            _AlternatesStrip.AlternateSelected += Strip_AlternateSelected;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowStartupLocation = WindowStartupLocation.Manual;

            int h = Screen.PrimaryScreen.Bounds.Height;
            Top = h - Height - 40;

            int w = Screen.PrimaryScreen.Bounds.Width;
            Left = (w - Width)/2;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        /// <summary>
        ///     数字小键盘输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            _Simulator.Keyboard.TextEntry(button.Content.ToString());
        }

        private void Key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowWordStrip(((TextBlock) sender).Text, e);
        }

        private void Key_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
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

        private void CallPinyinPanelButton_Click(object sender, RoutedEventArgs e)
        {
            _HandWriteGrid.Visibility = Visibility.Hidden;
            _PinyinGrid.Visibility = Visibility.Visible;
        }

        private void CallHandWriterPanel_Click(object sender, RoutedEventArgs e)
        {
            _PinyinGrid.Visibility = Visibility.Hidden;
            _HandWriteGrid.Visibility = Visibility.Visible;
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            ShowAlternatesStrip();
            DI.Get<AlternateCollection>().Recognize(_InkCanvas.Strokes);
        }

        private void ShowAlternatesStrip()
        {
            if (!_AlternatesStripEnable)
            {
                double left = 0;
                double top = 0;
                if (_PanelParams.WordsStripLocation.X == 0 && _PanelParams.WordsStripLocation.Y == 0)
                {
                    left = Left;
                    top = Top - _AlternatesStrip.Height - 5;
                }
                else
                {
                    left = _PanelParams.WordsStripLocation.X;
                    top = _PanelParams.WordsStripLocation.Y;
                }
                _AlternatesStrip.Left = left;
                _AlternatesStrip.Top = top;
                _AlternatesStrip.Show();
            }
        }

        private void Strip_AlternateSelected(object sender, EventArgs e)
        {
            _InkCanvas.Strokes.Clear();
            _AlternatesStripEnable = false;
            _AlternatesStrip.Hide();
        }

        private void HandWriterClearButton_Click(object sender, RoutedEventArgs e)
        {
            _InkCanvas.Strokes.Clear();
            DI.Get<AlternateCollection>().ClearAlternates();
        }

        private void KeyboardClick(object sender, RoutedEventArgs e)
        {
            ShowAlternatesStrip();
        }

        private void LetterClick(object sender, RoutedEventArgs e)
        {
            ShowAlternatesStrip();
            switch (_InputMode)
            {
                case TouchInputPanelParams.InputMode.Pinyin:
                    var py = DI.Get<PinyinSpliterCollection>();
                    py.AddLetter(((Button) sender).Content.ToString());
                    break;
                case TouchInputPanelParams.InputMode.Letter:
                case TouchInputPanelParams.InputMode.Number:
                case TouchInputPanelParams.InputMode.HandWriter:
                default:
                    _Simulator.Keyboard.TextEntry(((Button) sender).Content.ToString());
                    break;
            }
        }

        private void SymbolClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

    }
}