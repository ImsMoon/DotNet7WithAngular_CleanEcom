using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var result = await _mediator.Send(new GetProductListQuery());
            return Ok(result);
        }

         [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var result = await _mediator.Send(new GetProductQuery(id));
            return Ok(result);
        }

        //  [HttpGet("brands")]
        // public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        // {
        //     return Ok(await _productBrandRepo.ListAllAsync());
        // }

        // [HttpGet("types")]
        // public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductTypes()
        // {
        //     return Ok(await _productTypeRepo.ListAllAsync());
        // }
    }
}