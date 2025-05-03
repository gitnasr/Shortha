using Shortha.DTO;
using Shortha.Models;

namespace Shortha.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<bool> CreateSubscription(Subscription subscription);
        Task<IEnumerable<PackageInfo>> GetAllPackages();
        Task<Subscription?> GetSubscriptionByUserId(string userId);
        Task<bool> UpdateSubscription(Subscription subscription);
        Task<bool> UpgradeUser(string userId, int packageId);
    }
}