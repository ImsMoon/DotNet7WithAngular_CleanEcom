using AutoMapper;
using Ecom.Application.Features.Products.DTOs;
using Ecom.Domain;
using Microsoft.Extensions.Configuration;

namespace Ecom.Application.Features.Products.Mappings
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["AppUrl"] + source.PictureUrl;
            }
            return null;
        }
    }
}