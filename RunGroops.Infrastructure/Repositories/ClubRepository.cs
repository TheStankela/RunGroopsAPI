using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Infrastructure.Repository
{
    public class ClubRepository : IClubRepository
    {
        public Task<bool> AddClubAsync(Club club)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteClubAsync(Club club)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditClubAsync(Club club)
        {
            throw new NotImplementedException();
        }

        public Task<Club> GetClubByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Club> GetClubByNameAsync(string clubName)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Club>> GetClubsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Club>> GetClubsByCityAsync(string city)
        {
            throw new NotImplementedException();
        }
    }
}
