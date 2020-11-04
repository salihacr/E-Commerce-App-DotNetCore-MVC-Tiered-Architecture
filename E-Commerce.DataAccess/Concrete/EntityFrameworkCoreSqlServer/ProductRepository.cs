using System.Collections.Generic;
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

        public List<Product> GetProductsByCategory(string name)
        {
            using (var context = new AvocodeContext())
            {
                var products = context.Products.AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.Url.ToLower() == name.ToLower()));
                }
                return products.ToList();
            }
        }
    }
}