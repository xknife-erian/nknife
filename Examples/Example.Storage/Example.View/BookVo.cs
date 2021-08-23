using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Example.View
{
    public class BookVo
    {
        public string Id { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出版社
        /// </summary>
        public PublisherVo Publisher { get; set; }
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
