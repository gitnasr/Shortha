using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Paymob.DTO;
using Paymob.Interfaces;
using System.Net.Http.Json;


namespace Paymob.Providers
{
    public class PaymobGateway : IPaymentGateway
    {
        private readonly IConfiguration _config;
        private readonly ITokenProvider _tokenProvider;
        private readonly Interfaces.IHttpClientFactory _httpClientFactory;

        public PaymobGateway(
            IConfiguration config,
            ITokenProvider tokenProvider,
            Interfaces.IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _tokenProvider = tokenProvider;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PaymentLinkResponse> CreateInvoice(InvoicePayload invoice)
        {
            var token = await _tokenProvider.GetAccessToken();
            var client = await _httpClientFactory.CreateAuthenticatedClient(token);

            var url = _config["Paymob:PaymentLinkUrl"];
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = JsonContent.Create(invoice)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var paymentLink = JsonConvert.DeserializeObject<PaymentLinkResponse>(result);
                return paymentLink;
            }
            else
            {
                throw new Exception("Error getting payment link");
            }
        }
    }
}
