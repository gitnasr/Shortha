using System.ComponentModel.DataAnnotations;

namespace Shortha.DTO
{
    public class LogoutRequestPayload
    {
        [Required]
        
        public string Token { get; set; }
    }
}