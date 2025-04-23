namespace Shortha.Models
{
    public class Subscription
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public virtual AppUser User { get; set; } = null!;

        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public virtual Package Package { get; set; } = null!;
        public int PackageId { get; set; }
    }
}
