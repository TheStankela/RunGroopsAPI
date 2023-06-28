using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetClubByNameQuery(
        string name) : IRequest<Club>;
}
