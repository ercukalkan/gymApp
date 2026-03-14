using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GymApp.NutritionService.API.Middlewares;

public static class AuthMiddleware
{
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfigurationSection jwtSection)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["SecretKey"]!)),
                    ValidAudience = jwtSection["Audience"],
                    ValidateLifetime = true,
                    ValidIssuer = jwtSection["Issuer"]
                };
            }
        );

        services.AddAuthorization();

        return services;
    }

    public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
    {
        builder.UseAuthentication();
        builder.UseAuthorization();

        return builder;
    }
}