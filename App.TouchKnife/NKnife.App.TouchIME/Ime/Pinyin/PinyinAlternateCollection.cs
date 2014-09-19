using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using NKnife.Ioc;

namespace NKnife.App.TouchIme.Ime.Pinyin
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinAlternateCollection : ObservableCollection<string>
    {
        private const int WORD_COUNT = 12;

        private int _CurrentPage;
        private char[] _CurrentResult;
        private List<string> _CurrentPinyinCollection;
        private int _CurrentPinyinIndex;
        private bool _HasLast;

        private bool _HasPrevious;

        public PinyinAlternateCollection()
        {
            var separator = DI.Get<PinyinSeparatesCollection>();
            separator.PinyinSeparated += SeparatorOnPinyinSeparated;
            ResetCurrent();
        }

        public bool HasPrevious
        {
            get { return _HasPrevious; }
            set
            {
                _HasPrevious = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HasPrevious"));
            }
        }

        public bool HasLast
        {
            get { return _HasLast; }
            set
            {
                _HasLast = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HasLast"));
            }
        }

        private void ResetCurrent()
        {
            _CurrentPage = 1;
            HasPrevious = false;
            HasLast = false;
        }

        public bool CallNextAlternateGroup()
        {
            if (_CurrentPinyinCollection.Count <= (_CurrentPinyinIndex + 1))
            {
                _CurrentPinyinIndex = 0;
                return false;
            }
            _CurrentPinyinIndex++;
            char[] r = App.TouchIme.Ime.Pinyin.Pinyin.GetCharArrayOfPinyin(_CurrentPinyinCollection[_CurrentPinyinIndex]);
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
                    if (i < r.Length)
                    {
                        Add(r[_CurrentPage * i].ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            if (r == null || r.Length == 0)
            {
                ClearAlternates();
            }
            return true;
        }

        /// <summary>
        ///     当拼音框中的拼音已经分割好事件的处理
        /// </summary>
        private void SeparatorOnPinyinSeparated(object sender, PinyinSeparatedEventArgs e)
        {
            _CurrentPinyinCollection = e.Pinyin;
            _CurrentPinyinIndex = 0;
            char[] r = App.TouchIme.Ime.Pinyin.Pinyin.GetCharArrayOfPinyin(_CurrentPinyinCollection[_CurrentPinyinIndex]);
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
                    if (i < r.Length)
                    {
                        Add(r[_CurrentPage*i].ToString(CultureInfo.InvariantCulture));
                    }
                }
            }
            if (_CurrentPinyinCollection.Count == 1 && (r == null || r.Length == 0))
            {
                ClearAlternates();
            }
        }

        public void Previous()
        {
            _CurrentPage--;
            FillCurrentResult();
        }

        public void Next()
        {
            FillCurrentResult();
            _CurrentPage++;
        }

        private void FillCurrentResult()
        {
            Clear();
            for (int i = 0; i < WORD_COUNT; i++)
            {
                int index = _CurrentPage*WORD_COUNT + i;
                if (index < _CurrentResult.Length)
                {
                    string c = _CurrentResult[index].ToString();
                    Add(c);
                }
            }
            HasPrevious = _CurrentPage + 1 > 1;
            HasLast = (_CurrentPage)*WORD_COUNT < _CurrentResult.Length;
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