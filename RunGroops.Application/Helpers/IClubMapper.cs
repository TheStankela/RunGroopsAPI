using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Helpers
{
    public interface IClubMapper
    {
        Club MapClubRequestToClub(ClubRequest clubRequest);
    }
}