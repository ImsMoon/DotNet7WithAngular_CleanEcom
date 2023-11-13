using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.API.Exceptions;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.Queries;
using Ecom.Application.Features.Products.Specification;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseAPIController
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts([FromQuery]ProductSpecParams product)
        {
            var result = await _mediator.Send(new GetProductListQuery(product));
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
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