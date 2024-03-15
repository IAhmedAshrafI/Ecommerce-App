using Ecom.core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.HasData(

            new Category { Id = 1, Name = "Electronics", Description = "Electronic Items" },
            new Category { Id = 2, Name = "Games", Description = "Games Items" },
            new Category { Id = 3, Name = "laptops", Description = "laptops Items" }
            
            );
        }
    }
}
