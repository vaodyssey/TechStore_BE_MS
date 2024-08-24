﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Auth.Migrations;
using TechStore.Auth.Repositories;
using TechStore.Auth.Services;
using TechStore.Auth.Services.Impl;
using TechStore.Auth.Utils.JWT;
namespace TechStore.Auth.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IJWTUtils, JWTUtils>();
            services.AddAutoMapper(typeof(Program));
            services.AddDbContext<AuthDbContext>(
                options =>  
                    options.UseSqlServer(GetConnectionString(),
                builder=>builder.EnableRetryOnFailure(2,TimeSpan.FromSeconds(3),null))
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
