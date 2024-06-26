using Ecom.core.Entities;
using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecom.API.Extensions
{
  public static class UserManagerExtensions
  {
    public static async Task<AppUser> FindUserByClaimPrincipalWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
      var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
      return await userManager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == email);
    }

    public static async Task<AppUser> FindEmailByClaimPrincipal(this UserManager<AppUser> userManager, ClaimsPrincipal user)
    {
      var email = user?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
      return await userManager.Users.SingleOrDefaultAsync(x => x.Email == email);
    }
  }
}
