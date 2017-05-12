using System.Collections.Generic;
using System.Linq;
using LiteDB;
using NKnife.DataLite.Exceptions;
using NKnife.Interface.Datas.NoSql;

namespace NKnife.DataLite
{
    public abstract class CrudRepositoryBase<T, TId> : RepositoryBase<T>, ICrudRepository<T, TId>
    {
        protected CrudRepositoryBase()
        {
        }

        protected CrudRepositoryBase(string repositoryPath)
            : base(repositoryPath)
        {
        }

        #region Implementation of ICrudRepository<T,TId>

        /// <summary>
        ///     存储一条Document，可能是新建或者修改
        /// </summary>
        /// <returns>当是新document时，插入，返加true；当是已有document时，更新，且返回false。</returns>
        public bool Save(T entity)
        {
            if (entity == null)
                throw new ArgumentByEntityException("实体不能为空", nameof(entity));
            return Collection.Upsert(entity);
        }

        /// <summary>
        ///     Saves all given entities.
        /// </summary>
        /// <returns>return the saved entities</returns>
        public int Save(IEnumerable<T> entities)
        {
            if (entities == null)
                throw new ArgumentByEntityException("实体不能为空", nameof(entities));

            var enumerable = entities as T[] ?? entities.ToArray();
            var i = 0;
            foreach (var item in enumerable)
            {
                if (item == null)
                    continue;
                if (Collection.Upsert(item))
                    i++;
            }
            return i;
        }

        /// <summary>
        ///     Retrieves an entity by its id.
        /// </summary>
        public T FindOne(TId id)
        {
            return Collection.FindById(new BsonValue(id));
        }

        /// <summary>
        ///     Returns whether an entity with the given id exists.
        /// </summary>
        public bool Exists(TId id)
        {
            return Collection.Exists(Query.EQ("_id", new BsonValue(id)));
        }

        /// <summary>
        ///     Returns all instances of the type.
        /// </summary>
        public IEnumerable<T> FindAll()
        {
            return Collection.FindAll();
        }

        /// <summary>
        ///     Returns all instances of the type with the given IDs.
        /// </summary>
        public IEnumerable<T> FindAll(IEnumerable<TId> ids)
        {
            if (ids == null)
                throw new ArgumentByEntityException("查询时，传入参数不能为空", nameof(ids));
            var enumerable = ids as TId[] ?? ids.ToArray();
            var list = new List<T>(enumerable.Length);
            list.AddRange(enumerable.Select(id => Collection.FindById(new BsonValue(id))));
            return list;
        }

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        public IEnumerable<T> FindAll(IComparer<T> comparer)
        {
            var all = FindAll();
            var list = new List<T>(all);
            list.Sort(comparer);
            return list;
        }

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        public IEnumerable<T> FindAll(IEnumerable<TId> ids, IComparer<T> comparer)
        {
            var all = FindAll(ids);
            var list = new List<T>(all);
            list.Sort(comparer);
            return list;
        }

        /// <summary>
        ///     Returns the number of entities available.
        /// </summary>
        public long Count => Collection.Count();

        /// <summary>
        ///     Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">id must not be null</param>
        public bool Delete(TId id)
        {
            return Collection.Delete(new BsonValue(id));
        }

        /// <summary>
        ///     Deletes all entities managed by the repository.
        /// </summary>
        public bool DeleteAll()
        {
            return Database.DropCollection(BuildCollectionName());
        }

        #endregion
    }
}