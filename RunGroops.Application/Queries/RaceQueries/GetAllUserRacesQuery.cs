using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetAllUserRacesQuery : IRequest<ICollection<Race>>;
}
