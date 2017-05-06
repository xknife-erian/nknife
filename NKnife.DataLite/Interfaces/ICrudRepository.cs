﻿using System.Collections.Generic;

namespace NKnife.DataLite.Interfaces
{
    public interface ICrudRepository<T, ID> : IRepository<T>
    {
        /// <summary>
        /// Saves a given entity. Use the returned instance for further operations as the save operation might have changed the entity instance completely.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>return the saved entity</returns>
        T Save(T entity);

        /// <summary>
        /// Saves all given entities.
        /// </summary>
        /// <returns>return the saved entities</returns>
        IEnumerable<T> Save(IEnumerable<T> entities);

        /// <summary>
        /// Retrieves an entity by its id.
        /// </summary>
        T FindOne(ID id);

        /// <summary>
        /// Returns whether an entity with the given id exists.
        /// </summary>
        bool Exists(ID id);

        /// <summary>
        /// Returns all instances of the type.
        /// </summary>
        IEnumerable<T> FindAll();

        /// <summary>
        /// Returns all instances of the type with the given IDs.
        /// </summary>
        IEnumerable<T> FindAll(IEnumerable<ID> ids);

        /// <summary>
        /// Returns the number of entities available.
        /// </summary>
        long Count { get; }

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">id must not be null</param>
        void Delete(ID id);

        /// <summary>
        /// Deletes a given entity.
        /// </summary>
        void Delete(T entity);

        /// <summary>
        ///  deletes the given entities.
        /// </summary>
        /// <param name="entities">entities</param>
        void Delete(IEnumerable<T> entities);

        /// <summary>
        /// Deletes all entities managed by the repository.
        /// </summary>
        void DeleteAll();
    }
}