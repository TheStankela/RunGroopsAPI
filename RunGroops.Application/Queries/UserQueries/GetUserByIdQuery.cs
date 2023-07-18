using MediatR;
using RunGroops.Application.Models;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUserByIdQuery(string UserId) : IRequest<UserResponse>;
}
