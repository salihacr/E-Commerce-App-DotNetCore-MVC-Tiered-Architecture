using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.DataAccess.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string name);
    }
}