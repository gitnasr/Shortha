namespace Shortha.Domain
{
    public class Package
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int MaxUrls { get; set; } = -1; // -1 for unlimited

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public decimal price { get; set; } = 0;
        public virtual ICollection<Subscription> Subscriptions { get; set; } = new HashSet<Subscription>();

    }
}
