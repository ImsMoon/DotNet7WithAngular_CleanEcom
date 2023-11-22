using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Contracts;
using Ecom.Application.Features.Basket.DTOs;
using MediatR;

namespace Ecom.Application.Features.Basket.Queries
{
    public class GetBasketByIdQuery:IRequest<CustomerBasketDto>
    {
        public string Id { get; set; }

        public GetBasketByIdQuery(string id)
        {
            Id = id;
        }
    }

    public class GetBasketByIdQueryHandler : IRequestHandler<GetBasketByIdQuery, CustomerBasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public GetBasketByIdQueryHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(request.Id))
            {
                var basket = await _basketRepository.GetBasketAsync(request.Id);
                var result = _mapper.Map<CustomerBasketDto>(basket);
                return result;
            }
            return new CustomerBasketDto(request.Id);
        }
    }
}