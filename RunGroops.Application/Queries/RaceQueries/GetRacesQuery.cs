using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetRacesQuery(int pageNumber) : IRequest<ICollection<Race>>;
}
