using GymApp.NutritionService.Core.Caching;
using GymApp.NutritionService.Core.Repositories.Interfaces;
using GymApp.NutritionService.Data.Entities;
using GymApp.NutritionService.Core.Services.Interfaces;

namespace GymApp.NutritionService.Core.Services;

public class DietService(IRepository<Diet> _repository, IRedisService _redisService) : IDietService
{
    public async Task<Diet?> GetDietByIdAsync(Guid id)
    {
        var cachedDiet = await _redisService.GetAsync<Diet>(id.ToString());
        if (cachedDiet != null)
        {
            return cachedDiet;
        }

        var diet = await _repository.GetByIdAsync(id);
        if (diet != null)
        {
            await _redisService.SetAsync(id.ToString(), diet, TimeSpan.FromSeconds(20));
        }

        return diet;
    }

    public async Task<IEnumerable<Diet>> GetAllDietsAsync()
    {
        var cachedDiets = await _redisService.GetAsync<IEnumerable<Diet>>("all_diets");
        if (cachedDiets != null)
        {
            return cachedDiets;
        }

        var diets = await _repository.GetAllAsync();
        await _redisService.SetAsync("all_diets", diets, TimeSpan.FromSeconds(20));
        return diets;
    }

    public async Task AddDietAsync(Diet diet)
    {
        await _repository.AddAsync(diet);
    }

    public async Task UpdateDietAsync(Diet diet)
    {
        await _repository.UpdateAsync(diet);
    }

    public async Task DeleteDietAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
        await _redisService.RemoveAsync(id.ToString());
    }
}