using CommonDevPack.Infra.Cache.Redis;
using CommonDevPack.Infra.Cache.Tests.Redis.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonDevPack.Infra.Cache.Tests.Redis.CacheConfigurationExtensions;

[Collection(nameof(BaseConfigurationAppTest))]
public class CacheConfigurationExtensionsTestFixture : BaseConfigurationAppTest
{
    public IOptions<RedisConfiguration> GetRedisConfigurationInstance(IHost host) => 
        host.Services.GetService<IOptions<RedisConfiguration>>();
}

[CollectionDefinition(nameof(CacheConfigurationExtensionsTestFixture))]
public class CacheConfigurationExtensionsTestFixtureCollection
    : ICollectionFixture<CacheConfigurationExtensionsTestFixture>
{ }
