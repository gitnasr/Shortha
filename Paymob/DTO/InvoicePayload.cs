using Newtonsoft.Json;

namespace Paymob.DTO
{
    public class InvoicePayload
    {
        [JsonProperty("amount_cents")]
        public decimal AmountCents { get; set; }

        public DateTime ExpiresAt { get; set; }

        [JsonProperty("payment_methods")]
        public string PaymentMethodId { get; set; }

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }
    }
}
