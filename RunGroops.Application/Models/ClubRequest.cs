using Microsoft.AspNetCore.Http;
using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record ClubRequest(string Name,
        string Description,
        AddressRequest Address,
        ClubCategory ClubCategory);
}
