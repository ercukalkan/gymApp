using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GymApp.IdentityService.Data.Entities;
using GymApp.IdentityService.Core.DTOs;
using GymApp.IdentityService.API.Features.EventPublishers;
using GymApp.IdentityService.Core.Services;
using Microsoft.EntityFrameworkCore;

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
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO requestDTO)
    {
        var user = new ApplicationUser
        {
            Email = requestDTO.Email,
            UserName = requestDTO.UserName,
            FirstName = requestDTO.FirstName,
            LastName = requestDTO.LastName,
            DateOfBirth = requestDTO.DateOfBirth,
            PhoneNumber = requestDTO.PhoneNumber,
            Gender = requestDTO.Gender
        };

        var result = await _userManager.CreateAsync(user, requestDTO.Password);

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
            User = UserDTO.CreateDTOFromUser(user, await _userManager.GetRolesAsync(user), await _userManager.IsInRoleAsync(user, "admin"))
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

    [HttpGet]
    public async Task<ActionResult<IList<UserDTO>>> GetUsers()
    {
        var users = await _userManager.Users.ToListAsync();

        if (users == null) return NotFound();

        var userDTOs = new List<UserDTO>();

        foreach (var user in users)
        {
            userDTOs.Add(UserDTO.CreateDTOFromUser(user, await _userManager.GetRolesAsync(user), await _userManager.IsInRoleAsync(user, "admin")));
        }

        return Ok(userDTOs);
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserWithRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null) return NotFound();

        return Ok(UserDTO.CreateDTOFromUser(user, await _userManager.GetRolesAsync(user), await _userManager.IsInRoleAsync(user, "admin")));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] UserUpdateRequestDTO requestDTO)
    {
        var existingUser = await _userManager.FindByIdAsync(id);

        if (existingUser == null) return NotFound();

        var existingUserRoles = await _userManager.GetRolesAsync(existingUser);

        var rolesToRemove = existingUserRoles.Except(requestDTO.Roles);

        if (rolesToRemove.Any())
            await _userManager.RemoveFromRolesAsync(existingUser, rolesToRemove);

        var rolesToAdd = requestDTO.Roles.Except(existingUserRoles);

        if (rolesToAdd.Any())
            await _userManager.AddToRolesAsync(existingUser, rolesToAdd);

        existingUser.FirstName = requestDTO.UserDTO.FirstName ?? existingUser.FirstName;
        existingUser.LastName = requestDTO.UserDTO.LastName ?? existingUser.LastName;
        existingUser.Email = requestDTO.UserDTO.Email ?? existingUser.Email;
        existingUser.PhoneNumber = requestDTO.UserDTO.PhoneNumber ?? existingUser.PhoneNumber;
        existingUser.DateOfBirth = requestDTO.UserDTO.DateOfBirth ?? existingUser.DateOfBirth;

        var result = await _userManager.UpdateAsync(existingUser);

        if (result.Succeeded)
        {
            _logger.LogInformation("User {UserId} updated successfully", id);
            return Ok(existingUser);
        }

        return BadRequest(result.Errors);
    }
}