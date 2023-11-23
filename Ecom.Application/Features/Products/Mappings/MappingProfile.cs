using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Domain;

namespace Ecom.Application.Features.Products.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<ProductBrand,ProductBrandDto>().ReverseMap();
            CreateMap<ProductType,ProductTypeDto>().ReverseMap();
        }
    }
}