using MediatR;
using Microsoft.EntityFrameworkCore;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;
using RunGroops.Domain.Interfaces;
using RunGroops.Infrastructure.Context;

namespace RunGroops.Infrastructure.Repositories
{
    public class FriendRepository : IFriendRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public FriendRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<int> GetUserFriendsCount(string userId)
        {
            var result = await _applicationDbContext.Friends
                .Where(u => u.FriendRequestStatus == FriendRequestStatus.Approved &&
                (u.ToUserId == userId || u.FromUserId == userId))
                .CountAsync();

            return result;
        }
        public async Task<ICollection<Friend>> GetUserPendingFriendRequests(string userId)
        {
            return await _applicationDbContext.Friends.Where(u => u.ToUserId == userId && u.FriendRequestStatus == FriendRequestStatus.Pending).ToListAsync();
        }
        public async Task<bool> FriendRequestExists(string fromUserId, string toUserId)
        {
            var result = await _applicationDbContext.Friends
                                              .AnyAsync(fr => fr.FromUserId == fromUserId && fr.ToUserId == toUserId);

            return result;
        }
        public async Task<Friend?> GetRequestByUserIds(string fromUserId, string toUserId)
        {
            return await _applicationDbContext.Friends
                                      .FirstOrDefaultAsync(fr => fr.FromUserId == fromUserId &&
                                      fr.ToUserId == toUserId &&
                                      fr.FriendRequestStatus == FriendRequestStatus.Pending);

        }
        public async Task<bool> UpdateFriendRequest(Friend friend)
        {
            _applicationDbContext.Friends.Update(friend);

            return await Save();
        }
        public async Task<bool> CreateFriendRequest(Friend friend)
        {
            await _applicationDbContext.AddAsync(friend);

            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = await _applicationDbContext.SaveChangesAsync();

            return saved > 0 ? true : false;
        }
    }
}
