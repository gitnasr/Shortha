using Newtonsoft.Json;

namespace Paymob.DTO
{
    public class AccessTokenResponse
    {
        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
