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
           new Category(){Name="Kadın",Url="kadin"},
           new Category(){Name="Erkek",Url="erkek"},
           new Category(){Name="Sweat",Url="sweat"},
           new Category(){Name="Ayakkabı",Url="ayakkabi"},
           new Category(){Name="Çanta",Url="canta"}
        };
        private static Product[] Products = {
           new Product(){Name="T-Shirt", Url="t-shirt1", Price=50, Description="iyi tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt uzun", Url="t-shirtuzun", Price=70, Description="iyi uzun tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="T-Shirt kısa", Url="t-shirtkisa", Price=30, Description="iyi kısa tshirt", ImageUrl="1.jpg", IsApproved=true},
           new Product(){Name="Ayakkabı", Url="ayakkabiadidas", Price=300, Description="güzel ayakkabı", ImageUrl="3.jpg", IsApproved=true},
           new Product(){Name="Çizme", Url="ayakkabicizme", Price=350, Description="güzel çizme", ImageUrl="10.jpg", IsApproved=true},
           new Product(){Name="Sırt Çantası", Url="sirtcantasi", Price=120, Description="güzel çanta", ImageUrl="8.jpg", IsApproved=true},
           new Product(){Name="Kapşonlu", Url="kapsonlu", Price=80, Description="güzel kapşon", ImageUrl="7.jpg", IsApproved=true}
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