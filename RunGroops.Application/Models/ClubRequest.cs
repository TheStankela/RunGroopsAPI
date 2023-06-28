using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record ClubRequest(string Name,
        string Description,
        string ImageURL,
        AddressRequest Address,
        ClubCategory ClubCategory);
}
