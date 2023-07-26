using MediatR;
using RunGroops.Application.Models;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetClubsByNameQuery(
        string Name, int PageNumber, int PageSize) : IRequest<PagedList<Club>>;
}
