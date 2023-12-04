using Ecom.Application.Features.Basket.Commands;
using Ecom.Application.Features.Basket.DTOs;
using Ecom.Application.Features.Basket.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : BaseAPIController
    {
        private readonly IMediator _mediatr;

        public BasketController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<ActionResult> GetBasketById(string id)
        {
            var result = await _mediatr.Send(new GetBasketByIdQuery(id));
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasket(CustomerBasketDto basket)
        {
            var result = await _mediatr.Send(new UpdateBasketCommand(basket));
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasketAsync(string id)
        {
            return Ok(await _mediatr.Send(new DeleteBasketCommand(id)));
        }
    }
}