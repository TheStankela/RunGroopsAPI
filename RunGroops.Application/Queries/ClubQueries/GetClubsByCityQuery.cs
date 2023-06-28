using MediatR;
using RunGroops.Domain.EFModels;

namespace RunGroops.Application.Queries.ClubQueries
{
    public record GetClubsByCityQuery(
        string city) : IRequest<ICollection<Club>>;
}
