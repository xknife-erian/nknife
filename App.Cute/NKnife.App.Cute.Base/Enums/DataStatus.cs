using System;

namespace Didaku.Engine.Timeaxis.Base.Enums
{
    /// <summary>
    /// 数据远程持久化状态，表示是否需要补传
    /// </summary>
    [Flags]
    public enum RemotePersistenceStatus : byte
    {
        /// <summary>已上传
        /// </summary>
        Upload = 1,

        /// <summary>从未上传
        /// </summary>
        Never = 2,

        /// <summary>预约后未上传
        /// </summary>
        AfterGetTicket = 4,

        /// <summary>呼叫后未上传
        /// </summary>
        AfterCall = 8,

        /// <summary>工作完成后未上传
        /// </summary>
        AfterClose = 16,

        /// <summary>未上传
        /// </summary>
        None = Never | AfterCall | AfterGetTicket | AfterClose
    }
}