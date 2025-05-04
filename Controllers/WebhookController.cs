using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;

namespace Shortha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost("paymob")]
        public async Task<IActionResult> Webhook([FromBody] PaymobWebhookPayload user)
        {
            return Ok(user);
        }
    }
}
