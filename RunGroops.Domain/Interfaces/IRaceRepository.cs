using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IRaceRepository
    {
        public Task<ICollection<Race>> GetRacesAsync(int page);
        public Task<ICollection<Race>> GetRacesByCityAsync(string city);
        public Task<ICollection<Race>> GetAllUserRacesAsync(string userId);
        public Task<Race> GetRaceByIdAsync(int id);
        public Task<Race> GetRaceByNameAsync(string raceName);
        public Task<bool> AddRaceAsync(Race race);
        public Task<bool> DeleteRaceAsync(Race race);
        public Task<bool> UpdateRaceAsync(Race race);
        public Task<bool> RaceExists(int id, string? name);
    }
}
