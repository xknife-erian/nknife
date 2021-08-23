using System.Collections.Generic;

namespace NKnife.Interface.Datas.NoSql
{
    public interface ICrudRepository<T, in TId> : IRepository<T>
    {
        /// <summary>
        ///     存储一条Document，可能是新建或者修改
        /// </summary>
        /// <returns>当是新document时，插入，返加true；当是已有document时，更新，且返回false。</returns>
        bool Save(T entity);

        /// <summary>
        /// Saves all given entities.
        /// </summary>
        /// <returns>return the saved entities count</returns>
        int Save(IEnumerable<T> entities);

        /// <summary>
        /// Retrieves an entity by its id.
        /// </summary>
        T FindOne(TId id);

        /// <summary>
        /// Returns whether an entity with the given id exists.
        /// </summary>
        bool Exists(TId id);

        /// <summary>
        /// Returns all instances of the type.
        /// </summary>
        IEnumerable<T> FindAll();

        /// <summary>
        /// Returns all instances of the type with the given IDs.
        /// </summary>
        IEnumerable<T> FindAll(IEnumerable<TId> ids);

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        IEnumerable<T> FindAll(IComparer<T> comparer);

        /// <summary>
        /// Returns all entities sorted by the given options.
        /// </summary>
        IEnumerable<T> FindAll(IEnumerable<TId> ids, IComparer<T> comparer);

        /// <summary>
        /// Returns the number of entities available.
        /// </summary>
        long Count { get; }

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">id must not be null</param>
        bool Delete(TId id);

        /// <summary>
        /// Deletes all entities managed by the repository.
        /// </summary>
        bool DeleteAll();
    }
}