using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using LiteDB;
using NKnife.Interface.Datas.NoSql;

namespace NKnife.DataLite
{
    /// <summary>
    /// Extension of <see cref="ICrudRepository{T,TId}"/> to provide additional methods to retrieve entities using the pagination and sorting abstraction.
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    /// <typeparam name="TId">实体的ID的类型</typeparam>
    public abstract class PagingAndSortingRepositoryBase<T, TId> : CrudRepositoryBase<T, TId>, IPagingAndSortingRepository<T, TId>
    {
        protected PagingAndSortingRepositoryBase()
        {
        }

        protected PagingAndSortingRepositoryBase(string repositoryPath)
            : base(repositoryPath)
        {
        }

        #region Implementation of IPagingAndSortingRepository<T,ID>

        /// <summary>
        /// Returns a <see cref="IPage{T}"/> of entities meeting the paging restriction provided in the <see cref="IPageable{T}"/> object.
        /// </summary>
        public IPage<T> FindMulti(IPageable<T> pageable)
        {
            var result = Collection.Find(pageable.Predicate, (int) pageable.Offset, (int) pageable.PageSize);
            var list = new List<T>(result);
            if (pageable.Comparer != null)
                list.Sort(pageable.Comparer);
            var tatal = Collection.Count(pageable.Predicate);
            return new Page<T>(pageable, list.ToList(), (ulong) tatal);
        }

        #endregion
    }
}