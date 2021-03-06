using StackExchange.Redis;

namespace CommonDevPack.Infra.Cache.Redis.Interfaces;
public interface IRedisService
{
    bool IsConnected { get; }
    bool IsConnecting { get; }

    IAsyncEnumerable<string> GetAllKeysAsync(
        string? pattern = default, 
        int pageSize = int.MaxValue, 
        long cursor = 0,
        int pageOffset = 0
    );
    IAsyncEnumerable<T> GetAllValuesAsync<T>(
        string? pattern = default,
        int pageSize = int.MaxValue,
        long cursor = 0,
        int pageOffset = 0
    );
    IAsyncEnumerable<string> GetAllValuesAsync(
        string? pattern = default,
        int pageSize = int.MaxValue,
        long cursor = 0,
        int pageOffset = 0
    );
    string GetClientName();
    string GetStatus();
    bool Set(string key, string data);
    bool Set(string key, string value, int expirationTime = 0, When when = When.Always);
    Task<bool> SetAsync(string key, string value, int expirationTime = 0, When when = When.Always);
    Task<bool> SetAsync<T>(string key, T value, int expirationTime = 0, When when = When.Always);
    string Get(string key);
    Task<T> GetAsync<T>(string key);
    bool KeyExists(string key);
    void Delete(string key);
    Task<bool> DeleteAsync(string key);    
}
