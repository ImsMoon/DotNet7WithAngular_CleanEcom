using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecom.Domain.Entities;

namespace Ecom.Application.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string basketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}