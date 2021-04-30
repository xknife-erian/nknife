using System.Collections.Generic;
using Google.Protobuf.WellKnownTypes;

namespace NKnife.Storages
{
    /// <summary>
    /// 平台预书写的SQL语句集合，Key是数据库类型
    /// </summary>
    public class SqlSetMap : Dictionary<DatabaseType, SqlSet>
    {
    }

    public class SqlSet
    {
        public SubSqlSet Insert { get; set; } = new SubSqlSet();
        public SubSqlSet Table { get; set; } = new SubSqlSet();
        public SubSqlSet Update { get; set; } = new SubSqlSet();

        public class SubSqlSet : Dictionary<string, string>
        {
        }
    }
    //
    // public class DomainSql
    // {
    //     public string TypeName { get; set; }
    //     public Type Type { get; set; }
    //     public Dictionary<DatabaseType, string> CreateTable { get; set; } = new Dictionary<DatabaseType, string>(1);
    //     public Dictionary<DatabaseType, string> Insert { get; set; } = new Dictionary<DatabaseType, string>(1);
    //     public Dictionary<DatabaseType, string> Update { get; set; } = new Dictionary<DatabaseType, string>(1);
    // }
}
