using System.Collections.Generic;
using E_Commerce.Business.Abstract;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void Create(Order entity)
        {
            _orderRepository.Add(entity);
        }
        public List<Order> GetOrders(string userId)
        {
            return _orderRepository.GetOrders(userId);
        }
    }
}