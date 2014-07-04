using System.Collections.ObjectModel;
using System.Globalization;
using NKnife.Ioc;

namespace NKnife.Chinese.Ime.Pinyin
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinAlternateCollection : ObservableCollection<string>
    {
        private const int WORD_COUNT = 14;

        public PinyinAlternateCollection()
        {
            var separator = DI.Get<PinyinSeparatesCollection>();
            separator.HasCompletePinyin += SeparatorOnHasCompletePinyin;

            ResetCurrent();
        }

        private void ResetCurrent()
        {
            CurrentPage = 1;
            HasPrevious = false;
            HasLast = false;
        }

        public int CurrentPage { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasLast { get; set; }

        private char[] _CurrentResult;

        private void SeparatorOnHasCompletePinyin(object sender, PinyinSeparatesCollection.PinyinCompletedEventArgs e)
        {
            var pinyin = e.Pinyin;
            _CurrentResult = Pinyin.GetCharArrayOfPinyin(pinyin[0]);
            if (_CurrentResult != null && _CurrentResult.Length > 0)
            {
                ClearAlternates();
                if (_CurrentResult.Length > WORD_COUNT)
                {
                    HasLast = true;
                }
                for (int i = 0; i < WORD_COUNT; i++)
                {
                    if (i<_CurrentResult.Length)
                    {
                        Add(_CurrentResult[CurrentPage * i].ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            if (e.Pinyin.Count ==1 && (_CurrentResult == null || _CurrentResult.Length == 0))
            {
                ClearAlternates();
            }
        }

        /// <summary>
        ///     清空候选词
        /// </summary>
        public void ClearAlternates()
        {
            Clear();
            ResetCurrent();
        }
    }
}