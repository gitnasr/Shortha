namespace Shortha.Models
{
    public class Url
    {
        public Guid Id { get; set; } // Primary key
        public string ShortHash { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpirationDate { get; set; } = null;
        public bool IsActive { get; set; } = true;
        public bool isDeleted { get; set; } = false; // Deleted flag (Soft Delete)
        public bool isBlocked { get; set; } = false; // Blocked flag
        public bool isCustom { get; set; } = false; // Custom URL flag

        public virtual AppUser? User { get; set; } = null; // Navigation property for the user who created the URL, maybe null if not logged in
        public string? UserId { get; set; } = null;

        public virtual List<Visits> Visits { get; set; } = new List<Visits>(); // Navigation property for visits to this URL





    }
}
