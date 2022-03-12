using FluentAssertions;
using Xunit;

namespace CommonDevPack.Infra.Cache.Tests.Redis;


[Collection(nameof(CacheConfigurationExtensionsTestFixture))]
public class CacheConfigurationExtensionsTest
{
    private readonly CacheConfigurationExtensionsTestFixture _cacheConfigurationTestFixture;

    public CacheConfigurationExtensionsTest(CacheConfigurationExtensionsTestFixture cacheConfigurationTestFixture)
    {
        _cacheConfigurationTestFixture = cacheConfigurationTestFixture;
    }

    [Fact(DisplayName = nameof(AddRedisConfigureConnectionStringNull))]
    [Trait("Infra.Cache", "AddRedisConfigureConnectionStringNull")]
    public void AddRedisConfigureConnectionStringNull()
    {
        var host = _cacheConfigurationTestFixture.CreateDefaultBuilder("appsettings-fail.json");

        var redisConfiguration = _cacheConfigurationTestFixture.GetRedisConfigurationInstance(host);

        redisConfiguration.Value.ConnectionString.Should().BeNull();
        
    }

    [Fact(DisplayName = nameof(AddRedisConfigureConnectionStringNotNull))]
    [Trait("Infra.Cache", "AddRedisConfigureConnectionStringNotNull")]
    public void AddRedisConfigureConnectionStringNotNull()
    {
        var host = _cacheConfigurationTestFixture.CreateDefaultBuilder("appsettings.json");

        var redisConfiguration = _cacheConfigurationTestFixture.GetRedisConfigurationInstance(host);

        redisConfiguration.Value.ConnectionString.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(AddRedisConfigureCustomSectionKeyValid))]
    [Trait("Infra.Cache", "AddRedisConfigureCustomSectionKeyValid")]
    public void AddRedisConfigureCustomSectionKeyValid()
    {
        var host = _cacheConfigurationTestFixture.CreateDefaultBuilder("appsettings-customsection.json", "ConfigurationServices:Redis");

        var redisConfiguration = _cacheConfigurationTestFixture.GetRedisConfigurationInstance(host);

        redisConfiguration.Value.ConnectionString.Should().NotBeNull();
    }

    [Fact(DisplayName = nameof(AddRedisConfigureCustomSectionKeyInvalid))]
    [Trait("Infra.Cache", "AddRedisConfigureCustomSectionKeyInvalid")]
    public void AddRedisConfigureCustomSectionKeyInvalid()
    {
        var host = _cacheConfigurationTestFixture.CreateDefaultBuilder("appsettings-fail.json", "InvalidSection");

        var redisConfiguration = _cacheConfigurationTestFixture.GetRedisConfigurationInstance(host);

        redisConfiguration.Value.ConnectionString.Should().BeNull();
    }
}
