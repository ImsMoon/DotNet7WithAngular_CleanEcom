using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Contracts;
using Ecom.Application.Features.Basket.DTOs;
using Ecom.Domain.Entities;
using MediatR;

namespace Ecom.Application.Features.Basket.Command
{
    public class UpdateBasketCommand:IRequest<CustomerBasketDto>
    {
        public CustomerBasketDto customerBasketDto { get; set; }

        public UpdateBasketCommand(CustomerBasketDto customerBasketDto)
        {
            this.customerBasketDto = customerBasketDto;
        }
    }

    public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, CustomerBasketDto>
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public UpdateBasketCommandHandler(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        public async Task<CustomerBasketDto> Handle(UpdateBasketCommand request, CancellationToken cancellationToken)
        {
            if(request.customerBasketDto != null && !string.IsNullOrEmpty(request.customerBasketDto.Id))
            {
                var targetBasket = _mapper.Map<CustomerBasket>(request.customerBasketDto);
                var updatedBasket = await _basketRepository.UpdateBasketAsync(targetBasket);
                var result = _mapper.Map<CustomerBasketDto>(updatedBasket);
                return result;
            }
            return new CustomerBasketDto();
        }
    }
}