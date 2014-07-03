using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Ink;
using NKnife.Ioc;
using NKnife.TouchInput.Common.PinyinIme;
using NKnife.TouchInput.Common.Recognize;

namespace NKnife.TouchInput.Common
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class AlternateCollection : ObservableCollection<string>
    {
        public AlternateCollection()
        {
            var py = DI.Get<PinyinSpliterCollection>();
            py.HasCompletePinyin += PinyinSpliter_HasCompletePinyin;
        }

        private void PinyinSpliter_HasCompletePinyin(object sender, PinyinSpliterCollection.PinyinCompletedEventArgs e)
        {
            ClearAlternates();
            foreach (var hanzi in e.HanziArray)
            {
                Add(hanzi.ToString(CultureInfo.InvariantCulture));
            }
        }

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