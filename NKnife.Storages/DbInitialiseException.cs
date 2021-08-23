using System;

namespace NKnife.Storages
{
    /// <summary>
    /// 应该先执行SetConnections方法，设置需要初始化的数据库连接。
    /// </summary>
    public class DbInitialiseException : Exception
    {
        public DbInitialiseException():
            base("应该先执行SetConnections方法，设置需要初始化的数据库连接。")
        {
        }
    }
}