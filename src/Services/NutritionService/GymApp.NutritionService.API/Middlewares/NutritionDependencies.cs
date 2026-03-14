using GymApp.NutritionService.Core.Services;
using GymApp.NutritionService.Core.Repositories;
using GymApp.NutritionService.Core.Services.Interfaces;
using GymApp.NutritionService.Core.Repositories.Interfaces;

namespace GymApp.NutritionService.API.Middlewares;

public static class NutritionDependencies
{
    public static IServiceCollection AddNutritionServices(this IServiceCollection services)
    {
        services.AddScoped<IFoodService, FoodService>();
        services.AddScoped<IMealService, MealService>();
        services.AddScoped<IDietService, DietService>();

        return services;
    }

    public static IServiceCollection AddNutritionRepositories(this IServiceCollection services)
    {
        services.AddScoped<IFoodRepository, FoodRepository>();
        services.AddScoped<IMealRepository, MealRepository>();
        services.AddScoped<IDietRepository, DietRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}