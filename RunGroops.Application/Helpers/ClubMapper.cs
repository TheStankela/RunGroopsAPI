using Microsoft.AspNetCore.Http;
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
                ImageURL = "http://res.cloudinary.com/dwtfpyv5a/image/upload/v1688591125/bt3dah7rpnx1hk3b71zj.png",
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
