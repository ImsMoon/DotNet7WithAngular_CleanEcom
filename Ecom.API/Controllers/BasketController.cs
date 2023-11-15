using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Features.Basket.DTOs;
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
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasket(CustomerBasketDto basket)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            
        }
    }
}