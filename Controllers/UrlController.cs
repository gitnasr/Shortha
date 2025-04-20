using AutoMapper;
using IPinfo;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
using Shortha.Filters;
using Shortha.Interfaces;
using Shortha.Models;

namespace Shortha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IURL UrlRepo;
        private readonly IMapper Mapper;
        private readonly IPinfoClient client;
        public UrlController(IURL _url, IMapper mapper, IPinfoClient client)
        {
            UrlRepo = _url;
            Mapper = mapper;
            this.client = client;
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

                return Ok(tracker);

                //return Ok(Mapper.Map<PublicUrlResponse>(url));

            }
            else
            {
                return NotFound(new ErrorResponse(System.Net.HttpStatusCode.NotFound, "URL is Deleted or Not Found"));
            }
        }
    }
}
