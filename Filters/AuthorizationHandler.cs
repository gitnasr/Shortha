using Microsoft.AspNetCore.Authorization;
using Shortha.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Shortha.Filters
{

    public class NotBlacklistedRequirement : IAuthorizationRequirement { }

    public class NotBlacklistedHandler : AuthorizationHandler<NotBlacklistedRequirement>
    {
        private readonly IJwtProvider tokenProvider;
        public NotBlacklistedHandler(IJwtProvider _tokenProvider)
        {
            tokenProvider = _tokenProvider;

        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, NotBlacklistedRequirement requirement)
        {
            var jti = context.User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            if (jti != null)
            {
                var isBlacklisted = tokenProvider.IsBlacklisted(jti);
                if (!isBlacklisted)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;
        }
    }
}
