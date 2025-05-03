using Microsoft.AspNetCore.Identity;

namespace Shortha.Models
{
    public class AppUser : IdentityUser
    {
        public bool isPremium { get; set; } = false;
        public bool isBlocked { get; set; } = false;

        public virtual ICollection<Url> Urls { get; set; } = new List<Url>();
        public int SubscriptionId { get; set; } = 0;

        public virtual Subscription Subscription { get; set; } = null!;

    }
}
