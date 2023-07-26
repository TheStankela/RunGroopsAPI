using MediatR;
namespace RunGroops.Application.Queries.FriendQueries
{
    public record GetUserFriendsCountQuery(string UserId) : IRequest<int>;
}
