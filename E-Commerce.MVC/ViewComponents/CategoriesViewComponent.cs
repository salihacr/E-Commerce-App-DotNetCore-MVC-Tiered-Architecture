using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using E_Commerce.Business.Abstract;

namespace E_Commerce.MVC.ViewComponents
{
    public class CategoriesViewComponent:ViewComponent
    {
        private ICategoryService _categoryService;
        public CategoriesViewComponent(ICategoryService categoryService)
        {
            _categoryService=categoryService;
        }

        public IViewComponentResult Invoke()
        {
            if (RouteData.Values["action"].ToString()=="list")
                ViewBag.SelectedCategory = RouteData?.Values["id"];
            return View(_categoryService.GetAll());
        }
    }
}