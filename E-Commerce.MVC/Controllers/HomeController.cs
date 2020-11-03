using System;
using E_Commerce.DataAccess.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    // localhost:5000/home
    public class HomeController : Controller
    {
        private IProductRepository _productRepository;
        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        // default
        // localhost:5000/home/index
        public IActionResult Index()
        {
            int saat = DateTime.Now.Hour;
            ViewBag.Greeting = saat > 12 ? "İyi Günler" : "Günaydın";
            ViewBag.Username = "Salih";
            return View();
        }
        // localhost:5000/home/about
        /*public IActionResult About()
        {
            return View();
        }*/
    }
}