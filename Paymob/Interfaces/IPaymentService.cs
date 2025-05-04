using Paymob.DTO;
using Shortha.Domain;
using Shortha.DTO;

namespace Paymob.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentLinkResponse> CreatePayment(Payment payment, PackageInfo package);
    }
}
