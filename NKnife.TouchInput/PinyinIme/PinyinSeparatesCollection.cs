using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NKnife.Ioc;
using NKnife.Utility;

namespace NKnife.TouchInput.Common.PinyinIme
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinSeparatesCollection : ObservableCollection<string>
    {
        public StringBuilder _StringBuilder = new StringBuilder();
        public PinyinSeparator _Separator = new PinyinSeparator(DI.Get<ISyllableCollection>());

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
            var result = _Separator.Separate(_StringBuilder.ToString());
            if (result != null && result.Count > 0)
            {
                Clear();
                foreach (var r in result)
                {
                    Add(r);
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