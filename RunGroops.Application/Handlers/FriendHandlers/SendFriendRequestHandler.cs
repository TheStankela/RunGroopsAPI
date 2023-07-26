using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Commands.FriendCommands;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Enum;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.FriendHandlers
{
    public class SendFriendRequestHandler : IRequestHandler<SendFriendRequestCommand, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendRepository _friendRepository;
        private readonly IUserRepository _userRepository;
        public SendFriendRequestHandler(IHttpContextAccessor httpContextAccessor, IFriendRepository friendRepository, IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _friendRepository = friendRepository;
            _userRepository = userRepository;
        }
        public async Task<bool> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return false;

            if(await _friendRepository.FriendRequestExists(userId, request.ToUserId))
                return false;

            var fromUser = await _userRepository.GetUserByIdAsync(userId); 
            if (fromUser is null) 
                return false;

            var toUser = await _userRepository.GetUserByIdAsync(request.ToUserId);
            if (toUser is null) 
                return false;

            if(request.ToUserId == userId) 
                return false;

            var friendRequest = new Friend
            {
                FriendRequestStatus = FriendRequestStatus.Pending,
                FromUser = fromUser,
                ToUser = toUser,
            };

            return await _friendRepository.CreateFriendRequest(friendRequest);
        }
    }
}
