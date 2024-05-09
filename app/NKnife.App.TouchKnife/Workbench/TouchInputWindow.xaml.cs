using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using Common.Logging;
using NKnife.App.TouchInputKnife.Commons;
using NKnife.App.TouchInputKnife.Commons.HandWritten;
using NKnife.App.TouchInputKnife.Commons.Pinyin;
using NKnife.App.TouchInputKnife.Core;
using NKnife.App.TouchInputKnife.Properties;
using NKnife.Interface;
using NKnife.IoC;
using Application = System.Windows.Forms.Application;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace NKnife.App.TouchInputKnife.Workbench
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TouchInputWindow : Window, ITouchInput
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        #region 类成员变量

        private const string ENGLISH = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string SYMBOL = "“)^~}、/\\…|：；》《'%?<”>&({]![";

        private readonly Params _PanelParams = DI.Get<Params>();
        private readonly InputSimulator _Simulator = DI.Get<InputSimulator>();

        private Params.InputMode _InputMode = Params.InputMode.Pinyin;
        private Kernel _Kernel;

        private Point _OwnLocation;
        private Size _OwnSize;

        #endregion

        #region 构造函数及核心的启动

        public TouchInputWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            _OwnLocation = new Point((int) Left, (int) Top);
            _OwnSize = new Size((int) Width, (int) Height);
            LocationChanged += (sender, e) => { _OwnLocation = new Point((int) Left, (int) Top); };
            SizeChanged += (s, e) => { _OwnSize = new Size((int) Width, (int) Height); };
            Topmost = true;
            ShowInTaskbar = false;
            Hide();

            try
            {
                _HwWordListBox.ItemsSource = DI.Get<HwAlternateCollection>();
                _PinyinInputListBox.ItemsSource = DI.Get<PinyinCollection>();
                _PinyinWordPanel.DataContext = DI.Get<PinyinAlternateCollection>();
                _PinyinWordListBox.ItemsSource = DI.Get<PinyinAlternateCollection>();
                _logger.Info(string.Format("绑定拼音源完成."));
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("绑定拼音源出错:{0}", e.Message));
            }

            StartKernel();
        }

        private void StartKernel()
        {
            _Kernel = DI.Get<Kernel>();
            _Kernel.Start(this);
        }

        #endregion

        #region 当最外层Grid载入后，通过Windows的API控制窗体不再获取焦点

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        #endregion

        #region ITouchInput

        public void ShowInputView(short mode, Point location)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ShowInputViewDelegate(SyncShowInputView), mode, location);
        }

        public void HideInputView()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new HideInputViewDelegate(SyncHideInputView));
        }

        public void Exit()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, new ExitDelegate(SyncExit));
        }

        public Size OwnSize
        {
            get { return _OwnSize; }
        }

        public Point OwnLocation
        {
            get { return _OwnLocation; }
        }

        private void SyncShowInputView(short mode, Point location)
        {
            Top = location.Y;
            Left = location.X;
            //1.拼音;2.手写;3.符号;4.小写英文;5.大写英文;6.数字
            switch (mode)
            {
                case 1: //拼音
                    CallPinyinButton_Click(null, null);
                    ChineseAndEnglishFunction(_InputMode);
                    break;
                case 2: //手写
                    CallHandWriterButton_Click(null, null);
                    break;
                case 3: //符号
                    CallPinyinButton_Click(null, null);
                    _InputMode = Params.InputMode.Letter;
                    KeyboardSwitchCase(-1);
                    break;
                case 5: //大写英文
                    CallPinyinButton_Click(null, null);
                    _InputMode = Params.InputMode.Letter;
                    KeyboardSwitchCase(1);
                    break;
                case 4: //小写英文
                case 6: //数字
                    CallPinyinButton_Click(null, null);
                    _InputMode = Params.InputMode.Letter;
                    KeyboardSwitchCase(0);
                    break;
            }
            Show();
        }

        private void SyncHideInputView()
        {
            Hide();
        }

        private void SyncExit()
        {
            if (_Kernel != null)
            {
                _Kernel.Finish();
                Thread.Sleep(100);
            }
            Close();
            try
            {
                Environment.Exit(0);
            }
            catch (Exception)
            {
                Application.Exit();
            }
        }

        private delegate void ExitDelegate();

        private delegate void HideInputViewDelegate();

        private delegate void ShowInputViewDelegate(short mode, Point location);

        #endregion

        #region Tab控制: 手写 or 拼音

        private void CallPinyinButton_Click(object sender, RoutedEventArgs e)
        {
            _InputMode = Params.InputMode.Pinyin;

            _KeyboardGrid.Visibility = Visibility.Visible;
            _HandWriteGrid.Visibility = Visibility.Hidden;

            _PinyinWordGrid.Visibility = Visibility.Visible;
            _HwWordGrid.Visibility = Visibility.Hidden;
        }

        private void CallHandWriterButton_Click(object sender, RoutedEventArgs e)
        {
            _KeyboardGrid.Visibility = Visibility.Hidden;
            _HandWriteGrid.Visibility = Visibility.Visible;

            _PinyinWordGrid.Visibility = Visibility.Hidden;
            _HwWordGrid.Visibility = Visibility.Visible;
        }

        #endregion

        #region 点击键的功能

        /// <summary>数字小键盘输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            var content = button.Content.ToString();
            if (content.Length > 1)
                content = _PointButton.Text;
            _Simulator.Keyboard.TextEntry(content);
            _PanelParams.PlayVoice(OwnResources.键_数字);
        }

        /// <summary>当26个英文字母键的点击时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LetterClick(object sender, RoutedEventArgs e)
        {
            switch (_InputMode)
            {
                case Params.InputMode.Pinyin:
                {
                    KeyboardSwitchCase(1);
                    var py = DI.Get<PinyinCollection>();
                    py.AddLetter(((Button) sender).Content.ToString());
                    break;
                }
                case Params.InputMode.Letter:
                {
                    _Simulator.Keyboard.TextEntry(((Button) sender).Content.ToString());
                    break;
                }
            }
            _PanelParams.PlayVoice(OwnResources.键_全键盘);
        }

        /// <summary>空格键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpaceButtonClick(object sender, RoutedEventArgs e)
        {
            _Simulator.Keyboard.KeyPress(VirtualKeyCode.SPACE);
            _PanelParams.PlayVoice(OwnResources.键_全键盘);
        }

        /// <summary>符号键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SymbolClick(object sender, RoutedEventArgs e)
        {
            _Simulator.Keyboard.TextEntry(((Button) sender).Content.ToString());
            _PanelParams.PlayVoice(OwnResources.键_全键盘);
        }

        /// <summary>回退键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackFunctionButtonClick(object sender, RoutedEventArgs e)
        {
            _PanelParams.PlayVoice(OwnResources.键_功能);
            if (_InputMode == Params.InputMode.Pinyin)
            {
                var pyStrip = DI.Get<PinyinCollection>();
                if (!pyStrip.BackSpaceLetter())
                {
                    _Simulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
                }
            }
            else
            {
                _Simulator.Keyboard.KeyPress(VirtualKeyCode.BACK);
            }
        }

        /// <summary>回车键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterButtonClick(object sender, RoutedEventArgs e)
        {
            _Simulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            _PanelParams.PlayVoice(OwnResources.键_全键盘);
        }

        /// <summary>切换为全符号键盘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JumpToSymbolFunctionButtonClick(object sender, RoutedEventArgs e)
        {
            _InputMode = Params.InputMode.Letter;
            KeyboardSwitchCase(-1);
        }

        /// <summary>切换为全英文大写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpperButtonClick(object sender, RoutedEventArgs e)
        {
            if (_InputMode == Params.InputMode.Pinyin)
            {
                return;
            }
            KeyboardSwitchCase(1);
        }

        /// <summary>中英文切换功能键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChineseAndEnglishFunctionClick(object sender, RoutedEventArgs e)
        {
            _InputMode = (_InputMode == Params.InputMode.Pinyin) ? Params.InputMode.Letter : Params.InputMode.Pinyin;
            ChineseAndEnglishFunction(_InputMode);
        }

        private void ChineseAndEnglishFunction(Params.InputMode inputMode)
        {
            switch (inputMode)
            {
                case Params.InputMode.Letter:
                    _SwitchMainLabel.Text = "中";
                    _SwitchSubLabel.Text = "英语";
                    KeyboardSwitchCase(0);
                    break;
                case Params.InputMode.Pinyin:
                    _SwitchMainLabel.Text = "英";
                    _SwitchSubLabel.Text = "中文";
                    KeyboardSwitchCase(1);
                    break;
            }
        }

        /// <summary>键盘切换（-1:符号; 0:小写; 1:大写）
        /// </summary>
        /// <param name="n">-1:符号; 0:小写; 1:大写</param>
        private void KeyboardSwitchCase(short n)
        {
            foreach (UIElement element in _KeyboardGrid.Children)
            {
                var button = element as Button;
                if (button == null)
                    continue;
                Button btn = button;
                if (btn.Tag == null)
                {
                    continue;
                }
                string c = btn.Tag.ToString().ToUpper();
                if (ENGLISH.Contains(c))
                {
                    switch (n)
                    {
                        case 0:
                            btn.Content = c.ToLower();
                            break;
                        case 1:
                            btn.Content = c.ToUpper();
                            break;
                        case -1:
                            int i = ENGLISH.IndexOf(c, StringComparison.Ordinal);
                            btn.Content = SYMBOL[i];
                            break;
                    }
                }
            }
        }

        #endregion

        #region 手写板，每笔触的触发动作

        //每笔触的触发动作,解析手写
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            DI.Get<HwAlternateCollection>().Recognize(_InkCanvas.Strokes);
        }

        //清除手写板的笔迹
        private void HandWriterClearButton_Click(object sender, RoutedEventArgs e)
        {
            _InkCanvas.Strokes.Clear();
            DI.Get<HwAlternateCollection>().ClearAlternates();
        }

        private void HwWordListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var word = ((TextBlock)sender).Text;
            ShowWordStrip(word, e);
        }

        private void HwWordListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
            var word = ((TextBlock)sender).Text;
            _Simulator.Keyboard.TextEntry(word);
            _PanelParams.PlayVoice(OwnResources.划过);
            DI.Get<HwAlternateCollection>().ClearAlternates();
            _InkCanvas.Strokes.Clear();
        }

        #endregion

        #region 拼音候选条

        private void InputCharListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ShowWordStrip(((TextBlock)sender).Text, e);
        }

        private void PyWordListBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string word = ((TextBlock)sender).Text;
            ShowWordStrip(word, e);
        }

        private void InputCharListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();
        }

        private void PyWordListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            HideWordStrip();

            string word = ((TextBlock)sender).Text;
            _Simulator.Keyboard.TextEntry(word);

            DI.Get<Params>().PlayVoice(OwnResources.划过);
            var rs = DI.Get<PinyinAlternateCollection>().CallNextAlternateGroup();
            
            if (!rs)
            {   
                //候选词选择完成
                DI.Get<PinyinAlternateCollection>().ClearAlternates();
                DI.Get<PinyinCollection>().ClearInput();
            }
        }

        private void HasPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Previous();
        }

        private void HasLastButton_Click(object sender, RoutedEventArgs e)
        {
            DI.Get<PinyinAlternateCollection>().Next();
        }

        #endregion

        #region 选中字提示器

        private readonly CurrentWordStrip _WordPop = DI.Get<CurrentWordStrip>();

        private void ShowWordStrip(String word, MouseButtonEventArgs e)
        {
            System.Windows.Point point = e.GetPosition(this);

            _WordPop.Left = Left + point.X - _WordPop.Width / 2;
            _WordPop.Top = Top + point.Y - _WordPop.Height - 40;

            _WordPop.UpdateText(word);
            _WordPop.Show();
        }

        private void HideWordStrip()
        {
            _WordPop.Hide();
            _WordPop.UpdateText("");
        }

        #endregion

    }
}