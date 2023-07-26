using MediatR;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.RaceQueries
{
    public record GetRacesQuery(int PageNumber, int PageSize) : IRequest<PagedList<Race>>;
}
