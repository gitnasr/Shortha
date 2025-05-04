using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shortha.Application;
using Shortha.Domain;
using Shortha.DTO;
using System.Security.Claims;


namespace Shortha.Controllers
{
    [Authorize]
    [Route("api/visits")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisit _visitRepository;
        private readonly IMapper mapper;
        public VisitController(IVisit visitRepository, IMapper mapper)
        {
            _visitRepository = visitRepository;
            this.mapper = mapper;
        }

        [HttpGet("{shortUrl}")]
        public async Task<IActionResult> GetVisitsByShortUrl(string shortUrl)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Visit>? visits = await _visitRepository.GetVisitsByShortUrl(shortUrl);
            if (visits == null || !visits.Any())
            {
                return Ok(new { Visits = Array.Empty<object>() });
            }

            // Convert userId to Guid for comparison  
            if (visits.Select(visit => visit.Url.UserId).First() != userId)
            {
                return Ok(new { Visits = Array.Empty<object>() });
            }

            return Ok(visits.Select(v => mapper.Map<UrlVisitsResponse>(v)));
        }
    }
}
