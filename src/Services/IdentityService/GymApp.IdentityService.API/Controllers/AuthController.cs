using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.IdentityService.Data.Entities;
using GymApp.IdentityService.Core.DTOs;
using GymApp.IdentityService.API.Features.EventPublishers;
using GymApp.IdentityService.Core.Services;
using GymApp.IdentityService.Data.Context;

namespace GymApp.IdentityService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(
    ILogger<AuthController> _logger,
    UserManager<ApplicationUser> _userManager,
    SignInManager<ApplicationUser> _signInManager,
    NewUserCreatedEventPublisher _newUserCreatedEventPublisher,
    ITokenService _tokenService) : ControllerBase
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
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("Login attempt with non-existent email: {email}", request.Email);
            return Unauthorized(new { message = "Invalid credentials" });
        }

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            _logger.LogWarning("Failed login attempt for user: {UserId}", user.Id);

            if (result.IsLockedOut)
                return Unauthorized(new { message = "Account locked. Try again later." });

            return Unauthorized(new { message = "Invalid credentials" });
        }

        var accessToken = _tokenService.GenerateAccessToken(
            user.Id,
            user.Email!,
            user.UserName!,
            await _userManager.GetRolesAsync(user)
        );
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("User {userId} logged in successfully", user.Id);

        return Ok(new LoginResponseDTO
        {
            Success = true,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = 15,
            User = new UserDTO
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty
            }
        });
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