using Microsoft.AspNetCore.Identity;
using Shortha.Interfaces;

namespace Shortha.Models
{
    public class AppUser : IdentityUser, IAppUser
    {
        public bool isPremium { get; set; } = false;
        public bool isBlocked { get; set; } = false;
    }
}
