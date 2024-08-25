using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechStore.Products.Migrations;
using TechStore.Products.Repositories;
using TechStore.Products.Services;
namespace TechStore.Products.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IProductService, Services.Impl.ProductService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();            
            services.AddAutoMapper(Assembly.Load("TechStore.Products"));
            services.AddDbContext<ProductsDbContext>(
                options => {
                    options.UseSqlServer(GetConnectionString(),
                        builder => builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(3), null));
                    options.UseLazyLoadingProxies(true);
                }
            );
        }

        private static string GetConnectionString()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", true, true)
                        .Build();
            var strConn = config["ConnectionStrings:Test"];

            return strConn;
        }
    }
}
