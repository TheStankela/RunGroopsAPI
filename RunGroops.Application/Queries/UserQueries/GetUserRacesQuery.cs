using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.UserQueries
{
    public record GetUserRacesQuery(string UserId) : IRequest<ICollection<Race>>;
}
