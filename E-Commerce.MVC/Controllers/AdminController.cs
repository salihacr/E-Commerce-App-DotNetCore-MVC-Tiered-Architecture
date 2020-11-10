using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.MVC.Models;
using E_Commerce.Business.Abstract;
using E_Commerce.Entities.Concrete;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using E_Commerce.MVC.Extensions;

namespace E_Commerce.MVC.Controllers
{
    [Authorize]
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
            if (ModelState.IsValid)
            {
                var entity = new Product()
                {
                    Name = model.Name,
                    Url = model.Url,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = null
                };
                _productService.Add(entity);

                CreateMessage("Ekleme", $"{entity.Name} adlı ürün eklendi.", "success");

                return RedirectToAction("ProductList");
            }
            return View(model);
        }
        // güncelleme sayfasını getirir
        [HttpGet]
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _productService.GetByIdWithCategories((int)id);
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
                ImageUrl = entity.ImageUrl,
                IsApproved = entity.IsApproved,
                IsHome = entity.IsHome,
                SelectedCategories = entity.ProductCategories.Select(p => p.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct(ProductModel model, int[] categoryIds, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var entity = _productService.GetById(model.ProductId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.Url = model.Url;
                //entity.ImageUrl = model.ImageUrl;
                entity.Description = model.Description;
                entity.IsApproved = model.IsApproved;
                entity.IsHome = model.IsHome;
                if (file != null)
                {
                    // if ((file.ContentType.ToLower() == "png") || (file.ContentType.ToLower() == "jpg") || (file.ContentType.ToLower() == "jpeg"))
                    // {
                    //     if (file.Length < 1500000)
                    //     {
                    entity.ImageUrl = file.FileName;
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    entity.ImageUrl = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    //     }
                    // }

                }
                if (_productService.Update(entity, categoryIds))
                {
                    CreateMessage("Güncelleme", $"{entity.Name} adlı ürün güncellendi.", "success");
                    return RedirectToAction("ProductList");
                }

                CreateMessage("Hata", $"{_productService.ErrorMessage}", "danger");
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        public IActionResult DeleteProduct(int productId)
        {
            var entity = _productService.GetById(productId);
            if (entity != null)
            {
                _productService.Delete(entity);
            }
            CreateMessage("Silme", $"{entity.Name} adlı ürün silindi.", "danger");
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
            if (ModelState.IsValid)
            {
                var entity = new Category()
                {
                    Name = model.Name,
                    Url = model.Url
                };
                _categoryService.Add(entity);

                CreateMessage("Ekleme", $"{entity.Name} adlı kategori eklendi.", "success");
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        // güncelleme sayfasını getirir
        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _categoryService.GetByIdWithProducts((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Name = entity.Name,
                Url = entity.Url,
                Products = entity.ProductCategories.Select(p => p.Product).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = _categoryService.GetByIdWithProducts(model.CategoryId);
                if (entity == null)
                {
                    return NotFound();
                }
                entity.Name = model.Name;
                entity.Url = model.Url;

                _categoryService.Update(entity);


                CreateMessage("Güncelleme", $"{entity.Name} adlı kategori güncellendi.", "success");
                return RedirectToAction("CategoryList");
            }
            return View(model);
        }
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);
            if (entity != null)
            {
                _categoryService.Delete(entity);
            }
            CreateMessage("Silme", $"{entity.Name} adlı kategori silindi.", "danger");
            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public IActionResult DeleteFromCategory(int productId, int categoryId)
        {
            _categoryService.DeleteFromCategory(productId, categoryId);
            return Redirect("/admin/categories/" + categoryId);
        }
        public void CreateMessage(string title, string message, string alerttype)
        {
            TempData.Put("message", new AlertMessage()
            {
                Title = title,
                Message = message,
                AlertType = alerttype
            });
        }

    }
}