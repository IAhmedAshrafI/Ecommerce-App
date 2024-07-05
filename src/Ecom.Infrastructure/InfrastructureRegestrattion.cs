using Ecom.core.Entities;
using Ecom.core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Ecom.Infrastructure.Data.Config;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure
{
    public static class InfrastructureRegestrattion
    {
        public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {
      services.AddScoped<ITokenServices, TokenServices>();
      services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
      //services.AddScoped<IcategoryRepository, CategoryRepository>();
      //services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

      //configure Idenitty
      services.AddIdentity<AppUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();
      services.AddMemoryCache();
      services.AddAuthentication(opt =>
      {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
                    .AddJwtBearer(opt =>
                    {
                      opt.TokenValidationParameters = new TokenValidationParameters
                      {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                        ValidIssuer = configuration["Token:Issuer"],
                        ValidateIssuer = true,
                        ValidateAudience = false
                      };
                    });
      return services;
        }

    public static async void InfrastructureConfigMiddleware(this IApplicationBuilder app)
    {
      using (var scope = app.ApplicationServices.CreateScope())
      {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        await IdentitySeed.SeedUserAsync(userManager);
      }
    }

  }

  }

