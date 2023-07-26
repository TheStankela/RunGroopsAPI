using MediatR;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUsersQuery(int PageNumber, int pageSize) : IRequest<PagedList<UserResponse>>;
}
