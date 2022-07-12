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
        _configuration = servicesConfiguration.Value;
        _connection = redisConnection;
        _database = redisConnection.RedisCache.GetDatabase();
        _logger = logger;
    }

    private string? BuildPatternParameter(string? pattern = default)
        => pattern is not null ? $"*{pattern}*" : default;

    public bool IsConnected => _connection.RedisCache.IsConnected;
    public bool IsConnecting => _connection.RedisCache.IsConnecting;

    public string GetClientName()
    {
        return _connection.RedisCache.ClientName;
    }

    public string GetStatus()
    {
        return _connection.RedisCache.GetStatus();
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

    public async IAsyncEnumerable<T> GetAllValuesAsync<T>(
        string? pattern = default,
        int pageSize = int.MaxValue,
        long cursor = 0,
        int pageOffset = 0
    )
    {
        var keys = GetAllKeysAsync(
            BuildPatternParameter(pattern),
            pageSize,
            cursor,
            pageOffset
        );
        await foreach (var key in keys)
        {
            yield return await GetAsync<T>(key);
        }
    }

    public async IAsyncEnumerable<string> GetAllValuesAsync(
        string? pattern = default,
        int pageSize = int.MaxValue,
        long cursor = 0,
        int pageOffset = 0
    )
    {
        var keys = GetAllKeysAsync(
            BuildPatternParameter(pattern),
            pageSize,
            cursor,
            pageOffset
        );
        await foreach (var key in keys)
        {
            yield return Get(key);
        }
    }

    public async IAsyncEnumerable<string> GetAllKeysAsync(
        string? pattern = default,
        int pageSize = int.MaxValue,
        long cursor = 0,
        int pageOffset = 0
    )
    {
        var endpoint = _connection.RedisCache.GetEndPoints()?.FirstOrDefault();
        var server = _connection.RedisCache.GetServer(endpoint);
        IAsyncEnumerable<RedisKey> keys = server.KeysAsync(
            pattern: BuildPatternParameter(pattern),
            pageSize: pageSize,
            cursor: cursor,
            pageOffset: pageOffset
        );
        await foreach (RedisKey key in keys)
        {
            yield return key.ToString();
        }
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
