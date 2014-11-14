using System;

namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>记录一个时间轴中每个动作的交易信息
    /// </summary>
    public interface ITransaction
    {
        string Id { get; set; }

        /// <summary>识别码。这个识别码仅限定在短暂的时间内(当天，中午等)是不重复的。在排队系统中，这是一个排队的票号。
        /// </summary>
        string Identifier { get; set; }

        /// <summary>预约的队列的Id
        /// </summary>
        string Queue { get; set; }

        /// <summary>记录交易的开始时间。
        /// </summary>
        DateTime Time { get; set; }

        /// <summary>用户Id
        /// </summary>
        string User { get; set; }

        /// <summary>生成这条交易的Activity的Kind
        /// </summary>
        int Owner { get; set; }

        /// <summary>前置的Transaction的ID的集合
        /// </summary>
        string[] Previous { get; set; }
    }
}