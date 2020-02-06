using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core_WebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Core_WebApp.Services;
using Core_WebApp.CustomFilters;
namespace Core_WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Registers object in dependency
        /// 1. DB Conterxt  > EF Core DBContext
        /// 2. MVC Options > Filters, Formatters
        /// 3. Security > Authentication for users & Authorization 
        ///                                         > Based on roles > Role Base Policies  
        ///                                         > JSON web Tokens
        /// 4. Cookies
        /// 5. Cross Origin Resource Sharing (CORS) Policies > Web APIs
        /// 6. Custom Services  > Domain based service class aka business logic
        /// 7. Sessions
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
                options => options.Filters.Add(typeof(MyExceptionFilter))
                ); // MVC & Web Api request processing

            //Register the DbContext in DI Container
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppDbConnection"));
            });

            //Register repository services int h DI container
            services.AddScoped<IRepository<Category,int>, CategoryRepository>();
            services.AddScoped<IRepository<Product, int>, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// IApplicationBuilder > Used to HttpRequest using middlewares
        /// IWebHostEnvironment > Detect the hosting environment for execution
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //by default uses wwwroot folder 
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
