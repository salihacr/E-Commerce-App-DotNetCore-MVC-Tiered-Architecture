using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetByIdWithCategories(int id);
        Product GetProductDetails(string url);
        List<Product> GetProductsByCategory(string name, int page, int pageSize);
        List<Product> GetSearchResult(string searchString);
        List<Product> GetHomePageProducts();
        int GetCountByCategory(string category);
        void Update(Product product, int[] categoryIds);
    }
}