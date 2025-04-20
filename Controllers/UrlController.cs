using AutoMapper;
using IPinfo;
using Microsoft.AspNetCore.Mvc;
using Shortha.DTO;
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
        public async Task<IActionResult> GetUrlByHashAsync([FromBody] GetUrlFromHashRequest SubmittedHash)
        {
            Url? url = UrlRepo.GetUrlByShortUrl(SubmittedHash.hash);
            if (url != null)
            {
                string? userAgent = HttpContext.Request.Headers.UserAgent;
                string? ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                if (string.IsNullOrEmpty(userAgent) || string.IsNullOrEmpty(ipAddress))
                {
                    return BadRequest();
                }
                var Builder = new TrackerBuilder(userAgent, ipAddress, client);

                Tracker t = Builder.WithBrowser().WithOs().WithBrand().WithModel()
                    .WithIpAddress()
                    .Build();








                // Register a Visit
                //Mapper.Map<PublicUrlResponse>(url)
                return Ok(new
                {
                    t,
                });

            }
            else
            {
                return NotFound();
            }
        }
    }
}
