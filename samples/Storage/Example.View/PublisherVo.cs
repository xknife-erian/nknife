using Example.Common;

namespace Example.View
{
    public class PublisherVo
    {
        public string Id { get; set; }
        /// <summary>
        /// 书名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出版社创建的年代
        /// </summary>
        public int Year { get; set; }
        /// <summary>
        /// 出版社规模
        /// </summary>
        public Scale Scale { get; set; }
    }
}