using System.Collections.Generic;
using System.Threading.Tasks;

namespace NKnife.Storages
{
    /// <summary>
    /// 面向数据表的基本分页读取操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IStoragePageRead<T, in TId>
    {
        /// <summary>
        /// 分页查询指定ID的组织关联的实体
        /// </summary>
        /// <param name="organizationId">指定组织的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <returns>该组织管理（关联）的实体</returns>
        Task<IEnumerable<T>> PageByOrganizationAsync(string organizationId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false);

        /// <summary>
        /// 分页查询指定ID的社区关联的实体
        /// </summary>
        /// <param name="communityId">指定社区的ID</param>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <returns>该社区管理（关联）的实体</returns>
        Task<IEnumerable<T>> PageByCommunityAsync(string communityId, int pageSize, int pageNumber, SortDirection direction = SortDirection.NONE, bool skipDeleteTag = false);

        /// <summary>
        /// 查询指定社区ID关联的记录统计数量
        /// </summary>
        /// <param name="communityId">社区ID</param>
        Task<long> CountByCommunityAsync(string communityId, bool skipDeleteTag = false);

        /// <summary>
        /// 查询指定组织ID关联的记录统计数量
        /// </summary>
        /// <param name="organizationId">组织ID</param>
        Task<long> CountByOrganizationAsync(string organizationId, bool skipDeleteTag = false);
    }
}