using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.Business.Abstract
{
    public interface ICategoryService
    {
        Category GetByIdWithProducts(int categoryId);
        Category GetById(int id);
        List<Category> GetAll();
        void Add(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
        void DeleteFromCategory(int productId, int categoryId);
    }
}