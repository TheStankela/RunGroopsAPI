using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IClubRepository
    {
        public Task<IQueryable<Club>> GetClubsAsync();
        public Task<ICollection<Club>> GetClubsByCityAsync(string city);
        public Task<ICollection<Club>> GetAllUserClubsAsync(string userId);
        public Task<Club> GetClubByIdAsync(int id);
        public Task<IQueryable<Club>> GetClubsByNameAsync(string clubName);
        public Task<bool> AddClubAsync(Club club);
        public Task<bool> DeleteClubAsync(Club club);
        public Task<bool> UpdateClubAsync(Club club);
        public Task<bool> ClubExists(int id, string? name);
    }
}
