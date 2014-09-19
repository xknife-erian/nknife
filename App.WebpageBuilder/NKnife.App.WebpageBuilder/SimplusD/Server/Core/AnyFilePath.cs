using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.IO;

namespace Jeelu.SimplusD.Server
{
    public static class AnyFilePath
    {
        static AnyFilePath()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            SdSiteFilePath = config.AppSettings.Settings["SdsitPath"].Value;
            SdWebFilePath=config.AppSettings.Settings["WebPath"].Value;
        }
        public static string SdSiteFilePath ;
        public static string SdWebFilePath ;

        /// <summary>
        /// 返回服务器sdsites文件夹的绝对路径;如：E:\TestServer\Sdsites\zli\zling\uuuuuuu\sdsites\
        /// </summary>
        /// <param name="partPath"></param>
        /// <returns></returns>
        public static string GetSdsitesFolderAbsolutePath(string partPath)
        {
            string sdsitesPath = Path.Combine(SdSiteFilePath, partPath) + @"\" + CommonService.Sdsites + @"\";
            return sdsitesPath;
        }
        /// <summary>
        /// 返回服务器Temp文件夹的绝对路径;如：E:\TestServer\Sdsites\zli\zling\uuuuuuu\temp\
        /// </summary>
        /// <param name="partPath"></param>
        /// <returns></returns>
        public static string GetTempFolderAbsolutePath(string partPath)
        {
            string tempPath = Path.Combine(SdSiteFilePath, partPath) + @"\" + CommonService.Temp + @"\";
            return tempPath;
        }

        /// <summary>
        /// 返回项目在web服务器上的绝对路径;如:Y:\zha\zhangling\uuuuuuu
        /// </summary>
        /// <param name="partPath"></param>
        /// <returns></returns>
        public static string GetWebAbsolutePath(string partPath)
        {
            string webPath = Path.Combine(SdWebFilePath, partPath);
            return webPath;
        }
    }
}
