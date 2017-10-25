using System;
using NKnife.Channels.Interfaces;

namespace NKnife.Channels.EventParams
{
    /// <summary>
    /// 包含数据采集源的数据的基类
    /// </summary>
    public class ChannelAnswerDataEventArgs<T> : EventArgs
    {
        public IAnswer<T> Answer { get; }

        public ChannelAnswerDataEventArgs(IAnswer<T> answer)
        {
            Answer = answer;
        }
    }
}