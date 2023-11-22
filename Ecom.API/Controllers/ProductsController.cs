using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.API.Exceptions;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Application.Features.Products.Queries;
using Ecom.Application.Features.Products.Specification;
using Ecom.Application.ServiceContacts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : BaseAPIController
    {
        private readonly IMediator _mediator;
        private readonly ICleanEcomDbService _cleanEcomDbService;

        public ProductsController(IMediator mediator, ICleanEcomDbService cleanEcomDbService)
        {
            _mediator = mediator;
            _cleanEcomDbService = cleanEcomDbService;
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

        [HttpGet("/applymigration")]
        public async Task<IActionResult> ApplyMigration()
        {
            var result = await _cleanEcomDbService.ApplyMigrationsAsync();
            return Ok(result);
        }
    }
}