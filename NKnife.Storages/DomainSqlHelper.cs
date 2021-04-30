using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using Newtonsoft.Json;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace NKnife.Storages
{
    /// <summary>
    /// 生成DomainSql配置的帮助类。
    /// </summary>
    public static class DomainSqlHelper
    {
        private static readonly Dictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>> _TypePropertiesMap = new Dictionary<RuntimeTypeHandle, IEnumerable<PropertyInfo>>();
        private static readonly Dictionary<RuntimeTypeHandle, string> _TypeTableNameMap = new Dictionary<RuntimeTypeHandle, string>();

        private static readonly Dictionary<DatabaseType, string> _CommonWriteConnMap = new Dictionary<DatabaseType, string>();
        private static readonly Dictionary<DatabaseType, string> _CommonReadConnMap = new Dictionary<DatabaseType, string>();

        /// <summary>
        /// 增加通用的写数据库字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connString">数据库连接字符串</param>
        public static void AddCommonWriteConn(DatabaseType dbType, string connString)
        {
            if (_CommonWriteConnMap.ContainsKey(dbType))
                _CommonWriteConnMap[dbType] = connString;
            else
                _CommonWriteConnMap.Add(dbType, connString);
        }

        /// <summary>
        /// 增加通用的读数据库字符串
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connString">数据库连接字符串</param>
        public static void AddCommonReadConn(DatabaseType dbType, string connString)
        {
            if (_CommonReadConnMap.ContainsKey(dbType))
                _CommonReadConnMap[dbType] = connString;
            else
                _CommonReadConnMap.Add(dbType, connString);
        }

        /// <summary>
        /// 生成指定实体类型的数据库管理配置类。
        /// </summary>
        /// <param name="domain">指定实体类</param>
        /// <param name="dbType">数据库类型</param>
        /// <param name="defalutData">默认数据，该实体类的实例集合。默认为空</param>
        /// <param name="writeConn">写字符串，默认为空，当不指定时，采用通用的连接字符串</param>
        /// <param name="readConn">读字符串，默认为空，当不指定时，采用通用的连接字符串</param>
        /// <returns>指定实体类型的数据库管理配置类</returns>
        public static DomainSql Build(Type domain, DatabaseType dbType, object[] defalutData = null, string writeConn = "", string readConn = "")
        {
            var ds = new DomainSql
            {
                Type = domain,
                TypeName = domain.Name,
                CurrentDbType = dbType
            };
            if (!string.IsNullOrEmpty(writeConn))
                ds.Write = writeConn;
            else if (_CommonWriteConnMap.ContainsKey(dbType))
                ds.Write = _CommonWriteConnMap[dbType];
            if (!string.IsNullOrEmpty(readConn))
                ds.Read = readConn;
            else if (_CommonReadConnMap.ContainsKey(dbType))
                ds.Read = _CommonReadConnMap[dbType];

            var sql = GetCreateTableSql(dbType, domain);
            ds.CreateTable.Add(dbType, sql);
            sql = GetInsertTemplateSql(dbType, domain);
            ds.Insert.Add(dbType, sql);
            sql = GetUpdateTemplateSql(dbType, domain);
            ds.Update.Add(dbType, sql);

            if (null != defalutData && defalutData.Length > 0)
            {
                var data = GetInsertDefaultDataSql(dbType, defalutData);
                ds.DefaultData.Add(dbType, data);
            }

            return ds;
        }

        public static string GetCreateTableSql(DatabaseType dbType, Type type)
        {
            var name = GetTableName(type);
            var allProperties = TypePropertiesCache(type);

            var sbTableSql = new StringBuilder();
            var sbIndexSql = new StringBuilder();
            for (var i = 0; i < allProperties.Count; i++)
            {
                var ps = allProperties[i];
                sbTableSql.Append($"\t{ps.Name}");
                SetDbType(dbType, sbTableSql, ps);

                if (HasKeyAttribute(ps) || IsId(ps)) sbTableSql.Append(" PRIMARY KEY");

                if (HasIndexAttribute(ps)) sbIndexSql.Append($"\rCREATE INDEX {ps.Name} ON {name}({ps.Name});");

                if (IsNotNull(ps)) sbTableSql.Append(" NOT NULL");

                if (i < allProperties.Count - 1) sbTableSql.Append(",\r");
            }

            var sql = $"CREATE TABLE {name} (\r{sbTableSql}\r);";
            if (sbIndexSql.Length > 0)
                sql = $"{sql}{sbIndexSql}";
            return sql;
        }

        /// <summary>
        ///     根据类属性的类型设置数据库字段类型
        /// </summary>
        /// <param name="databaseType"></param>
        /// <param name="sb">sql语句</param>
        /// <param name="p">属性</param>
        private static void SetDbType(DatabaseType databaseType, StringBuilder sb, PropertyInfo p)
        {
            var pt = p.PropertyType;
            if (pt == typeof(int) || pt == typeof(uint) || pt == typeof(short) || pt == typeof(ushort))
            {
                sb.Append(" INT");
            }
            else if (pt == typeof(long) || pt == typeof(ulong))
            {
                sb.Append(" BIGINT");
            }
            else if (pt == typeof(float) || pt == typeof(double) || pt == typeof(decimal))
            {
                sb.Append(" FLOAT");
            }
            else if (pt == typeof(DateTime))
            {
                sb.Append(" DATETIME");
            }
            else if (pt.IsEnum || pt == typeof(bool))
            {
                switch (databaseType)
                {
                    case DatabaseType.MySql:
                        sb.Append(" SMALLINT");
                        break;
                    default:
                        sb.Append(" TINYINT");
                        break;
                }
            }
            else
            {
                var attr = Attribute.GetCustomAttribute(p, typeof(MaxLengthAttribute));
                if (attr != null && attr is MaxLengthAttribute attribute && attribute.Length < 250)
                {
                    var length = attribute.Length;
                    sb.Append(length <= 0 ? " CHAR" : $" CHAR({length})");
                }
                else
                {
                    sb.Append(" TEXT");
                }
            }
        }

        private static bool IsNotNull(PropertyInfo propertyInfo)
        {
            var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(RequiredAttribute));
            return attr != null;
        }

        private static bool IsId(PropertyInfo propertyInfo)
        {
            return propertyInfo.Name == "Id";
        }

        private static bool HasKeyAttribute(PropertyInfo propertyInfo)
        {
            var attr = Attribute.GetCustomAttribute(propertyInfo, typeof(KeyAttribute));
            return attr != null;
        }

        private static bool HasIndexAttribute(PropertyInfo ps)
        {
            var attr = Attribute.GetCustomAttribute(ps, typeof(IndexAttribute));
            return attr != null;
        }

        public static string GetInsertTemplateSql(DatabaseType dbType, Type type)
        {
            var name = GetTableName(type);
            var allProperties = TypePropertiesCache(type);

            var sbColumnList = new StringBuilder(string.Empty);
            var sbParameterList = new StringBuilder(string.Empty);
            for (var i = 0; i < allProperties.Count; i++)
            {
                var property = allProperties[i];
                sbColumnList.Append(property.Name);
                sbParameterList.AppendFormat("@{0}", property.Name);
                if (i < allProperties.Count - 1)
                {
                    sbColumnList.Append(", ");
                    sbParameterList.Append(", ");
                }
            }

            return $"INSERT INTO {name}(\r\t{sbColumnList}\r) Values(\r\t{sbParameterList}\r);";
        }

        /// <summary>
        /// 生成插入默认数据的Sql语句
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="domains">该数据实体类型的实例集合</param>
        /// <returns>Sql语句</returns>
        public static string GetInsertDefaultDataSql(DatabaseType dbType, params object[] domains)
        {
            var sb = new StringBuilder();
            foreach (var domain in domains)
            {
                var type = domain.GetType();
                var name = GetTableName(type);
                var allProperties = TypePropertiesCache(type);

                var sbColumnList = new StringBuilder(string.Empty);
                var sbParameterList = new StringBuilder(string.Empty);
                for (var i = 0; i < allProperties.Count; i++)
                {
                    var property = allProperties[i];
                    sbColumnList.Append(property.Name);
                    var result = property.GetMethod.Invoke(domain, null);

                    if (result is short || result is int || result is long || result is float || result is double || result is decimal ||
                        result is ushort || result is uint || result is ulong)
                        sbParameterList.AppendFormat("{0}", result);
                    else if (result is bool b)
                        sbParameterList.AppendFormat("{0}", b ? 1 : 0);
                    else if (result is Enum)
                        sbParameterList.AppendFormat("{0}", (int)result);
                    else
                        sbParameterList.AppendFormat("'{0}'", result);

                    if (i < allProperties.Count - 1)
                    {
                        sbColumnList.Append(", ");
                        sbParameterList.Append(", ");
                    }
                }

                sb.Append($"INSERT INTO {name}({sbColumnList}) Values({sbParameterList});").Append('\r');
            }

            return sb.ToString();
        }

        public static string GetUpdateTemplateSql(DatabaseType dbType, Type type)
        {
            var name = GetTableName(type);
            var allProperties = TypePropertiesCache(type);

            var sbColumnList = new StringBuilder();
            for (var i = 0; i < allProperties.Count; i++)
            {
                var property = allProperties[i];
                sbColumnList.Append($"{property.Name}=@{property.Name}");
                if (i < allProperties.Count - 1) sbColumnList.Append(", ");
            }

            return $"UPDATE {name} SET {sbColumnList}";
        }

        /// <summary>
        ///     根据Class名字得出数据库中数据表名
        /// </summary>
        public static string GetTableName(Type type)
        {
            if (_TypeTableNameMap.TryGetValue(type.TypeHandle, out var name))
                return name;

            var info = type;
            var tableAttrName = (info.GetCustomAttributes(false).FirstOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic)?.Name;

            if (tableAttrName != null)
            {
                name = tableAttrName;
            }
            else
            {
                name = $"{type.Name}";
                if (type.IsInterface && name.StartsWith("I"))
                    name = name.Substring(1);
            }

            _TypeTableNameMap[type.TypeHandle] = name;
            return name;
        }

        /// <summary>
        /// 从实体类中查找该类型的所有属性
        /// </summary>
        private static List<PropertyInfo> TypePropertiesCache(Type type)
        {
            if (_TypePropertiesMap.TryGetValue(type.TypeHandle, out var pis))
                return pis.ToList();

            var properties = type.GetProperties().ToArray();
            _TypePropertiesMap[type.TypeHandle] = properties;
            return properties.ToList();
        }
    }

    /// <summary>
    ///     Sql语句类型
    /// </summary>
    public enum SqlString
    {
        Table,
        Insert,
        Update
    }
}