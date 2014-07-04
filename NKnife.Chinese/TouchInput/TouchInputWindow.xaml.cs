using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using WindowsInput;
using WindowsInput.Native;
using NKnife.Chinese.Ime.HandWritten;
using NKnife.Chinese.Ime.Pinyin;
using NKnife.Chinese.TouchInput.Common;
using NKnife.Ioc;
using NKnife.Wrapper.API;
using Button = System.Windows.Controls.Button;

namespace NKnife.Chinese.TouchInput
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TouchInputWindow : Window
    {
        private readonly Params _PanelParams = DI.Get<Params>();
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();

        private Params.InputMode _InputMode = Params.InputMode.Pinyin;

        public TouchInputWindow()
        {
            InitializeComponent();
            Topmost = true;
            ShowInTaskbar = false;
            _HandWriteGrid.Visibility = Visibility.Hidden;

            _PyStrip.AlternateSelected += PyStrip_AlternateSelected;
            _HwStrip.AlternateSelected += HwStrip_AlternateSelected;
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
            HidePyStrip();
            HideHwStrip();
        }

        private void CallHandWriterPanel_Click(object sender, RoutedEventArgs e)
        {
            _PinyinGrid.Visibility = Visibility.Hidden;
            _HandWriteGrid.Visibility = Visibility.Visible;
            HidePyStrip();
            HideHwStrip();
        }

        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            ShowHwStrip();
            DI.Get<HwAlternateCollection>().Recognize(_InkCanvas.Strokes);
        }

        private void HandWriterClearButton_Click(object sender, RoutedEventArgs e)
        {
            _InkCanvas.Strokes.Clear();
            _HwStrip.Hide();
            _HwStripEnable = false;
            DI.Get<HwAlternateCollection>().ClearAlternates();
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
            Params.PlayVoice(Properties.Resources.键_数字);
        }

        private void KeyboardClick(object sender, RoutedEventArgs e)
        {
            Params.PlayVoice(Properties.Resources.键_功能);
        }

        private void LetterClick(object sender, RoutedEventArgs e)
        {
            ShowPyStrip();
            switch (_InputMode)
            {
                case Params.InputMode.Pinyin:
                    var py = DI.Get<PinyinSeparatesCollection>();
                    py.AddLetter(((Button) sender).Content.ToString());
                    break;
                case Params.InputMode.Letter:
                case Params.InputMode.Number:
                case Params.InputMode.HandWriter:
                default:
                    _Simulator.Keyboard.TextEntry(((Button) sender).Content.ToString());
                    break;
            }
            Params.PlayVoice(Properties.Resources.键_全键盘);
        }

        private void SymbolClick(object sender, RoutedEventArgs e)
        {
            Params.PlayVoice(Properties.Resources.键_全键盘);
        }


        #region Show,Hide

        private readonly PyAlternatesStrip _PyStrip = DI.Get<PyAlternatesStrip>();
        private readonly HwAlternatesStrip _HwStrip = DI.Get<HwAlternatesStrip>();
        private readonly CurrentWordStrip _WordPop = DI.Get<CurrentWordStrip>();

        /// <summary>
        ///     拼音候选词词框是否可用（显示）
        /// </summary>
        private bool _PyStripEnable;

        /// <summary>
        ///     手写候选词词框是否可用（显示）
        /// </summary>
        private bool _HwStripEnable;

        private void PyStrip_AlternateSelected(object sender, EventArgs e)
        {
            HidePyStrip();
        }

        private void HwStrip_AlternateSelected(object sender, EventArgs e)
        {
            HideHwStrip();
        }

        private void ShowPyStrip()
        {
            if (!_PyStripEnable)
            {
                double left = 0;
                double top = 0;
                if (_PanelParams.WordsStripLocation.X == 0 && _PanelParams.WordsStripLocation.Y == 0)
                {
                    left = Left;
                    top = Top - _PyStrip.Height - 5;
                }
                else
                {
                    left = _PanelParams.WordsStripLocation.X;
                    top = _PanelParams.WordsStripLocation.Y;
                }
                _PyStrip.Left = left;
                _PyStrip.Top = top;
                _PyStrip.Show();
                _PyStripEnable = true;
                DI.Get<PyAlternateCollection>().ClearAlternates();
                DI.Get<PinyinSeparatesCollection>().ClearInput();
            }
        }

        private void HidePyStrip()
        {
            _PyStripEnable = false;
            _PyStrip.Hide();
        }

        private void ShowHwStrip()
        {
            if (!_HwStripEnable)
            {
                double left = 0;
                double top = 0;
                if (_PanelParams.WordsStripLocation.X == 0 && _PanelParams.WordsStripLocation.Y == 0)
                {
                    left = Left;
                    top = Top - _HwStrip.Height - 5;
                }
                else
                {
                    left = _PanelParams.WordsStripLocation.X;
                    top = _PanelParams.WordsStripLocation.Y;
                }
                _HwStrip.Left = left;
                _HwStrip.Top = top;
                _HwStrip.Show();
                _HwStripEnable = true;
            }
        }

        private void HideHwStrip()
        {
            _HwStripEnable = false;
            _HwStrip.Hide();
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

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            Params.PlayVoice(Properties.Resources.键_功能);
            if (_PyStripEnable)
            {
                _PyStrip.BackSpace();
            }
            else
            {
                _Simulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
            }
        }
    }
}