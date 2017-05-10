using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using LiteDB;
using NKnife.DataLite.Interfaces;

namespace NKnife.DataLite
{
    public abstract class PagingAndSortingRepositoryBase<T, TId> : CrudRepositoryBase<T, TId>, IPagingAndSortingRepository<T, TId>
    {
        protected PagingAndSortingRepositoryBase(string repositoryPath)
            : base(repositoryPath)
        {
        }

        #region Implementation of IPagingAndSortingRepository<T,ID>

        /// <summary>
        /// Returns a <see cref="IPage{T}"/> of entities meeting the paging restriction provided in the {@code Pageable} object.
        /// </summary>
        public IPage<T> FindAll(IPageable<T> pageable)
        {
            var result = Collection.Find(pageable.Predicate, (int) pageable.Offset, (int) pageable.PageSize);
            var list = new List<T>(result);
            if (pageable.Comparer != null)
                list.Sort(pageable.Comparer);
            return new Page<T>(pageable, list.ToList(), (ulong) Count);
        }

        #endregion
    }
}