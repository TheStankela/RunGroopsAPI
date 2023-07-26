using Microsoft.AspNetCore.Http;
using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record UpdateRaceRequest(
        string Name,
        string Description,
        AddressRequest Address,
        RaceCategory RaceCategory,
        IFormFile File
        );
}
