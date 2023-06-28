using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetAllClubsQuery : IRequest<ICollection<Club>>;
}
