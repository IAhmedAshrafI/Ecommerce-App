using AutoMapper;
using Ecom.API.Errors;
using Ecom.API.Extensions;
using Ecom.core.Dtos;
using Ecom.core.Entities;
using Ecom.Core.Dtos;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Ecom.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : ControllerBase
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenServices _tokenServices;
    private readonly IMapper _mapper;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenServices tokenServices, IMapper mapper)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _tokenServices = tokenServices;
      _mapper = mapper;
    }



    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
      var user = await _userManager.FindByEmailAsync(dto.Email);
      if (user is null) return Unauthorized(new BaseCommonResponse(401));

      var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
      if (result is null || result.Succeeded == false) return Unauthorized(new BaseCommonResponse(401));

      return Ok(new UserDto
      {
        DisplayName = user.DisplayName,
        Email = user.Email,
        Token = _tokenServices.CreateToken(user)
      });

    }


    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {

      if (CheckEmailExist(dto.Email).Result.Value)
      {
        return new BadRequestObjectResult(new ApiValidationErrorResponse
        {
          Errors = new[] { "This Email is Already Taken" }
        });
      }

      var user = new AppUser
      {
        DisplayName = dto.DisplayName,
        UserName = dto.Email,
        Email = dto.Email
      };

      var result = await _userManager.CreateAsync(user, dto.Password);
      if (result.Succeeded == false) return BadRequest(new BaseCommonResponse(400));
      return Ok(new UserDto
      {
        DisplayName = dto.DisplayName,
        Email = dto.Email,
        Token = _tokenServices.CreateToken(user)
      });
    }



    [Authorize]
    [HttpGet("get-current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
      var user = await _userManager.FindEmailByClaimPrincipal(HttpContext.User);
      return Ok(new UserDto
      {
        DisplayName = user.DisplayName,
        Email = user.Email,
        Token = _tokenServices.CreateToken(user)
      });

    }
    [HttpGet("check-email-exist")]
    public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
    {
      var result = await _userManager.FindByEmailAsync(email);
      if (result is not null)
      {
        return true;
      }
      return false;
    }


    [Authorize]
    [HttpGet("get-user-address")]
    public async Task<IActionResult> GetUserAddress()
    {
      var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);

      var _result = _mapper.Map<Address, AddressDto>(user.Address);
      return Ok(_result);
    }


    [Authorize]
    [HttpPut("update-user-address")]
    public async Task<IActionResult> UpdateUserAddress(AddressDto dto)
    {
      var user = await _userManager.FindUserByClaimPrincipalWithAddress(HttpContext.User);
      user.Address = _mapper.Map<AddressDto, Address>(dto);
      var result = await _userManager.UpdateAsync(user);
      if (result.Succeeded) return Ok(_mapper.Map<Address, AddressDto>(user.Address));

      return BadRequest($"problem while updating this user {HttpContext.User}");
    }


  }
}
