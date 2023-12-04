using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.Specification;
using Ecom.Application.GenericRepos;
using Ecom.Application.Helper.DTOs;
using Ecom.Domain;
using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductListQuery:IRequest<Pagination<ProductDto>>
    {
        public ProductSpecParams productSpecParams {get;set;}

        public GetProductListQuery(ProductSpecParams productSpecParams)
        {
            this.productSpecParams = productSpecParams;
        }
    }

    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, Pagination<ProductDto>>
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IMapper mapper, IGenericRepository<Product> productsRepo)
        {
            _mapper = mapper;
            _productsRepo = productsRepo;
        }

        public async Task<Pagination<ProductDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(request.productSpecParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(request.productSpecParams);

            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);

            var data = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            return new Pagination<ProductDto>(request.productSpecParams.PageIndex,
            request.productSpecParams.PageSize, totalItems, data);
        }
    }
}