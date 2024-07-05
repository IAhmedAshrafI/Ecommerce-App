
using Ecom.API.Errors;
using Ecom.core.Interfaces;
using Ecom.Core.Entities;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PaymentsController : ControllerBase
  {
    private readonly IPaymentServices _paymentServices;

    public PaymentsController(IPaymentServices paymentServices)
    {
      _paymentServices = paymentServices;
    }
    [Authorize]
    [HttpPost("{basketId}")]
    public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(string basketId)
    {
      var basket = await _paymentServices.CreateOrUpdatePaymentIntent(basketId);
      if (basket == null) return BadRequest(new BaseCommonResponse(400, "Problem With Your Basket"));

      return basket;
    }
  }
}
