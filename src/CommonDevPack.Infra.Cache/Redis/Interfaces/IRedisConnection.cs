using StackExchange.Redis;

namespace CommonDevPack.Infra.Cache.Redis.Interfaces;
public interface IRedisConnection
{
    IConnectionMultiplexer RedisCache { get; }
}
