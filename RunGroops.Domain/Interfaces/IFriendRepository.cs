using RunGroops.Domain.EFModels;

namespace RunGroops.Domain.Interfaces
{
    public interface IFriendRepository
    {
        Task<int> GetUserFriendsCount(string userId);
        Task<ICollection<Friend>> GetUserPendingFriendRequests(string userId);
        Task<Friend?> GetRequestByUserIds(string fromUserId, string toUserId);
        Task<bool> UpdateFriendRequest(Friend friend);
        Task<bool> FriendRequestExists(string fromUserId, string toUserId);
        Task<bool> CreateFriendRequest(Friend friend);

    }
}