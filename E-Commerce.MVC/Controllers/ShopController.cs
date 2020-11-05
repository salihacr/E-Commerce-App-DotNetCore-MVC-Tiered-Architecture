using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Concrete;
using E_Commerce.MVC.ViewModels;
using E_Commerce.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Commerce.MVC.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        public ShopController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Details(string url)
        {
            if (url == null)
            {
                return NotFound();
            }
            Product product = _productService.GetProductDetails(url);
            if (product == null)
            {
                return NotFound();
            }
            return View(new ProductDetailModel
            {
                Product = product,
                Categories = product.ProductCategories.Select(i => i.Category).ToList()
            });
        }
        public IActionResult List(string category)
        {
            var productViewModel = new ProductListViewModel()
            {
                Products = _productService.GetProductsByCategory(category)
            };

            return View(productViewModel);
        }
    }
}