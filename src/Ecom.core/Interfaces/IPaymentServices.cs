using Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.core.Interfaces
{
  public interface IPaymentServices
  {
    public Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
  }
}
