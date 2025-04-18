using Microsoft.AspNetCore.Identity;

namespace Shortha.Models
{
    public class AppUser : IdentityUser
    {
        public bool isPremium { get; set; } = false;
        public bool isBlocked { get; set; } = false;
    }
}
