using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.MVC.Models
{
    public class ProductDetailModel
    {
        public Product Product { get; set; }
        public List<Category> Categories { get; set; }

    }
}