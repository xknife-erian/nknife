using System.Collections.Generic;

namespace NKnife.DataLite.Interfaces
{
    /// <summary>
    /// Extension of {@link CrudRepository} to provide additional methods to retrieve entities using the pagination and sorting abstraction.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IPagingAndSortingRepository<T, in TId> : ICrudRepository<T, TId>
    {

        /// <summary>
        /// Returns a <see cref="IPage{T}"/> of entities meeting the paging restriction provided in the {@code Pageable} object.
        /// </summary>
        IPage<T> FindAll(IPageable<T> pageable);
    }
}