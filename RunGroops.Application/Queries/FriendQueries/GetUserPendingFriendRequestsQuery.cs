using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.FriendQueries
{
    public record GetUserPendingFriendRequestsQuery() : IRequest<ICollection<Friend>>;
}
