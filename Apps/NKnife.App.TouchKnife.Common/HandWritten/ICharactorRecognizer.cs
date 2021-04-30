using System.Windows.Ink;

namespace NKnife.App.TouchInputKnife.Commons.HandWritten
{
    /// <summary>
    /// 手写识别器接口
    /// </summary>
    public interface ICharactorRecognizer
    {
        /// <summary>
        /// Get 识别器名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 识别
        /// </summary>
        /// <param name="strokes">笔迹集合</param>
        /// <returns>候选词数组</returns>
        string[] Recognize(StrokeCollection strokes);
    }
}
