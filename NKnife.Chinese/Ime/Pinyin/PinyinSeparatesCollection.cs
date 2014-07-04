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
        private readonly StringBuilder _StringBuilder = new StringBuilder();
        private readonly PinyinSeparator _Separator = new PinyinSeparator(DI.Get<ISyllableCollection>());

        public void UpdateSource(IEnumerable<string> src)
        {
            Clear();
            foreach (string word in src)
            {
                Add(word);
            }
        }

        public void AddLetter(string letter)
        {
            _StringBuilder.Append(letter.ToLowerInvariant());
            ProcessPinyin();
        }

        private void ProcessPinyin()
        {
            Clear();

            var result = _Separator.Separate(_StringBuilder.ToString());
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
        }

        public event EventHandler<PinyinCompletedEventArgs> HasCompletePinyin;

        protected virtual void OnHasCompletePinyin(PinyinCompletedEventArgs e)
        {
            EventHandler<PinyinCompletedEventArgs> handler = HasCompletePinyin;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public class PinyinCompletedEventArgs : EventArgs
        {
            public PinyinCompletedEventArgs(List<string> pinyin)
            {
                Pinyin = pinyin;
            }

            public List<string> Pinyin { get; set; }
        }

        public void BackSpaceLetter()
        {
            if (_StringBuilder.Length > 0)
            {
                _StringBuilder.Remove(_StringBuilder.Length - 1, 1);
                ProcessPinyin();
            }
        }

        public void ClearInput()
        {
            _StringBuilder.Clear();
            Clear();
        }
    }
}