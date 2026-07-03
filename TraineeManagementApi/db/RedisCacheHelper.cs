namespace TraineeManagementApi.db;
using StackExchange.Redis;
using System.Text.Json;

public class RedisCacheHelper
{
    // private static readonly ConfigurationOptions conf = new ConfigurationOptions {
    //     EndPoints = { "redis:6379" },
    //     // User = "",
    //     Password = ""
    // };
    private static ConnectionMultiplexer redis;
    private static IDatabase db;
    private static readonly object _lock = new object();

    public static void Initialize(IConfiguration configuration)
    {
        if (redis != null) return; // Prevent double initialization

        lock (_lock)
        {
            if (redis != null) return;

            var connectionString = configuration.GetConnectionString("RedisConnection") 
                ?? throw new InvalidOperationException("Connections String: 'Redis connection string not found'");

            redis = ConnectionMultiplexer.Connect(connectionString);
            db = redis.GetDatabase();
        }
    }

    public static async Task SetObjectAsync<T>(string key, T value, double expiryInMinutes, ILogger logger)
    {
        string json = JsonSerializer.Serialize(value);
        await db.StringSetAsync(key, json, TimeSpan.FromMinutes(expiryInMinutes));
        logger.LogInformation($"Entry serialized in Redis with key {key}");
    }

    public static async Task<T?> GetObjectAsync<T>(string key, ILogger logger)
    {
        string? value = await db.StringGetAsync(key);
        if (value == null) 
        {
            logger.LogWarning($"Entry {key} not found in Redis");
            return default;
        }
        logger.LogInformation($"Found entry with key {key}");
        return JsonSerializer.Deserialize<T>(value);
    }

    public static async Task KeyDelete(string key, ILogger logger)
    {
        await db.KeyDeleteAsync(key);
        logger.LogInformation($"Entry with key {key} deleted in redis");
    }
}