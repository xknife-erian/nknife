using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NKnife.Interface;

namespace ScpiKnife
{
    public class ScpiParser : IParser<XmlElement, ScpiCommandList>
    {
        public ScpiCommandList Parse(XmlElement element)
        {
            string isScpiStr = element.GetAttribute("format");
            bool isScpi = true;
            bool.TryParse(isScpiStr, out isScpi);
            XmlNodeList nodes = element.SelectNodes("command[@isConfig='true']");
            if (nodes == null)
                throw new ScpiParseException();

            var cmdlist = new ScpiCommandList();
            var rootCmd = new ScpiCommand(isScpi);
            foreach (XmlElement confEle in nodes)
            {
                rootCmd.Description = confEle.GetAttribute("content");
                rootCmd.Command = confEle.GetAttribute("command");
                if (!confEle.HasChildNodes)
                    continue;
                foreach (XmlElement confContentEle in confEle.ChildNodes)
                {
                    #region config element

                    ScpiCommand cmd = ParseGpibCommand(isScpi, confContentEle, rootCmd.Command);

                    if (!confContentEle.HasChildNodes)
                        continue;

                    #region 有配置子项

                    foreach (XmlElement groupElement in confContentEle.ChildNodes)
                    {
                        if (groupElement.LocalName.ToLower() != "group")
                            continue;
                        if (!groupElement.HasChildNodes)
                            continue;
                        //将所有命令解析成链式后，置入Tag中，待显示时再进行链式生成菜单
                        ScpiCommand groupCmd = ParseGpibCommand(isScpi, groupElement, rootCmd.Command);
                        foreach (XmlElement gpElement in groupElement.ChildNodes)
                        {
                            ScpiCommand gpCmd = ParseGpibCommand(isScpi, gpElement, groupCmd.Command);
                            groupCmd.Next = gpCmd;
                        }
                        //cmd.Tag = groupCmd;
                    }

                    #endregion

                    #endregion
                }
            }
            return cmdlist;
        }

        private static ScpiCommand ParseGpibCommand(bool isScpi, XmlElement element, string rootCmd)
        {
            var cmd = new ScpiCommand(isScpi);
            cmd.Description = element.GetAttribute("content");
            if (element.LocalName == "command")
                cmd.Command = string.Format("{0}:{1}", rootCmd, element.GetAttribute("command"));
            else if (element.LocalName == "param")
                cmd.Command = string.Format("{0} {1}", rootCmd, element.GetAttribute("command"));
            return cmd;
        }

    }
}
