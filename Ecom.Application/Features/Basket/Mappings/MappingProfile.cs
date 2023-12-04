using AutoMapper;
using Ecom.Application.Features.Basket.DTOs;
using Ecom.Domain.Entities;

namespace Ecom.Application.Features.Basket.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItem,BasketItemDto>().ReverseMap();
        }
    }
}