using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;
using System.Security.Claims;

namespace Shortha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        public SubscriptionController(
            ISubscriptionRepository subscriptionRepository

            )
        {

            _subscriptionRepository = subscriptionRepository;


        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _subscriptionRepository.GetAllPackages();
            return Ok(packages);
        }

        [HttpPost]
        public async Task<IActionResult> UpgradeUser([FromBody] UserUpgradeRequest userUpgrade)
        {
            if (userUpgrade == null || userUpgrade.PackageId <= 0)
            {
                return BadRequest("Invalid package ID.");
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

            var result = await _subscriptionRepository.UpgradeUser(userId, userUpgrade.PackageId);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to create subscription.");
            }

        }
    }
}
