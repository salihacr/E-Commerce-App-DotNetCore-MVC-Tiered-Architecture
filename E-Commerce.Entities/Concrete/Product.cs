using System.Collections.Generic;

namespace E_Commerce.Entities.Concrete
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public int CategoryId { get; set; }

        public List<ProductCategory> ProductCategory { get; set; }
    }
}