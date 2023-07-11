using Microsoft.AspNetCore.Http;
using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record UpdateClubRequest(string Name,
        string Description,
        AddressRequest Address,
        ClubCategory ClubCategory,
        IFormFile File);
}
