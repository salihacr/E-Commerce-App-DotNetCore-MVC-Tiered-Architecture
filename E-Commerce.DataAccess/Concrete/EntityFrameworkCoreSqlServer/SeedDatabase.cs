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
                    context.AddRange(ProductCategories);
                }
            }
            context.SaveChanges();
        }
        private static Category[] Categories = {
           new Category(){Name="Kadın"},
           new Category(){Name="Erkek"},
           new Category(){Name="Sweat"},
           new Category(){Name="Ayakkabı"},
           new Category(){Name="Çanta"}
        };
        private static Product[] Products = {
           new Product(){Name="T-Shirt", Url="denemeurl", Price=50, Description="iyi tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt uzun", Url="denemeurl2", Price=70, Description="iyi uzun tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt kısa", Url="denemeurl3", Price=30, Description="iyi kısa tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="Ayakkabı", Url="denemeurl10", Price=300, Description="güzel ayakkabı", ImageUrl="3.jpg", IsApproved=true},
           new Product(){Name="Çizme", Url="denemeurl11", Price=350, Description="güzel çizme", ImageUrl="10.jpg", IsApproved=true},
           new Product(){Name="Sırt Çantası", Url="denemeurl15", Price=120, Description="güzel çanta", ImageUrl="8.jpg", IsApproved=true},
           new Product(){Name="Kapşonlu", Url="denemeurl100", Price=80, Description="güzel kapşon", ImageUrl="7.jpg", IsApproved=true}
        };
        private static ProductCategory[] ProductCategories ={
            new ProductCategory(){Product=Products[0],Category=Categories[1]},
            new ProductCategory(){Product=Products[0],Category=Categories[2]},
            new ProductCategory(){Product=Products[1],Category=Categories[1]},
            new ProductCategory(){Product=Products[1],Category=Categories[2]},
            new ProductCategory(){Product=Products[2],Category=Categories[2]},
            new ProductCategory(){Product=Products[3],Category=Categories[3]},
            new ProductCategory(){Product=Products[4],Category=Categories[3]},
            new ProductCategory(){Product=Products[5],Category=Categories[4]},
            new ProductCategory(){Product=Products[6],Category=Categories[2]}
        };
    }
}