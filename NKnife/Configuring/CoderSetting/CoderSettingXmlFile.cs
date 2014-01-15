using Gean.Xml;

namespace NKnife.Configuring.CoderSetting
{
    /// <summary>
    /// 描述一个CoderSetting（程序员配置）XML文件。
    /// </summary>
    public class CoderSettingXmlFile : AbstractXmlDocument
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoderSettingXmlFile"/> class.
        /// </summary>
        /// <param name="filePath">XML文件的物理绝对路径</param>
        public CoderSettingXmlFile(string filePath)
            : base(filePath)
        {

        }
    }
}
