using System.Threading.RateLimiting;

namespace GymApp.NutritionService.API.Middlewares;

public static class RateLimitingMiddleware
{
    public static IServiceCollection AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = 429;

                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    await context.HttpContext.Response.WriteAsync(
                        $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s).",
                        token
                    );
                }
                else
                {
                    await context.HttpContext.Response.WriteAsync(
                        "Too many requests. Please try again later. ",
                        token
                    );
                }
            };

            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(
                    partitionKey: httpContext.User.Identity?.Name ?? httpContext.Request.Headers.Host.ToString(),
                    factory: partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 10,
                        QueueLimit = 0,
                        Window = TimeSpan.FromMinutes(1)
                    }
                )
            );
        });

        return services;
    }

    public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder builder)
    {
        return builder.UseRateLimiter();
    }
}