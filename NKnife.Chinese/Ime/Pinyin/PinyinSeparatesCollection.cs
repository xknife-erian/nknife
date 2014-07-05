using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NKnife.Ioc;

namespace NKnife.Chinese.Ime.Pinyin
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinSeparatesCollection : ObservableCollection<string>
    {
        private readonly PinyinSeparator _Separator = new PinyinSeparator(DI.Get<ISyllableCollection>());
        private readonly StringBuilder _StringBuilder = new StringBuilder();

        public void UpdateSource(IEnumerable<string> src)
        {
            Clear();
            foreach (string word in src)
            {
                Add(word);
            }
        }

        /// <summary>
        ///     在最后增加一位输入的字母
        /// </summary>
        public void AddLetter(string letter)
        {
            _StringBuilder.Append(letter.ToLowerInvariant());
            PinyinSeparating(); //当增加一个字母以后，进行拼音的分割
        }

        /// <summary>
        ///     开始连续拼音字符串的分割
        /// </summary>
        private void PinyinSeparating()
        {
            Clear(); //
            string target = _StringBuilder.ToString();
            List<string> result = _Separator.Separate(target);
            if (result == null || result.Count <= 0)
            {
                Add(target); //如果结果为空，直接将待处理串添入
            }
            else
            {
                //当结果不为空时
                var i = 0;
                foreach (string r in result)
                {
                    var index = _StringBuilder.IndexOf(r, i);
                    if (index > i)
                    {
                        Add(_StringBuilder.ToString(i, index - i));
                        i += index - i;
                    }
                    Add(r);
                    i += r.Length;
                }
                if (i < _StringBuilder.Length)
                {
                    Add(_StringBuilder.ToString(i, _StringBuilder.Length - i));
                }
                OnPinyinSeparated(new PinyinSeparatedEventArgs(result));
            }
        }

        /// <summary>
        ///     回退连续的拼音字符串，即消除字符串中的最后一位
        /// </summary>
        public void BackSpaceLetter()
        {
            if (_StringBuilder.Length > 0)
            {
                _StringBuilder.Remove(_StringBuilder.Length - 1, 1);
                PinyinSeparating();
            }
        }

        /// <summary>
        ///     完全清除连续的拼音字符串
        /// </summary>
        public void ClearInput()
        {
            _StringBuilder.Clear();
            Clear();
        }

        #region Event

        /// <summary>
        ///     当连续的拼音字符串分割完成时发生
        /// </summary>
        public event EventHandler<PinyinSeparatedEventArgs> PinyinSeparated;

        /// <summary>
        ///     当连续的拼音字符串分割完成时发生
        /// </summary>
        protected virtual void OnPinyinSeparated(PinyinSeparatedEventArgs e)
        {
            EventHandler<PinyinSeparatedEventArgs> handler = PinyinSeparated;
            if (handler != null)
                handler(this, e);
        }

        #endregion
    }
}

/*
            if (result != null && result.Count > 0)
            {
                int i = 0;
                foreach (var r in result)
                {
                    Add(r);
                    i = i + r.Length;
                }
                int k = _StringBuilder.IndexOf(this[0]);
                if (k > 0)
                {
                    Insert(0, _StringBuilder.ToString(0, k));
                    i = i + k;
                }
                if (i < _StringBuilder.Length)
                {
                    var y = _StringBuilder.ToString(i, _StringBuilder.Length - i);
                    Add(y);
                }
                OnHasCompletePinyin(new PinyinCompletedEventArgs(result));
            }
            else
            {
                Add(_StringBuilder.ToString());
            }

*/