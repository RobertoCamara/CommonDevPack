namespace CommonDevPack.Infra.Cache.Redis;
public class RedisConfiguration
{
    public const string SectionKeyDefault = "Redis";
    public string ConnectionString { get; set; }
    public int DefaultExpirationTimeInSeconds { get; set; } = 3600;
}
