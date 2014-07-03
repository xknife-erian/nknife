using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace NKnife.TouchInput.Common
{
    /// <summary>
    ///     一个放置输入法或输入控件产生的待选词(字)的集合
    /// </summary>
    public class InputCharCollection : ObservableCollection<String>
    {
        public void UpdateSource(IEnumerable<string> src)
        {
            Clear();
            foreach (string word in src)
            {
                Add(word);
            }
        }
    }
}