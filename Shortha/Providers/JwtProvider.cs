using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Shortha.Application;
using Shortha.Domain;
using Shortha.Extentions;
using System.Security.Claims;
using System.Text;

namespace Shortha.Providers
{

    public class JwtProvider : IJwtProvider
    {
        private readonly IConfiguration _configuration;
        private readonly IRedisProvider redis;
        public JwtProvider(IConfiguration configuration, IRedisProvider _redis)
        {
            _configuration = configuration;
            redis = _redis;

        }

        public string GenerateToken(AppUser appUser, string role)
        {
            var SecretKey = _configuration["Jwt:Secret"];
            var SecutriyKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(SecutriyKey, SecurityAlgorithms.Aes128CbcHmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, appUser.UserName),
                    new Claim(ClaimTypes.Email, appUser.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),    // This is a Claim that is used to identify this token exactly. for blacklisting.
                    new Claim(ClaimTypes.Role, role)
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


            // Check first if the token is valid,
            // i don't want spammy requests to my redis server. -_-

            bool isValid = tokenHandler.CanReadToken(token);
            if (!isValid)
            {
                return;
            }

            // If the token is valid,
            // then we need to check if it's already expired.
            var ValidationResult = await tokenHandler.ValidateTokenAsync(token,
                AppIdentity.GetTokenValidationParameters(_configuration)
                );

            if (ValidationResult.IsValid)
            {


                var TokenPayload = tokenHandler.ReadJsonWebToken(token);
                var tokenId = TokenPayload.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                var tokenExpiresIn = TokenPayload.ValidTo;

                var expiresAtInTimeSpan = tokenExpiresIn - DateTime.UtcNow;
                if (tokenId != null)
                {
                    redis.SetValue(tokenId, token, expiresAtInTimeSpan);
                }
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
