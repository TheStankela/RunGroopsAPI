using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetRacesByNameQuery(string RaceName) : IRequest<ICollection<Race>>;
}
