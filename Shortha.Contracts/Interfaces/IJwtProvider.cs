
using Shortha.Domain;

namespace Shortha.Application
{
    public interface IJwtProvider
    {
        Task BlacklistToken(string token);
        string GenerateToken(AppUser appUser, string role);
        bool IsBlacklisted(string tokenId);
    }
}
