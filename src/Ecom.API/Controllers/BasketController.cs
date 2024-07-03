using AutoMapper;
using Ecom.core.Interfaces;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BasketController : ControllerBase
  {
    private readonly IUnitOfWork _uOW;
    private readonly IMapper _mapper;

    public BasketController(IUnitOfWork UOW, IMapper mapper)
    {
      _uOW = UOW;
      _mapper = mapper;
    }

    [HttpGet("get-basket-item/{Id}")]
    public async Task<IActionResult> GetBasketById(string Id)
    {
      var _basket = await _uOW.BasketRepository.GetBasketAsync(Id);
      return Ok(_basket ?? new CustomerBasket(Id));
    }

    [HttpPost("update-basket")]
    public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
    {
      var _basket = await _uOW.BasketRepository.UpdateBasketAsync(customerBasket);

      return Ok(_basket);
    }

    [HttpDelete("delete-basket-item/{Id}")]
    public async Task<IActionResult> DeleteBasket(string Id)
    {
      return Ok(await _uOW.BasketRepository.DeleteBasketAsync(Id));
    }


  }
}
