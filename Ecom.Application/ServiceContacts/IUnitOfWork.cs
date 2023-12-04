using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.GenericRepos;
using Ecom.Domain.Entities;

namespace Ecom.Application.ServiceContacts
{
    public interface IUnitOfWork:IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseClass;
        Task<int> Complete();
    }
}