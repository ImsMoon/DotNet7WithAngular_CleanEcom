using Ecom.Application.Specifications;
using Ecom.Domain.Entities.OrderAggregate;

namespace Ecom.Application.Features.Orders.Specifications
{
    public class OrdersWithItemsAndOrderingSpecification:BaseSpecification<Order>
    {
        public OrdersWithItemsAndOrderingSpecification(string email):base(o=>o.BuyerEmail == email)
        {
            AddInclude(o=>o.OrderItems);
            AddInclude(o=>o.DeliveryMethod);
            AddOrderByDesc(o=>o.OrderDate);
        }

        public OrdersWithItemsAndOrderingSpecification(int id,string email) 
            : base(o=>o.Id == id && o.BuyerEmail == email)
        {
            AddInclude(o=>o.OrderDate);
            AddInclude(o=>o.DeliveryMethod);
        }
    }
}