using System.Collections.Specialized;
using System.Xml;

namespace NKnife.AutoUpdater.Interfaces
{
    public interface IUpdaterFileVerify
    {
        /// <summary>从索引文件中获取文件校验器（C#类）的版本号
        /// </summary>
        bool VerifyIndexFileVerifyVersion(XmlNode updateIndex, out string version);

        /// <summary>根据指定的XML检查自动更新程序是否需要更新
        /// </summary>
        bool VerifyUpdater(XmlNode updateIndex, out string targetFileName);

        /// <summary>校验主程序是否需要更新，并返回需要更新的压缩包文件
        /// </summary>
        bool VerifyMainApplication(XmlNode updateIndex, out string targetFileName);

        /// <summary>校验是否有Patch文件(补丁程序)，如果有，返回补丁程序的文件的集合
        /// </summary>
        bool VerifyPatchs(XmlNode updateIndex, out StringCollection patchs);
    }
}