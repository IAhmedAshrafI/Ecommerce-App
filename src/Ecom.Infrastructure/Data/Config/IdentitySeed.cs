using Ecom.core.Entities;
using Ecom.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Data.Config
{
  public class IdentitySeed
  {
    public static async Task SeedUserAsync(UserManager<AppUser> userManager)
    {
      if (!userManager.Users.Any())
      {
        //create new user
        var user = new AppUser
        {
          DisplayName = "Ali",
          Email = "ali@ali.com",
          UserName = "ali@ali.com",
          Address = new Address
          {
            FirstName = "ali",
            LastName = "mohamed",
            City = "Giza",
            State = "haram",
            Street = "test street",
            ZipCode = "123"

          }
        };
        await userManager.CreateAsync(user, "P@$$w0rd");
      }
    }
  }
}
