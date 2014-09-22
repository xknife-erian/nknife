using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.WordSegmentor;
using Jeelu.Billboard;
using System.Windows.Forms;

namespace Jeelu.KeywordResonator.Client
{
    /// <summary>
    /// 窗体应用程序使用前针对各种环境的初始化
    /// </summary>
    internal static class InitializeApplication
    {
        /// <summary>
        /// 初始化的核心方法
        /// </summary>
        internal static void Initialize()
        {
            ///初始资源文件的使用环境
            ResourcesReader.InitializeResources(Service.PathService.Directory.ConfigLanguage, null);
            //Service.Logger.Log(.Add(new Log(LogType.Information,"初始资源文件的使用环境"));
            ///初始软件选项
            Service.Option.InitializeOption(Service.PathService.File.OptionFile);
            ///初始化一个供全局使用的分词类型对象(WordSeg)
            Service.JWordSegmentor = JWordSegmentorService.Creator(
                                                            Service.PathService.Directory.Config,
                                                            Service.PathService.Directory.SegData);
            ///初始化数据库服务
            Service.DbHelper = DbHelper.Creator(Service.PathService.File.SqlXml);
        }
    }
}
