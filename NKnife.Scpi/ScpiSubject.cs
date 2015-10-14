using System.Xml;

namespace ScpiKnife
{
    /// <summary>
    /// 面向一个工作指令主题的指令集合
    /// </summary>
    public class ScpiSubject
    {
        /// <summary>
        /// 工作指令主题的描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 前导指令集合
        /// </summary>
        public ScpiGroup Preload { get; set; }
        /// <summary>
        /// 采集指令集合
        /// </summary>
        public ScpiGroup Collect { get; set; }

        /// <summary>
        /// 指令集合所在的主题集合
        /// </summary>
        public ScpiSubjectCollection OwnerCollection { get; set; }

        public XmlElement BuildXmlElement(ScpisXmlFile xmlFile)
        {
            var element = xmlFile.NewElement("subject");
            return element;
        }
    }
}