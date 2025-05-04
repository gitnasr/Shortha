using System.ComponentModel.DataAnnotations;

namespace Shortha.DTO
{
    public class UrlCreateRequest
    {
        [Required]
        [Url(ErrorMessage ="Your Provided Invalid URL")]
        public string url { get; set; }
   



    }
}