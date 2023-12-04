using AutoMapper;
using Ecom.Application.Features.Orders.DTOs;
using Ecom.Domain.Entities.OrderAggregate;
using Microsoft.Extensions.Configuration;

namespace Ecom.Application.Features.Orders.Mappings
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderItemUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return _configuration["ApiUrl"]+source.ItemOrdered.PictureUrl;
            }
            return null;
        }
    }
}