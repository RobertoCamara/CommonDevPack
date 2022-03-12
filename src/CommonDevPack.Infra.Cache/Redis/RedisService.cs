using CommonDevPack.Infra.Cache.Redis.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System.Globalization;
using System.Text.Json;

namespace CommonDevPack.Infra.Cache.Redis;
public class RedisService : IRedisService
{
    private readonly IRedisConnection _connection = null;
    private readonly IDatabase _database = null;
    private readonly RedisConfiguration _configuration;
    private readonly ILogger<RedisService> _logger;

    public RedisService(IRedisConnection redisConnection,
        IOptions<RedisConfiguration> servicesConfiguration,
        ILogger<RedisService> logger)
    {
        _configuration =
            servicesConfiguration.Value ?? throw new ArgumentNullException(nameof(servicesConfiguration), "Parameter cannot be null.");

        _connection = redisConnection;
        _database = redisConnection.RedisCache.GetDatabase();
        _logger = logger;
    }

    public void Delete(string key)
    {
        _database.KeyDelete(key);
    }
    public async Task<bool> DeleteAsync(string key)
    {
        return await _database.KeyDeleteAsync(key);
    }

    public string Get(string key)
    {
        var value = _database.StringGet(key);

        return value == RedisValue.Null
            ? string.Empty
            : Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var value = await _database.StringGetAsync(key);

        if (value.IsNullOrEmpty) return default;

        return JsonSerializer.Deserialize<T>(value);
    }

    public bool KeyExists(string key)
    {
        return _database.KeyExists(key);
    }

    public bool Set(string key, string data)
    {
        return _database.StringSet(key, data, TimeSpan.FromSeconds(_configuration.DefaultExpirationTimeInSeconds), When.NotExists);
    }

    public bool Set(string key, string value, int expirationTime = 0, When when = When.Always)
    {
        return _database.StringSet(key: key, value: value,
            TimeSpan.FromSeconds(SetExpirationTime(expirationTime)), when);
    }

    public async Task<bool> SetAsync(string key, string value, int expirationTime = 0, When when = When.Always)
    {
        return await _database.StringSetAsync(key: key, value: value,
            TimeSpan.FromSeconds(SetExpirationTime(expirationTime)), when);
    }

    public async Task<bool> SetAsync<T>(string key, T value, int expirationTime = 0, When when = When.Always)
    {
        var json = JsonSerializer.Serialize(value);
        return await _database.StringSetAsync(key, json, expiry: TimeSpan.FromSeconds(SetExpirationTime(expirationTime)), when);
    }

    private int SetExpirationTime(int expirationTime = 0)
    {
        return expirationTime == 0
            ? _configuration.DefaultExpirationTimeInSeconds
            : expirationTime;
    }
}
