using Ecom.core.Entities;

namespace Ecom.Core.Entities.Orders
{
  public class DeliveryMethod : BaseEntity<int>
  {
    public string ShortName { get; set; }
    public string DeliveryTime { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
  }
}
