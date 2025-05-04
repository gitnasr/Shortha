using Shortha.DTO;
using Shortha.Models;

namespace Shortha.Providers
{
    public class InvoicePayload
    {

    }
    public class PaymobClient
    {
        public string ApiKey { get; set; }
        public string MerchantId { get; set; }

        private readonly IConfiguration config;

        public PaymobClient(IConfiguration _config)
        {

            config = _config;
            ApiKey = config["Paymob:ApiKey"];
            MerchantId = config["Paymob:MerchantId"];

        }
        private  HttpClient Client()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + config["Paymob:ApiKey"]);

            return client;  
        }
        public async Task<string> CreateInvoice(InvoicePayload invoice)
        {
            var client = Client();
            var url = config["Paymob:PaymentLinkUrl"];
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Content.ReadFromJsonAsync<InvoicePayload>(invoice);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                // Deserialize the response if needed
                 var Invoice = JsonConvert.DeserializeObject<PaymentLinkResponse>(result);
                return result;
            }
            else
            {
                throw new Exception("Error getting payment link");
            }

        }
    public class PaymobProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisProvider redisProvider;

        public PaymobProvider(IConfiguration configuration, IRedisProvider redisProvider)
        {
            _configuration = configuration;
            this.redisProvider = redisProvider;
        }


        public async Task CreatePaymentLink(Payment payment, PackageInfo package)
        {
           // Paymob 
        }
    }
}
