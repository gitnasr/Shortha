using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Shortha.Models;

namespace Shortha.Providers
{
    public class JwtProvider
    {
        private readonly IConfiguration _configuration;
        private readonly RedisProvider redis;
        public JwtProvider(IConfiguration configuration, RedisProvider _redis)
        {
            _configuration = configuration;
            redis = _redis;

        }

        public string GenerateToken(AppUser appUser)
        {
            var SecretKey = _configuration["Jwt:Secret"];
            var SecutriyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(SecutriyKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(ClaimTypes.Email, appUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    // This is a Claim that is used to identify this token exactly. for blacklisting.

                    ]),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = credentials,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],

            };
            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return token;
        }

        public async Task BlacklistToken(string token)
        {

            var tokenHandler = new JsonWebTokenHandler();


            // Check first if the token is valid, i don't want spammy requests to my redis server. -_-

            bool isValid = tokenHandler.CanReadToken(token);
            if (!isValid)
            {
                return;
            }

            // If the token is valid, then we need to check if it's already expired.
           var ValidationResult = await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           }); // This should be got from the configuration

            if (!ValidationResult.IsValid)
            {
                return ;
            }



            var TokenPayload = tokenHandler.ReadJsonWebToken(token);
            var tokenId = TokenPayload.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var tokenExpiresIn = TokenPayload.ValidTo;

            var expiresAtInTimeSpan = tokenExpiresIn - DateTime.UtcNow;
            if (tokenId != null)
            {
                redis.SetValue(tokenId, token, expiresAtInTimeSpan);
            }
          

            
        }

        public bool IsBlacklisted(string tokenId)
        {
            
                var blacklistedToken = redis.GetValue(tokenId);
                if (blacklistedToken != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
           
        }

    }
}
