using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using NKnife.Utility;

namespace NKnife.TouchInput.Common.PinyinIme
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class PinyinSpliterCollection : ObservableCollection<string>
    {
        public StringBuilder _StringBuilder = new StringBuilder();

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
            _StringBuilder.Append(letter.ToLower());
            char[] hanzi = Pinyin.GetCharArrayOfPinyin(_StringBuilder.ToString());
            if (!UtilityCollection.IsNullOrEmpty(hanzi))
            {
                string pinyin = _StringBuilder.ToString();
                if (Count >= 1 && _StringBuilder.Length > 1)
                    SetItem(Count - 1, pinyin);
                else
                    Add(pinyin);
                _StringBuilder.Clear();
                OnHasCompletePinyin(new PinyinCompletedEventArgs(pinyin, hanzi));
            }
            else
            {
                if (Count > 1 && _StringBuilder.Length > 1)
                    SetItem(Count - 1, _StringBuilder.ToString());
                else
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
            public PinyinCompletedEventArgs(string pinyin, char[] hanziArray)
            {
                Pinyin = pinyin;
                HanziArray = hanziArray;
            }

            public string Pinyin { get; set; }
            public char[] HanziArray { get; set; }
        }
    }
}