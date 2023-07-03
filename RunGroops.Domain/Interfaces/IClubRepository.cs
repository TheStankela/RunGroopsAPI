using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IClubRepository
    {
        public Task<ICollection<Club>> GetClubsAsync(int page);
        public Task<ICollection<Club>> GetClubsByCityAsync(string city);
        public Task<Club> GetClubByIdAsync(int id);
        public Task<Club> GetClubByNameAsync(string clubName);
        public Task<bool> AddClubAsync(Club club);
        public Task<bool> DeleteClubAsync(Club club);
        public Task<bool> UpdateClubAsync(Club club);
        public Task<bool> ClubExists(int id, string? name);
    }
}
