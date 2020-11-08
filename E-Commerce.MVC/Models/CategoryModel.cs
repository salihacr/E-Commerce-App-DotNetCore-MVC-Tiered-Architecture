using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.MVC.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Kategori adı zorunlu bir alandır.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5 ile 60 karakter arasında olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Kategori url zorunlu bir alandır.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5 ile 60 karakter arasında olmalıdır.")]
        public string Url { get; set; }
        public List<Product> Products { get; set; }
    }
}