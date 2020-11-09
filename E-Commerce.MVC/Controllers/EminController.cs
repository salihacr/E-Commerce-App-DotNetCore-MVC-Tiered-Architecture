using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using E-Commerce.MVC.Models;

namespace E-Commerce.MVC.Controllers
{
    public class EminController : Controller
    {
        public EminController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}