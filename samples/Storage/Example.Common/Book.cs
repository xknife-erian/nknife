using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Example.Common
{
    /// <summary>
    /// 收藏的书籍
    /// </summary>
    [Table(nameof(Book))]
    public class Book
    {
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        [StringLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// 出版社
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 书籍的标准ISBN编号
        /// </summary>
        public string ISBN { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public float Price { get; set; }
    }
}