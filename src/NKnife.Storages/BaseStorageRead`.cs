using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace NKnife.Storages
{
    public abstract class BaseStorageRead<T> : BaseStorage, IStorageRead<T, string>
    {
        protected readonly DomainSql _domainSql;

        protected BaseStorageRead(IConnectionManager connectionManager, SimpleCRUD.ITableNameResolver tableNameResolver, DomainSqlConfig option)
            : base(connectionManager, tableNameResolver)
        {
            var key = typeof(T).Name;
            if (option.SqlMap != null && option.SqlMap.ContainsKey(key))
                _domainSql = option.SqlMap[key];
        }

        /// <summary>
        /// 返回本仓库的实体类类型
        /// </summary>
        protected override Type GetEntityType()
        {
            return typeof(T);
        }

        #region Implementation of IStorageRead<T,in string>

        /// <summary>
        ///     分页查询方法
        /// </summary>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="where">条件子句</param>
        /// <param name="orderby">排序子句</param>
        /// <returns>当前页的数据集合</returns>
        public virtual async Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, string where = "", string orderby = "")
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            var result = await conn.GetListPagedAsync<T>(pageNumber, pageSize, where, orderby);
            return result;
        }

        /// <summary>
        ///     根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        public virtual async Task<T> FindOneByIdAsync(string id)
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            return await conn.QueryFirstAsync<T>($"SELECT * FROM {BuildTableName()} WHERE Id='{id}'");
        }

        /// <summary>
        ///     指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        /// <returns>记录是否存在，true时存在指定ID的记录，false反之。</returns>
        public virtual async Task<bool> ExistAsync(string id)
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            var sql = $"SELECT COUNT(*) FROM {BuildTableName()} WHERE Id='{id}'";
            return await conn.ExecuteAsync(sql) > 0;
        }

        /// <summary>
        ///     查询记录的数据记录统计数量
        /// </summary>
        /// <returns>数量</returns>
        public virtual async Task<long> CountAsync()
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            var count = await conn.RecordCountAsync<T>();
            return count;
        }

        /// <summary>
        ///     获取所有的组织
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            var sql = $"SELECT * FROM {BuildTableName()}"; // WHERE State='Normal'";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        ///     根据指定的Where子句（查询条件）获取记录
        /// </summary>
        /// <param name="whereSql">Where子句（查询条件）</param>
        /// <param name="param"></param>
        public async Task<IEnumerable<T>> FindAsync(string whereSql, object param = null)
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            return await conn.GetListAsync<T>(whereSql, param);
        }

        /// <summary>
        ///     根据指定的实体属性获取记录
        /// </summary>
        public async Task<IEnumerable<T>> FindAsync(T entity)
        {
            var conn = _ConnectionManager.OpenReadConnection(_domainSql, false);
            return await conn.GetListAsync<T>(entity);
        }

        #endregion
    }
}