using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetProductsByCategory(string name);
        Product GetProductDetails(int id);
        Product GetById(int id);
        List<Product> GetAll();
        void Add(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}