using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace GymApp.Shared.RedisCache.Configuration;

public static class RedisCacheConfiguration
{
    public static IServiceCollection AddRedisConfiguration(
        this IServiceCollection collection,
        string redisConnectionString)
    {
        return collection.AddSingleton<IConnectionMultiplexer>(config => ConnectionMultiplexer.Connect(redisConnectionString));
    }
}