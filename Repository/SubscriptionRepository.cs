using Microsoft.EntityFrameworkCore;
using Shortha.DTO;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Repository
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly AppDB _context;
        private readonly PackagesRepository _packagesRepository;
        public SubscriptionRepository(AppDB context, PackagesRepository packagesRepository)
        {
            _context = context;
            _packagesRepository = packagesRepository;
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

    

        public async Task<bool> UpgradeUser(string userId, int packageId)
        {

            var currentSubscription = await _context.Subscriptions
               .Where(s => s.UserId == userId)
               .FirstOrDefaultAsync();
            var package = await _context.Packages.FindAsync(packageId);

            if (currentSubscription != null || package == null)
            {
                return false;
            }


            var subscription = new Subscription
            {
                UserId = userId,
                PackageId = packageId,

            };

            await _context.Subscriptions.AddAsync(subscription);

            return await _context.SaveChangesAsync() > 0;




        }
    }
}
