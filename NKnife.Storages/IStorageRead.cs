using System.Collections.Generic;
using System.Threading.Tasks;

namespace NKnife.Storages
{
    /// <summary>
    /// 针对存储层的查询方法封装, 并读写分离管理。
    /// </summary>
    public interface IStorageRead<T, in TId>
    {
        /// <summary>
        /// 指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        Task<bool> ExistAsync(TId id);

        /// <summary>
        /// 分页查询方法
        /// </summary>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="where">条件子句</param>
        /// <param name="orderby">排序子句</param>
        /// <returns>当前页的数据集合</returns>
        Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, string where = "", string orderby = "");

        /// <summary>
        /// 查询数据记录的总数量
        /// </summary>
        /// <returns>数量</returns>
        Task<long> CountAsync();

        /// <summary>
        /// 根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <returns></returns>
        Task<T> FindOneByIdAsync(TId id);

        /// <summary>
        /// 获取所有的记录
        /// </summary>
        /// <returns>所有的记录</returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// 根据指定的Where子句（查询条件）获取记录
        /// </summary>
        /// <param name="whereSql">Where子句（查询条件）</param>
        /// <param name="param">子句的参数</param>
        Task<IEnumerable<T>> FindAsync(string whereSql, object param = null);

        /// <summary>
        /// 根据指定的实体属性获取记录
        /// </summary>
        Task<IEnumerable<T>> FindAsync(T entity);
    }
}
