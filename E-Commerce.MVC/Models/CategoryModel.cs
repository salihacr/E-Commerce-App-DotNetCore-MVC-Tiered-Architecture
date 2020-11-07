using System.Collections.Generic;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.MVC.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}