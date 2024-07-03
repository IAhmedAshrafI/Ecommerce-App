using Ecom.Core.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
  public class DeliveryMethodConfiguration : IEntityTypeConfiguration<DeliveryMethod>
  {
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {

      builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
      builder.HasData(
          new DeliveryMethod { Id = 1, ShortName = "DHL", Description = "Fastest Delivery time", Price = 20 },
          new DeliveryMethod { Id = 2, ShortName = "Aramex", Description = "Get it With 3 days", Price = 10 },
          new DeliveryMethod { Id = 3, ShortName = "Fedex", Description = "Slower but Cheap", Price = 5 },
          new DeliveryMethod { Id = 4, ShortName = "Jumia", Description = "Free", Price = 0 }
          );
    }
  }
}
