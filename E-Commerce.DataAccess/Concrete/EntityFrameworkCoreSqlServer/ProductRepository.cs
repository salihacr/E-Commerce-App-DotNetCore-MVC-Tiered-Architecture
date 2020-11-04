using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class ProductRepository : EFCoreGenericRepository<Product, AvocodeContext>, IProductRepository
    {
        public Product GetProductDetails(int id)
        {
            using (var context = new AvocodeContext())
            {
                return context.Products
                .Where(i => i.ProductId == id)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefault();
            }
        }
    }
}