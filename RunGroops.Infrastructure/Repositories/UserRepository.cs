using Microsoft.EntityFrameworkCore;
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
        public async Task<ICollection<AppUser>> GetUsersAsync(int page)
        {
            var result = await _context.Users
               .Skip(page * 5)
               .Take(5)
               .ToListAsync();

            return result;
        }
        public async Task<AppUser?> GetUserByIdAsync(string userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<ICollection<Club>> GetUserClubsAsync(string userId)
        {
            return await _context.Clubs.Where(u => u.AppUserId == userId).ToListAsync();
        }
    }
}
