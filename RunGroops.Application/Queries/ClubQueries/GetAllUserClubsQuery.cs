using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetAllUserClubsQuery() : IRequest<ICollection<Club>>;
}
