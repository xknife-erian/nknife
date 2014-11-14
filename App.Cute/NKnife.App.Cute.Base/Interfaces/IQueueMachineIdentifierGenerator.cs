namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>����һ��ͨ���Ŷӻ�������Ʊʱʶ�����������
    /// </summary>
    public interface IQueueMachineIdentifierGenerator : IIdentifierGenerator
    {

        /// <summary>���зֶα���(��Ʊǰ׺)
        /// </summary>
        string CallHead { get; set; }

        /// <summary>��Ʊ����(�������б���)
        /// </summary>
        ushort TicketLength { get; set; }

        /// <summary>��ǰ�������ɳ�Ʊ����Ϊ0ʱ��ʾ������
        /// </summary>
        ushort TicketMaxCount { get; set; }

        /// <summary>��Ʊ��ʼ����
        /// </summary>
        ushort TicketStartNumber { get; set; }

        /// <summary>��Ʊ��ֹ����
        /// </summary>
        ushort TicketEndNumber { get; set; }

    }
}