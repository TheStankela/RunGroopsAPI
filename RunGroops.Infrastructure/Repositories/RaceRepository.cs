using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;

namespace RunGroops.Infrastructure.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        public RaceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddRaceAsync(Race race)
        {
            await _context.Races.AddAsync(race);
            return await Save();
        }

        public async Task<bool> DeleteRaceAsync(Race race)
        {
            _context.Races.Remove(race);
            return await Save();
        }

        public async Task<ICollection<Race>> GetAllUserRacesAsync(string userId)
        {
            return await _context.Races.Where(r => r.AppUserId == userId).ToListAsync();
        }

        public async Task<Race?> GetRaceByIdAsync(int id)
        {
            return await _context.Races.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<IQueryable<Race>> GetRacesByNameAsync(string raceName)
        {
            return _context.Races.Include(r => r.Address).Where(r => r.Name.ToLower().Contains(raceName.ToLower()));
        }
        public async Task<IQueryable<Race>> GetRacesAsync()
        {
            return _context.Races.Include(c => c.Address);
        }

        public async Task<ICollection<Race>> GetRacesByCityAsync(string city)
        {
            return await _context.Races.Include(r => r.Address).Where(r => r.Address.City == city).ToListAsync();
        }
        public async Task<bool> UpdateRaceAsync(Race race)
        {
            _context.Update(race);
            return await Save();
        }
        public async Task<bool> RaceExists(int id, string? name)
        {
            return await _context.Races.AnyAsync(r => r.Id == id || r.Name == name);
        }
        private async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
