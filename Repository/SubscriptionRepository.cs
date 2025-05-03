using Microsoft.EntityFrameworkCore;
using Shortha.Models;

namespace Shortha.Repository
{
    public class SubscriptionRepository
    {
        private readonly AppDB _context;
        public SubscriptionRepository(AppDB context)
        {
            _context = context;
        }
        public async Task<Subscription?> GetSubscriptionByUserId(string userId)
        {
            return await _context.Subscriptions
                .Include(sub => sub.Package)
                
                .Where(s => s.UserId == userId).FirstOrDefaultAsync();
        }
        public async Task<bool> CreateSubscription(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateSubscription(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
