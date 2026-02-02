using Microsoft.Extensions.DependencyInjection;
using GymApp.IdentityService.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace GymApp.IdentityService.Data.DbSeeder;

public static class DbSeeder
{
    public static async Task Initialize(IServiceProvider serviceProvider, string testPassword)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(nameof(DbSeeder));

        try
        {
            logger.LogInformation("Starting database seeding...");

            var usersToCreate = new List<(string username, string role)>
            {
                ("admin", "admin"),
                ("manager", "manager"),
            };

            for (int i = 1; i <= 5; i++)
            {
                usersToCreate.Add(($"trainer{i}", "trainer"));
                usersToCreate.Add(($"student{i}", "student"));
            }

            foreach (var (username, role) in usersToCreate)
            {
                await Prepare(serviceProvider, username, testPassword, role);
            }

            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database seeding.");
            throw;
        }
    }

    private static async Task Prepare(IServiceProvider serviceProvider, string username, string password, string role)
    {
        try
        {
            var user = await CreateUserIfNotExist(serviceProvider, username, password);
            await CreateRolesIfNotExist(serviceProvider, role);
            await AssignRolesToUsersIfNotAssigned(serviceProvider, user, role);
        }
        catch (Exception ex)
        {
            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(DbSeeder));
            logger.LogError(ex, "Error preparing user '{Username}' with role '{Role}'.", username, role);
            throw;
        }
    }

    private static async Task<ApplicationUser> CreateUserIfNotExist(IServiceProvider serviceProvider, string username, string password)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        var userCheck = await userManager.FindByNameAsync(username);

        if (userCheck == null)
        {
            var email = $"{username}@test.com";
            userCheck = new ApplicationUser
            {
                UserName = username,
                Email = email,
                EmailConfirmed = true
            };

            var createResult = await userManager.CreateAsync(userCheck, password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Failed to create user '{username}'. Errors: {errors}");
            }
        }

        return userCheck;
    }

    private static async Task CreateRolesIfNotExist(IServiceProvider serviceProvider, string roleName)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        IdentityResult identityResult;

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            identityResult = await roleManager.CreateAsync(new IdentityRole(roleName));

            if (!identityResult.Succeeded)
            {
                var errors = string.Join(", ", identityResult.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Failed to create role '{roleName}'. Errors: {errors}");
            }
        }
    }

    private static async Task AssignRolesToUsersIfNotAssigned(IServiceProvider serviceProvider, ApplicationUser user, string role)
    {
        ArgumentNullException.ThrowIfNull(user);

        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await userManager.IsInRoleAsync(user, role))
        {
            var addToRoleResult = await userManager.AddToRoleAsync(user, role);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join(", ", addToRoleResult.Errors.Select(error => error.Description));
                throw new InvalidOperationException($"Failed to add role '{role}' to user '{user.UserName}'. Errors: {errors}");
            }
        }
    }
}