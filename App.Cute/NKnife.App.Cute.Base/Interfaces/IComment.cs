namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>�������ʵ�壺����
    /// </summary>
    public interface IComment
    {
        /// <summary>����ʵ�������ݿ��е�Id
        /// </summary>
        long Id { get; set; }

        /// <summary>�������۵��û�ID
        /// </summary>
        string PersonId { get; set; }

        /// <summary>������ָ��Ľ���<see cref="ITransaction"/>��Id
        /// </summary>
        string TracsactionId { get; set; }

        /// <summary>��������
        /// </summary>
        string Text { get; set; }

        /// <summary>���۵ȼ�
        /// </summary>
        ushort Level { get; set; }
    }
}