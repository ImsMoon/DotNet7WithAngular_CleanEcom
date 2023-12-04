using AutoMapper;
using Ecom.Application.Features.Orders.Contracts;
using Ecom.Application.Features.Orders.DTOs;
using MediatR;

namespace Ecom.Application.Features.Orders.Queries
{
    public class GetDeliveryMethodsQuery:IRequest<IReadOnlyList<DeliveryMethodDto>>
    {
        
    }

    public class GetDeliveryMethodsQueryHandler : IRequestHandler<GetDeliveryMethodsQuery, IReadOnlyList<DeliveryMethodDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetDeliveryMethodsQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DeliveryMethodDto>> Handle(GetDeliveryMethodsQuery request, CancellationToken cancellationToken)
        {
            var deliveryMethods = await _orderRepository.GetDeliveryMethodsAsync();
            return _mapper.Map<IReadOnlyList<DeliveryMethodDto>>(deliveryMethods);
        }
    }
}