using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetClubByIdQuery(
        int id) : IRequest<Club>;
}
