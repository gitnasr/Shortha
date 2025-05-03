using Shortha.Models;

namespace Shortha.Interfaces
{
    public interface IJwtProvider
    {
        Task BlacklistToken(string token);
        string GenerateToken(AppUser appUser, string role);
        bool IsBlacklisted(string tokenId);
    }
}
