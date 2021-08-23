using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace NKnife.Storages
{
    /// <summary>
    ///     数据库相关管理的全局服务
    /// </summary>
    public class DbService : IDbService
    {
        private static readonly Logger _Logger = LogManager.GetCurrentClassLogger();

        private readonly DomainSqlConfig _domainSqlConfig;
        private readonly IConnectionManager _connectionManager;

        protected DbService(DomainSqlConfig domainSqlConfig, IConnectionManager connectionManager)
        {
            _domainSqlConfig = domainSqlConfig;
            _connectionManager = connectionManager;
        }

        #region Implementation of IDbService

        private bool _isStart;

        /// <summary>
        ///     启动数据库相关管理的全局服务。包括初始化数据库，包括库中的表是否存在，如不存在创建。
        /// </summary>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            if (_isStart)
                return Task.Delay(50, cancellationToken);
            _Logger.Info("IHostedService-StartAsync:启动数据库相关管理的全局服务。……");

            foreach (var domainSql in _domainSqlConfig.SqlMap.Values)
            {
                var connection = _connectionManager.OpenReadConnection(domainSql, false);
                VerifyTableExists(connection.CreateCommand(), domainSql);
            }

            _Logger.Info("IHostedService-StartAsync:启动数据库相关管理的全局服务。启动完成。");
            _isStart = true;
            return Task.Delay(500, cancellationToken);
        }

        /// <summary>
        ///     停止数据库相关管理的全局服务。
        /// </summary>
        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.Delay(50, cancellationToken);
        }

        #endregion

        /// <summary>
        /// 1. 检查数据表，如果表不存在，则创建表
        /// </summary>
        protected virtual void VerifyTableExists(IDbCommand command, DomainSql domainSql)
        {
            var tableName = domainSql.TypeName;
            switch (domainSql.CurrentDbType)
            {
                case DatabaseType.SqLite:
                {
                    command.CommandText = $"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{tableName}';";
                    break;
                }
                case DatabaseType.MySql:
                {
                    command.CommandText = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA='{domainSql.DatabaseName}' AND TABLE_NAME='{tableName}';";
                    break;
                }
                case DatabaseType.PostgreSql:
                {
                    command.CommandText = $"SELECT COUNT(*) FROM pg_class WHERE relname='{tableName}';";
                    break;
                }
                case DatabaseType.SqlServer:
                {
                    command.CommandText = $"SELECT * FROM dbo.SysObjects WHERE ID=object_id(N'[{tableName}]') AND OBJECTPROPERTY(ID, 'IsTable')=1;";
                    break;
                }
            }
            var result = (long)command.ExecuteScalar();
            if (1 != result)
            {
                _Logger.Info($"{domainSql.TypeName}在数据库中不存在，准备创建...");
                command.CommandText = domainSql.CreateTable[domainSql.CurrentDbType];
                command.ExecuteNonQuery();
                _Logger.Info($"执行建表{domainSql.TypeName}：\r\n{domainSql.CreateTable[domainSql.CurrentDbType]}");
                // 2. 向表中填充默认数据
                FillDefaultData(command, domainSql);
                _Logger.Info($"向表[{domainSql.TypeName}]中填充默认数据：\r\n{domainSql.CreateTable[domainSql.CurrentDbType]}");
                return;
            }
            _Logger.Debug($"数据表{domainSql.TypeName}正常。");
        }

        /// <summary>
        /// 2. 向表中填充默认数据
        /// </summary>
        /// <param name="command"></param>
        /// <param name="domainSql"></param>
        protected virtual void FillDefaultData(IDbCommand command, DomainSql domainSql)
        {
            if (domainSql.DefaultData == null || domainSql.DefaultData.Count <= 0)
            {
                _Logger.Debug("没有默认数据需要填充。");
                return;
            }

            var sql = domainSql.DefaultData[domainSql.CurrentDbType];
            command.CommandText = sql;
            var result = command.ExecuteScalar();
            _Logger.Info($"向表[{domainSql.TypeName}]中填充默认数据：{result}");
        }
    }
}