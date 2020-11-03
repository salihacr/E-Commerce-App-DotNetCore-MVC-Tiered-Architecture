using System.Collections.Generic;

namespace E_Commerce.Entities.Concrete
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public List<ProductCategory> ProductCategory { get; set; }
    }
}