using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Commands.FriendCommands;
using RunGroops.Domain.Enum;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.FriendHandlers
{
    public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendRepository _friendRepository;
        public AcceptFriendRequestHandler(IHttpContextAccessor httpContextAccessor, IFriendRepository friendRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _friendRepository = friendRepository;
        }
        public async Task<bool> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                return false;

            if (!await _friendRepository.FriendRequestExists(request.FromUserId, userId))
                return false;

            var friendRequest = await _friendRepository.GetRequestByUserIds(request.FromUserId, userId);

            if(friendRequest is null) 
                return false;

            if (friendRequest.FriendRequestStatus != FriendRequestStatus.Pending)
                return false;

            friendRequest.FriendRequestStatus = FriendRequestStatus.Approved;

            return await _friendRepository.UpdateFriendRequest(friendRequest);
        }
    }
}
