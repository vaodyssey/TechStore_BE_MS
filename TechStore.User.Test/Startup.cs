using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TechStore.User.Migrations;
using TechStore.User.Repositories;
using TechStore.User.Services;
using TechStore.User.Utils.JWT;
namespace TechStore.User.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, Services.Impl.AuthService.AuthService>();
            services.AddTransient<IUserService, Services.Impl.UserService.UserService>();
            services.AddTransient<IOrderService, Services.Impl.OrderService.OrderService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTUtils, JWTUtils>();
            services.AddAutoMapper(Assembly.Load("TechStore.User"));
            services.AddDbContext<UserDbContext>(
                options =>
                    options.UseSqlServer(GetConnectionString(),
                builder => builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(3), null))
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
