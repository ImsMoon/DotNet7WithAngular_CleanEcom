using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecom.Application.Specifications;
using Ecom.Domain;

namespace Ecom.Application.Features.Products.Specification
{
    public class ProductsWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        // class without applying any filter conditions to get total item count 
        public ProductsWithFiltersForCountSpecification(ProductSpecParams productSpecParams) : 
        base(x=> (string.IsNullOrEmpty(productSpecParams.Search) || x.Name!.ToLower().Contains(productSpecParams.Search!))
        && (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)
        && (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId))
        {
        }
    }
}