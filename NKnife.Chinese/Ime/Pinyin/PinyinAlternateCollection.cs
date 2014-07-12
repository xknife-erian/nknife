using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using NKnife.Ioc;

namespace NKnife.Chinese.Ime.Pinyin
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinAlternateCollection : ObservableCollection<string>
    {
        private const int WORD_COUNT = 12;

        public PinyinAlternateCollection()
        {
            var separator = DI.Get<PinyinSeparatesCollection>();
            separator.PinyinSeparated += SeparatorOnPinyinSeparated;

            ResetCurrent();
        }

        private void ResetCurrent()
        {
            _CurrentPage = 1;
            HasPrevious = false;
            HasLast = false;
        }

        private int _CurrentPage;

        private bool _HasPrevious;
        public bool HasPrevious
        {
            get { return _HasPrevious; }
            set
            {
                _HasPrevious = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HasPrevious"));
            }
        }

        private bool _HasLast;
        public bool HasLast
        {
            get { return _HasLast; }
            set
            {
                _HasLast = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HasLast"));
            }
        }

        private char[] _CurrentResult;

        /// <summary>
        /// 当拼音框中的拼音已经分割好事件的处理
        /// </summary>
        private void SeparatorOnPinyinSeparated(object sender, PinyinSeparatedEventArgs e)
        {
            var pinyin = e.Pinyin;
            var r = Pinyin.GetCharArrayOfPinyin(pinyin[0]);
            if (r != null && r.Length > 0)
            {
                _CurrentResult = new char[r.Length];
                Array.Copy(r, _CurrentResult, r.Length);
                ClearAlternates();
                if (r.Length > WORD_COUNT)
                {
                    HasLast = true;
                }
                for (int i = 0; i < WORD_COUNT; i++)
                {
                    if (i<r.Length)
                    {
                        Add(r[_CurrentPage * i].ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            if (e.Pinyin.Count ==1 && (r == null || r.Length == 0))
            {
                ClearAlternates();
            }
        }

        public void Previous()
        {

        }

        public void Last()
        {
            Clear();
            _CurrentPage++;
            for (int i = 0; i < WORD_COUNT; i++)
            {
                if (i < _CurrentResult.Length)
                {
                    Add(_CurrentResult[_CurrentPage * i].ToString(CultureInfo.InvariantCulture));
                }
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