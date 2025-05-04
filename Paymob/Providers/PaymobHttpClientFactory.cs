namespace Paymob.Providers
{
    public class PaymobHttpClientFactory : Interfaces.IHttpClientFactory
    {
        public async Task<HttpClient> CreateAuthenticatedClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            return client;
        }
    }
}
