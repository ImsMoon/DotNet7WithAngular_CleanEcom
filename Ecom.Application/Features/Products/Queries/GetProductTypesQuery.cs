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
    public class GetProductTypesQuery:IRequest<IReadOnlyList<ProductTypeDto>>
    {
        
    }

    public class GetProductTypesQueryHandler : IRequestHandler<GetProductTypesQuery, IReadOnlyList<ProductTypeDto>>
    {
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;

        public GetProductTypesQueryHandler(IGenericRepository<ProductType> productTypeRepo, IMapper mapper)
        {
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ProductTypeDto>> Handle(GetProductTypesQuery request, CancellationToken cancellationToken)
        {
            var types = await _productTypeRepo.ListAllAsync();
            var result = _mapper.Map<IReadOnlyList<ProductTypeDto>>(types);
            return result;
        }
    }
}