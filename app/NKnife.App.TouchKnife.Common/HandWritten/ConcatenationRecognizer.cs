using System.Collections.Generic;
using System.Linq;
using System.Windows.Ink;
using System.Windows.Input;

namespace NKnife.App.TouchInputKnife.Commons.HandWritten
{
    /// <summary>
    /// 在识别前将笔迹做一定的连接的识别器，识别率略有提升
    /// </summary>
    public class ConcatenationRecognizer : ICharactorRecognizer
    {
        /// <summary>
        /// Get 识别器名称
        /// </summary>
        public string Name
        {
            get { return "连续笔迹识别器"; }
        }

        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="strokes">笔迹集合</param>
        /// <returns>候选词数组</returns>
        public string[] Recognize(StrokeCollection strokes)
        {
            if (strokes == null || strokes.Count == 0)
                return Params.EmptyAlternates;

            var stroke = GetCombinedStore(strokes);

            var analyzer = new InkAnalyzer();
            analyzer.AddStroke(stroke, Params.SimplifiedChineseLanguageId);
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

            return Params.EmptyAlternates;
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
