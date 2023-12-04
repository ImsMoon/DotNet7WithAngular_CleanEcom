using System.Collections;
using Ecom.Application.GenericRepos;
using Ecom.Application.ServiceContacts;
using Ecom.Domain.Entities;
using Ecom.Infrastructure.Repositories;

namespace Ecom.Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _dbContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Complete()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseClass
        {
            if(_repositories ==  null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if(!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_dbContext);

                _repositories.Add(type,repositoryInstance);
            }

            return (IGenericRepository<TEntity>) _repositories[type]!;
        }
    }
}