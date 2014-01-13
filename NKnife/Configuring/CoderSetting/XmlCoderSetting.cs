using System;
using System.Xml;
using NLog;

namespace Gean.Configuring.CoderSetting
{
    /// <summary>内容保存在XML中的CoderSetting（程序员配置）的抽象类
    /// </summary>
    public abstract class XmlCoderSetting : CoderSetting<XmlElement>
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        /// <summary>Gets or sets 选项数据所在的XML文件中的Element节点。
        /// </summary>
        /// <value>The element.</value>
        protected XmlElement Element { get; private set; }

        /// <summary>初始化选项数据
        /// </summary>
        /// <param name="source">The source.</param>
        protected override void Initializes(XmlElement source)
        {
            Element = source;
            base.Initializes(source);
        }

        /// <summary>保存设定的选项值
        /// </summary>
        /// <returns></returns>
        public virtual bool Save()
        {
            try
            {
                string klass = Element.GetAttribute("class");
                CoderSettingXmlFile file = CoderSettingService.ME.XmlFileMap[klass];
                file.Save();
                _Logger.Info(string.Format("选项数据所在的XmlElement保存成功。{0}", file.FilePath));
                return true;
            }
            catch (Exception e)
            {
                _Logger.Error(
                    string.Format("选项数据所在的XmlElement保存时发生异常。数据内容:{0}。\r\n异常信息:{1}", Element.OuterXml, e.Message), e);
                return false;
            }
        }
    }
}