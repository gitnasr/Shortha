namespace Shortha.Domain
{
    public class Subscription
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = false;
        public Payment Payment { get; set; } = null!;
        public int PaymentId { get; set; }

        public virtual Package Package { get; set; } = null!;
        public int PackageId { get; set; }
    }
}
