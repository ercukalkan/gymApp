using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.IdentityService.Data.Entities;
using GymApp.IdentityService.Core.DTOs;

namespace GymApp.IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(UserManager<ApplicationUser> _userManager, SignInManager<ApplicationUser> _signInManager) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded) return Ok();

        return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);

        if (result.Succeeded) return Ok();

        return Unauthorized();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}