using RunGroops.Domain.Enum;

namespace RunGroops.Application.Models
{
    public record UpdateClubRequest(string Name,
        string Description,
        string ImageURL,
        ClubCategory ClubCategory);
}
