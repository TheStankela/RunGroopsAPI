using MediatR;
using Microsoft.EntityFrameworkCore;
using RunGroops.Application.Models;
using RunGroops.Application.Queries.FriendQueries;
using RunGroops.Domain.Enum;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.FriendHandlers
{
    public class GetUserFriendsCountHandler : IRequestHandler<GetUserFriendsCountQuery, int>
    {
        private readonly IFriendRepository _friendRepository;
        public GetUserFriendsCountHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<int> Handle(GetUserFriendsCountQuery request, CancellationToken cancellationToken)
        {
           return await _friendRepository.GetUserFriendsCount(request.UserId);
        }
    }
}
