using System.Linq;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public class CategoryRepository : EFCoreGenericRepository<Category, AvocodeContext>, ICategoryRepository
    {
        public void DeleteFromCategory(int productId, int categoryId)
        {
            using (var context = new AvocodeContext())
            {
                var cmd = "delete from productcategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd, productId, categoryId);
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new AvocodeContext())
            {
                return context.Categories
                    .Where(i => i.CategoryId == categoryId)
                    .Include(i => i.ProductCategories)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefault();
            }
        }
    }
}