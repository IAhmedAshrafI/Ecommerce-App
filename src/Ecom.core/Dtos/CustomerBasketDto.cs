using Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos
{
  public class CustomerBasketDto
  {
    public CustomerBasketDto()
    {
        
    }

    public CustomerBasketDto(string id)
    {
      Id = id;
    }
    public string Id { get; set; }
    public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
  }
}
