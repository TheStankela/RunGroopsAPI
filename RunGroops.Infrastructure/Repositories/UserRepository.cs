using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;

namespace RunGroops.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IQueryable<AppUser>> GetUsersAsync()
        {
            IQueryable<AppUser> usersQuery = _context.Users;

            return usersQuery;
        }
        public async Task<AppUser?> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<IQueryable<AppUser>> GetUsersByNameAsync(string userName)
        {
            IQueryable<AppUser> usersQuery = _context.Users.Where(u => u.UserName.ToLower().Contains(userName.ToLower()));

            return usersQuery;
        }
        public async Task<ICollection<Club>> GetUserClubsAsync(string userId)
        {
            return await _context.Clubs.Where(u => u.AppUserId == userId).ToListAsync();
        }
        public async Task<ICollection<Race>> GetUserRacesAsync(string userId)
        {
            return await _context.Races.Where(u => u.AppUserId == userId).ToListAsync();
        }
        public async Task<bool> UpdateUser(AppUser appUser)
        {
            _context.Update(appUser);
            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        public async Task<bool> UserExists(string userId)
        {
            var result = await _context.Users.AnyAsync(u => u.Id == userId);

            return result; 
        }
    }
}
