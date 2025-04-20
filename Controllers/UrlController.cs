using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Filters;
using Shortha.Interfaces;
using Shortha.Models;
using System.Net;

namespace Shortha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IURL UrlRepo;
        private readonly IMapper Mapper;
        private readonly IVisit VisitsRepo;
        public UrlController(IURL _url, IMapper mapper, IVisit visitsRepo)
        {
            UrlRepo = _url;
            Mapper = mapper;
            VisitsRepo = visitsRepo;
        }
        [HttpPost]
        public IActionResult CreateNew([FromBody] UrlCreateRequest SubmittedURL)
        {
            if (ModelState.IsValid)
            {

                Url url = UrlRepo.CreateUrl(Mapper.Map<Url>(SubmittedURL));
                CreatedUrl CreatedURL = Mapper.Map<CreatedUrl>(url);
                return Ok(CreatedURL);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [ServiceFilter(typeof(GetUrlValidation))]
        public IActionResult GetUrlByHash([FromQuery] GetUrlFromHashRequest SubmittedHash)
        {

            Url? url = UrlRepo.GetUrlByShortUrl(SubmittedHash.hash);
            if (url != null)
            {
                // access tracker is easy now
                var tracker = HttpContext.Items["Tracker"] as Tracker;

                bool vist = VisitsRepo.CreateVisit(tracker, url.Id);
                if (!vist)
                {
                    return StatusCode(500, new ErrorResponse(HttpStatusCode.InternalServerError, "Somethin went error"));
                }

                return Ok(Mapper.Map<PublicUrlResponse>(url));

            }
            else
            {
                return NotFound(new ErrorResponse(HttpStatusCode.NotFound, "URL is Deleted or Not Found"));
            }
        }

        [HttpPost("custom")]
        public IActionResult CreateWithCustomHash([FromBody] CreateCustomUrlRequest createCustomUrlRequest)
        {
            // check if hash is not found
            bool isExisted = UrlRepo.IsHashExists(createCustomUrlRequest.custom);
            if (isExisted)
            {
                return BadRequest(new ErrorResponse(HttpStatusCode.BadRequest, "Hash already exists"));
            }
            else
            {
                Url url = UrlRepo.CreateUrl(Mapper.Map<Url>(createCustomUrlRequest), createCustomUrlRequest.custom);
                CreatedUrl CreatedURL = Mapper.Map<CreatedUrl>(url);
                return Ok(CreatedURL);
            }

        }
    }
}
