namespace Shortha.Providers
{
    public interface IRedisProvider
    {
        void SetValue(string key, string value);
        void SetValue(string key, string value, TimeSpan expireTime);
        string? GetValue(string key);
    }
}
