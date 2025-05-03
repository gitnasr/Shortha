using StackExchange.Redis;

namespace Shortha.Providers
{
    public interface IRedisProvider
    {
        void SetValue(string key, string value);
        void SetValue(string key, string value, TimeSpan expireTime);
        string? GetValue(string key);
    }
    public class RedisProvider : IRedisProvider
    {
        private readonly ConnectionMultiplexer redis;
        public RedisProvider()
        {
            redis = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { { "redis-11844.c335.europe-west2-1.gce.redns.redis-cloud.com", 11844 } },
                User = "default",
                Password = "ODpkEK12pdFUxxJgaOLt84ozG3aa1G5D" // Yes i know, iwon't do this in production, this is dummy db for development.
            }
        );
        }
        private IDatabase GetDatabase()
        {
            return redis.GetDatabase();
        }
        public void SetValue(string key, string value)
        {
            var db = GetDatabase();
            db.StringSetAsync(key, value);
        }
        public void SetValue(string key, string value, TimeSpan expireTime)
        {
            var db = GetDatabase();
            db.StringSetAsync(key, value, expireTime);
        }
        public string? GetValue(string key)
        {

            var db = GetDatabase();
            var value = db.StringGet(key);
            if (value.IsNullOrEmpty)
            {
                return null;
            }
            return value.ToString();
        }
    }
}
