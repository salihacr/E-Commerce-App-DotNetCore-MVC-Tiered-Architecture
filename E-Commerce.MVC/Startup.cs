using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using E_Commerce.Business.Abstract;
using E_Commerce.Business.Concrete;
using E_Commerce.DataAccess.Abstract;
using E_Commerce.DataAccess.Concrete.EntityFrameworkCoreSqlServer;
using E_Commerce.MVC.EmailServices;
using E_Commerce.MVC.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;



namespace E_Commerce.MVC
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // identity
            services.AddDbContext<ApplicationContext>(options =>
            options.UseSqlServer("Server =SALIH; Database=AvocodeTestDb; Trusted_Connection = True"));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;

                // lockout
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.AllowedForNewUsers = true;

                //options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".E_Commerce.Security.Cookie",
                    SameSite = SameSiteMode.Strict
                };
            });

            // repositories for dependency injection data access
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICartRepository, CartRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            // services for dependency injection business layer
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<ICartService, CartManager>();

            // email sender dependency injection
            services.AddScoped<IEmailSender, SmtpEmailSender>(i =>
            new SmtpEmailSender(
                _configuration["EmailSender:Host"],
                _configuration.GetValue<int>("EmailSender:Port"),
                _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                _configuration["EmailSender:UserName"],
                _configuration["EmailSender:Password"]
                )
            );


            // MVC
            // RAZOR PAGES
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // Static Files MiddleWare
            app.UseStaticFiles();// wwwroot
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                RequestPath = "/modules"
            });

            // uygulama geliştirme sırasında bu bölge çalışır
            if (env.IsDevelopment())
            {
                SeedDatabase.Seed();
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHsts();
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Ana Dizin // localhost:5000
                /*endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });*/
                app.UseEndpoints(endpoints =>
                {
                    // Admin Pages

                    /*Products*/
                    endpoints.MapControllerRoute(
                       name: "adminproducts",
                       pattern: "admin/products",
                       defaults: new { controller = "Admin", Action = "ProductList" }
                   );
                    endpoints.MapControllerRoute(
                       name: "adminproductadd",
                       pattern: "admin/products/add",
                       defaults: new { controller = "Admin", Action = "AddProduct" }
                   );
                    endpoints.MapControllerRoute(
                         name: "adminproductedit",
                         pattern: "admin/products/{id?}",
                         defaults: new { controller = "Admin", Action = "EditProduct" }
                     );


                    /*Categories*/
                    endpoints.MapControllerRoute(
                        name: "admincategories",
                        pattern: "admin/categories",
                        defaults: new { controller = "Admin", Action = "CategoryList" }
                    );
                    endpoints.MapControllerRoute(
                       name: "admincategoryadd",
                       pattern: "admin/categories/add",
                       defaults: new { controller = "Admin", Action = "AddCategory" }
                   );
                    endpoints.MapControllerRoute(
                          name: "admincategoryedit",
                          pattern: "admin/categories/{id?}",
                          defaults: new { controller = "Admin", Action = "EditCategory" }
                      );


                    // User Pages
                    endpoints.MapControllerRoute(
                       name: "search",
                       pattern: "search",
                       defaults: new { controller = "Shop", Action = "search" }
                   );
                    endpoints.MapControllerRoute(
                       name: "productdetails",
                       pattern: "{url}",
                       defaults: new { controller = "Shop", Action = "details" }
                   );
                    endpoints.MapControllerRoute(
                        name: "products",
                        pattern: "products/{category?}",
                        defaults: new { controller = "Shop", Action = "list" }
                    );
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
                });


            });
        }
    }
}
