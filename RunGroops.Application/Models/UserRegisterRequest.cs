using System.ComponentModel.DataAnnotations;

namespace RunGroops.Application.Models
{
    public class UserRegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
