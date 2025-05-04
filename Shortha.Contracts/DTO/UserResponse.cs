
namespace Shortha.DTO
{
    public class SubscriptionResponse
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }

        public PackageInfo package { get; set; }
    }
    public class UserResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int Links { get; set; } = 0;

        public SubscriptionResponse Subscription { get; set; } = null!;


    }
}
