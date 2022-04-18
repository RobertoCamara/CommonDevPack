
using Bogus;
using CommonDevPack.Infra.Cache.Redis;
using CommonDevPack.Infra.Cache.Redis.Interfaces;
using Infra.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;
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
        _testRedisConfiguration.WriteConfigurations();

        SimpleExample();

        Console.WriteLine("Generate Random Data");
        await GenerateRandomData(100);

        var values = _redisService.GetAllValuesAsync();

        await foreach (var item in values)
        {
            Console.WriteLine($"Values: {item}");
        }

        Console.ReadLine();
    }    

    private static void SimpleExample()
    {
        var key = Guid.NewGuid().ToString();
        var value = DateTime.Now.ToString();

        _redisService.Set(key, value);

        Console.WriteLine($"Value set for redis to key: {key}\r\n->> Value: {value}");

        Console.WriteLine($"Get from redis by Key: {key}\r\n->> result: {_redisService.Get(key)}");
    }

    private static async Task GenerateRandomData(int numberOfRegisters = 10)
    {
        var faker = new Faker();

        for (int i = 0; i < numberOfRegisters; i++)
        {
            var finance = new
            {
                accountName = faker.Finance.AccountName(),
                creditCardNumber = faker.Finance.CreditCardNumber()
            };

           await _redisService.SetAsync(Guid.NewGuid().ToString(), finance);
        }
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

    public void WriteConfigurations()
    {
        Console.WriteLine($"Configurations:\r\n->> {_redisConfiguration.ConnectionString}\r\n->> {_redisConfiguration.DefaultExpirationTimeInSeconds}\r\n"); 
    }
}