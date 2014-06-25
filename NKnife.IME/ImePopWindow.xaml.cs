using System;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using NKnife.Wrapper.API;

namespace NKnife.IME
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
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {     
            const int GWL_EXSTYLE = (-20);
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            //API.User32.GetWindowLong(hwnd, GWL_EXSTYLE);
            API.User32.SetWindowLong(hwnd, GWL_EXSTYLE, 0x8000000);
            _InkAnalyer = new InkAnalyzer();
            _HintNode = _InkAnalyer.CreateAnalysisHint();
            _HintNode.Guide.Columns = 1;
            _HintNode.Guide.Rows = 1;
            _HintNode.WordMode = true;
            _HintNode.TopInkBreaksOnly = true;
        }

        private void InkCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _HintNode.Location.MakeInfinite();
            _InkAnalyer.RemoveStrokes(InkCanvas.Strokes);
            _InkAnalyer.AddStrokes(InkCanvas.Strokes);
            _InkAnalyer.SetStrokesLanguageId(InkCanvas.Strokes, 0x0804);
            _InkAnalyer.SetStrokesType(InkCanvas.Strokes, StrokeType.Writing);
            AnalysisStatus status = _InkAnalyer.Analyze();
            if (status.Successful)
            {
                for (int i = 0; i < _InkAnalyer.GetAlternates().Count; i++)
                {
                    string resultStr = _InkAnalyer.GetAlternates()[i].RecognizedString;
                    if (resultStr.Length == 1)
                    {
                        TextBox.Text = resultStr;
                    }
                }
            }
        }

    }
}