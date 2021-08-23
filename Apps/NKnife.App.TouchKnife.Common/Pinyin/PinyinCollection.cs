using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Common.Logging;
using NKnife.IoC;

namespace NKnife.App.TouchInputKnife.Commons.Pinyin
{
    /// <summary>
    ///     一个放置产生的待选词(拼音)的集合
    /// </summary>
    public class PinyinCollection : ObservableCollection<string>
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();

        private readonly PinyinSeparator _Separator;
        private readonly StringBuilder _StringBuilder = new StringBuilder();

        public PinyinCollection()
        {
            try
            {
                _Separator = new PinyinSeparator(DI.Get<ISyllableCollection>());
                _logger.Info(string.Format("PinyinCollection实例正常"));
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("PinyinCollection实例异常:{0}", e.Message));
            }
        }

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
            Clear(); 
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
        public bool BackSpaceLetter()
        {
            if (_StringBuilder.Length > 0)
            {
                _StringBuilder.Remove(_StringBuilder.Length - 1, 1);
                PinyinSeparating();
                return true;
            }
            return false;
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

        public event EventHandler NoInput;

        protected virtual void OnNoInput()
        {
            EventHandler handler = NoInput;
            if (handler != null) 
                handler(this, EventArgs.Empty);
        }

        #endregion
    }
}