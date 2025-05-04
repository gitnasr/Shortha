using Shortha.Models;

namespace Shortha.Repository
{
    public class PaymentRepository
    {
        private readonly AppDB _context;
        public PaymentRepository(AppDB context)
        {
            _context = context;
        }

        public async Task<bool> CreatePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
