using System.Net;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Controllers
{
    [Authorize]
    [Route("api/visits")]
    [ApiController]
    public class VisitController : ControllerBase
    {
        private readonly IVisit _visitRepository;
        private readonly IMapper mapper;
        public VisitController(IVisit visitRepository, IMapper mapper )
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
            if (visits.Select(Visit => Visit.Url.UserId).First() != userId)
            {
                return Ok(new { Visits = Array.Empty<object>() });

            }


            return Ok(visits.Select(v => mapper.Map<UrlVisitsResponse>(v)));
        }
    }
}
