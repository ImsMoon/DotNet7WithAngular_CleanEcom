

namespace Ecom.Domain.Entities.OrderAggregate
{
    public class DeliveryMethod:BaseClass
    {
        public string? ShortName { get; set; }
        public string? DeliveryTime { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}