using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using NLog;

namespace NKnife.Storages
{
    public abstract class BaseStorage : IStorage
    {
        protected readonly IConnectionManager _ConnectionManager;
        protected readonly SimpleCRUD.ITableNameResolver _tableNameResolver;
        private string _tableName;

        protected BaseStorage(IConnectionManager connectionManager, SimpleCRUD.ITableNameResolver tableNameResolver)
        {
            _ConnectionManager = connectionManager;
            _tableNameResolver = tableNameResolver;
        }

        /// <summary>
        ///     该实体类是否需要分表进行存储
        /// </summary>
        public bool IsCutIntoMultiTables { get; protected set; } = false;

        /// <summary>
        ///     根据当前数据表对应的实体类，生成数据表表名
        /// </summary>
        /// <param name="tableNameFunc">表名生成的逻辑函数</param>
        /// <returns>数据表表名</returns>
        public virtual string BuildTableName(Func<IStorage, string> tableNameFunc = null)
        {
            if (string.IsNullOrEmpty(_tableName))
            {
                if (null != tableNameFunc)
                    _tableName = tableNameFunc.Invoke(this);
                else if(!IsCutIntoMultiTables)
                    _tableName = _tableNameResolver.ResolveTableName(GetEntityType());
                else
                    _tableName = CutIntoMultiTables();
            }

            return _tableName;
        }

        private string CutIntoMultiTables()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 返回本仓库的实体类类型
        /// </summary>
        protected abstract Type GetEntityType();
    }
}
