
using CommonDevPack.Infra.Cache.Redis;
using CommonDevPack.Infra.Cache.Redis.Interfaces;
using Infra.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

class Program
{
    private static TestRedisConfiguration _testRedisConfiguration;
    private static IRedisService _redisService;

    static Program()
    {
        var host = AppConfiguration.CreateDefaultBuilder();
        _testRedisConfiguration = host.Services.GetService<TestRedisConfiguration>();
        _redisService = host.Services.GetService<IRedisService>();
    }

    static async Task Main(string[] args)
    {
        var configurations = _testRedisConfiguration.WriteConfigurations();

        Console.WriteLine(configurations);

        var key = Guid.NewGuid().ToString();
        var value = DateTime.Now.ToString();
        
        _redisService.Set(key, value);

        Console.WriteLine($"Value set for redis to key: {key}\r\n->> Value: {value}");

        Console.WriteLine($"Get from redis by Key: {key}\r\n->> result: {_redisService.Get(key)}");

        Console.ReadLine();
    }    
}

public class TestRedisConfiguration
{
    private readonly RedisConfiguration _redisConfiguration;

    public TestRedisConfiguration(IOptions<RedisConfiguration> redisConfiguration)
    {
        if (redisConfiguration == null)
            throw new ArgumentNullException(nameof(redisConfiguration));

        _redisConfiguration = redisConfiguration.Value;
    }

    public string WriteConfigurations()
    {
        return $"Configurations:\r\n->> {_redisConfiguration.ConnectionString}\r\n->> {_redisConfiguration.DefaultExpirationTimeInSeconds}\r\n"; 
    }
}