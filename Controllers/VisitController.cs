using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Interfaces;

namespace Shortha.Controllers
{
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
            var visits = await _visitRepository.GetVisitsByShortUrl(shortUrl);
            if (visits == null || !visits.Any())
            {
                return NotFound(
                    new ErrorResponse(
                        HttpStatusCode.NotFound,
                        "No visits found for the provided short URL."
                    ));
            }
            return Ok(visits.Select(v => mapper.Map<UrlVisitsResponse>(v)));
        }
    }
}
