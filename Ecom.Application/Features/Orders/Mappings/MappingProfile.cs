using AutoMapper;
using Ecom.Application.Features.Orders.DTOs;
using Ecom.Application.Helper.DTOs;
using Ecom.Domain.Entities.OrderAggregate;

namespace Ecom.Application.Features.Orders.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<AddressDto,Address>().ReverseMap();
            CreateMap<Order,OrderToReturnDto>()
                .ForMember(d=>d.DeliveryMethod, o=>o.MapFrom(s=>s.DeliveryMethod.ShortName))
                .ForMember(d=>d.ShippingPrice, o=> o.MapFrom(s=>s.DeliveryMethod.Price));
            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d=>d.ProductId, o=>o.MapFrom(s=>s.ItemOrdered.ProductItemId))
                .ForMember(d=>d.ProductId, o=>o.MapFrom(s=>s.ItemOrdered.ProductName))
                .ForMember(d=>d.ProductId, o=>o.MapFrom(s=>s.ItemOrdered.PictureUrl))
                .ForMember(d=>d.PictureUrl, o=>o.MapFrom<OrderItemUrlResolver>());
        }
    }
}