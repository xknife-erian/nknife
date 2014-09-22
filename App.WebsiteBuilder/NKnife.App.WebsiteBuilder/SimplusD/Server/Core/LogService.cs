using System;
using System.Collections.Generic;
using System.Text;
using Jeelu.Data;

namespace Jeelu.SimplusD.Server
{
    public class LogService
    {
        private static string serviceIp = "192.168.1.187";
        private static string database = "simplusdinformation";
        
        /// <summary>
        /// 连接数据库
        /// </summary>
        public static void init()
        {
            DataAccess.Initialize(DbProvider.MySql, serviceIp, database, "Zling", "zl123");
        }

        ///记录Sdsite生成时的情况
        public static void WriteBuildSiteLog(SdsiteXmlDocument doc, LogLevel level, string infoStr)
        {
            string dt = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();
            string siteid = doc.DocumentElement.GetAttribute ("id");
            string sql = "insert into site_build_log(SiteID,SiteName,CurrentTime,Level,Information) values(?id,?name,?time,?level,?info)";

            DataAccess.SExecuteNonQuery(sql, siteid, doc.SdsiteName, dt, (int)level, infoStr);
        }

        ///记录Server运行时的情况
        public static void WriteServerRunLog(LogLevel level, string infoStr)
        {
            string dt = DateTime.Now.ToString() + "." + DateTime.Now.Millisecond.ToString();
            string sql = "insert into sd_server_log(CurrentTime,Level,Information) values(?dt,?level,?info)";
            
            DataAccess.SExecuteNonQuery(sql, dt, (int)level, infoStr);
        }

    }
    /// <summary>
    /// Log的级别
    /// </summary>
    public enum LogLevel
    {
        None = 0,
        Fail = 1,
        Error = 2,
        Success = 3,
        Info =4,
    }

}
