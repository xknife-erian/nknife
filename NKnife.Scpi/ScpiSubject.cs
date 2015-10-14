using System.Collections.Generic;
using System.Xml;

namespace ScpiKnife
{
    /// <summary>
    /// 面向一个工作指令主题的指令集合
    /// </summary>
    public class ScpiSubject
    {
        public ScpiSubject()
        {
            Collect = new ScpiGroup {Category = ScpiCommandGroupCategory.Collect};
            Preload = new ScpiGroup {Category = ScpiCommandGroupCategory.Initializtion};
        }

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

        public void Build(ref XmlElement element)
        {
        }

        public static IEnumerable<ScpiSubject> Parse(XmlElement scpigroups)
        {
            var subjects = new List<ScpiSubject>();
            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();
                var ele = subjectNode as XmlElement;
                scpiSubject.Description = ele.GetAttribute("description");

                var groupNodes = ele.SelectNodes("group");
                if (groupNodes == null)
                {
                    continue;
                }
                foreach (XmlNode groupNode in groupNodes)
                {
                    if (!(groupNode is XmlElement))
                        continue;
                    var groupElement = (XmlElement) groupNode;
                    if (groupElement.HasAttribute("way"))
                    {
                        var way = groupElement.GetAttribute("way");
                        switch (way)
                        {
                            case "init":
                                scpiSubject.Preload = ScpiGroup.Prase(groupElement);
                                break;
                            case "collect":
                                scpiSubject.Collect = ScpiGroup.Prase(groupElement);
                                break;
                        }
                    }
                }
                subjects.Add(scpiSubject);
            }
            return subjects;
        }
    }
}