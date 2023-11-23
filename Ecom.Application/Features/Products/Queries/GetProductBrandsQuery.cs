using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.GenericRepos;
using Ecom.Domain;
using MediatR;

namespace Ecom.Application.Features.Products.Queries
{
    public class GetProductBrandsQuery:IRequest<IReadOnlyList<ProductBrandDto>>
    {
        
    }

    public class GetProductBrandsQueryHandler : IRequestHandler<GetProductBrandsQuery, IReadOnlyList<ProductBrandDto>>
    {
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IMapper _mapper;

        public GetProductBrandsQueryHandler(IGenericRepository<ProductBrand> productBrandRepo, IMapper mapper)
        {
            _productBrandRepo = productBrandRepo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductBrandDto>> Handle(GetProductBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _productBrandRepo.ListAllAsync();
            var result = _mapper.Map<IReadOnlyList<ProductBrandDto>>(brands);
            return result;
        }
    }
}