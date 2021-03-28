using System.Collections.Generic;
using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class OrderRepository : EFCoreGenericRepository<Order, AvocodeContext>, IOrderRepository
    {
        public List<Order> GetOrders(string userId)
        {
            using (var context = new AvocodeContext())
            {

                var orders = context.Orders
                                    .Include(i => i.OrderItems)
                                    .ThenInclude(i => i.Product)
                                    .AsQueryable();

                if (!string.IsNullOrEmpty(userId))
                {
                    orders = orders.Where(i => i.UserId == userId);
                }

                return orders.ToList();
            }
        }
    }
}