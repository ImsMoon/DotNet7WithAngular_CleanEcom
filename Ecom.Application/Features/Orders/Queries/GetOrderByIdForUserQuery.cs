using AutoMapper;
using Ecom.Application.Features.Orders.Contracts;
using Ecom.Application.Features.Orders.DTOs;
using MediatR;

namespace Ecom.Application.Features.Orders.Queries
{
    public class GetOrderByIdForUserQuery:IRequest<OrderToReturnDto>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public GetOrderByIdForUserQuery(int id, string? email)
        {
            Id = id;
            Email = email;
        }
    }

    public class GetOrderByIdForUserQueryHandler : IRequestHandler<GetOrderByIdForUserQuery, OrderToReturnDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrderByIdForUserQueryHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderToReturnDto> Handle(GetOrderByIdForUserQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.Id,request.Email!);
            if(order == null) return new OrderToReturnDto();
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}