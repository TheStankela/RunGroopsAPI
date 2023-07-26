using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUsersByNameQuery(
        string UserName,
        int Page,
        int PageSize) : IRequest<PagedList<UserResponse>>;
}
