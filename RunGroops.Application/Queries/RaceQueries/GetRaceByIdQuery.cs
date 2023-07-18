using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetRaceByIdQuery(int id) : IRequest<Race>;
}
