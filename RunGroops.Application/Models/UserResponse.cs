using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunGroops.Application.Models
{
    public class UserResponse
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public int? Mileage { get; set; }
        public int? Pace { get; set; }
        public string? ImageURL { get; set; }
    }
}
