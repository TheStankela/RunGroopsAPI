using RunGroops.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace RunGroops.Application.Models
{
    public class UserRegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int? Mileage { get; set; }
        public int? Pace { get; set; }
        [Required]
        public string NickName { get; set; }
        public UserCategory UserCategory { get; set; }
    }
}
