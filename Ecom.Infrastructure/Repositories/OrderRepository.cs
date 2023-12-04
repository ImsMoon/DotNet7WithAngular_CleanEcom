using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Application.Features.Basket.Contracts;
using Ecom.Application.Features.Orders.Contracts;
using Ecom.Application.Features.Orders.Specifications;
using Ecom.Application.ServiceContacts;
using Ecom.Domain;
using Ecom.Domain.Entities.OrderAggregate;

namespace Ecom.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        // basket repo is a redis repo that's why it's not included in unitofwork
        public OrderRepository(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int delieveryMethodId, string basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);

            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id,productItem.Name!,productItem.PictureUrl!);
                var orderItem = new OrderItem(itemOrdered,productItem.Price, item.Quantity);
                items.Add(orderItem);                
            }
            
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(delieveryMethodId);

            var subtotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items,buyerEmail,shippingAddress,deliveryMethod,subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            var result = await _unitOfWork.Complete();

            if(result <= 0) return null;
            //saved in db so delete from redis basket repo 
            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(id,buyerEmail);
            return await _unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpecification(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}