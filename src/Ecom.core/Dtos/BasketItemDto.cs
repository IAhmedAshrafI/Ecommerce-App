using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos
{
  public class BasketItemDto
  {
    [Required]
    public int Id { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductPicture { get; set; }
    [Required]
    [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Greater Thant Zero")]
    public decimal Price { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = "Quantity Must Greater Thant Zero")]
    [Required]
    public int Quantity { get; set; }
    [Required]
    public string Category { get; set; }
  }
}
