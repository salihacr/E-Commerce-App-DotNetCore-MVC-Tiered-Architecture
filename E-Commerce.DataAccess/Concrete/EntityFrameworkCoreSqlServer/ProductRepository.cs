using System.Collections.Generic;
using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class ProductRepository : EFCoreGenericRepository<Product, AvocodeContext>, IProductRepository
    {
        public int GetCountByCategory(string category)
        {
            using (var context = new AvocodeContext())
            {
                var products = context.Products.Where(i => i.IsApproved).AsQueryable();
                if (!string.IsNullOrEmpty(category))
                {
                    products = products
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.Url.ToLower() == category.ToLower()));
                }
                return products.Count();
            }
        }

        public List<Product> GetHomePageProducts()
        {
            using (var context = new AvocodeContext())
            {
                return context.Products
                    .Where(i => i.IsApproved && i.IsHome == true)
                    .ToList();

            }
        }

        public Product GetProductDetails(string url)
        {
            using (var context = new AvocodeContext())
            {
                return context.Products
                .Where(i => i.Url == url)
                .Include(i => i.ProductCategories)
                .ThenInclude(i => i.Category)
                .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string name, int page, int pageSize)
        {
            using (var context = new AvocodeContext())
            {
                var products = context
                    .Products
                    .Where(i => i.IsApproved)
                    .AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    products = products
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Category)
                    .Where(i => i.ProductCategories.Any(a => a.Category.Url.ToLower() == name.ToLower()));
                }
                return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }
    }
}