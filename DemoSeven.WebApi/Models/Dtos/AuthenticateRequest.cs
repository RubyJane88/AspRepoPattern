using System.ComponentModel.DataAnnotations;

namespace DemoSeven.WebApi.Models.Dtos
{

    public class AuthenticateRequest
    {
        [Required] 
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}