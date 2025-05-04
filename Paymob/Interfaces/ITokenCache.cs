namespace Paymob.Interfaces
{
    public interface ITokenCache
    {
        string GetCachedToken();
        void CacheToken(string token, TimeSpan expiry);
    }
}
