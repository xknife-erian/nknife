using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    internal class SeparatorXmlElement : XmlElement
    {
        public SeparatorXmlElement(XmlDocument doc)
            : base(string.Empty, "Separator", string.Empty, doc) { }

        /// <summary>
        /// 控件的位置，如:菜单、工具栏，两者都在
        /// </summary>
        internal ControlPlace Place
        {
            get
            {
                if (!this.HasAttribute("place"))
                {
                    return ControlPlace.Nothing;
                }
                string place = this.GetAttribute("place");
                switch (place)
                {
                    case "ToolBar|Menu":
                    case "Menu|ToolBar":
                        return ControlPlace.MenuAndToolbar;
                    case "Menu":
                        return ControlPlace.Menu;
                    case "ToolBar":
                        return ControlPlace.ToolBar;
                    case "":
                    default:
                        return ControlPlace.Nothing;
                }
            }
        }

    }
}
