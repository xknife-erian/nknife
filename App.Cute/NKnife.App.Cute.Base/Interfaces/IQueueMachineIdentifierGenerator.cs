namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>描述一个通过排队机产生号票时识别符的生成器
    /// </summary>
    public interface IQueueMachineIdentifierGenerator : IIdentifierGenerator
    {

        /// <summary>队列分段编码(号票前缀)
        /// </summary>
        string CallHead { get; set; }

        /// <summary>号票长度(包含队列编码)
        /// </summary>
        ushort TicketLength { get; set; }

        /// <summary>当前队列最多可出票量，为0时表示不限制
        /// </summary>
        ushort TicketMaxCount { get; set; }

        /// <summary>号票起始数字
        /// </summary>
        ushort TicketStartNumber { get; set; }

        /// <summary>号票终止数字
        /// </summary>
        ushort TicketEndNumber { get; set; }

    }
}