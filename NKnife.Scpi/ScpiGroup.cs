using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace ScpiKnife
{
    /// <summary>
    ///     一组指令，将按顺序执行
    /// </summary>
    public class ScpiGroup : LinkedList<ScpiCommand>
    {
        public ScpiCommandGroupCategory Category { get; set; }

        public static ScpiGroup Prase(XmlElement groupElement)
        {
            var group = new ScpiGroup();
            group.Category = ScpiCommandGroupCategory.None;
            if (groupElement.HasAttribute("way"))
            {
                var way = groupElement.GetAttribute("way");
                switch (way)
                {
                    case "init":
                        group.Category = ScpiCommandGroupCategory.Initializtion;
                        break;
                    case "collect":
                        group.Category = ScpiCommandGroupCategory.Collect;
                        break;
                }
            }
            foreach (XmlElement scpiElement in groupElement.ChildNodes)
            {
                var scpiCommand = ScpiCommand.Parse(scpiElement);
                if (scpiCommand == null)
                    continue;
                group.AddLast(scpiCommand);
            }
            return group;
        }

        public void Build(ref XmlElement element)
        {
            element.RemoveAll();
            switch (Category)
            {
                case ScpiCommandGroupCategory.Collect:
                    element.SetAttribute("way", "init");
                    break;
                case ScpiCommandGroupCategory.Initializtion:
                    element.SetAttribute("way", "collect");
                    break;
                case ScpiCommandGroupCategory.None:
                    element.SetAttribute("way", "none");
                    break;
            }
            foreach (ScpiCommand scpiCommand in this)
            {
                Debug.Assert(element.OwnerDocument != null, "element.OwnerDocument != null");
                var commandElement = element.OwnerDocument.CreateElement("scpi");
                scpiCommand.Build(ref commandElement);
                element.AppendChild(commandElement);
            }
        }
    }
}