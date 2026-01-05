using StackExchange.Redis;

namespace GymApp.NutritionService.Core.Caching;

public interface IRedisService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan timespan);
    Task RemoveAsync(string key);
}
