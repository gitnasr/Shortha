namespace Shortha.Models
{
    public class Visits
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; } = DateTime.UtcNow;
        public string? UserAgent { get; set; } = null;
        public string? IpAddress { get; set; } = null;
        public string? Referrer { get; set; } = null;
        public string? Country { get; set; } = null;
        public string? Region { get; set; } = null;
        public string? City { get; set; } = null;
        public string? Device { get; set; } = null;
        public string? Browser { get; set; } = null;
        public string? Os { get; set; } = null;
        public Guid UrlId { get; set; } // Foreign key to the URL
        public virtual Url Url { get; set; }  // Navigation property for the URL
        public int Count { get; set; } = 0; // Count of visits to this URL

    }
}
