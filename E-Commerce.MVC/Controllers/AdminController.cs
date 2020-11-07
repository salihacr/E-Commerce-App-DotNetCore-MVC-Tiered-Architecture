using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.MVC.Models;
using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Concrete;
using Newtonsoft.Json;

namespace E_Commerce.MVC.Controllers
{
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult ProductList()
        {
            return View(new ProductListViewModel()
            {
                Products = _productService.GetAll()
            });
        }
        // ekleme sayfasını getirir
        [HttpGet]
        public IActionResult AddProduct()
        {
            return View();
        }
        // ekleme yaptıktan sonra çalışır 
        [HttpPost]
        public IActionResult AddProduct(ProductModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
            _productService.Add(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} adlı ürün eklendi.",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }
        // güncelleme sayfasını getirir
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                ProductId = entity.ProductId,
                Name = entity.Name,
                Url = entity.Url,
                Price = entity.Price,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditProduct(ProductModel model)
        {
            var product = _productService.GetById(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }/*
            var entity = new Product()
            {
                Name = model.Name,
                Url = model.Url,
                Price = model.Price,
                Description = model.Description,
                ImageUrl = model.ImageUrl
            };
            */
            product.Name = model.Name;
            product.Price = model.Price;
            product.Url = model.Url;
            product.ImageUrl = model.ImageUrl;
            product.Description = model.Description;

            _productService.Update(product);
            var msg = new AlertMessage()
            {
                Message = $"{product.Name} adlı ürün güncellendi.",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("ProductList");
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} adlı ürün silindi.",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("ProductList");
        }


        /*
        
            Kategoriler => Kategori controllera taşınacak
        */
        public IActionResult CategoryList()
        {
            return View(new CategoryListViewModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        // ekleme sayfasını getirir
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name,
                Url = model.Url
            };
            _categoryService.Add(entity);

            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} adlı kategori eklendi.",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList");
        }
        // güncelleme sayfasını getirir
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var category = _categoryService.GetById(model.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = model.Name;
            category.Url = model.Url;


            _categoryService.Update(category);
            var msg = new AlertMessage()
            {
                Message = $"{category.Name} adlı kategori güncellendi.",
                AlertType = "success"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);

            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            var msg = new AlertMessage()
            {
                Message = $"{entity.Name} adlı kategori silindi.",
                AlertType = "danger"
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
            return RedirectToAction("CategoryList");
        }

    }
}