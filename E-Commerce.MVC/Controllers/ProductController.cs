using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.MVC.Controllers
{
    public class ProductController : Controller
    {
        // default
        // localhost:5000/product/Index
        public IActionResult Index()
        {
            return View();
        }
        // localhost:5000/product/list
        public IActionResult List()
        {
            return View();
        }
        // localhost:5000/product/details
        public IActionResult Details(int id)
        {
            return View();
        }
    }
}