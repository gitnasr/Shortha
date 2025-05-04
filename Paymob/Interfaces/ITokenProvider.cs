namespace Paymob.Interfaces
{
    public interface ITokenProvider
    {
        Task<string> GetAccessToken();
    }
}
