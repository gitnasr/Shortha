using Newtonsoft.Json;

namespace Paymob.DTO
{

    public class PaymentLinkResponse
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("client_url")]
        public string ClientUrl { get; set; }
    }
}
