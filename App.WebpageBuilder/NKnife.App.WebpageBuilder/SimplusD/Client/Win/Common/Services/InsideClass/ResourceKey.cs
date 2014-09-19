using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Jeelu.SimplusD.Client.Win
{
    public static partial class Service
    {
        static public class ResourceKey
        {
            static public string GetChannelIconKey(XmlElement channelNode)
            {
                string strKey = null;
                if (channelNode.GetAttribute("id") == Utility.Const.ChannelRootId)
                {
                    strKey = "channel.img.site";
                }
                else if (channelNode.GetAttribute("is_language") == "true")
                {
                    strKey = "channel.img.language";
                }
                else
                {
                    switch (channelNode.Attributes["channel_type"].Value)
                    {
                        case "1":
                            strKey = "channel.img.type.baseChannel";
                            break;
                        case "2":
                            strKey = "channel.img.type.exChannel";
                            break;
                        case "3":
                            strKey = "channel.img.type.actChannel";
                            break;
                        default:
                            throw new System.Exception("未知的频道类型：" + channelNode.Attributes["channel_type"].Value);
                    }
                }

                return strKey;
            }
        }
    }
}