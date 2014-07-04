using System;
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
    public class PyAlternateCollection : ObservableCollection<string>
    {
        private const int WORD_COUNT = 14;

        public PyAlternateCollection()
        {
            var separator = DI.Get<PinyinSeparatesCollection>();
            separator.HasCompletePinyin += SeparatorOnHasCompletePinyin;

            ResetCurrent();
        }

        public int CurrentPage { get; set; }
        public bool Previous { get; set; }
        public bool HasLast { get; set; }

        private char[] _CurrentResult;

        private void SeparatorOnHasCompletePinyin(object sender, PinyinSeparatesCollection.PinyinCompletedEventArgs e)
        {
            var pinyin = e.Pinyin;
            _CurrentResult = Pinyin.GetCharArrayOfPinyin(pinyin[0]);
            if (_CurrentResult != null && _CurrentResult.Length > 0)
            {
                ResetCurrent();
                Clear();
                if (_CurrentResult.Length > WORD_COUNT)
                {
                    HasLast = true;
                }
                for (int i = 0; i < WORD_COUNT; i++)
                {
                    Add(_CurrentResult[CurrentPage * i].ToString(CultureInfo.InvariantCulture));
                }
            }
        }

        private void ResetCurrent()
        {
            CurrentPage = 1;
            Previous = false;
            HasLast = false;
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