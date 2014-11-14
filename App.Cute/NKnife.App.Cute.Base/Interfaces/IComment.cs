namespace Didaku.Engine.Timeaxis.Base.Interfaces
{
    /// <summary>领域核心实体：评论
    /// </summary>
    public interface IComment
    {
        /// <summary>评论实体在数据库中的Id
        /// </summary>
        long Id { get; set; }

        /// <summary>发起评论的用户ID
        /// </summary>
        string PersonId { get; set; }

        /// <summary>该评论指向的交易<see cref="ITransaction"/>的Id
        /// </summary>
        string TracsactionId { get; set; }

        /// <summary>评论正文
        /// </summary>
        string Text { get; set; }

        /// <summary>评价等级
        /// </summary>
        ushort Level { get; set; }
    }
}