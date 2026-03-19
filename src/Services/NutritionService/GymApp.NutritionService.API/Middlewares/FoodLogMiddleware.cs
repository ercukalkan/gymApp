namespace GymApp.NutritionService.API.Middlewares;

public class FoodLogMiddleware(RequestDelegate next, ILogger<FoodLogMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        logger.LogInformation("Method: {}, Path: {}", context.Request.Method, context.Request.Path);
        await next(context);
    }
}

public static class FoodLogMiddlewareExtension
{
    public static IApplicationBuilder UseFoodLogMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseWhen(
            context =>
            {
                if (context.Request.RouteValues.TryGetValue("controller", out object? value))
                {
                    return value is not null && (string)value == "Food";
                }
                return false;
            },
            builder =>
            {
                builder.UseMiddleware<FoodLogMiddleware>();
            }
        );
    }
}