using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<IQueryable<AppUser>> GetUsersAsync();
        Task<ICollection<Club>> GetUserClubsAsync(string userId);
        Task<ICollection<Race>> GetUserRacesAsync(string userId);
        Task<IQueryable<AppUser>> GetUsersByNameAsync(string userName);
        Task<bool> UserExists(string userId);
        Task<bool> UpdateUser(AppUser appUser);

    }
}