using System;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    // localhost:5000/home
    public class HomeController : Controller
    {
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