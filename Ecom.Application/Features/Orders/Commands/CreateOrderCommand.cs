using AutoMapper;
using Ecom.Application.Features.Orders.Contracts;
using Ecom.Application.Features.Orders.DTOs;
using Ecom.Application.Helper.DTOs;
using Ecom.Domain.Entities.OrderAggregate;
using MediatR;

namespace Ecom.Application.Features.Orders.Commands
{
    public class CreateOrderCommand:IRequest<OrderToReturnDto>
    {
        public OrderDto OrderDto { get; set; }
        public string? Email { get; set; }
        public CreateOrderCommand(OrderDto orderDto, string? email)
        {
            this.OrderDto = orderDto;
            Email = email;
        }
    }

    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderToReturnDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderToReturnDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var address = _mapper.Map<AddressDto,Address>(request.OrderDto.ShipToAddress!);

            var order = await _orderRepository.CreateOrderAsync(request.Email!,request.OrderDto.DeliveryMethodId,
                request.OrderDto.BasketId!, address);
            if(order == null) return new OrderToReturnDto();
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}