// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRepository.cs" company="IntegraColor, Ltd.">
//  © 2013 IntegraColor Ltd. - All rights reserved.
// </copyright>
// <summary>
//   The Repository interface.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SecondIINoneMC.Web.Data.Repository.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// The Repository interface.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the object.
    /// </typeparam>
    /// <typeparam name="TKey">
    /// The type of the key.
    /// </typeparam>
    public interface IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// Adds the supplied entity to the data store.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Add(T entity);

        /// <summary>
        /// Deletes the supplied entity from the data store.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Delete(T entity);

        /// <summary>
        /// Updates the supplied entity in the data store.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        void Update(T entity);

        /// <summary>
        /// The gets the entity whose primary key matches the supplied id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The matching entity.<see cref="T"/>.
        /// </returns>
        T GetById(TKey id);

        /// <summary>
        /// Returns an enumerable list of all of the entities in the data store.
        /// </summary>
        /// <returns>
        /// An enumerable collection of all of the entities in this repository.<see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> All();

        /// <summary>
        /// Finds all entities in the repository whose criteria matches the supplied predicate expression.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The enumerable collection of matching entities.<see cref="IEnumerable"/>.
        /// </returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
