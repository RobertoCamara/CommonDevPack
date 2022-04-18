using CommonDevPack.Infra.Cache.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.ConsoleApp;
public class AppConfiguration
{
    public static IHost CreateDefaultBuilder()
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
                services
                    .AddSingleton<TestRedisConfiguration>()
                    .AddRedisConfigure(context.Configuration))
            .ConfigureLogging((_, logging) =>
            {
                logging.ClearProviders();
                logging.AddSimpleConsole(options => options.IncludeScopes = true);
                logging.AddEventLog();
            })
            .Build();
    }
}
