using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortha.Application;
using Shortha.Domain;
using Shortha.DTO;
using Shortha.Infrastructure.Repository;
using System.Security.Claims;

namespace Shortha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly PaymentRepository _paymentRepository;
        private readonly PackagesRepository _packagesRepository;
        public SubscriptionController(
            ISubscriptionRepository subscriptionRepository,
            PaymentRepository paymentRepository, PackagesRepository packagesRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _paymentRepository = paymentRepository;
            _packagesRepository = packagesRepository;


        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllPackages()
        {
            var packages = await _packagesRepository.GetAllPackages();
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
            var isValidGuid = Guid.TryParse(userId, out Guid userGuid);

            if (userId == null || !isValidGuid)
            {
                return BadRequest("User ID not found.");
            }
            // Create a Payment Request
            // But Check First if there's active payment request for the user

            var existingSubscription = await _paymentRepository.GetPendingPaymentByUserId(userGuid);

            if (existingSubscription != null)
            {
                return BadRequest("You already have a pending payment request.");
            }

            // Create a new payment request

            var package = await _packagesRepository.GetPackageById(userUpgrade.PackageId);

            var payment = new Payment
            {
                UserId = userGuid,
                Amount = package.Price, // Set the amount based on the package
                CreatedAt = DateTime.UtcNow,
                RefranceId = Guid.NewGuid().ToString(),
            };

            // Save the payment request to the database
            var paymentResult = await _paymentRepository.CreatePayment(payment);
            if (!paymentResult)
            {
                return BadRequest("Failed to create payment request.");
            }

            // Create a Payment Link Based on the payment method

            //var paymentLink = await _paymobProvider.Create(payment, package);
            return Ok();

        }
    }
}
