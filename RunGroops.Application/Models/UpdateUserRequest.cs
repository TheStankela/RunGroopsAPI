using Microsoft.AspNetCore.Http;
using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record UpdateUserRequest(string UserName,
        string Description,
        int Pace,
        int Mileage,
        UserCategory UserCategory,
        IFormFile File);
}
