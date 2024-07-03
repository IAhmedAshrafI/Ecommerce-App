using AutoMapper;
using Ecom.core.Dtos;
using Ecom.core.Entities;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Entities.Orders;

namespace Ecom.API.MappingProfiles
{
  public class MappingUser : Profile
  {
    public MappingUser()
    {
      CreateMap<Address, AddressDto>().ReverseMap();
      CreateMap<ShipAddress, AddressDto>().ReverseMap();

    }
  }
}
