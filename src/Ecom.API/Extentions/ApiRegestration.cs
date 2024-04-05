using Ecom.API.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Ecom.API.Extensions
{
    public static class ApiRegestration
    {
        public static IServiceCollection AddApiRegestration(this IServiceCollection services)
        {
            //Configure Automapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //Configure IFileProvider
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState
                                .Where(x => x.Value.Errors.Count > 0)
                                .SelectMany(x => x.Value.Errors)
                                .Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            //Enable CORS
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200");
                });
            });
            return services;
        }
    }
}