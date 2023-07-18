using MediatR;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRacesHandler : IRequestHandler<GetRacesQuery, ICollection<Race>>
    {
        private readonly IRaceRepository _raceRepository;

        public GetRacesHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<ICollection<Race>> Handle(GetRacesQuery request, CancellationToken cancellationToken)
        {
            return await _raceRepository.GetRacesAsync(request.pageNumber);
        }
    }
}
