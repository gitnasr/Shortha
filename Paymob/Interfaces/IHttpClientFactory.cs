namespace Paymob.Interfaces
{

    public interface IHttpClientFactory
    {
        Task<HttpClient> CreateAuthenticatedClient(string token);
    }
}
