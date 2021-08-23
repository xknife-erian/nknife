using System.Collections.Generic;

namespace NKnife.Storages
{
    /// <summary>
    ///     数据组件的连接字符串
    /// </summary>
    public class DomainSqlConfig
    {
        /// <summary>
        ///     预书写的Sql语句集合
        /// </summary>
        public Dictionary<string, DomainSql> SqlMap { get; set; } = new Dictionary<string, DomainSql>();
    }
}