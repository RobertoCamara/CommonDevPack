namespace CommonDevPack.Infra.Cache.Redis;
public class RedisConfiguration
{
    public const string SectionKeyDefault = "Redis";
    public string ConnectionString { get; set; }
    public int DefaultExpirationTimeInSeconds { get; set; }
    public int ExpirationTimeStatus { get; set; }
}
