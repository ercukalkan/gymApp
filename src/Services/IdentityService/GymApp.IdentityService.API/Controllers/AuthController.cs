using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.IdentityService.Data.Entities;
using GymApp.IdentityService.Core.DTOs;
using GymApp.IdentityService.API.Features.EventPublishers;

namespace GymApp.IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ILogger<AuthController> _logger,
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    NewUserCreatedEventPublisher _newUserCreatedEventPublisher) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            _logger.LogCritical("User '{Username}' registered successfully.", user.UserName);
            _logger.LogInformation("Trying to create Student entity by publishing NewUserCreatedEvent.");

            await _newUserCreatedEventPublisher.PublishNewUserCreated(user.Id, user.UserName!);

            return Ok();
        }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _userManager.DeleteAsync(user);
        if (result.Succeeded)
        {
            return NoContent();
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test()
    {
        await Task.Yield();
        return Ok("Auth Service is running.");
    }
}