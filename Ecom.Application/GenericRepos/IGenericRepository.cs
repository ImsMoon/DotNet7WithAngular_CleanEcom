using Ecom.Application.Specifications;
using Ecom.Domain.Entities;

namespace Ecom.Application.GenericRepos
{
    public interface IGenericRepository<T> where T: BaseClass
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}