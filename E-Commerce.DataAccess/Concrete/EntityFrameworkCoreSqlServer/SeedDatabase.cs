using System.Linq;
using E_Commerce.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new AvocodeContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }
                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(Products);
                }
            }
            context.SaveChanges();
        }
        private static Category[] Categories = {
           new Category(){Name="Sweat"},
           new Category(){Name="Ayakkabı"},
           new Category(){Name="Çanta"}
        };
        private static Product[] Products = {
           new Product(){Name="T-Shirt",Price=50,Description="iyi tshirt",IsApproved=true},
           new Product(){Name="T-Shirt Uzun",Price=30,Description="iyi uzun kol",IsApproved=true},
           new Product(){Name="T-Shirt Kısa",Price=70,Description="iyi kısa kol",IsApproved=true}
        };
    }
}