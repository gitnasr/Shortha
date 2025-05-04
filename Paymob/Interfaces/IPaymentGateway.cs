using Paymob.DTO;

namespace Paymob.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentLinkResponse> CreateInvoice(InvoicePayload invoice);
    }
}
