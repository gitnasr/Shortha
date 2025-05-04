using Paymob.Interfaces;
using Shortha.Providers;

namespace Paymob.Providers
{

    public class RedisTokenCache : ITokenCache
    {
        private readonly IRedisProvider _redisProvider;
        private const string TOKEN_KEY = "PaymobAccessToken";

        public RedisTokenCache(IRedisProvider redisProvider)
        {
            _redisProvider = redisProvider;
        }

        public string GetCachedToken()
        {
            return _redisProvider.GetValue(TOKEN_KEY);
        }

        public void CacheToken(string token, TimeSpan expiry)
        {
            _redisProvider.SetValue(TOKEN_KEY, token, expiry);
        }
    }
}
