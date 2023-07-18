using MediatR;
using RunGroops.Application.Queries.RaceQueries;
using RunGroops.Domain.EFModels;
using RunGroops.Domain.Interfaces;

namespace RunGroops.Application.Handlers.RaceHandlers
{
    public class GetRaceByIdHandler : IRequestHandler<GetRaceByIdQuery, Race>
    {
        private readonly IRaceRepository _raceRepository;
        public GetRaceByIdHandler(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<Race> Handle(GetRaceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _raceRepository.GetRaceByIdAsync(request.id);
        }
    }
}
