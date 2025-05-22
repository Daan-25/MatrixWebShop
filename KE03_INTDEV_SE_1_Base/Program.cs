using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            
            builder.Services.AddDbContext<MatrixIncDbContext>(
                options => options.UseSqlite("Data Source=MatrixInc.db"));
            
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();
            builder.Services.AddScoped<UserService>();
            
            builder.Services.AddAuthentication("MyCookieAuth")
                .AddCookie("MyCookieAuth", options =>
                {
                    options.LoginPath = "/Login";
                });

            builder.Services.AddAuthorization();
            
            builder.Services.AddRazorPages();

            var app = builder.Build();
            
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<MatrixIncDbContext>();
                context.Database.EnsureCreated();
                MatrixIncDbInitializer.Initialize(context);
            }

            app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseSession();

            app.UseRouting();
            
            app.UseAuthentication();
            
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
