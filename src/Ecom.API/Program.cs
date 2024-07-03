using Ecom.Infrastructure;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Ecom.API.Middleware;
using Ecom.API.Extensions;
using StackExchange.Redis;
using Ecom.Infrastructure.Repositories;
using Ecom.Core.Services;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
  var securitySchema = new OpenApiSecurityScheme
  {
    Name = "Authorization",
    Description = "JWt Auth Bearer",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    Reference = new OpenApiReference
    {
      Id = "Bearer",
      Type = ReferenceType.SecurityScheme
    }
  };
  s.AddSecurityDefinition("Bearer", securitySchema);
  var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
  s.AddSecurityRequirement(securityRequirement);
});
builder.Services.InfrastructureConfiguration(builder.Configuration);

builder.Services.AddSingleton<IConnectionMultiplexer>(i =>
{
  var configure = ConfigurationOptions.Parse(builder.Configuration.GetConnectionString("Redis"), true);
  return ConnectionMultiplexer.Connect(configure);
});

builder.Services.AddScoped<IOrderServices, OrderServices>();


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                             builder =>
                             {
                          builder.WithOrigins("http://localhost:4200")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

//add configration for auto mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//add configration for file provider
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors(myAllowSpecificOrigins);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

InfrastructureRegestrattion.InfrastructureConfigMiddleware(app);

app.Run();
