using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<ICollection<AppUser>> GetUsersAsync(int page);
        Task<ICollection<Club>> GetUserClubsAsync(string userId);
    }
}