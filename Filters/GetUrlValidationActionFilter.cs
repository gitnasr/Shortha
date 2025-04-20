using IPinfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shortha.DTO;
using System.Net;

namespace Shortha.Filters
{
    public class GetUrlValidation : IActionFilter
    {
        private readonly IPinfoClient _client;
        public GetUrlValidation(IPinfoClient client)
        {
            _client = client;
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            var query = context.HttpContext.Request.Query;
            var hash = query["hash"].ToString();
            var fingerprint = query["fingerprint"].ToString();
            var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(userAgent)
                || string.IsNullOrEmpty(ipAddress)
                || string.IsNullOrEmpty(hash)
                || string.IsNullOrEmpty(fingerprint))
            {
                context.Result = new BadRequestObjectResult(new ErrorResponse(HttpStatusCode.BadRequest, "Looks like a manipulated request -_-"));

                return;
            }

            var builder = new TrackerBuilder(userAgent, ipAddress, _client)
                .WithBrowser().WithOs().WithBrand().WithModel().WithIpAddress();

            var tracker = builder.Build();


            context.HttpContext.Items["Tracker"] = tracker;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
