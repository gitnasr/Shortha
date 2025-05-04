using Shortha.Domain;

namespace Shortha.Application
{
    public interface ISubscriptionRepository
    {
        Task<bool> CreateSubscription(Subscription subscription);
        Task<Subscription?> GetSubscriptionByUserId(string userId);
        Task<bool> UpdateSubscription(Subscription subscription);
        Task<bool> UpgradeUser(string userId, int packageId);
    }
}