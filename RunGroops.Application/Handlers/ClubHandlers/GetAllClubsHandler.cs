using MediatR;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetAllClubsHandler : IRequestHandler<GetAllClubsQuery, ICollection<Club>>
    {
        private readonly IClubRepository _clubRepository;
        public GetAllClubsHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<ICollection<Club>> Handle(GetAllClubsQuery request, CancellationToken cancellationToken)
        {
            return await _clubRepository.GetClubsAsync(request.Page);
        }
    }
}
