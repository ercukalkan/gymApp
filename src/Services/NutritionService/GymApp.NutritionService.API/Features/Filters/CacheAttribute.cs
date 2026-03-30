using Microsoft.AspNetCore.Mvc.Filters;
using GymApp.Shared.RedisCache;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.NutritionService.API.Features.Filters;

/// <summary>
/// Attribute that enables caching of HTTP response bodies for a specified duration.
/// </summary>
/// <remarks>
/// This attribute implements <see cref="IAsyncResourceFilter"/> to intercept and cache the response body
/// of an action method using Redis. The cached response is stored with a fixed key and can be retrieved
/// for subsequent requests within the specified time period.
/// </remarks>
/// <param name="seconds">The duration in seconds for which the response should be cached in Redis.</param>
[AttributeUsage(AttributeTargets.Method)]
public class CacheAttribute(int seconds) : Attribute, IAsyncResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        IRedisService redisService = context.HttpContext.RequestServices.GetRequiredService<IRedisService>();

        ResourceExecutedContext executedContext = await next();

        if (executedContext.Result is OkObjectResult okObjectResult)
        {
            if (okObjectResult.Value != null)
            {
                await redisService.SetAsync("cached_123", okObjectResult.Value, TimeSpan.FromSeconds(seconds));
            }
        }
    }
}