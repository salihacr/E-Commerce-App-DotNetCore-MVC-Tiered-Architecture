using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using E_Commerce.Entities.Concrete;

namespace E_Commerce.MVC.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Ürün adı zorunlu bir alandır.")]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "Ürün ismi 5 ile 60 karakter arasında olmalıdır.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Url zorunlu bir alandır.")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "Ürün url si 5 ile 120 karakter arasında olmalıdır.")]
        public string Url { get; set; }

        [Required(ErrorMessage = "Fiyat zorunlu bir alandır.")]
        [Range(1, 100000, ErrorMessage = "Fiyat 1 ile 100000 arasında olmalıdır.")]
        public double? Price { get; set; }

        [Required(ErrorMessage = "Açıklama zorunlu bir alandır.")]
        public string Description { get; set; }

        // [Required(ErrorMessage = "Ürün resmi zorunlu bir alandır.")]
        public string ImageUrl { get; set; }

        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}