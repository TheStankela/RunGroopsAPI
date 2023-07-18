using MediatR;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRacesByNameHandler : IRequestHandler<GetRacesByNameQuery, ICollection<Race>>
    {
        private readonly IRaceRepository _raceRepository;
        public GetRacesByNameHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<ICollection<Race>> Handle(GetRacesByNameQuery request, CancellationToken cancellationToken)
        {
           return await _raceRepository.GetRacesByNameAsync(request.RaceName);
        }
    }
}
