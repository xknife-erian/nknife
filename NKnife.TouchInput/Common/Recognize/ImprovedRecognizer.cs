using System.Collections.Generic;
using System.Linq;
using System.Windows.Ink;
using System.Windows.Input;

namespace NKnife.TouchInput.Common.Recognize
{
    /// <summary>
    /// 改进的识别器
    /// </summary>
    public class ImprovedRecognizer : ICharactorRecognizer
    {
        /// <summary>
        /// Get 识别器名称
        /// </summary>
        public string Name
        {
            get { return "改进的识别器"; }
        }

        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="strokes">笔迹集合</param>
        /// <returns>候选词数组</returns>
        public string[] Recognize(StrokeCollection strokes)
        {
            if (strokes == null || strokes.Count == 0)
                return TouchInputPanelParams.EmptyAlternates;

            var stroke = GetCombinedStore(strokes);

            var analyzer = new InkAnalyzer();
            analyzer.AddStroke(stroke, TouchInputPanelParams.SimplifiedChineseLanguageId);
            analyzer.SetStrokeType(stroke, StrokeType.Writing);

            var status = analyzer.Analyze();
            if (status.Successful)
            {
                var result = analyzer.GetAlternates()
                    .OfType<AnalysisAlternate>()
                    .Select(x => x.RecognizedString)
                    .ToArray();
                analyzer.Dispose();
                return result;
            }

            analyzer.Dispose();

            return TouchInputPanelParams.EmptyAlternates;
        }

        private static Stroke GetCombinedStore(IEnumerable<Stroke> strokes)
        {
            var points = new StylusPointCollection();
            foreach (var stroke in strokes)
            {
                points.Add(stroke.StylusPoints);
            }
            return new Stroke(points);
        }
    }
}
