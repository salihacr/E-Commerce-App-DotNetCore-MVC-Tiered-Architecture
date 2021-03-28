using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class CartRepository : EFCoreGenericRepository<Cart, AvocodeContext>, ICartRepository
    {
        public void ClearCart(int cartId)
        {
            using (var context = new AvocodeContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0";
                context.Database.ExecuteSqlRaw(cmd, cartId);
            }
        }

        public void DeleteFromCart(int cartId, int productId)
        {
            using (var context = new AvocodeContext())
            {
                var cmd = @"delete from CartItems where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, cartId, productId);
            }
        }

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
        public override void Update(Cart entity)
        {
            using (var context = new AvocodeContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
    }
}