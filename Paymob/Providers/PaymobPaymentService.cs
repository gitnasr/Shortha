using Microsoft.Extensions.Configuration;
using Paymob.DTO;
using Paymob.Interfaces;
using Shortha.Domain;
using Shortha.DTO;

namespace Paymob.Providers
{
    public class PaymobPaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IPaymentGateway _paymentGateway;

        public PaymobPaymentService(
            IConfiguration configuration,
            IPaymentGateway paymentGateway)
        {
            _configuration = configuration;
            _paymentGateway = paymentGateway;
        }

        public async Task<PaymentLinkResponse> CreatePayment(Payment payment, PackageInfo package)
        {
            var invoice = new InvoicePayload
            {
                AmountCents = package.Price,
                ExpiresAt = payment.ExpirationDate,
                ReferenceId = payment.RefranceId.ToString(),
                PaymentMethodId = _configuration["Paymob:PaymentMethodId"]
            };

            return await _paymentGateway.CreateInvoice(invoice);
        }

    }
}
