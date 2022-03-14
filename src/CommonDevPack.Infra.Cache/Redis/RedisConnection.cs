using CommonDevPack.Infra.Cache.Redis.Interfaces;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace CommonDevPack.Infra.Cache.Redis;
public class RedisConnection : IRedisConnection
{
    public IConnectionMultiplexer RedisCache { get; }

    public RedisConnection(IOptions<RedisConfiguration> servicesConfiguration,
        bool preventthreadtheft = false)
    {
        ArgumentNullException.ThrowIfNull(servicesConfiguration.Value.ConnectionString, "ConnectionString");

        ConnectionMultiplexer.SetFeatureFlag("preventthreadtheft", preventthreadtheft);
        RedisCache = ConnectionMultiplexer.Connect(servicesConfiguration.Value.ConnectionString);
    }
}
