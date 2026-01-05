using System.Text.Json;
using StackExchange.Redis;

namespace GymApp.NutritionService.Core.Caching;

public class RedisService(IConnectionMultiplexer redisMultiplexer) : IRedisService
{
    private readonly IDatabaseAsync redisDB = redisMultiplexer.GetDatabase(1);

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await redisDB.StringGetAsync(key);

        if (value.IsNullOrEmpty)
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan timespan)
    {
        var jsonData = JsonSerializer.Serialize(value);
        await redisDB.StringSetAsync(key, jsonData, timespan);
    }

    public async Task RemoveAsync(string key)
    {
        await redisDB.KeyDeleteAsync(key);
    }
}