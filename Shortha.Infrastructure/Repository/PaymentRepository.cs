﻿using Microsoft.EntityFrameworkCore;
using Shortha.Application.Enums;
using Shortha.Domain;
using Shortha.Infrastructure.Configurations;

namespace Shortha.Infrastructure.Repository
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

        public async Task<Payment?> GetPendingPaymentByUserId(Guid userId)
        {
            return await _context.Payments
                .Where(p => p.UserId == userId && p.Status == PaymentStatus.Pending)
                .FirstOrDefaultAsync();

        }


    }
}
