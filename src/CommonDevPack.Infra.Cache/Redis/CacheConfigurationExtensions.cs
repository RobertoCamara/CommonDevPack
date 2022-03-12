using CommonDevPack.Infra.Cache.Redis.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommonDevPack.Infra.Cache.Redis;
public static class CacheConfigurationExtensions
{
    public static IServiceCollection AddRedisConfigure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RedisConfiguration>(configuration.GetSection(RedisConfiguration.SectionKeyDefault));
        AddRedisConfigure(services);
        return services;        
    }

    public static IServiceCollection AddRedisConfigure(this IServiceCollection services, IConfiguration configuration, string customSectionKey)
    {
        services.Configure<RedisConfiguration>(configuration.GetSection(customSectionKey));
        AddRedisConfigure(services);
        return services;
    }

    private static void AddRedisConfigure(IServiceCollection services)
    {
        services.AddSingleton<IRedisConnection, RedisConnection>();
        services.AddScoped<IRedisService, RedisService>();
    }
}


