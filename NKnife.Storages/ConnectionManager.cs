using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using NLog;
using Npgsql;

namespace NKnife.Storages
{
    /// <summary>
    ///     系统的数据库管理器
    /// </summary>
    public class ConnectionManager : IConnectionManager
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, IDbConnection> _readConnectionMap = new Dictionary<string, IDbConnection>();
        private readonly Dictionary<string, IDbConnection> _writeConnectionMap = new Dictionary<string, IDbConnection>();

        /// <summary>
        ///     打开“写”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public virtual IDbConnection OpenWriteConnection(DomainSql domainSql, bool isWrite)
        {
            return OpenDbConnection(_writeConnectionMap, domainSql.CurrentDbType, isWrite ? domainSql.Write : domainSql.Read);
        }

        /// <summary>
        ///     关闭“写”数据库连接
        /// </summary>
        public virtual void CloseWriteConnection(DomainSql domainSql, bool isWrite)
        {
            CloseDbConnection(_writeConnectionMap, isWrite ? domainSql.Write : domainSql.Read);
        }

        /// <summary>
        ///     打开“读”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        public virtual IDbConnection OpenReadConnection(DomainSql domainSql, bool isWrite)
        {
            return OpenDbConnection(_readConnectionMap, domainSql.CurrentDbType, isWrite ? domainSql.Write : domainSql.Read);
        }

        /// <summary>
        ///     关闭“读”数据库连接
        /// </summary>
        public virtual void CloseReadConnection(DomainSql domainSql, bool isWrite)
        {
            CloseDbConnection(_readConnectionMap, isWrite ? domainSql.Write : domainSql.Read);
        }

        protected virtual IDbConnection OpenDbConnection(IDictionary<string, IDbConnection> map, DatabaseType dbType, string connString)
        {
            if (!map.TryGetValue(connString, out var connection))
            {
                switch (dbType)
                {
                    case DatabaseType.MySql:
                        connection = new MySqlConnection(connString);
                        break;
                    case DatabaseType.PostgreSql:
                        connection = new NpgsqlConnection(connString);
                        break;
                    case DatabaseType.SqLite:
                        default:
                        connection = new SqliteConnection(connString);
                        break;
                }

                connection.Open();
                map.Add(connString, connection);
                _Logger.Info($"{dbType}:{connString}首次连接成功。");
            }

            if (connection.State != ConnectionState.Broken && connection.State != ConnectionState.Closed)
                return connection;

            if (connection.State == ConnectionState.Broken)
                connection.Close();

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                _Logger.Info($"{dbType}:{connString}再次连接成功。");
                return connection;
            }

            return connection;
        }

        protected virtual void CloseDbConnection(IDictionary<string, IDbConnection> map, string dbKey)
        {
            if (map.TryGetValue(dbKey, out var connection))
            {
                connection.Close();
                connection.Dispose();
                if (map.Remove(dbKey))
                    connection = null;
            }
        }

    }
}