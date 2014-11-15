using System;
using NKnife.App.Cute.Base.Common;

namespace NKnife.App.Cute.Base.EventArg
{
    /// <summary>
    /// 当统计数量发生改变时包含事件数据的类
    /// </summary>
    public class CountChangedEventArgs : EventArgs
    {
        public string Item { get; private set; }
        public Count Count { get; private set; }
        public CountChangedEventArgs(string id, Count count)
        {
            Item = id;
            Count = count;
        }
    }

    public delegate void CountChangedEventHandler(object sender, CountChangedEventArgs e);

}