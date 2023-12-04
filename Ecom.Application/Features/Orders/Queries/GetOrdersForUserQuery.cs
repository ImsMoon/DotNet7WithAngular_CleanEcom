using AutoMapper;
using Ecom.Application.Features.Orders.Contracts;
using Ecom.Application.Features.Orders.DTOs;
using MediatR;

namespace Ecom.Application.Features.Orders.Queries
{
    public class GetOrdersForUserQuery:IRequest<IReadOnlyList<OrderToReturnDto>>
    {
        public string? Email { get; set; }

        public GetOrdersForUserQuery(string? email)
        {
            Email = email;
        }
    }

    public class GetOrdersForUserQueryHandler : IRequestHandler<GetOrdersForUserQuery, IReadOnlyList<OrderToReturnDto>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersForUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<OrderToReturnDto>> Handle(GetOrdersForUserQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetOrdersForUserAsync(request.Email!);
            return _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
        }
    }
}