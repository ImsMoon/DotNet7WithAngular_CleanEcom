using Ecom.API.Exceptions;
using Ecom.Application.Features.Orders.Commands;
using Ecom.Application.Features.Orders.DTOs;
using Ecom.Application.Features.Orders.Queries;
using Ecom.Infrastructure.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController  : BaseAPIController
    {
        private readonly IMediator _mediatr;

        public OrdersController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(OrderDto orderDto)
        {
            string email = getCurrentUserEmail();
            var result = await _mediatr.Send(new CreateOrderCommand(orderDto, email));
            if (result == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
            return Ok(result);
        }

        
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = getCurrentUserEmail();
            var result = await _mediatr.Send(new GetOrdersForUserQuery(email));
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdForUser(int id)
        {
            var email = getCurrentUserEmail();
            var result = await _mediatr.Send(new GetOrderByIdForUserQuery(id,email));
            if (result == null) return NotFound(new ApiResponse(404));
            return Ok(result);
        }

         [HttpGet("deliveryMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            return Ok(await _mediatr.Send(new GetDeliveryMethodsQuery()));
        }

        private string getCurrentUserEmail()
        {
            return HttpContext.User.RetrieveEmailFromPrincipal();
        }
    }
}