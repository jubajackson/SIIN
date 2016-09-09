using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondIINoneMC.Web.Data.Repository.Abstract;

namespace SecondIINoneMC.Web.Data.Repository.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;
        void Save();
    }
}
