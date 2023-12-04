using System.Linq.Expressions;

namespace Ecom.Application.Specifications
{
    public interface ISpecification<T>  
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }   
        Expression<Func<T,Object>> OrderBy {get;}
        Expression<Func<T,Object>> OrderByDesc {get;}
        int Take {get;}
        int Skip {get;}
        bool IsPaginationEnabled {get;}

    }
}