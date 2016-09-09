namespace SecondIINoneMC.Web.Data.Repository.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    using SecondIINoneMC.Web.Data;
    using SecondIINoneMC.Web.Data.Repository.Abstract;
    using SecondIINoneMC.Web.Data.EntityFramework;

    /// <summary>
    /// The repository.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    /// <typeparam name="TKey">
    /// </typeparam>
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly SIINMCDataEntitiesContext _context;

        /// <summary>
        /// The _dbset.
        /// </summary>
        private readonly IDbSet<T> _dbset;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T,TKey}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public Repository(SIINMCDataEntitiesContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = System.Data.Entity.EntityState.Deleted;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        public virtual void Update(T entity)
        {
            //var entry = _context.Entry(entity);
            //_dbset.Attach(entity);
            //entry.State = System.Data.EntityState.Modified;

            if (entity == null)
            {
                throw new ArgumentException("Cannot update a null entity.");
            }

            var entry = _context.Entry<T>(entity);
            
            // Retreive the Id through reflection
            var pkey = _dbset.Create().GetType().GetProperty("Id").GetValue(entity);

            if (entry.State == System.Data.Entity.EntityState.Detached)
            {
                var set = _context.Set<T>();
                T attachedEntity = set.Find(pkey);  // You need to have access to key

                if (attachedEntity != null)
                {
                    var attachedEntry = _context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = System.Data.Entity.EntityState.Modified; // This should attach entity
                }
            }
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById(TKey id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// The all.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<T> All()
        {
            return _dbset;
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="predicate">
        /// The predicate.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        // public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        // where T : class
        // {
        // if (includes != null)
        // {
        // query = includes.Aggregate(query,
        // (current, include) => current.Include(include));
        // }

        // return query;
        // }
    }
}