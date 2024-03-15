using System;
using Ecom.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.HasData(
                
                new Product { Id = 1, Name = "Iphone 12", Description = "Iphone 12", Price = 1000, CategoryId = 1 },
                new Product { Id = 2, Name = "Iphone 11", Description = "Iphone 11", Price = 900, CategoryId = 1 },
                new Product { Id = 3, Name = "Iphone 10", Description = "Iphone 10", Price = 800, CategoryId = 1 },
                new Product { Id = 4, Name = "Lenovo", Description = "Lenovo legion 5 pro", Price = 10000, CategoryId = 3 }

                );

        }
    }
}
