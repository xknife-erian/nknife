using System.Collections.Generic;
using System.IO;

namespace ScpiKnife
{
    /// <summary>
    /// 指令主题集合
    /// </summary>
    public class ScpiSubjectCollection : List<ScpiSubject>
    {
        /// <summary>
        /// 指令主题所属的仪器品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 指令主题所属的仪器型号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 指令集合所在的文件
        /// </summary>
        public FileInfo File { get; set; }
    }
}