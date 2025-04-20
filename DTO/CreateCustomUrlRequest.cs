using System.ComponentModel.DataAnnotations;

namespace Shortha.DTO
{
    public class CreateCustomUrlRequest
    {
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        [MinLength(3)]
        public string custom { get; set; }
    }
}
