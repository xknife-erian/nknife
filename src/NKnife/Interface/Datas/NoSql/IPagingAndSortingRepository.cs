namespace NKnife.Interface.Datas.NoSql
{
    /// <summary>
    /// Extension of <see cref="ICrudRepository{T,TId}"/> to provide additional methods to retrieve entities using the pagination and sorting abstraction.
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="TId">实体的ID的类型</typeparam>
    public interface IPagingAndSortingRepository<T, in TId> : ICrudRepository<T, TId>
    {

        /// <summary>
        /// Returns a <see cref="IPage{T}"/> of entities meeting the paging restriction provided in the <see cref="IPageable{T}"/> object.
        /// </summary>
        IPage<T> FindMulti(IPageable<T> pageable);
    }
}