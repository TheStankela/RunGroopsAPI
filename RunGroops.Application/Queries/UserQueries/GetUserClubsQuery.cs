using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUserClubsQuery(string UserId) : IRequest<ICollection<Club>>;
}
