using CommonDevPack.Infra.Cache.Tests.Redis.CacheConfigurationExtensions;
using FluentAssertions;
using System;
using Xunit;

namespace CommonDevPack.Infra.Cache.Tests.Redis.RedisConnection;

[Collection(nameof(CacheConfigurationExtensionsTestFixture))]
public class RedisConnectionTest
{
    private readonly CacheConfigurationExtensionsTestFixture _cacheConfigurationTestFixture;

    public RedisConnectionTest(CacheConfigurationExtensionsTestFixture cacheConfigurationTestFixture)
    {
        _cacheConfigurationTestFixture = cacheConfigurationTestFixture;
    }

    [Fact(DisplayName = nameof(ExceptionInInstantiate))]
    [Trait("Infra.Cache", "ExceptionInInstantiate - RedisConnection")]
    public void ExceptionInInstantiate()
    {
        var host = _cacheConfigurationTestFixture.CreateDefaultBuilder("appsettings-fail.json");

        var redisConfiguration = _cacheConfigurationTestFixture.GetRedisConfigurationInstance(host);

        Action action =
            () => new Cache.Redis.RedisConnection(redisConfiguration);

        action.Should()
              .Throw<ArgumentNullException>()
              .WithMessage("Value cannot be null. (Parameter 'ConnectionString')");        

    }
}
