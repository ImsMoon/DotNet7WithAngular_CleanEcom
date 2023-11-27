using System.ComponentModel.DataAnnotations;

namespace Ecom.Application.Features.Basket.DTOs
{
    public class CustomerBasketDto
    {
        public CustomerBasketDto()
        {
        }

        public CustomerBasketDto(string id)
        {
            Id = id;
        }

        [Required]
        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
    
}