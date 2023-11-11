using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ecom.Application.Specifications;
using Ecom.Domain;

namespace Ecom.Application.Features.Products.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType!);
            AddInclude(x => x.ProductBrand!);
        }
    }
}