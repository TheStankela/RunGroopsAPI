using MediatR;
using RunGroops.Application.Queries.ClubQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.ClubHanders
{
    public class GetClubsByCityHandler : IRequestHandler<GetClubsByCityQuery, ICollection<Club>>
    {
        private readonly IClubRepository _clubRepository;
        public GetClubsByCityHandler(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }

        public async Task<ICollection<Club>> Handle(GetClubsByCityQuery request, CancellationToken cancellationToken)
        {
            return await _clubRepository.GetClubsByCityAsync(request.city);
        }
    }
}
