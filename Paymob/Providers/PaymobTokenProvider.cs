using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Paymob.DTO;
using Paymob.Interfaces;
using System.Net.Http.Json;

namespace Paymob.Providers
{
    public class PaymobTokenProvider : ITokenProvider
    {
        private readonly IConfiguration _config;
        private readonly ITokenCache _tokenCache;
        private readonly HttpClient _httpClient;

        public PaymobTokenProvider(
            IConfiguration config,
            ITokenCache tokenCache)
        {
            _config = config;
            _tokenCache = tokenCache;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetAccessToken()
        {
            // Try to get token from cache first
            var token = _tokenCache.GetCachedToken();
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }

            // If token is not found in cache, make a request to get a new token
            var url = _config["Paymob:AccessTokenUrl"];
            var apiKey = _config["Paymob:ApiKey"];
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(new { api_key = apiKey })
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<AccessTokenResponse>(result);
                if (data == null || string.IsNullOrEmpty(data.Token))
                {
                    throw new Exception("Error getting access token");
                }

                // Cache the token for future use
                _tokenCache.CacheToken(data.Token, TimeSpan.FromMinutes(60));
                return data.Token;
            }
            else
            {
                throw new Exception("Error getting access token");
            }
        }
    }
}
