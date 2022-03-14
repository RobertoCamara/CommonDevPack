using CommonDevPack.Infra.Cache.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using Xunit;

namespace CommonDevPack.Infra.Cache.Tests.Redis.Shared;
public class BaseConfigurationAppTest
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
}

