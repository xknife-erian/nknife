using System;
using Example.Common;

namespace Example.View
{
    /// <summary>
    /// 书籍购买记录
    /// </summary>
    public class BuyingRecordVo
    {
        public string Id { get; set; }
        /// <summary>
        /// 书籍的原拥有者
        /// </summary>
        public PersonVo OriginalOwner { get; set; }
        /// <summary>
        /// 书籍的现拥有者
        /// </summary>
        public PersonVo CurrentOwner { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradingTime { get; set; }
        /// <summary>
        /// 交易价格
        /// </summary>
        public float TradingPrice { get; set; }
        /// <summary>
        /// 本次交易的书籍
        /// </summary>
        public string Book { get; set; }
    }
}