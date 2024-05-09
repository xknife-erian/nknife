using System;
using System.Data;
using System.Threading.Tasks;

namespace NKnife.Storages
{
    /// <summary>
    /// 数据库连接管理器
    /// </summary>
    public interface IConnectionManager
    {
        /// <summary>
        ///     打开“写”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection OpenWriteConnection(DomainSql domainSql, bool isWrite);

        /// <summary>
        ///     关闭“写”数据库连接
        /// </summary>
        void CloseWriteConnection(DomainSql domainSql, bool isWrite);

        /// <summary>
        ///     打开“读”数据库连接，并返回该连接
        /// </summary>
        /// <returns>数据库连接</returns>
        IDbConnection OpenReadConnection(DomainSql domainSql, bool isWrite);

        /// <summary>
        ///     关闭“读”数据库连接
        /// </summary>
        void CloseReadConnection(DomainSql domainSql, bool isWrite);
    }
}