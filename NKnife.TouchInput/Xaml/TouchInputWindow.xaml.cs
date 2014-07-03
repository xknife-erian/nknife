using System;
using System.IO;
using System.Media;
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
        private readonly Kernal _PanelParams = DI.Get<Kernal>();
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();

        private Kernal.InputMode _InputMode = Kernal.InputMode.Pinyin;

        public TouchInputWindow()
        {
            InitializeComponent();
            Topmost = true;
            ShowInTaskbar = false;
            _HandWriteGrid.Visibility = Visibility.Hidden;

            _AlternatesStrip.AlternateSelected += Strip_AlternateSelected;
            _HwAlternatesStrip.AlternateSelected += HwStrip_AlternateSelected;
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

        private void Key_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowWordPop(((TextBlock) sender).Text, e);
        }

        private void Key_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordPop();
        }


        private void CallPinyinPanelButton_Click(object sender, RoutedEventArgs e)
        {
            _HandWriteGrid.Visibility = Visibility.Hidden;
            _PinyinGrid.Visibility = Visibility.Visible;
            HideAlternatesStrip();
            HideHwAlternatesStrip();
        }

        private void CallHandWriterPanel_Click(object sender, RoutedEventArgs e)
        {
            _PinyinGrid.Visibility = Visibility.Hidden;
            _HandWriteGrid.Visibility = Visibility.Visible;
            HideAlternatesStrip();
            HideHwAlternatesStrip();
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            ShowHwAlternatesStrip();
            DI.Get<AlternateCollection>().Recognize(_InkCanvas.Strokes);
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Kernal.PlayClickVoice(Properties.Resources.键_手写);
        }

        private void HandWriterClearButton_Click(object sender, RoutedEventArgs e)
        {
            _InkCanvas.Strokes.Clear();
            _HwAlternatesStrip.Hide();
            _HwAlternatesStripEnable = false;
            DI.Get<AlternateCollection>().ClearAlternates();
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
            Kernal.PlayClickVoice(Properties.Resources.键_数字);
        }

        private void KeyboardClick(object sender, RoutedEventArgs e)
        {
            Kernal.PlayClickVoice(Properties.Resources.键_功能);
        }

        private void LetterClick(object sender, RoutedEventArgs e)
        {
            ShowAlternatesStrip();
            switch (_InputMode)
            {
                case Kernal.InputMode.Pinyin:
                    var py = DI.Get<PinyinSpliterCollection>();
                    py.AddLetter(((Button) sender).Content.ToString());
                    break;
                case Kernal.InputMode.Letter:
                case Kernal.InputMode.Number:
                case Kernal.InputMode.HandWriter:
                default:
                    _Simulator.Keyboard.TextEntry(((Button) sender).Content.ToString());
                    break;
            }
            Kernal.PlayClickVoice(Properties.Resources.键_全键盘);
        }

        private void SymbolClick(object sender, RoutedEventArgs e)
        {
            Kernal.PlayClickVoice(Properties.Resources.键_全键盘);
        }


        #region Show,Hide

        private readonly AlternatesStrip _AlternatesStrip = DI.Get<AlternatesStrip>();
        private readonly HwAlternatesStrip _HwAlternatesStrip = DI.Get<HwAlternatesStrip>();
        private readonly CurrentWordStrip _WordPop = DI.Get<CurrentWordStrip>();

        /// <summary>
        ///     拼音候选词词框是否可用（显示）
        /// </summary>
        private bool _AlternatesStripEnable;

        /// <summary>
        ///     手写候选词词框是否可用（显示）
        /// </summary>
        private bool _HwAlternatesStripEnable;

        private void Strip_AlternateSelected(object sender, EventArgs e)
        {
            HideAlternatesStrip();
        }

        private void HwStrip_AlternateSelected(object sender, EventArgs e)
        {
            HideHwAlternatesStrip();
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

        private void HideAlternatesStrip()
        {
            _AlternatesStripEnable = false;
            _AlternatesStrip.Hide();
        }

        private void ShowHwAlternatesStrip()
        {
            if (!_HwAlternatesStripEnable)
            {
                double left = 0;
                double top = 0;
                if (_PanelParams.WordsStripLocation.X == 0 && _PanelParams.WordsStripLocation.Y == 0)
                {
                    left = Left;
                    top = Top - _HwAlternatesStrip.Height - 5;
                }
                else
                {
                    left = _PanelParams.WordsStripLocation.X;
                    top = _PanelParams.WordsStripLocation.Y;
                }
                _HwAlternatesStrip.Left = left;
                _HwAlternatesStrip.Top = top;
                _HwAlternatesStrip.Show();
            }
        }

        private void HideHwAlternatesStrip()
        {
            _HwAlternatesStripEnable = false;
            _HwAlternatesStrip.Hide();
            _InkCanvas.Strokes.Clear();
        }

        private void ShowWordPop(String word, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(this);

            _WordPop.Left = Left + point.X - _WordPop.Width/2;
            _WordPop.Top = Top + point.Y - _WordPop.Height - 40;

            _WordPop.UpdateText(word);
            _WordPop.Show();
        }

        private void HideWordPop()
        {
            _WordPop.Hide();
            _WordPop.UpdateText("");
        }

        #endregion

    }
}