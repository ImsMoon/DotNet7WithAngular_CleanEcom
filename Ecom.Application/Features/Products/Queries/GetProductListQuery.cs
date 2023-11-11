using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.RepositoryContacts;
using Ecom.Application.Features.Products.Specification;
using Ecom.Domain;
using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductListQuery:IRequest<IReadOnlyList<ProductDto>>
    {
        
    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, IReadOnlyList<ProductDto>>
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IMapper mapper, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IGenericRepository<Product> productsRepo)
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productsRepo = productsRepo;
        }

        public async Task<IReadOnlyList<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification();
            var products = await _productsRepo.ListAsync(spec);
            var result = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return result;
        }
    }
}