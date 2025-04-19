using System.ComponentModel.DataAnnotations;

namespace Shortha.DTO
{
    public class GetUrlFromHashRequest
    {
        [Required]
        [MaxLength(10, ErrorMessage = "Hash length must be 10 characters.")]
        [MinLength(3, ErrorMessage = "Hash length must be 3 characters.")]
        public string hash { get; set; }
    }
}
