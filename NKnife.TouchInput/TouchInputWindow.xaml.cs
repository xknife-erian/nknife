using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using NKnife.Wrapper.API;
using Button = System.Windows.Controls.Button;

namespace NKnife.TouchInput
{
    /// <summary>
    ///     MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImePopWindow : Window
    {
        private AnalysisHintNode _HintNode;
        private InkAnalyzer _InkAnalyer;

        public ImePopWindow()
        {
            InitializeComponent();
            Topmost = true;
            ShowInTaskbar = false;
            //MouseDown += Window_MouseDown;
            //int height = Screen.PrimaryScreen.Bounds.Height;
            //int width = Screen.PrimaryScreen.Bounds.Width;
            //Width = width - 50;
            //Height = height*0.4;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //注意：如果在构造函数中，窗体的句柄无法获得
            //通过WinAPI的调用控制窗体不再获取焦点
            const int GWL_EXSTYLE = (-20);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            API.User32.SetWindowLong(handle, GWL_EXSTYLE, 0x8000000);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
//            if (e.LeftButton == MouseButtonState.Pressed)
//            {
//                DragMove();
//            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
//            _InkAnalyer = new InkAnalyzer();
//            _HintNode = _InkAnalyer.CreateAnalysisHint();
//            _HintNode.Guide.Columns = 1;
//            _HintNode.Guide.Rows = 1;
//            _HintNode.WordMode = true;
//            _HintNode.TopInkBreaksOnly = true;
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _HintNode.Location.MakeInfinite();
            _InkAnalyer.RemoveStrokes(_InkCanvas.Strokes);
            _InkAnalyer.AddStrokes(_InkCanvas.Strokes);
            _InkAnalyer.SetStrokesLanguageId(_InkCanvas.Strokes, 0x0804);
            _InkAnalyer.SetStrokesType(_InkCanvas.Strokes, StrokeType.Writing);
            AnalysisStatus status = _InkAnalyer.Analyze();
            if (status.Successful)
            {
                AnalysisAlternateCollection alternates = _InkAnalyer.GetAlternates();
                foreach (AnalysisAlternate t in alternates)
                {
                    string resultStr = t.RecognizedString;
                    if (resultStr.Length == 1)
                    {
                        //TextBox.Text =  TextBox.Text+ resultStr;
                    }
                }
            }
        }

        private void NumberButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button) sender;
            SendKeys.SendWait(button.Content.ToString());
            //_TextBox.Text = button.Content.ToString();
        }

        private void ButtonLostFocus(object sender, RoutedEventArgs e) { e.Handled = false; }

    }
}