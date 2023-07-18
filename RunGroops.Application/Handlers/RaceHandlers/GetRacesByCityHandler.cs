using MediatR;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRacesByCityHandler : IRequestHandler<GetRacesByCityQuery, ICollection<Race>>
    {
        private readonly IRaceRepository _raceRepository;
        public GetRacesByCityHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<ICollection<Race>> Handle(GetRacesByCityQuery request, CancellationToken cancellationToken)
        {
            return await _raceRepository.GetRacesByCityAsync(request.City);
        }
    }
}
