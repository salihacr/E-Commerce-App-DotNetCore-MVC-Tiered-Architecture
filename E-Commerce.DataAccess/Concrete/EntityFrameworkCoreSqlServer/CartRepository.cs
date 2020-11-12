using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class CartRepository : EFCoreGenericRepository<Cart, AvocodeContext>, ICartRepository
    {
        public Cart GetByUserId(string userId)
        {
            using (var context = new AvocodeContext())
            {
                return context.Carts
                .Include(i => i.CartItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(i => i.UserId == userId);
            }
        }
    }
}