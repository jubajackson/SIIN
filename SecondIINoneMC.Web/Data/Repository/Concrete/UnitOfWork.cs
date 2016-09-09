namespace SecondIINoneMC.Web.Data.Repository.Concrete
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;

    using SecondIINoneMC.Core;
    using SecondIINoneMC.Web.Data;
    using SecondIINoneMC.Web.Data.Entities;
    using SecondIINoneMC.Web.Data.Repository.Abstract;
    using SecondIINoneMC.Web.Data.Repository.Concrete;
    using SecondIINoneMC.Web.Data.EntityFramework;

    /// <summary>
    /// </summary>
    /// <typeparam name="TContext">
    /// </typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork
        where TContext : SIINMCDataEntitiesContext, new()
    {
        #region Fields

        /// <summary>
        /// </summary>
        private SIINMCDataEntitiesContext _ctx;

        /// <summary>
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// </summary>
        private Dictionary<Type, object> _repositories;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        public UnitOfWork()
        {
            this._ctx = new TContext();
            this._repositories = new Dictionary<Type, object>();
            this._disposed = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{TContext}" /> class. Specifying the optional configuration
        /// values for the underlying Database Context.
        /// </summary>
        /// <param name="proxyCreationEnabled">Enables Proxy Creation for entities.</param>
        /// <param name="autoDetectChangesEnabled">Enables self-tracking entities.</param>
        public UnitOfWork(bool proxyCreationEnabled = true, bool autoDetectChangesEnabled = true)
        {
            this._ctx = new TContext();
            this._repositories = new Dictionary<Type, object>();
            this._disposed = false;

            this.ProxyCreationEnabled = proxyCreationEnabled;
            this.AutoDetectChangesEnabled = autoDetectChangesEnabled;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// </summary>
        public bool ProxyCreationEnabled
        {
            get
            {
                return this._ctx.Configuration.ProxyCreationEnabled;
            }

            set
            {
                this._ctx.Configuration.ProxyCreationEnabled = value;
            }
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                return this._ctx.Configuration.AutoDetectChangesEnabled;
            }

            set
            {
                this._ctx.Configuration.AutoDetectChangesEnabled = value;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity">
        /// </typeparam>
        /// <returns>
        /// </returns>
        public IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
        {
            if (this._repositories.Keys.Contains(typeof(TEntity)))
            {
                return this._repositories[typeof(TEntity)] as IRepository<TEntity, TKey>;
            }

            var repository = new Repository<TEntity, TKey>(this._ctx);
            this._repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        /// <summary>
        /// </summary>
        public void Save()
        {
            try
            {
                this._ctx.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var message = new List<string>();

                foreach (var error in ex.EntityValidationErrors)
                {
                    message.Add(ex.Message);

                    // Sample object name: 
                    //      System.Data.Entity.DynamicProxies.UserAccount_C1C5591CDC0FEAD50B27D31BD64E610951C93C7EDEECFC825862CB9EF87ECDB5

                    if (error.ValidationErrors.Any())
                    {
                        string obj;

                        try
                        {
                            if (error.Entry.Entity.ToString().Contains("_"))
                            {
                                obj = error.Entry.Entity.ToString().Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries)[0];
                                string[] objArray = obj.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                                obj = objArray.Last();
                            }
                            else
                            {
                                obj = error.Entry.Entity.ToString();
                            }
                        }
                        catch (Exception)
                        {
                            obj = error.Entry.Entity.ToString();
                        }

                        message.AddRange(error.ValidationErrors.Select(err2 => string.Format("{0}.{1} -- {2}", obj, err2.PropertyName, err2.ErrorMessage)));
                    }
                }

                throw new Exception(
                    string.Format(
                        "Validation Errors occurred while saving: {0}",
                        string.Join(", ", message)),
                        ex);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="disposing">
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._ctx.Dispose();
                }

                this._disposed = true;
            }
        }

        #endregion
    }
}