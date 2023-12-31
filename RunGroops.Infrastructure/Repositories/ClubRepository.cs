﻿using Microsoft.EntityFrameworkCore;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;

namespace RunGroops.Infrastructure.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;
        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddClubAsync(Club club)
        {
            await _context.AddAsync(club);
            return await Save();
        }
        public async Task<bool> DeleteClubAsync(Club club)
        {
            _context.Remove(club);
            return await Save();
        }
        public async Task<bool> UpdateClubAsync(Club club)
        {
            _context.Update(club);
            return await Save();
        }
        public async Task<Club?> GetClubByIdAsync(int id)
        {
           return await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(c => c.Id == id); 
        }
        public async Task<IQueryable<Club>> GetClubsByNameAsync(string clubName)
        {
            return _context.Clubs.Include(r => r.Address).Where(r => r.Name.ToLower().Contains(clubName.ToLower()));
        }
        public async Task<IQueryable<Club>> GetClubsAsync()
        {
            return _context.Clubs.Include(c => c.Address);
        }
        public async Task<ICollection<Club>> GetClubsByCityAsync(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City == city).Include(c => c.Address).ToListAsync();
        }
        public async Task<bool> ClubExists(int id, string? name)
        {
            return await _context.Clubs.AnyAsync(c => c.Id == id || c.Name == name);
        }
        public async Task<ICollection<Club>> GetAllUserClubsAsync(string userId)
        {
            return await _context.Clubs.Where(c => c.AppUserId == userId).ToListAsync();
        }
        private async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
    }
}
