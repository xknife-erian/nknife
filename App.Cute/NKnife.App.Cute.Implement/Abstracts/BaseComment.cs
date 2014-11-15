using NKnife.App.Cute.Base.Interfaces;

namespace NKnife.App.Cute.Implement.Abstracts
{
    /// <summary>对一次预约或预约产生的交易进行了评论
    /// </summary>
    public class BaseComment : IComment
    {
        #region Implementation of IComment

        /// <summary>评论实体在数据库中的Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>发起评论的用户ID
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>该评论指向的交易<see cref="ITransaction"/>的Id
        /// </summary>
        public string TracsactionId { get; set; }

        /// <summary>评论正文
        /// </summary>
        public string Text { get; set; }

        /// <summary>评价等级
        /// </summary>
        public ushort Level { get; set; }

        #endregion
    }
}
