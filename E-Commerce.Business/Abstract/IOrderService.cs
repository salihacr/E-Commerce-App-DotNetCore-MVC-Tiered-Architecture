using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
        List<Order> GetOrders(string userId);
    }
}