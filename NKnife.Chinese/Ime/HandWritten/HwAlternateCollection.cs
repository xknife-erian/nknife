using System.Collections.ObjectModel;
using System.Windows.Ink;
using NKnife.Ioc;

namespace NKnife.Chinese.Ime.HandWritten
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class HwAlternateCollection : ObservableCollection<string>
    {
        public void Recognize(StrokeCollection sc)
        {
            string[] alts = DI.Get<ICharactorRecognizer>().Recognize(sc);
            ClearAlternates();
            foreach (string alt in alts)
                Add(alt);
        }

        /// <summary>
        ///     清空候选词
        /// </summary>
        public void ClearAlternates()
        {
            Clear();
        }
    }
}