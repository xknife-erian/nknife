using System;
using System.Windows;
using System.Windows.Controls;
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
        private AnalysisHintNode hint;
        private InkAnalyzer theInkAnalyer;

        public ImePopWindow()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {     
            int GWL_EXSTYLE = (-20); // get - retrieves information about a specified window            
            IntPtr HWND = new WindowInteropHelper(this).Handle;
            API.User32.SetWindowLong(HWND, GWL_EXSTYLE, (0x8000000));
            theInkAnalyer = new InkAnalyzer();
            hint = theInkAnalyer.CreateAnalysisHint();
            hint.Guide.Columns = 1;
            hint.Guide.Rows = 1;
            hint.WordMode = true;
            hint.TopInkBreaksOnly = true;
        }

        private void inkCanvs_MouseUp(object sender, MouseButtonEventArgs e)
        {
            hint.Location.MakeInfinite();
            theInkAnalyer.RemoveStrokes(InkCanvas.Strokes);
            theInkAnalyer.AddStrokes(InkCanvas.Strokes);
            theInkAnalyer.SetStrokesLanguageId(InkCanvas.Strokes, 0x0804);
            theInkAnalyer.SetStrokesType(InkCanvas.Strokes, StrokeType.Writing);
            AnalysisStatus status = theInkAnalyer.Analyze();
            if (status.Successful)
            {
                for (int i = 0; i < theInkAnalyer.GetAlternates().Count; i++)
                {
                    //var thisButton = FindName("b" + i) as Button;
                    string resultStr = theInkAnalyer.GetAlternates()[i].RecognizedString;
                    if (resultStr.Length == 1)
                    {
                        //thisButton.Content = resultStr;
                    }
                }
            }
        }
    }
}