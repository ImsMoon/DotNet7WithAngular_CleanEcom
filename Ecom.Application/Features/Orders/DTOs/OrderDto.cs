using Ecom.Application.Helper.DTOs;

namespace Ecom.Application.Features.Orders.DTOs
{
    public class OrderDto
    {
        public string? BasketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDto? ShipToAddress { get; set; }
    }
}