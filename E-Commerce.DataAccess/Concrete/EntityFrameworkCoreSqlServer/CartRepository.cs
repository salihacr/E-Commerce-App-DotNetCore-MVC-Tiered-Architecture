using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class CartRepository : EFCoreGenericRepository<Cart, AvocodeContext>, ICartRepository
    {

    }
}