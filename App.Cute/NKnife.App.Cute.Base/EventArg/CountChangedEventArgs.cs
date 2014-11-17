using System;
using NKnife.App.Cute.Base.Common;

namespace NKnife.App.Cute.Base.EventArg
{
    /// <summary>
    /// ��ͳ�����������ı�ʱ�����¼����ݵ���
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