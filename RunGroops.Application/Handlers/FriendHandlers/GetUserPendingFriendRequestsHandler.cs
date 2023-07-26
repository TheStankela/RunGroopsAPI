using MediatR;
using Microsoft.AspNetCore.Http;
using RunGroops.Application.Queries.FriendQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;
using System.Security.Claims;

namespace RunGroops.Application.Handlers.FriendHandlers
{
    public class GetUserPendingFriendRequestsHandler : IRequestHandler<GetUserPendingFriendRequestsQuery, ICollection<Friend>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFriendRepository _friendRepository;
        public GetUserPendingFriendRequestsHandler(IHttpContextAccessor httpContextAccessor, IFriendRepository friendRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _friendRepository = friendRepository;
        }
        public async Task<ICollection<Friend>?>? Handle(GetUserPendingFriendRequestsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
                return null;

            return await _friendRepository.GetUserPendingFriendRequests(userId);
        }
    }
}
