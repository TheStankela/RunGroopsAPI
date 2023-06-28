using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Helpers
{
    public class ClubMapper : IClubMapper
    {
        public Club MapClubRequestToClub(ClubRequest clubRequest)
        {
            var club = new Club
            {
                Name = clubRequest.Name,
                Description = clubRequest.Description,
                ImageURL = clubRequest.ImageURL,
                ClubCategory = clubRequest.ClubCategory,
                Address = new Address
                {
                    Country = clubRequest.Address.Country,
                    City = clubRequest.Address.City,
                    Street = clubRequest.Address.Street,
                    Zip = clubRequest.Address.Zip,
                }
            };

            return club;
        }
    }
}
