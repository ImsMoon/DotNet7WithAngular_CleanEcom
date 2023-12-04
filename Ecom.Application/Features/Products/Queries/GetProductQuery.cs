using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.Specification;
using Ecom.Application.GenericRepos;
using Ecom.Domain;
using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductQuery:IRequest<ProductDto>
    {
        public int Id { get; set; }

        public GetProductQuery(int id)
        {
            Id = id;
        }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IMapper mapper, IGenericRepository<ProductBrand> productBrandRepo, IGenericRepository<ProductType> productTypeRepo, IGenericRepository<Product> productsRepo)
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _productsRepo = productsRepo;
        }

        public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(request.Id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            var result = _mapper.Map<Product,ProductDto>(product);
            return result;
        }
    }
}