using System;
using System.Collections.Generic;

namespace NKnife.Storages
{
    /// <summary>
    ///     面向一个实体类型的读写分离的数据库设置
    /// </summary>
    public class DomainSql
    {
        /// <summary>
        ///     实体类的类名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        ///     实体类类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        ///     当前数据库类型
        /// </summary>
        public DatabaseType CurrentDbType { get; set; }

        /// <summary>
        ///     数据库名
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        ///     写数据库的连接字符串
        /// </summary>
        public string Write { get; set; }

        /// <summary>
        ///     读数据库的连接字符串
        /// </summary>
        public string Read { get; set; }

        /// <summary>
        ///     该类型面向不同数据库的建表语句
        /// </summary>
        public Dictionary<DatabaseType, string> CreateTable { get; set; } = new Dictionary<DatabaseType, string>(1);

        /// <summary>
        ///     该类型数据表中的默认数据字典。Key是数据库类型，Value是用来插入默认数据的SQL语句。
        /// </summary>
        public Dictionary<DatabaseType, string> DefaultData { get; set; } = new Dictionary<DatabaseType, string>(1);

        /// <summary>
        ///     该类型面向不同数据库的插入语句
        /// </summary>
        public Dictionary<DatabaseType, string> Insert { get; set; } = new Dictionary<DatabaseType, string>(1);

        /// <summary>
        ///     该类型面向不同数据库的更新语句
        /// </summary>
        public Dictionary<DatabaseType, string> Update { get; set; } = new Dictionary<DatabaseType, string>(1);
    }
}