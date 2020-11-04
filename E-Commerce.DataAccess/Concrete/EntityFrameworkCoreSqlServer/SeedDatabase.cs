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
           new Product(){Name="T-Shirt", Url="denemeurl", Price=50, Description="iyi tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt uzun", Url="denemeurl2", Price=70, Description="iyi uzun tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt kısa", Url="denemeurl3", Price=30, Description="iyi kısa tshirt", ImageUrl="1.jpg", IsApproved=true}
        };
    }
}