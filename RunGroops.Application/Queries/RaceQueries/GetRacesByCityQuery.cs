using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetRacesByCityQuery(string City) : IRequest<ICollection<Race>>;
}
