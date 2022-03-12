using CommonDevPack.Infra.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace CommonDevPack.Infra.Cache.Tests.Redis;
public class CacheConfigurationExtensionsTestFixture
{
    public IConfiguration GetConfiguration(string appsettings) =>
           new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(appsettings, false, true)
                .Build();

    public IHost CreateDefaultBuilder(string appsettings, string? customSectionKey = null) =>
        Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                if (string.IsNullOrWhiteSpace(customSectionKey))
                    services.AddRedisConfigure(GetConfiguration(appsettings));
                else
                    services.AddRedisConfigure(GetConfiguration(appsettings), customSectionKey);
            })
            .Build();

    public IOptions<RedisConfiguration> GetRedisConfigurationInstance(IHost host) => 
        host.Services.GetService<IOptions<RedisConfiguration>>();
}

[CollectionDefinition(nameof(CacheConfigurationExtensionsTestFixture))]
public class CacheConfigurationExtensionsTestFixtureCollection
    : ICollectionFixture<CacheConfigurationExtensionsTestFixture>
{ }
